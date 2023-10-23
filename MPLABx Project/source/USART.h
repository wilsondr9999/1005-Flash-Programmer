/* 
 * File:                USART.h
 * Author:              Dan Wilson, 2021
 * 
 * Created on November 28, 2021, 8:14 PM
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

#ifndef XC_HEADER_USART
#define	XC_HEADER_USART

#include <xc.h> // include processor files - each processor file is guarded.  

// Function declarations
void SerialPortSetup(void);
void SerialPortSendData(unsigned char dataToSend[], unsigned short length);
void SerialPortReceiveData(unsigned char dataReceived[], unsigned short length);
void SerialPortTransmitInterruptService(void);
void SerialPortReceiveInterruptService(void);

#endif	/* XC_HEADER_USART */
