/* 
 * File:   conbits.h
 * Author: Dan Wilson
 * Comments:
 * Revision history: 
 */

#ifndef XC_HEADER_CONBITS
#define	XC_HEADER_CONBITS

#include <xc.h> // include processor files - each processor file is guarded.  

// PIC16F15256 Configuration Bit Settings

// 'C' source line config statements

// CONFIG1
#pragma config FEXTOSC = OFF    // External Oscillator Mode Selection bits (Oscillator not enabled)
#pragma config RSTOSC = HFINTOSC_1MHZ// Power-up Default Value for COSC bits (HFINTOSC (1 MHz))
#pragma config CLKOUTEN = OFF   // Clock Out Enable bit (CLKOUT function is disabled; I/O function on RA6)
#pragma config VDDAR = HI       // VDD Range Analog Calibration Selection bit (Internal analog systems are calibrated for operation between VDD = 2.3V - 5.5V)

// CONFIG2
#pragma config MCLRE = EXTMCLR  // Master Clear Enable bit (If LVP = 0, MCLR pin is port defined function; If LVP = 1, RE3 pin function is MCLR)
#pragma config PWRTS = PWRT_OFF // Power-up Timer Selection bits (PWRT is disabled)
#pragma config WDTE = OFF       // WDT Operating Mode bits (WDT disabled; SEN is ignored)
#pragma config BOREN = OFF      // Brown-out Reset Enable bits (Brown-out Reset disabled)
#pragma config BORV = LO        // Brown-out Reset Voltage Selection bit (Brown-out Reset Voltage (VBOR) set to 1.9V)
#pragma config PPS1WAY = OFF    // PPSLOCKED One-Way Set Enable bit (The PPSLOCKED bit can be set and cleared as needed (unlocking sequence is required))
#pragma config STVREN = OFF     // Stack Overflow/Underflow Reset Enable bit (Stack Overflow or Underflow will not cause a reset)

// CONFIG3

// CONFIG4
#pragma config BBSIZE = BB512   // Boot Block Size Selection bits (512 words boot block size)
#pragma config BBEN = OFF       // Boot Block Enable bit (Boot Block is disabled)
#pragma config SAFEN = OFF      // SAF Enable bit (SAF is disabled)
#pragma config WRTAPP = OFF     // Application Block Write Protection bit (Application Block is not write-protected)
#pragma config WRTB = OFF       // Boot Block Write Protection bit (Boot Block is not write-protected)
#pragma config WRTC = OFF       // Configuration Registers Write Protection bit (Configuration Registers are not write-protected)
#pragma config WRTSAF = OFF     // Storage Area Flash (SAF) Write Protection bit (SAF is not write-protected)
#pragma config LVP = OFF        // Low Voltage Programming Enable bit (High Voltage on MCLR/Vpp must be used for programming)

// CONFIG5
#pragma config CP = OFF         // User Program Flash Memory Code Protection bit (User Program Flash Memory code protection is disabled)

// #pragma config statements should precede project file includes.
// Use project enums instead of #define for ON and OFF.

#endif	/* XC_HEADER_TEMPLATE_H */
