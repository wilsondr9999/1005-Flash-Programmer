# Flash Programmer

This is a simple PCB, firmware, and software that can write binary image files to the Microchip SST39 series of flash ROMs.  These flash ROMs are one of the very few flash ROMs on the market that are still active production and in DIP packages, making them a popular replacement for the difficult-to-find and out-of-production AT28 series.

This programmer handles the SST39SF010, SST39SF020, and SST39SF040 parts (128KB, 256KB, and 512KB respectively).

This programmer’s PCB design uses all through-hole parts for easy soldering.  The general design is based on Ben Eater’s [Arduino-based Flash Programmer](https://www.youtube.com/watch?v=K88pgWhEb1M), using a microcontroller unit and shift registers to increase the number of available outputs.  Ben’s microcontroller is the Arduino Nano, here I’m using a Microchip PIC16.

The firmware on the PIC16 implements a command protocol that runs over a serial interface.  The board contains a simple RS232 serial level shifter that allows the PC to communicate with the microcontroller.

On the PC, a command line program is provided to read and write to the flash memory.  The command line program is written in Visual C# with Visual Studio.
