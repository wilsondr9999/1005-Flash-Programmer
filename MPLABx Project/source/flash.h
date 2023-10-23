/* 
 * File:   flash.h
 * Author: Dan Wilson
 * Comments:
 * Revision history: 
 * 
 *          1.4.0.0     Changed lengths to unsigned shorts vs unsigned char to allow for longer lengths and remove confusion caused by 00=256

 */

#ifndef XC_HEADER_FLASH
#define	XC_HEADER_FLASH

#include <xc.h> // include processor files - each processor file is guarded.  

// Function Declarations
unsigned char readFlashChipId(unsigned char retBytes[], unsigned short *numRetBytes);
unsigned char readFlash(unsigned long addr, unsigned short numBytes, unsigned char retByes[], unsigned short *numRetBytes);
unsigned char writeFlash(unsigned long addr, unsigned short numBytes, unsigned char dataBytes[]);
unsigned char eraseFlash(void);
unsigned char eraseFlashSector(unsigned long addr);

unsigned char waitPollData(unsigned long addr, unsigned char origData);
unsigned char clearAddressRegisters(void);
unsigned char setAddressRegisters(unsigned long addr, unsigned char OE, unsigned char CE);
unsigned char writeOneByte(unsigned long addr, unsigned char data);
unsigned char sendOneByte(unsigned char data);
unsigned char softwareIdEntry(void);
unsigned char softwareIdExit(void);

#endif	/* XC_HEADER_FLASH */

