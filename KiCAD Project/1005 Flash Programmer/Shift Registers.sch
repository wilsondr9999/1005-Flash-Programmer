EESchema Schematic File Version 4
EELAYER 30 0
EELAYER END
$Descr USLetter 11000 8500
encoding utf-8
Sheet 5 6
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
L dk_Logic-Shift-Registers:SN74HC595N U501
U 1 1 61BD783C
P 3850 2800
F 0 "U501" H 3650 3703 60  0000 C CNN
F 1 "SN74HC595NE4" H 3650 3597 60  0000 C CNN
F 2 "digikey-footprints:DIP-16_W7.62mm" H 4050 3000 60  0001 L CNN
F 3 "https://www.ti.com/general/docs/suppproductinfo.tsp?distId=10&gotoUrl=https%3A%2F%2Fwww.ti.com%2Flit%2Fgpn%2Fsn74hc595" H 4050 3100 60  0001 L CNN
F 4 "296-36142-5-ND" H 4050 3200 60  0001 L CNN "Digi-Key_PN"
F 5 "SN74HC595NE4" H 4050 3300 60  0001 L CNN "MPN"
F 6 "Integrated Circuits (ICs)" H 4050 3400 60  0001 L CNN "Category"
F 7 "Logic - Shift Registers" H 4050 3500 60  0001 L CNN "Family"
F 8 "https://www.ti.com/general/docs/suppproductinfo.tsp?distId=10&gotoUrl=https%3A%2F%2Fwww.ti.com%2Flit%2Fgpn%2Fsn74hc595" H 4050 3600 60  0001 L CNN "DK_Datasheet_Link"
F 9 "https://www.digikey.com/en/products/detail/texas-instruments/SN74HC595NE4/1571270" H 4050 3700 60  0001 L CNN "DK_Detail_Page"
F 10 "IC SHIFT REGISTER 8BIT 16-DIP" H 4050 3800 60  0001 L CNN "Description"
F 11 "Texas Instruments" H 4050 3900 60  0001 L CNN "Manufacturer"
F 12 "Active" H 4050 4000 60  0001 L CNN "Status"
	1    3850 2800
	1    0    0    -1  
$EndComp
$Comp
L dk_Logic-Shift-Registers:SN74HC595N U502
U 1 1 61BD9621
P 3850 4900
F 0 "U502" H 3650 5803 60  0000 C CNN
F 1 "SN74HC595NE4" H 3650 5697 60  0000 C CNN
F 2 "digikey-footprints:DIP-16_W7.62mm" H 4050 5100 60  0001 L CNN
F 3 "https://www.ti.com/general/docs/suppproductinfo.tsp?distId=10&gotoUrl=https%3A%2F%2Fwww.ti.com%2Flit%2Fgpn%2Fsn74hc595" H 4050 5200 60  0001 L CNN
F 4 "296-36142-5-ND" H 4050 5300 60  0001 L CNN "Digi-Key_PN"
F 5 "SN74HC595NE4" H 4050 5400 60  0001 L CNN "MPN"
F 6 "Integrated Circuits (ICs)" H 4050 5500 60  0001 L CNN "Category"
F 7 "Logic - Shift Registers" H 4050 5600 60  0001 L CNN "Family"
F 8 "https://www.ti.com/general/docs/suppproductinfo.tsp?distId=10&gotoUrl=https%3A%2F%2Fwww.ti.com%2Flit%2Fgpn%2Fsn74hc595" H 4050 5700 60  0001 L CNN "DK_Datasheet_Link"
F 9 "https://www.digikey.com/en/products/detail/texas-instruments/SN74HC595NE4/1571270" H 4050 5800 60  0001 L CNN "DK_Detail_Page"
F 10 "IC SHIFT REGISTER 8BIT 16-DIP" H 4050 5900 60  0001 L CNN "Description"
F 11 "Texas Instruments" H 4050 6000 60  0001 L CNN "Manufacturer"
F 12 "Active" H 4050 6100 60  0001 L CNN "Status"
	1    3850 4900
	1    0    0    -1  
$EndComp
$Comp
L power:VDD #PWR0501
U 1 1 61BDC230
P 4650 1200
F 0 "#PWR0501" H 4650 1050 50  0001 C CNN
F 1 "VDD" H 4665 1373 50  0000 C CNN
F 2 "" H 4650 1200 50  0001 C CNN
F 3 "" H 4650 1200 50  0001 C CNN
	1    4650 1200
	1    0    0    -1  
$EndComp
$Comp
L Device:C C501
U 1 1 61BDD96A
P 4650 1600
F 0 "C501" H 4765 1646 50  0000 L CNN
F 1 "0.1uF" H 4765 1555 50  0000 L CNN
F 2 "Capacitor_THT:C_Disc_D4.7mm_W2.5mm_P5.00mm" H 4688 1450 50  0001 C CNN
F 3 "~" H 4650 1600 50  0001 C CNN
F 4 "CAP CER 0.1UF 50V X7R RADIAL" H 4650 1600 50  0001 C CNN "Description"
F 5 "445-173588-1-ND" H 4650 1600 50  0001 C CNN "Digi-Key_PN"
F 6 "Ceramic Capacitors" H 4650 1600 50  0001 C CNN "Family"
F 7 "FG28X7R1H104KNT06" H 4650 1600 50  0001 C CNN "MPN"
F 8 "TDK Corporation" H 4650 1600 50  0001 C CNN "Manufacturer"
	1    4650 1600
	1    0    0    -1  
$EndComp
$Comp
L power:VSS #PWR0502
U 1 1 61BDED6F
P 4650 1750
F 0 "#PWR0502" H 4650 1600 50  0001 C CNN
F 1 "VSS" H 4665 1923 50  0000 C CNN
F 2 "" H 4650 1750 50  0001 C CNN
F 3 "" H 4650 1750 50  0001 C CNN
	1    4650 1750
	-1   0    0    1   
$EndComp
Wire Wire Line
	3650 2100 3650 1450
Wire Wire Line
	3650 1450 4450 1450
Wire Wire Line
	4650 1450 4650 1200
Connection ~ 4650 1450
Wire Wire Line
	3650 4200 4450 4200
Wire Wire Line
	4450 4200 4450 1450
Connection ~ 4450 1450
Wire Wire Line
	4450 1450 4650 1450
$Comp
L power:VSS #PWR0503
U 1 1 61BDFFB8
P 3550 6250
F 0 "#PWR0503" H 3550 6100 50  0001 C CNN
F 1 "VSS" H 3565 6423 50  0000 C CNN
F 2 "" H 3550 6250 50  0001 C CNN
F 3 "" H 3550 6250 50  0001 C CNN
	1    3550 6250
	-1   0    0    1   
$EndComp
Wire Wire Line
	3550 6250 3550 6150
Wire Wire Line
	3550 3400 4400 3400
Wire Wire Line
	4400 3400 4400 6150
Wire Wire Line
	4400 6150 3550 6150
Connection ~ 3550 6150
Wire Wire Line
	3550 6150 3550 5500
Wire Wire Line
	4050 3200 4050 3750
Wire Wire Line
	4050 3750 3150 3750
Wire Wire Line
	3150 4600 3250 4600
NoConn ~ 4050 5300
Wire Wire Line
	3250 2600 2950 2600
Wire Wire Line
	2950 2600 2950 4700
Wire Wire Line
	2950 6150 3550 6150
Wire Wire Line
	3250 4700 2950 4700
Connection ~ 2950 4700
Wire Wire Line
	2950 4700 2950 6150
Wire Wire Line
	3250 2700 2850 2700
Wire Wire Line
	2850 2700 2850 4800
Wire Wire Line
	2850 4800 3250 4800
Wire Wire Line
	3250 2800 2750 2800
Wire Wire Line
	2750 2800 2750 4900
Wire Wire Line
	2750 4900 3250 4900
Wire Wire Line
	3150 3750 3150 4600
Wire Wire Line
	3250 2900 3050 2900
Wire Wire Line
	3050 2900 3050 5000
Wire Wire Line
	3050 5000 3250 5000
Wire Wire Line
	3050 2900 3050 1450
Wire Wire Line
	3050 1450 3650 1450
Connection ~ 3050 2900
Connection ~ 3650 1450
Wire Wire Line
	3250 2500 2200 2500
Text Label 2400 2500 0    50   ~ 0
SR_SDATA
Wire Wire Line
	2850 2700 2200 2700
Connection ~ 2850 2700
Text Label 2400 2700 0    50   ~ 0
SR_RCLK
Wire Wire Line
	2750 2800 2200 2800
Connection ~ 2750 2800
Text Label 2400 2800 0    50   ~ 0
SR_SCLK
Text HLabel 2200 2500 0    50   Input ~ 0
SR_SDATA
Text HLabel 2200 2700 0    50   Input ~ 0
SR_RCLK
Text HLabel 2200 2800 0    50   Input ~ 0
SR_SCLK
Wire Wire Line
	4050 2400 5400 2400
Wire Wire Line
	4050 2500 5400 2500
Wire Wire Line
	4050 2600 5400 2600
Wire Wire Line
	4050 2700 5400 2700
Wire Wire Line
	4050 2800 5400 2800
Wire Wire Line
	4050 2900 5400 2900
Wire Wire Line
	4050 3000 5400 3000
Wire Wire Line
	4050 3100 5400 3100
Wire Wire Line
	4050 4500 5400 4500
Wire Wire Line
	4050 4600 5400 4600
Wire Wire Line
	4050 4700 5400 4700
Wire Wire Line
	4050 5100 5050 5100
Wire Wire Line
	4050 5200 4950 5200
Text Label 5100 2400 0    50   ~ 0
A8
Text Label 5100 2500 0    50   ~ 0
A9
Text Label 5100 2600 0    50   ~ 0
A10
Text Label 5100 2700 0    50   ~ 0
A11
Text Label 5100 2800 0    50   ~ 0
A12
Text Label 5100 2900 0    50   ~ 0
A13
Text Label 5100 3000 0    50   ~ 0
A14
Text Label 5100 3100 0    50   ~ 0
A15
Text Label 5100 4500 0    50   ~ 0
A16
Text Label 5100 4600 0    50   ~ 0
A17
Text Label 5100 4700 0    50   ~ 0
A18
Text Label 4700 5100 0    50   ~ 0
~OE
Text Label 4700 5200 0    50   ~ 0
~CE
Entry Wire Line
	5400 2400 5500 2500
Entry Wire Line
	5400 2500 5500 2600
Entry Wire Line
	5400 2600 5500 2700
Entry Wire Line
	5400 2700 5500 2800
Entry Wire Line
	5400 2800 5500 2900
Entry Wire Line
	5400 2900 5500 3000
Entry Wire Line
	5400 3000 5500 3100
Entry Wire Line
	5400 3100 5500 3200
Entry Wire Line
	5400 4500 5500 4600
Entry Wire Line
	5400 4600 5500 4700
Entry Wire Line
	5400 4700 5500 4800
Text Label 5500 5700 0    50   ~ 0
A[8..18]
Wire Wire Line
	4950 5200 4950 6150
Wire Wire Line
	5050 5100 5050 6150
Text HLabel 4950 6150 3    50   Output ~ 0
~CE
Text HLabel 5050 6150 3    50   Output ~ 0
~OE
Text HLabel 5500 6150 3    50   Output ~ 0
A[8..18]
NoConn ~ 4050 4800
NoConn ~ 4050 4900
NoConn ~ 4050 5000
$Comp
L Device:C C502
U 1 1 61C41A0B
P 5150 1600
F 0 "C502" H 5265 1646 50  0000 L CNN
F 1 "0.1uF" H 5265 1555 50  0000 L CNN
F 2 "Capacitor_THT:C_Disc_D4.7mm_W2.5mm_P5.00mm" H 5188 1450 50  0001 C CNN
F 3 "~" H 5150 1600 50  0001 C CNN
F 4 "CAP CER 0.1UF 50V X7R RADIAL" H 5150 1600 50  0001 C CNN "Description"
F 5 "445-173588-1-ND" H 5150 1600 50  0001 C CNN "Digi-Key_PN"
F 6 "Ceramic Capacitors" H 5150 1600 50  0001 C CNN "Family"
F 7 "FG28X7R1H104KNT06" H 5150 1600 50  0001 C CNN "MPN"
F 8 "TDK Corporation" H 5150 1600 50  0001 C CNN "Manufacturer"
	1    5150 1600
	1    0    0    -1  
$EndComp
$Comp
L power:VSS #PWR0504
U 1 1 61C42389
P 5150 1750
F 0 "#PWR0504" H 5150 1600 50  0001 C CNN
F 1 "VSS" H 5165 1923 50  0000 C CNN
F 2 "" H 5150 1750 50  0001 C CNN
F 3 "" H 5150 1750 50  0001 C CNN
	1    5150 1750
	-1   0    0    1   
$EndComp
Wire Wire Line
	4650 1450 5150 1450
Wire Bus Line
	5500 2500 5500 6150
$EndSCHEMATC
