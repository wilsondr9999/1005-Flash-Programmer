/*
 * File:   flash.c
 * Author: WilsonDR
 * Comments:
 * Revision history: 
 *
 * Created on December 6, 2021, 5:48 PM
 * 
 *          1.4.0.0     Changed lengths to unsigned shorts vs unsigned char to allow for longer lengths and remove confusion caused by 00=256
 */

#include <xc.h>
#include "flash.h"
#include "pic16f15254.h"
#include "pins.h"

unsigned char readFlash(unsigned long addr, unsigned short numBytes, unsigned char retBytes[], unsigned short *numRetBytes) {
    // Reads numBytes of data from Flash starting at address addr and puts in it retBytes
    
    unsigned short length,bufPtr;
    
    // Initialize 
    bufPtr=0;
    length=numBytes;
    
    // Set the address registers, pull CE and OE low (enabled), exit if error
    if (!setAddressRegisters(addr,0x0,0x0)) {
        *numRetBytes=0;
        return 0;
    }
    
    // In a loop, read each byte of Flash, put it in the retBytes buffer
    while (length>0) {
        // Read byte from Flash
        retBytes[bufPtr]=FL_DATA;               // Retrieve byte from Flash
        bufPtr++;
        FL_ADDRL++;
        length--;
        if (FL_ADDRL==0x00 && length>0) {       // Increment Flash address low-order byte to next address, and if we cross a page boundary with more bytes to read,
            addr+=0x100;                        // we have to increment to the next page and set up the address registers again
            if (!setAddressRegisters(addr,0x0,0x0)) {       
                *numRetBytes=0;                 // Exit if error setting address registers
                return 0;
            }
        }
    }
    clearAddressRegisters();                    // Clear the address registers
    *numRetBytes=numBytes;                      // Set number of bytes we're returning
    return 1;                                   // Return successful
}

unsigned char writeFlash(unsigned long addr, unsigned short numBytes, unsigned char dataBytes[]) {
    // Writes data from the dataBytes buffer to address addr

    unsigned short length,bufPtr;
    
    // Initialize 
    bufPtr=0;
    length=numBytes;
    
    // In a loop, write each byte to Flash
    while (length>0) {
        // Write byte
        if (!writeOneByte(addr,dataBytes[bufPtr])) return 0;
        bufPtr++;
        addr++;
        length--;
    }
    
    clearAddressRegisters();
    return 1;
}

unsigned char readFlashChipId(unsigned char retBytes[], unsigned short *numRetBytes) {
    // Reads the Flash chip ID and returns those bytes
    
    if (!softwareIdEntry()) return 0;
    if (!readFlash(0x000000,2,retBytes,numRetBytes)) return 0;
    if (!softwareIdExit()) return 0;
    return 1;
}

unsigned char writeOneByte(unsigned long addr, unsigned char data) {
    // Writes a byte to a single address in the Flash
    
    // Send sequence to the Flash chip
    if (!setAddressRegisters(0x005555,0x1,0x0)) return 0;
    if (!sendOneByte(0xaa)) return 0;
    if (!setAddressRegisters(0x002aaa,0x1,0x0)) return 0;
    if (!sendOneByte(0x55)) return 0;
    if (!setAddressRegisters(0x005555,0x1,0x0)) return 0;
    if (!sendOneByte(0xA0)) return 0;
    if (!setAddressRegisters(addr,0x1,0x0)) return 0;
    if (!sendOneByte(data)) return 0;
    if (!waitPollData(addr,data)) return 0;
    return 1;
}

unsigned char eraseFlash() {
    // Erases the entire flash chip to all 0xff bytes
    
    // Send sequence to the Flash chip
    if (!setAddressRegisters(0x005555,0x1,0x0)) return 0;
    if (!sendOneByte(0xaa)) return 0;
    if (!setAddressRegisters(0x002aaa,0x1,0x0)) return 0;
    if (!sendOneByte(0x55)) return 0;
    if (!setAddressRegisters(0x005555,0x1,0x0)) return 0;
    if (!sendOneByte(0x80)) return 0;
    if (!setAddressRegisters(0x005555,0x1,0x0)) return 0;
    if (!sendOneByte(0xaa)) return 0;
    if (!setAddressRegisters(0x002aaa,0x1,0x0)) return 0;
    if (!sendOneByte(0x55)) return 0;
    if (!setAddressRegisters(0x005555,0x1,0x0)) return 0;
    if (!sendOneByte(0x10)) return 0;
    if (!waitPollData(0x000000,0xff)) return 0;
    return 1;
}

unsigned char eraseFlashSector(unsigned long addr) {
    // Erases one 4K sector of Flash to all 0xff bytes
    
    // Send sequence to Flash chip
    if (!setAddressRegisters(0x005555,0x1,0x0)) return 0;
    if (!sendOneByte(0xaa)) return 0;
    if (!setAddressRegisters(0x002aaa,0x1,0x0)) return 0;
    if (!sendOneByte(0x55)) return 0;
    if (!setAddressRegisters(0x005555,0x1,0x0)) return 0;
    if (!sendOneByte(0x80)) return 0;
    if (!setAddressRegisters(0x005555,0x1,0x0)) return 0;
    if (!sendOneByte(0xaa)) return 0;
    if (!setAddressRegisters(0x002aaa,0x1,0x0)) return 0;
    if (!sendOneByte(0x55)) return 0;
    if (!setAddressRegisters(addr,0x1,0x0)) return 0;
    if (!sendOneByte(0x30)) return 0;
    if (!waitPollData(0x000000,0xff)) return 0;
    return 1;
}

unsigned char waitPollData(unsigned long addr, unsigned char origData) {
    // Polls the chip looking at address addr, looking at data bit D7
    // Repeatedly polls the chip until the D7 bit is the same as the MSB of the origData
    
    unsigned char done=0;
    unsigned char readData=0;
    
    while (!done) {
        // Set address registers and pull OE and CE low
        if (!setAddressRegisters(addr,0x0,0x0)) return 0;
        // Read data
        readData=FL_DATA;                                   // Get the data byte from the flash data port
        // Turn off CE and OE
        if (!setAddressRegisters(addr,0x1,0x1)) return 0;
        // See if the D7 bit is what we expect, if not go check again
        if ((readData & 0x80) == (origData & 0x80)) done=1;
    }
    
    return 1;
}

unsigned char softwareIdEntry() {
    // This routine enters software ID mode on the Flash chip
    
    // Send sequence to the Flash chip
    if (!setAddressRegisters(0x005555,0x1,0x0)) return 0;
    if (!sendOneByte(0xaa)) return 0;
    if (!setAddressRegisters(0x002aaa,0x1,0x0)) return 0;
    if (!sendOneByte(0x55)) return 0;
    if (!setAddressRegisters(0x005555,0x1,0x0)) return 0;
    if (!sendOneByte(0x90)) return 0;
    return 1;
}

unsigned char softwareIdExit() {
    // This routine enters software ID mode on the Flash chip
    
    // Send sequence to the Flash chip
    if (!setAddressRegisters(0x005555,0x1,0x0)) return 0;
    if (!sendOneByte(0xaa)) return 0;
    if (!setAddressRegisters(0x002aaa,0x1,0x0)) return 0;
    if (!sendOneByte(0x55)) return 0;
    if (!setAddressRegisters(0x005555,0x1,0x0)) return 0;
    if (!sendOneByte(0xf0)) return 0;
    return 1;
}

unsigned char sendOneByte(unsigned char data) {
    // Sends a single byte to the Flash
    // This is a base routine that is called by others
    
    // Place data on the data bus
    FL_DATATRIS=0x00;                           // Set Flash data port as output
    FL_DATA=data;                               // Set the data on the bus
    
    // Pulse WE to send the byte
    FL_WE=0;
    FL_WE=1;
    
    // Set bus back to input
    FL_DATA=0x00;
    FL_DATATRIS=0xff;                           // Set PORTB as input
    
    return 1;
}

unsigned char setAddressRegisters(unsigned long addr, unsigned char OE, unsigned char CE) {
    // Sets the shift registers and PORTA to the address given in addr, and sets OE and CE as given by the OE/CE arguments
    
    unsigned char i,done;
    unsigned long mask;
    
    // Initialize 
    done=0;
    
    addr&=0x07ffff;                                    // Mask address to the max for this Flash, and set OE and CE as low for now
    if (CE) addr|=0x800000; else addr&=0x7fffff;       // Set CE as required
    if (OE) addr|=0x400000; else addr&=0xbfffff;       // Set OE as required

    // Set up shift registers (high 16 bits) with high-order bits of address + OE and CE
    mask=0x800000;
    for (i=16; i>0; i--) {
        // Shift this bit out to the shift registers
        SR_SDATA=((addr & mask) ? 1 : 0);
        SR_SCLK=1;           // Pulse the shift register serial clock
        SR_SCLK=0;
        mask>>=1;
    }
    // Now pulse the shift register reg clock to send the bits to their outputs
    SR_RCLK=1;
    SR_RCLK=0;
    
    // Now set port A to the low-order 8 bits of address
    FL_ADDRL=(addr&0xff);
    
    return 1;                                   // Successful
}

unsigned char clearAddressRegisters() {
    // Clears the shift registers to 0x000000 address, and !CE and !OE high (not enabled)
    
    return setAddressRegisters(0x000000,0x1,0x1);
}
