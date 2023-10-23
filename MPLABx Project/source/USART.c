/*
 * File:                USART.c
 * Author:              Dan Wilson, 2021
 *
 * Created on November 28, 2021, 8:14 PM
 * 
 * Library code for USART serial communications at 115.2 kbps
 * 
 * This code enforces half-duplex communication (either transmit or receive but cannot do both simultaneously)
 * This is required with the simple UART RS232-TTL level shifter.
 * 
 * Set up the terminal emulator on the PC as follows:
 *          8 data bits, no parity, 1 stop bit
 *          Local echo must be turned on (since this drive is half duplex, it cannot reliably echo the terminal characters)
 *          New line mode should be turned on (terminal emulator converts a typed Enter (0x0d) into 0x0d 0x0a)
 * 
 * Version History:
 *          1.2.0.0     Split serial functions into their own source file for inclusion in other projects
 *          1.4.0.0     Changed lengths to unsigned shorts vs unsigned char to allow for longer lengths and remove confusion caused by 00=256
 * 
 * Inputs:  USART_PICRX UART receive (DTE -> DCE, i.e. computer to PIC)
 * Outputs: USART_PICTX UART transmit (DCE -> DTE, i.e. PIC to computer)
 *          LED_PICRX   LED for receive
 *          LED_PICTX   LED for transmit
 */

#include <xc.h>
#include "USART.h"
#include "pic16f15254.h"
#include "pins.h"

// Global Variables
// These variables are declared volatile because they're access in the ISR
volatile    unsigned char   *buf;               // Pointer to data
volatile    unsigned short  bufPtr=0;           // Pointer to bytes in the buffer
volatile    unsigned char   portInUse=0;        // Flag to indicate that the port is in use
volatile    unsigned short  bufLength;          // Length of data that we're reading

// Function Definitions
void SerialPortSetup() {
    // Serial port setup
    //SP1BRG=16;                      // Value for 115.2 kbps (actual 117.6 kbps) for 8 MHz clock (BRG16=1, BRGH=1)
    //SP1BRG=68;                      // Value for 115.2 kbps (actual 115.9 kbps) for 32 MHz clock (BRG16=1, BRGH=1)
    SP1BRG=34;                      // Value for 230.4 kbps (actual 228.6 kbps) for 32 MHz clock (BRG16=1, BRGH=1)
    TX1STAbits.BRGH=1;
    BAUD1CONbits.BRG16=1;
    USART_PICRX=1;                  // Drive PIC Rx pin high before changing it to an input to avoid glitch on receive line
    TRISCbits.TRISC7=1;             // Port C pin 7 as input (tri-stated)
    RC6PPSbits.RC6PPS=0x05;         // Send UART transmit to pin PORTC[6]
    RX1PPSbits.PORT=0x02;           // Listen for UART receive on pin PORTC[7]
    RX1PPSbits.PIN=0x07;
    TX1STAbits.CSRC=0;              // Not used for asynchronous operation
    TX1STAbits.TX9=0;               // 8-bit transmission format
    TX1STAbits.SYNC=0;              // Asynchronous mode
    RC1STAbits.SPEN=1;              // Serial port enabled
    BAUD1CONbits.SCKP=0;            // Standard polarity - idle state is high (5V)
    
    // Rx/Tx LEDs
    LED_PICRX=0;                    // Turn receive LED off
    LED_PICTX=0;                    // Turn transmit LED off
}

void SerialPortSendData(unsigned char dataToSend[], unsigned short length) {
    // Send Data routine

    // Transmits length bytes from dataToSend to the PC
    // Copies the strToSend data into the transmit buffer txBuf, then sets up the serial port to use
    // interrupts to send the data
    // Blocks until sending is completed
    
    // Can't send if a receive is in progress
    if (portInUse) return;
    
    buf=dataToSend;                 // Pointer to data
    bufPtr=0;                       // Pointer to start of data
    bufLength=length;               // Set up length value - this is number of bytes to transmit, starting with [0]
    portInUse=1;                    // Transmission in progress
    LED_PICTX=1;                    // Turn on transmit LED
    
    PIE1bits.TX1IE=1;               // Enable UART transmit interrupts - this tells the PIC that there is more data to send
    TX1STAbits.TXEN=1;              // Enable UART to transmit, this will immediately generate an interrupt
    
    while (PIE1bits.TX1IE);         // Wait until we have no more data to send
    while (!TX1STAbits.TRMT);       // Wait until the last character has finished transmitting
    
    TX1STAbits.TXEN=1;              // Disable UART transmitter

    LED_PICTX=0;                    // Turn off transmit LED
    portInUse=0;                    // Transmission no longer in progress
    bufPtr=0;                       // Reset pointer to transmit buffer
    
    return;
}

void SerialPortReceiveData(unsigned char dataReceived[], unsigned short length) {
    // Receive data routine
    
    // Receives length data from the computer
    // Puts the received data into dataReceived
    // Blocks until reception is completed
    
    // Can't receive if transmit is in progress
    if (portInUse) return;
    
    buf=dataReceived;               // Pointer to data
    bufPtr=0;                       // Pointer to start of buffer
    bufLength=length;               // Set up length value - this is number of bytes to receive, starting with [0]
    portInUse=1;                    // Reception in progress
    
    PIE1bits.RC1IE=1;               // Enable UART receive interrupts
    RC1STAbits.CREN=1;              // Enable the UART receiver, this will generate an interrupt when a character has been received
    
    while (PIE1bits.RC1IE);         // Wait until we've received all data
    
    RC1STAbits.CREN=0;              // Disable the UART receiver
    
    LED_PICRX=0;                    // Turn off receive LED
    portInUse=0;                    // Reception no longer in progress
    bufPtr=0;                       // Reset pointer to receive buffer
    
    return;
}

void SerialPortTransmitInterruptService(void) {
    // Transmit Interrupt Service Routine
    // This code is called from an ISR in main.c when (PIE1bits.TX1IE & PIR1bits.TX1IF)
    // (transmit interrupts are enabled and the transmit interrupt flag is set)
    
    // Send the next byte - txPtr is the next character to transmit
    TX1REG=buf[bufPtr++];
    bufLength--;                    // One less character to send
    
    if (bufLength==0x00) {
        // We've reached the end of the string, no further data to transmit
        portInUse=0;                // Transmission no longer in progress
        bufPtr=0;                   // Reset txPtr
        PIE1bits.TX1IE=0;           // Disable transmit interrupts, this tells the PIC that there is no more data to send
    }
}

void SerialPortReceiveInterruptService(void) {
    // Receive Interrupt Service Routine
    // This code is called from an ISR in main.c when (PIE1bits.RC1IE & PIR1bits.RC1IF)
    // (receive interrupts are enabled and the receive interrupt flag is set)
    
    // Local variables
    char    tempData;                   // Used in serial receive routine

    LED_PICRX=1;                        // Turn on receive LED
    bufLength--;                        // One less byte to receive

    // Check for any errors
    if (RC1STAbits.FERR) {
        // There was a framing error, read the received character, discard it, and stop reception
        tempData=RC1REG;
        PIE1bits.RC1IE=0;               // Disable interrupts, we've had a framing error
    } else if (RC1STAbits.OERR) {
        // There is an overrun error, stop reception
        PIE1bits.RC1IE=0;               // Disable interrupts, we've had an overrun error
    } else {
        // No error, get the next byte out of the receive register and put it into rxBuf
        tempData=RC1REG;
        buf[bufPtr++]=tempData;
        if (bufLength==0x00) {
            // We received the last byte, put it in the buffer and stop reception
            PIE1bits.RC1IE=0;           // Disable receive interrupts, we've received all characters
        }
    }
}