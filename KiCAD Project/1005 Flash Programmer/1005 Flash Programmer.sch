EESchema Schematic File Version 4
EELAYER 30 0
EELAYER END
$Descr USLetter 11000 8500
encoding utf-8
Sheet 1 6
Title "1005 Flash Programmer"
Date "2021-12-12"
Rev "1"
Comp "Dan Wilson"
Comment1 ""
Comment2 ""
Comment3 "Copyright © 2021 Dan Wilson"
Comment4 "SST39SF0x0A Flash Programmer"
$EndDescr
$Sheet
S 1000 1000 1000 500 
U 61B67328
F0 "UART Level Converter" 50
F1 "1004 UART Level Converter.sch" 50
F2 "uTxD" I R 2000 1150 50 
F3 "uRxD" O R 2000 1350 50 
$EndSheet
$Sheet
S 1000 2000 1000 500 
U 61B6B9EE
F0 "5V Power Supply" 50
F1 "5V Power Supply.sch" 50
$EndSheet
$Sheet
S 3500 1000 2500 1500
U 61B83974
F0 "Micro Controller" 50
F1 "Micro Controller.sch" 50
F2 "SR_SDATA" O R 6000 1100 50 
F3 "SR_SCLK" O R 6000 1200 50 
F4 "SR_RCLK" O R 6000 1300 50 
F5 "~FL_WE" O R 6000 1400 50 
F6 "USART_uTX" O L 3500 1150 50 
F7 "USART_uRX" I L 3500 1350 50 
F8 "D[0..7]" B L 3500 2200 50 
F9 "A[0..7]" O R 6000 2200 50 
$EndSheet
Wire Wire Line
	2000 1150 3500 1150
Wire Wire Line
	2000 1350 3500 1350
$Sheet
S 7500 1000 2000 1500
U 61BD71FF
F0 "Shift Registers" 50
F1 "Shift Registers.sch" 50
F2 "SR_SDATA" I L 7500 1100 50 
F3 "SR_RCLK" I L 7500 1300 50 
F4 "SR_SCLK" I L 7500 1200 50 
F5 "~CE" O L 7500 1400 50 
F6 "~OE" O L 7500 1500 50 
F8 "A[8..18]" O L 7500 2200 50 
$EndSheet
Wire Wire Line
	6000 1100 7500 1100
Wire Wire Line
	7500 1200 6000 1200
Wire Wire Line
	6000 1300 7500 1300
$Sheet
S 3500 3500 2500 1500
U 61C0AA4A
F0 "Flash Memory" 50
F1 "Flash Memory.sch" 50
F2 "~FL_WE" I R 6000 3650 50 
F3 "~OE" I R 6000 3950 50 
F4 "~CE" I R 6000 3800 50 
F5 "D[0..7]" B L 3500 4700 50 
F6 "A[0..7]" I L 3500 3850 50 
F7 "A[8..18]" I R 6000 4700 50 
$EndSheet
Wire Wire Line
	6000 1400 6250 1400
Wire Wire Line
	6250 1400 6250 3650
Wire Wire Line
	6250 3650 6000 3650
Wire Wire Line
	6000 3800 6400 3800
Wire Wire Line
	6400 3800 6400 1400
Wire Wire Line
	6400 1400 7500 1400
Wire Wire Line
	7500 1500 6550 1500
Wire Wire Line
	6550 1500 6550 3950
Wire Wire Line
	6550 3950 6000 3950
Wire Bus Line
	7500 2200 7000 2200
Wire Bus Line
	7000 2200 7000 4700
Wire Bus Line
	7000 4700 6000 4700
Wire Bus Line
	3500 3850 3350 3850
Wire Bus Line
	3350 3850 3350 3000
Wire Bus Line
	3350 3000 6100 3000
Wire Bus Line
	6100 3000 6100 2200
Wire Bus Line
	6100 2200 6000 2200
Wire Bus Line
	3500 2200 3150 2200
Wire Bus Line
	3150 2200 3150 4700
Wire Bus Line
	3150 4700 3500 4700
$Comp
L Mechanical:MountingHole H101
U 1 1 61C5F7D2
P 8050 4000
F 0 "H101" H 8150 4046 50  0000 L CNN
F 1 "MountingHole" H 8150 3955 50  0000 L CNN
F 2 "MountingHole:MountingHole_3.2mm_M3" H 8050 4000 50  0001 C CNN
F 3 "~" H 8050 4000 50  0001 C CNN
	1    8050 4000
	1    0    0    -1  
$EndComp
$Comp
L Mechanical:MountingHole H102
U 1 1 61C60227
P 8050 4200
F 0 "H102" H 8150 4246 50  0000 L CNN
F 1 "MountingHole" H 8150 4155 50  0000 L CNN
F 2 "MountingHole:MountingHole_3.2mm_M3" H 8050 4200 50  0001 C CNN
F 3 "~" H 8050 4200 50  0001 C CNN
	1    8050 4200
	1    0    0    -1  
$EndComp
$Comp
L Mechanical:MountingHole H103
U 1 1 61C60496
P 8050 4400
F 0 "H103" H 8150 4446 50  0000 L CNN
F 1 "MountingHole" H 8150 4355 50  0000 L CNN
F 2 "MountingHole:MountingHole_3.2mm_M3" H 8050 4400 50  0001 C CNN
F 3 "~" H 8050 4400 50  0001 C CNN
	1    8050 4400
	1    0    0    -1  
$EndComp
$Comp
L Mechanical:MountingHole H104
U 1 1 61C607D6
P 8050 4600
F 0 "H104" H 8150 4646 50  0000 L CNN
F 1 "MountingHole" H 8150 4555 50  0000 L CNN
F 2 "MountingHole:MountingHole_3.2mm_M3" H 8050 4600 50  0001 C CNN
F 3 "~" H 8050 4600 50  0001 C CNN
	1    8050 4600
	1    0    0    -1  
$EndComp
$EndSCHEMATC
