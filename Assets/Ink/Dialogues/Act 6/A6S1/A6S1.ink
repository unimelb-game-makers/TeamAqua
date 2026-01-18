INCLUDE ../../Globals/Globals.ink
INCLUDE A6_S1_D1.ink
INCLUDE A6_S1_D2.ink
INCLUDE A6_S1_D3.ink
INCLUDE A6_S1_D4.ink
// Variable Setup
CONST DIALOGUE_1 = "A6_S1_D1"
CONST DIALOGUE_2 = "A6_S1_D2"
CONST DIALOGUE_3 = "A6_S1_D3"
CONST DIALOGUE_4 = "A6_S1_D4"
{
	- dialogue_id == DIALOGUE_1:
		-> A6_S1_D1
	- dialogue_id == DIALOGUE_2:
		-> A6_S1_D2
	- dialogue_id == DIALOGUE_3:
		-> A6_S1_D3
	- dialogue_id == DIALOGUE_4:
		-> A6_S1_D4
}
