/* 
 * File:                pins.h
 * Author:              Dan Wilson
 * Comments:
 * Revision history:    
 */

#ifndef XC_HEADER_PINS_H
#define	XC_HEADER_PINS_H

#include <xc.h> // include processor files - each processor file is guarded.  

// These definitions are to attach convenient names to pins
/*
 * Inputs:  PORTC[7]    UART receive (DTE -> DCE, i.e. computer to PIC)
 * Outputs: PORTC[6]    UART transmit (DCE -> DTE, i.e. PIC to computer)
 *          PORTC[5]    LED for receive
 *          PORTC[4]    LED for transmit
 *          PORTC[0]    Serial data to go out to shift registers
 *          PORTC[1]    Serial clock for shift registers
 *          PORTC[2]    Register clock for shift registers
 *          PORTC[3]    Write Enable for Flash
 *          PORTA       Low-order 8 bits of address
 * Bidir:   PORTB       Data bus to Flash
*/

#define     USART_PICRX     LATCbits.LATC7
#define     USART_PIXTX     LATCbits.LATC6
#define     LED_PICRX       LATCbits.LATC5
#define     LED_PICTX       LATCbits.LATC4
#define     SR_SDATA        LATCbits.LATC0
#define     SR_SCLK         LATCbits.LATC1
#define     SR_RCLK         LATCbits.LATC2
#define     FL_WE           LATCbits.LATC3
#define     FL_ADDRL        PORTA
#define     FL_DATA         PORTB
#define     FL_DATATRIS     TRISB

#endif	/* XC_HEADER_PINS_H */
