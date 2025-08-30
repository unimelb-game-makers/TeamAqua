INCLUDE ../../Globals/Globals.ink
INCLUDE A5_S4_D1.ink
INCLUDE A5_S4_D2.ink
INCLUDE A5_S4_D3.ink
INCLUDE A5_S4_D4.ink
INCLUDE A5_S4_D5.ink
// Variable Setup
CONST DIALOGUE_1 = "A5_S4_D1"
CONST DIALOGUE_2 = "A5_S4_D2"
CONST DIALOGUE_3 = "A5_S4_D3"
CONST DIALOGUE_4 = "A5_S4_D4"
CONST DIALOGUE_5 = "A5_S4_D5"
{
	- dialogue_id == DIALOGUE_1:
		-> A5_S4_D1
	- dialogue_id == DIALOGUE_2:
		-> A5_S4_D2
	- dialogue_id == DIALOGUE_3:
		-> A5_S4_D3
	- dialogue_id == DIALOGUE_4:
		-> A5_S4_D4
	- dialogue_id == DIALOGUE_5:
		-> A5_S4_D5
}
