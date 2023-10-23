EESchema Schematic File Version 4
EELAYER 30 0
EELAYER END
$Descr USLetter 11000 8500
encoding utf-8
Sheet 6 6
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
L DW_Library:32-6554-10 J601
U 1 1 61C174C7
P 5050 3850
F 0 "J601" H 5050 4765 50  0000 C CNN
F 1 "32-6554-10" H 5050 4674 50  0000 C CNN
F 2 "DW_Footprint:32-6554-10" H 5050 4150 50  0001 C CNN
F 3 "https://www.arieselec.com/wp-content/uploads/2020/02/10001-universal-dip-zif-test-socket.pdf" H 5050 4150 50  0001 C CNN
F 4 "https://www.arieselec.com/wp-content/uploads/2020/02/10001-universal-dip-zif-test-socket.pdf" H 5050 3850 50  0001 C CNN "DK_Datasheet_Link"
F 5 "CONN IC DIP SOCKET ZIF 32POS TIN" H 5050 3850 50  0001 C CNN "Description"
F 6 "A304-ND" H 5050 3850 50  0001 C CNN "Digi-Key_PN"
F 7 "Sockets for ICs, Transistors" H 5050 3850 50  0001 C CNN "Family"
F 8 "32-6554-10" H 5050 3850 50  0001 C CNN "MPN"
F 9 "Aries Electronics" H 5050 3850 50  0001 C CNN "Manufacturer"
	1    5050 3850
	1    0    0    -1  
$EndComp
$Comp
L power:VDD #PWR0601
U 1 1 61C1BCE8
P 5650 2500
F 0 "#PWR0601" H 5650 2350 50  0001 C CNN
F 1 "VDD" H 5665 2673 50  0000 C CNN
F 2 "" H 5650 2500 50  0001 C CNN
F 3 "" H 5650 2500 50  0001 C CNN
	1    5650 2500
	1    0    0    -1  
$EndComp
$Comp
L Device:C C601
U 1 1 61C1D85D
P 6000 2650
F 0 "C601" H 6115 2696 50  0000 L CNN
F 1 "0.1uF" H 6115 2605 50  0000 L CNN
F 2 "Capacitor_THT:C_Disc_D4.7mm_W2.5mm_P5.00mm" H 6038 2500 50  0001 C CNN
F 3 "~" H 6000 2650 50  0001 C CNN
F 4 "CAP CER 0.1UF 50V X7R RADIAL" H 6000 2650 50  0001 C CNN "Description"
F 5 "445-173588-1-ND" H 6000 2650 50  0001 C CNN "Digi-Key_PN"
F 6 "Ceramic Capacitors" H 6000 2650 50  0001 C CNN "Family"
F 7 "FG28X7R1H104KNT06" H 6000 2650 50  0001 C CNN "MPN"
F 8 "TDK Corporation" H 6000 2650 50  0001 C CNN "Manufacturer"
	1    6000 2650
	1    0    0    -1  
$EndComp
$Comp
L power:VSS #PWR0603
U 1 1 61C1E3FD
P 4500 5500
F 0 "#PWR0603" H 4500 5350 50  0001 C CNN
F 1 "VSS" H 4515 5673 50  0000 C CNN
F 2 "" H 4500 5500 50  0001 C CNN
F 3 "" H 4500 5500 50  0001 C CNN
	1    4500 5500
	-1   0    0    1   
$EndComp
$Comp
L power:VSS #PWR0602
U 1 1 61C1ECF1
P 6000 2800
F 0 "#PWR0602" H 6000 2650 50  0001 C CNN
F 1 "VSS" H 6015 2973 50  0000 C CNN
F 2 "" H 6000 2800 50  0001 C CNN
F 3 "" H 6000 2800 50  0001 C CNN
	1    6000 2800
	-1   0    0    1   
$EndComp
Wire Wire Line
	6000 2500 5650 2500
Wire Wire Line
	5450 2500 5450 3150
Connection ~ 5650 2500
Wire Wire Line
	5650 2500 5450 2500
Wire Wire Line
	5450 3250 6500 3250
Text Label 6150 3250 0    50   ~ 0
~FL_WE
Text HLabel 6500 3250 2    50   Input ~ 0
~FL_WE
Wire Wire Line
	5450 3950 6500 3950
Wire Wire Line
	5450 4150 6500 4150
Text Label 6150 4150 0    50   ~ 0
~CE
Text Label 6150 3950 0    50   ~ 0
~OE
Text HLabel 6500 3950 2    50   Input ~ 0
~OE
Text HLabel 6500 4150 2    50   Input ~ 0
~CE
Wire Wire Line
	4650 4650 4500 4650
Wire Wire Line
	4500 4650 4500 5500
Wire Wire Line
	5450 4250 6000 4250
Wire Wire Line
	5450 4350 6000 4350
Wire Wire Line
	5450 4450 6000 4450
Wire Wire Line
	5450 4550 6000 4550
Wire Wire Line
	5450 4650 6000 4650
Wire Wire Line
	4650 4350 4000 4350
Wire Wire Line
	4650 4450 4000 4450
Text Label 5750 4250 0    50   ~ 0
D7
Text Label 5750 4350 0    50   ~ 0
D6
Text Label 5750 4450 0    50   ~ 0
D5
Text Label 5750 4550 0    50   ~ 0
D4
Text Label 5750 4650 0    50   ~ 0
D3
Text Label 4150 4350 0    50   ~ 0
D0
Text Label 4150 4450 0    50   ~ 0
D1
Text Label 4150 4550 0    50   ~ 0
D2
Entry Wire Line
	6000 4250 6100 4350
Entry Wire Line
	6000 4350 6100 4450
Entry Wire Line
	6000 4450 6100 4550
Entry Wire Line
	6000 4550 6100 4650
Entry Wire Line
	6000 4650 6100 4750
Entry Wire Line
	4000 4350 3900 4450
Entry Wire Line
	4000 4450 3900 4550
Wire Wire Line
	4000 4550 4650 4550
Entry Wire Line
	4000 4550 3900 4650
Wire Bus Line
	3900 4850 6100 4850
Wire Bus Line
	6100 4850 6100 5500
Connection ~ 6100 4850
Text Label 6100 5200 0    50   ~ 0
D[0..7]
Text HLabel 6100 5500 3    50   BiDi ~ 0
D[0..7]
Wire Wire Line
	4650 3150 3250 3150
Wire Wire Line
	4650 3550 3650 3550
Text Label 3450 3150 0    50   ~ 0
A18
Text Label 3450 3250 0    50   ~ 0
A16
Text Label 3450 3350 0    50   ~ 0
A15
Text Label 3450 3450 0    50   ~ 0
A12
Text Label 3850 3550 0    50   ~ 0
A7
Text Label 3850 3650 0    50   ~ 0
A6
Text Label 3850 3750 0    50   ~ 0
A5
Text Label 3850 3850 0    50   ~ 0
A4
Text Label 3850 3950 0    50   ~ 0
A3
Text Label 3850 4050 0    50   ~ 0
A2
Text Label 3850 4150 0    50   ~ 0
A1
Text Label 3850 4250 0    50   ~ 0
A0
Entry Wire Line
	3650 3550 3550 3650
Entry Wire Line
	3650 3650 3550 3750
Entry Wire Line
	3650 3750 3550 3850
Entry Wire Line
	3650 3850 3550 3950
Entry Wire Line
	3650 3950 3550 4050
Entry Wire Line
	3650 4050 3550 4150
Entry Wire Line
	3650 4150 3550 4250
Entry Wire Line
	3650 4250 3550 4350
Wire Wire Line
	3650 4250 4650 4250
Wire Wire Line
	3650 4150 4650 4150
Wire Wire Line
	3650 4050 4650 4050
Wire Wire Line
	3650 3950 4650 3950
Wire Wire Line
	3650 3850 4650 3850
Wire Wire Line
	3650 3650 4650 3650
Wire Wire Line
	3650 3750 4650 3750
Text Label 3550 5200 0    50   ~ 0
A[0..7]
Text HLabel 3550 5500 3    50   Input ~ 0
A[0..7]
Entry Wire Line
	3250 3150 3150 3250
Entry Wire Line
	3250 3250 3150 3350
Entry Wire Line
	3250 3350 3150 3450
Entry Wire Line
	3250 3450 3150 3550
Wire Wire Line
	3250 3450 4650 3450
Wire Wire Line
	3250 3250 4650 3250
Wire Wire Line
	3250 3350 4650 3350
Wire Wire Line
	5450 3350 7100 3350
Wire Wire Line
	5450 3450 7100 3450
Wire Wire Line
	5450 3550 7100 3550
Wire Wire Line
	5450 3650 7100 3650
Wire Wire Line
	5450 3750 7100 3750
Wire Wire Line
	5450 3850 7100 3850
Wire Wire Line
	5450 4050 7100 4050
Entry Wire Line
	7100 3350 7200 3450
Entry Wire Line
	7100 3450 7200 3550
Entry Wire Line
	7100 3550 7200 3650
Entry Wire Line
	7100 3650 7200 3750
Entry Wire Line
	7100 3750 7200 3850
Entry Wire Line
	7100 3850 7200 3950
Entry Wire Line
	7100 4050 7200 4150
Text Label 6900 3350 0    50   ~ 0
A17
Text Label 6900 3450 0    50   ~ 0
A14
Text Label 6900 3550 0    50   ~ 0
A13
Text Label 6900 3650 0    50   ~ 0
A8
Text Label 6900 3750 0    50   ~ 0
A9
Text Label 6900 3850 0    50   ~ 0
A11
Text Label 6900 4050 0    50   ~ 0
A10
Wire Bus Line
	7200 6000 3900 6000
Wire Bus Line
	3900 6000 3900 5850
Wire Bus Line
	3900 4450 3900 4850
Wire Bus Line
	6100 4350 6100 4850
Wire Bus Line
	3150 3250 3150 6000
Wire Bus Line
	7200 3450 7200 6000
Wire Bus Line
	3550 3650 3550 5500
Connection ~ 3900 6000
Wire Bus Line
	3900 6000 3150 6000
Text Label 4500 6000 0    50   ~ 0
A[8..18]
Text HLabel 3900 5850 1    50   Input ~ 0
A[8..18]
$EndSCHEMATC
