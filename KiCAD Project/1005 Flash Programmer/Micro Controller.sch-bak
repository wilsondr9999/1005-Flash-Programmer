EESchema Schematic File Version 4
EELAYER 30 0
EELAYER END
$Descr USLetter 11000 8500
encoding utf-8
Sheet 4 6
Title "1005 Flash Programmer"
Date "2021-12-12"
Rev "1"
Comp "Dan Wilson"
Comment1 ""
Comment2 ""
Comment3 "Copyright © 2021 Dan Wilson"
Comment4 "SST39SF0x0A Flash Programmer"
$EndDescr
$Comp
L MCU_Microchip_PIC16:PIC16F15356-xSP U401
U 1 1 61B83F45
P 5400 3950
F 0 "U401" H 5400 3950 50  0000 C CNN
F 1 "PIC16F15256-xSP" H 5400 3850 50  0000 C CNN
F 2 "Package_DIP:DIP-28_W7.62mm" H 4600 4450 50  0001 C CNN
F 3 "https://ww1.microchip.com/downloads/en/DeviceDoc/PIC16F15256-74-75-76-28-40-Pin-Microcontrollers-40002305B.pdf" H 4600 4450 50  0001 C CNN
F 4 "https://ww1.microchip.com/downloads/en/DeviceDoc/PIC16F15256-74-75-76-28-40-Pin-Microcontrollers-40002305B.pdf" H 5400 3950 50  0001 C CNN "DK_Datasheet_Link"
F 5 "IC MCU 8BIT 28KB FLASH 28SPDIP" H 5400 3950 50  0001 C CNN "Description"
F 6 "150-PIC16F15256-I/SP-ND" H 5400 3950 50  0001 C CNN "Digi-Key_PN"
F 7 "Embedded - Microcontrollers" H 5400 3950 50  0001 C CNN "Family"
F 8 "PIC16F15256-I/SP" H 5400 3950 50  0001 C CNN "MPN"
F 9 "Microchip Technology" H 5400 3950 50  0001 C CNN "Manufacturer"
	1    5400 3950
	1    0    0    -1  
$EndComp
$Comp
L power:VDD #PWR0401
U 1 1 61B85C8A
P 5950 2450
F 0 "#PWR0401" H 5950 2300 50  0001 C CNN
F 1 "VDD" H 5965 2623 50  0000 C CNN
F 2 "" H 5950 2450 50  0001 C CNN
F 3 "" H 5950 2450 50  0001 C CNN
	1    5950 2450
	1    0    0    -1  
$EndComp
Wire Wire Line
	5950 2450 5400 2450
Wire Wire Line
	5400 2450 5400 2950
$Comp
L power:VSS #PWR0403
U 1 1 61B868CC
P 5300 5700
F 0 "#PWR0403" H 5300 5550 50  0001 C CNN
F 1 "VSS" H 5315 5873 50  0000 C CNN
F 2 "" H 5300 5700 50  0001 C CNN
F 3 "" H 5300 5700 50  0001 C CNN
	1    5300 5700
	-1   0    0    1   
$EndComp
Wire Wire Line
	5300 4950 5300 5050
Wire Wire Line
	5400 4950 5400 5050
Wire Wire Line
	5400 5050 5300 5050
Connection ~ 5300 5050
Wire Wire Line
	5300 5050 5300 5700
$Comp
L Device:R R401
U 1 1 61B874CF
P 4550 2600
F 0 "R401" H 4620 2646 50  0000 L CNN
F 1 "10K" H 4620 2555 50  0000 L CNN
F 2 "Resistor_THT:R_Axial_DIN0207_L6.3mm_D2.5mm_P10.16mm_Horizontal" V 4480 2600 50  0001 C CNN
F 3 "~" H 4550 2600 50  0001 C CNN
F 4 "https://www.yageo.com/upload/media/product/productsearch/datasheet/lr/YAGEO%20MFR_datasheet_2021v1.pdf" H 4550 2600 50  0001 C CNN "DK_Datasheet_Link"
F 5 "RES 10K OHM 1% 1/4W AXIAL" H 4550 2600 50  0001 C CNN "Description"
F 6 "10.0KXBK-ND" H 4550 2600 50  0001 C CNN "Digi-Key_PN"
F 7 "Through Hole Resistors" H 4550 2600 50  0001 C CNN "Family"
F 8 "MFR-25FBF52-10K" H 4550 2600 50  0001 C CNN "MPN"
F 9 "YAGEO" H 4550 2600 50  0001 C CNN "Manufacturer"
	1    4550 2600
	1    0    0    -1  
$EndComp
Connection ~ 5400 2450
Wire Wire Line
	4600 3150 4550 3150
Text Label 4500 2050 0    50   ~ 0
~MCLR
$Comp
L Device:C C401
U 1 1 61B887D7
P 5950 2600
F 0 "C401" H 6065 2646 50  0000 L CNN
F 1 "0.1uF" H 6065 2555 50  0000 L CNN
F 2 "Capacitor_THT:C_Disc_D4.7mm_W2.5mm_P5.00mm" H 5988 2450 50  0001 C CNN
F 3 "~" H 5950 2600 50  0001 C CNN
F 4 "CAP CER 0.1UF 50V X7R RADIAL" H 5950 2600 50  0001 C CNN "Description"
F 5 "445-173588-1-ND" H 5950 2600 50  0001 C CNN "Digi-Key_PN"
F 6 "Ceramic Capacitors" H 5950 2600 50  0001 C CNN "Family"
F 7 "FG28X7R1H104KNT06" H 5950 2600 50  0001 C CNN "MPN"
F 8 "TDK Corporation" H 5950 2600 50  0001 C CNN "Manufacturer"
	1    5950 2600
	1    0    0    -1  
$EndComp
Connection ~ 5950 2450
$Comp
L power:VSS #PWR0402
U 1 1 61B88F82
P 5950 2750
F 0 "#PWR0402" H 5950 2600 50  0001 C CNN
F 1 "VSS" H 5965 2923 50  0000 C CNN
F 2 "" H 5950 2750 50  0001 C CNN
F 3 "" H 5950 2750 50  0001 C CNN
	1    5950 2750
	-1   0    0    1   
$EndComp
$Comp
L Connector_Generic:Conn_01x05 J401
U 1 1 61B89A86
P 5400 1850
F 0 "J401" V 5364 1562 50  0000 R CNN
F 1 "ICSP" V 5273 1562 50  0000 R CNN
F 2 "Connector_PinHeader_2.54mm:PinHeader_1x05_P2.54mm_Vertical" H 5400 1850 50  0001 C CNN
F 3 "https://app.adam-tech.com/products/download/data_sheet/201605/ph1-xx-ua-data-sheet.pdf" H 5400 1850 50  0001 C CNN
F 4 "https://app.adam-tech.com/products/download/data_sheet/201605/ph1-xx-ua-data-sheet.pdf" H 5400 1850 50  0001 C CNN "DK_Datasheet_Link"
F 5 "CONN HEADER VERT 5POS 2.54MM" H 5400 1850 50  0001 C CNN "Description"
F 6 "2057-PH1-05-UA-ND" H 5400 1850 50  0001 C CNN "Digi-Key_PN"
F 7 "Rectangular Connectors - Headers, Male Pins" H 5400 1850 50  0001 C CNN "Family"
F 8 "PH1-05-UA" H 5400 1850 50  0001 C CNN "MPN"
F 9 "Adam Tech" H 5400 1850 50  0001 C CNN "Manufacturer"
	1    5400 1850
	0    -1   -1   0   
$EndComp
Wire Wire Line
	4300 3150 4300 2050
Wire Wire Line
	4300 2050 5200 2050
Wire Wire Line
	5300 2050 5300 2450
Connection ~ 5300 2450
Wire Wire Line
	5300 2450 5400 2450
Wire Wire Line
	5400 5050 6550 5050
Wire Wire Line
	6550 5050 6550 2200
Wire Wire Line
	6550 2200 5400 2200
Wire Wire Line
	5400 2200 5400 2050
Connection ~ 5400 5050
Wire Wire Line
	5500 2050 5500 2150
Wire Wire Line
	5500 2150 6600 2150
Wire Wire Line
	6600 2150 6600 4750
Wire Wire Line
	6600 4750 6200 4750
Wire Wire Line
	5600 2050 5600 2100
Wire Wire Line
	5600 2100 6650 2100
Wire Wire Line
	6650 2100 6650 4650
Wire Wire Line
	6650 4650 6200 4650
Wire Wire Line
	4550 2450 5300 2450
Wire Wire Line
	4550 2750 4550 3150
Connection ~ 4550 3150
Wire Wire Line
	4550 3150 4300 3150
Wire Wire Line
	6600 4750 7050 4750
Connection ~ 6600 4750
Wire Wire Line
	6650 4650 7050 4650
Connection ~ 6650 4650
Wire Wire Line
	6200 4550 7050 4550
Wire Wire Line
	6200 4450 7050 4450
Wire Wire Line
	6200 4350 7050 4350
Wire Wire Line
	6200 4250 7050 4250
Wire Wire Line
	6200 4150 7050 4150
Wire Wire Line
	6200 4050 7050 4050
Text Label 6800 4050 0    50   ~ 0
D0
Text Label 6800 4150 0    50   ~ 0
D1
Text Label 6800 4250 0    50   ~ 0
D2
Text Label 6800 4350 0    50   ~ 0
D3
Text Label 6800 4450 0    50   ~ 0
D4
Text Label 6800 4550 0    50   ~ 0
D5
Text Label 6800 4650 0    50   ~ 0
D6
Text Label 6800 4750 0    50   ~ 0
D7
Entry Wire Line
	7050 4050 7150 4150
Entry Wire Line
	7050 4150 7150 4250
Entry Wire Line
	7050 4250 7150 4350
Entry Wire Line
	7050 4350 7150 4450
Entry Wire Line
	7050 4450 7150 4550
Entry Wire Line
	7050 4550 7150 4650
Entry Wire Line
	7050 4650 7150 4750
Entry Wire Line
	7050 4750 7150 4850
Text Label 7150 5300 0    50   ~ 0
D[0..7]
Wire Wire Line
	6200 3150 7550 3150
Wire Wire Line
	6200 3250 7550 3250
Wire Wire Line
	6200 3350 7550 3350
Wire Wire Line
	6200 3450 7550 3450
Wire Wire Line
	6200 3550 7550 3550
Wire Wire Line
	6200 3650 7550 3650
Wire Wire Line
	6200 3750 7550 3750
Wire Wire Line
	6200 3850 7550 3850
Text Label 7300 3150 0    50   ~ 0
A0
Text Label 7300 3250 0    50   ~ 0
A1
Text Label 7300 3350 0    50   ~ 0
A2
Text Label 7300 3450 0    50   ~ 0
A3
Text Label 7300 3550 0    50   ~ 0
A4
Text Label 7300 3650 0    50   ~ 0
A5
Text Label 7300 3750 0    50   ~ 0
A6
Text Label 7300 3850 0    50   ~ 0
A7
Entry Wire Line
	7550 3150 7650 3250
Entry Wire Line
	7550 3250 7650 3350
Entry Wire Line
	7550 3350 7650 3450
Entry Wire Line
	7550 3450 7650 3550
Entry Wire Line
	7550 3550 7650 3650
Entry Wire Line
	7550 3650 7650 3750
Entry Wire Line
	7550 3750 7650 3850
Entry Wire Line
	7550 3850 7650 3950
Text Label 7650 5300 0    50   ~ 0
A[0..7]
Wire Wire Line
	4600 4050 3800 4050
Wire Wire Line
	4600 4150 3800 4150
Wire Wire Line
	4600 4250 3800 4250
Wire Wire Line
	4600 4350 3800 4350
Wire Wire Line
	4600 4650 3800 4650
Wire Wire Line
	4600 4750 3800 4750
Text Label 3850 4050 0    50   ~ 0
SR_SDATA
Text Label 3850 4150 0    50   ~ 0
SR_SCLK
Text Label 3850 4250 0    50   ~ 0
SR_RCLK
Text Label 3850 4350 0    50   ~ 0
~FL_WE
Text Label 3850 4450 0    50   ~ 0
LED_uTX
Text Label 3850 4550 0    50   ~ 0
LED_uRX
Text Label 3850 4650 0    50   ~ 0
USART_uTX
Text Label 3850 4750 0    50   ~ 0
USART_uRX
$Comp
L Device:R R402
U 1 1 61BA7A58
P 2850 5100
F 0 "R402" H 2920 5146 50  0000 L CNN
F 1 "300" H 2920 5055 50  0000 L CNN
F 2 "Resistor_THT:R_Axial_DIN0207_L6.3mm_D2.5mm_P10.16mm_Horizontal" V 2780 5100 50  0001 C CNN
F 3 "https://www.yageo.com/upload/media/product/productsearch/datasheet/lr/YAGEO%20MFR_datasheet_2021v1.pdf" H 2850 5100 50  0001 C CNN
F 4 "https://www.yageo.com/upload/media/product/productsearch/datasheet/lr/YAGEO%20MFR_datasheet_2021v1.pdf" H 2850 5100 50  0001 C CNN "DK_Datasheet_Link"
F 5 "RES 300 OHM 1% 1/4W AXIAL" H 2850 5100 50  0001 C CNN "Description"
F 6 "MFR-25FBF52-300R-ND" H 2850 5100 50  0001 C CNN "Digi-Key_PN"
F 7 "Through Hole Resistors" H 2850 5100 50  0001 C CNN "Family"
F 8 "MFR-25FBF52-300R" H 2850 5100 50  0001 C CNN "MPN"
F 9 "YAGEO" H 2850 5100 50  0001 C CNN "Manufacturer"
	1    2850 5100
	1    0    0    -1  
$EndComp
$Comp
L Device:R R403
U 1 1 61BA86EC
P 3150 5100
F 0 "R403" H 3220 5146 50  0000 L CNN
F 1 "300" H 3220 5055 50  0000 L CNN
F 2 "Resistor_THT:R_Axial_DIN0207_L6.3mm_D2.5mm_P10.16mm_Horizontal" V 3080 5100 50  0001 C CNN
F 3 "https://www.yageo.com/upload/media/product/productsearch/datasheet/lr/YAGEO%20MFR_datasheet_2021v1.pdf" H 3150 5100 50  0001 C CNN
F 4 "https://www.yageo.com/upload/media/product/productsearch/datasheet/lr/YAGEO%20MFR_datasheet_2021v1.pdf" H 3150 5100 50  0001 C CNN "DK_Datasheet_Link"
F 5 "RES 300 OHM 1% 1/4W AXIAL" H 3150 5100 50  0001 C CNN "Description"
F 6 "MFR-25FBF52-300R-ND" H 3150 5100 50  0001 C CNN "Digi-Key_PN"
F 7 "Through Hole Resistors" H 3150 5100 50  0001 C CNN "Family"
F 8 "MFR-25FBF52-300R" H 3150 5100 50  0001 C CNN "MPN"
F 9 "YAGEO" H 3150 5100 50  0001 C CNN "Manufacturer"
	1    3150 5100
	1    0    0    -1  
$EndComp
$Comp
L Device:LED D401
U 1 1 61BACD14
P 2850 5550
F 0 "D401" V 2889 5432 50  0000 R CNN
F 1 "LED" V 2798 5432 50  0000 R CNN
F 2 "LED_THT:LED_D5.0mm" H 2850 5550 50  0001 C CNN
F 3 "https://media.digikey.com/pdf/Data%20Sheets/Lite-On%20PDFs/LTL-307A.pdf" H 2850 5550 50  0001 C CNN
F 4 "https://media.digikey.com/pdf/Data%20Sheets/Lite-On%20PDFs/LTL-307A.pdf" H 2850 5550 50  0001 C CNN "DK_Datasheet_Link"
F 5 "LED AMBER DIFFUSED T-1 3/4 T/H" H 2850 5550 50  0001 C CNN "Description"
F 6 "160-1949-ND" H 2850 5550 50  0001 C CNN "Digi-Key_PN"
F 7 "LED Indication - Discrete" H 2850 5550 50  0001 C CNN "Family"
F 8 "LTL-307A" H 2850 5550 50  0001 C CNN "MPN"
F 9 "LITEON" H 2850 5550 50  0001 C CNN "Manufacturer"
	1    2850 5550
	0    -1   -1   0   
$EndComp
Wire Wire Line
	2850 4550 2850 4950
Wire Wire Line
	2850 4550 4600 4550
Wire Wire Line
	2850 5250 2850 5400
Wire Wire Line
	3150 4450 3150 4950
Wire Wire Line
	3150 4450 4600 4450
$Comp
L Device:LED D402
U 1 1 61BB9171
P 3150 5550
F 0 "D402" V 3189 5432 50  0000 R CNN
F 1 "LED" V 3098 5432 50  0000 R CNN
F 2 "LED_THT:LED_D5.0mm" H 3150 5550 50  0001 C CNN
F 3 "https://media.digikey.com/pdf/Data%20Sheets/Lite-On%20PDFs/LTL-4233.pdf" H 3150 5550 50  0001 C CNN
F 4 "https://media.digikey.com/pdf/Data%20Sheets/Lite-On%20PDFs/LTL-4233.pdf" H 3150 5550 50  0001 C CNN "DK_Datasheet_Link"
F 5 "LED GREEN DIFFUSED T-1 3/4 T/H" H 3150 5550 50  0001 C CNN "Description"
F 6 "160-1130-ND" H 3150 5550 50  0001 C CNN "Digi-Key_PN"
F 7 "LED Indication - Discrete" H 3150 5550 50  0001 C CNN "Family"
F 8 "LTL-4233" H 3150 5550 50  0001 C CNN "MPN"
F 9 "LITEON" H 3150 5550 50  0001 C CNN "Manufacturer"
	1    3150 5550
	0    -1   -1   0   
$EndComp
Wire Wire Line
	3150 5250 3150 5400
Wire Wire Line
	2850 5700 3150 5700
Connection ~ 3150 5700
Wire Wire Line
	3150 5700 5300 5700
Text HLabel 3800 4050 0    50   Output ~ 0
SR_SDATA
Text HLabel 3800 4150 0    50   Output ~ 0
SR_SCLK
Text HLabel 3800 4250 0    50   Output ~ 0
SR_RCLK
Text HLabel 3800 4350 0    50   Output ~ 0
~FL_WE
Text HLabel 3800 4650 0    50   Output ~ 0
USART_uTX
Text HLabel 3800 4750 0    50   Input ~ 0
USART_uRX
Connection ~ 5300 5700
Wire Bus Line
	7650 3250 7650 5450
Wire Bus Line
	7150 4150 7150 5450
Text HLabel 7150 5450 3    50   BiDi ~ 0
D[0..7]
Text HLabel 7650 5450 3    50   Output ~ 0
A[0..7]
$EndSCHEMATC
