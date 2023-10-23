/*
 * File:   main.c
 * Author: WilsonDR
 *
 * Created on November 27, 2021, 3:22 PM
 * 
 * Flash Programmer
 * 
 * This code can program the Microchip SST39SF010A, SST39SF020A, and SST39SF040A Flash memory ICs
 *  
 * Version History:
 *          1.0.0.0     Initial version
 *          1.1.0.0     Remove dependence on sscanf and sprintf to free up program memory
 *          1.2.0.0     Change serial port routines to work with lengths vice termination character
 *                      Add ATR for identification, add Flash read routine
 *          1.3.0.0     Reprogram to remote the low-order shift register and use port A instead for speed improvement.
 *          1.4.0.0     Implemented chip erase, flash write, sector erase, and changed all lengths to unsigned short vice unsigned char.
 *          1.4.1.0     Recompiled for PIC16F15254 (4K word) vice 16F15256
 * 
 * Inputs:  USART_PICRX UART receive (DTE -> DCE, i.e. computer to PIC)
 * Outputs: USATY_PICTX UART transmit (DCE -> DTE, i.e. PIC to computer)
 *          LED_PICRX   LED for receive
 *          LED_PICTX   LED for transmit
 *          SR_SDATA    Serial data to go out to shift registers
 *          SR_SCLK     Serial clock for shift registers
 *          SR_RCLK     Register clock for shift registers
 *          FL_WE       Write Enable for Flash
 *          FL_ADDRL    Low-order 8 bits of address
 *          FL_DATATRIS Tri-state register for the Flash data port
 * Bidir:   FL_DATA     Data bus to Flash
 */

#include <xc.h>
#include <string.h>
#include "pic16f15254.h"
#include "conbits.h"
#include "USART.h"
#include "flash.h"
#include "pins.h"

// Constants
#define         ATR_SIZE    12
unsigned char   ATR[ATR_SIZE] = {0x3b,0x38,0x45,0x00,'F','l','s','h','P','r','g',0x12};

// Variables
// Buffers
unsigned char   cmdBuf[5];              // Buffer for reading commands
unsigned char   ackBuf[2];              // Buffer for sending acknowledgements
unsigned char   dataBuf[256];           // Buffer for reading input

// Parameters
unsigned char   cmd;                    // Holds the command
unsigned long   addr;                   // Holds the address given on the command line
unsigned short  numBytes;               // Holds the number of bytes to read/write

// Return values
unsigned char   successful;             // Flag to hold whether the operation was successful
unsigned short  numRetBytes;            // Number of bytes to return from dataBuf
unsigned char   returnBytes;            // Flag to hold whether we will return bytes or not

// Interrupt Service Routine (ISR) Declaration
void __interrupt() isr(void);

// Function Declarations

// Main
void main(void) {
    // Local Variables
    unsigned char successful;
    
    // Set up Oscillator
    OSCENbits.HFOEN=1;              // Enable high-frequency oscillator
    OSCFRQ=5;                       // Set for 32 MHz
    while (!OSCSTATbits.HFOR);      // Wait until oscillator is ready

    // Standard port initialization
    ANSELA=0;                       // Disable analog functions, use digital GPIO
    ANSELB=0;
    ANSELC=0;
    WPUA=0;                         // Disable weak pull-up on all pins
    WPUB=0;
    WPUC=0;
    INLVLA=0;                       // Set input level for all pins to TTL-compatible
    INLVLB=0;
    INLVLC=0;
    SLRCONA=0;                      // Set all pin slew rates to maximum
    SLRCONB=0;
    SLRCONC=0;
    ODCONA=0;                       // Turn off open-drain on all pins, act as standard push-pull driver
    ODCONB=0;
    ODCONC=0;
    TRISA=0;                        // Set all pins as output
    TRISB=0;
    TRISC=0;
    
    // Pin initialization for shift registers and data port
    SR_SDATA=1;                     // Serial data out
    SR_SCLK=0;                      // Serial clock
    SR_RCLK=0;                      // Register clock
    FL_WE=1;                        // !Write Enable
    
    successful=clearAddressRegisters();     // Set all address lines low (address 0x000000), set !CE and !OE to high (not enabled)
    FL_DATATRIS=0xff;               // Data port as input
    
    
    // Now set up pins for this application
    // Serial port setup
    SerialPortSetup();
    
    // Enable interrupts
    INTCONbits.PEIE=1;              // Enable peripheral interrupts
    ei();                           // Enable global interrupts

    // Send an Answer to Reset (ATR)
    SerialPortSendData(ATR,ATR_SIZE);

    // Main Loop
    while (1) {
        // Init
        cmd=0;
        numBytes=0;
        addr=0;
        successful=0;
        numRetBytes=0;
        returnBytes=0;
        
        // Receive a 5-byte command into the cmdBuf
        SerialPortReceiveData(cmdBuf,0x05);
        
        // Parse the command, obtain parameters, and dispatch
        switch (cmdBuf[0]) {
            case 0x80:
                // Read Flash command
                // Get address
                addr=((unsigned long)cmdBuf[1]<<16 | (unsigned long)cmdBuf[2]<<8 | (unsigned long)cmdBuf[3]);
                // Get number of bytes to read
                numBytes=(cmdBuf[4]==0x00 ? 0x100 : cmdBuf[4]);             // Protocol with host dictates that 0x00 length = 256 bytes

                // Dispatch the command
                successful=readFlash(addr,numBytes,dataBuf,&numRetBytes);
                
                // Set up the ack buffer
                if (successful) {
                    ackBuf[0]=0x81;
                    ackBuf[1]=(unsigned char)numRetBytes;                   // Protocol with host dictates that 0x00 length = 256 bytes
                    returnBytes=1;
                } else {
                    ackBuf[0]=0xC0;
                    returnBytes=0;
                }
                break;
            case 0x81:
                // Write Flash command
                // Get address
                addr=((unsigned long)cmdBuf[1]<<16 | (unsigned long)cmdBuf[2]<<8 | (unsigned long)cmdBuf[3]);
                // Get number of bytes to write
                numBytes=(cmdBuf[4]==0x00 ? 0x100 : cmdBuf[4]);             // Protocol with host dictates that 0x00 length = 256 bytes
                
                // Set up the ack buffer, this will ask the PC to send numBytes of data to write
                ackBuf[0]=0x91;
                ackBuf[1]=(unsigned char)numBytes;                          // Protocol with host dictates that 0x00 length = 256 bytes
                SerialPortSendData(ackBuf,2);
                
                // Receive those bytes to the dataBuf
                SerialPortReceiveData(dataBuf,numBytes);
                
                // Dispatch the command
                successful=writeFlash(addr,numBytes,dataBuf);
                // Set up the ack buffer
                if (successful) {
                    ackBuf[0]=0x80;
                    returnBytes=0;
                } else {
                    ackBuf[0]=0xC0;
                    returnBytes=0;
                }
                break;
            case 0x82:
                // Sector erase command
                // Get address
                addr=((unsigned long)cmdBuf[1]<<16 | (unsigned long)cmdBuf[2]<<8 | (unsigned long)cmdBuf[3]);
                // Number of bytes is not relevant, we will only erase 1 sector
                // It will be the 4K sector where the address falls (sector starting addresses are on 4K boundaries)
                
                // Make the address a 4K boundary address
                addr&=0xfff000;
                
                // Dispatch the command
                successful=eraseFlashSector(addr);
                
                // Set up the ack buffer
                if (successful) {
                    ackBuf[0]=0x80;
                    returnBytes=0;
                } else {
                    ackBuf[0]=0xC0;
                    returnBytes=0;
                }
                break;
             case 0x83:
                // Get Flash Chip ID command
                
                // Dispatch the command
                successful=readFlashChipId(dataBuf,&numRetBytes);
                
                // Set up the ack buffer
                if (successful) {
                    ackBuf[0]=0x81;
                    ackBuf[1]=(unsigned char)numRetBytes;                   // Protocol with host dictates that 0x00 length = 256 bytes;
                    returnBytes=1;
                } else {
                    ackBuf[0]=0xC0;
                    returnBytes=0;
                }
                break;
            case 0x84:
                // Chip Erase command
                
                // Dispatch the command
                successful=eraseFlash();
                
                // Set up the ack buffer
                if (successful) {
                    ackBuf[0]=0x80;
                    returnBytes=0;
                } else {
                    ackBuf[0]=0xC0;
                    returnBytes=0;
                }
                break;
            case 0xbf:
                // Software reset
                // Just return from main and the chip will restart
                return;
                break;
            default:
                break;
        }

        // Send the acknowledgement from the ack buffer and any data bytes
        SerialPortSendData(ackBuf,(returnBytes+1));
        if (returnBytes) SerialPortSendData(dataBuf,numRetBytes);
    }    
    return;
}

// Interrupt Service Routine (ISR)
void __interrupt() InterruptServiceRoutine(void) {
    // Interrupt Service Routine
    
    // See if this interrupt is a UART transmit interrupt
    if (PIE1bits.TX1IE & PIR1bits.TX1IF) {
        SerialPortTransmitInterruptService();
    }
    
    // See if this interrupt is a UART receive interrupt
    if (PIE1bits.RC1IE & PIR1bits.RC1IF) {
        SerialPortReceiveInterruptService();
    }
}