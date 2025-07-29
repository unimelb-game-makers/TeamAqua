INCLUDE ../../Globals/Globals.ink
INCLUDE A0_S2_D1.ink
INCLUDE A0_S2_D2.ink
INCLUDE A0_S2_D0.ink
INCLUDE A0_S2_D3.ink

// Variable Setup
CONST DIALOGUE_0 = "A0_S2_D0"
CONST DIALOGUE_1 = "A0_S2_D1"
CONST DIALOGUE_2 = "A0_S2_D2"
CONST DIALOGUE_3 = "A0_S2_D3"


CONST QUEST_1 = "A1_S1_Q1"

{
    - dialogue_id == DIALOGUE_0:
        -> A0_S2_D1
    - dialogue_id == DIALOGUE_1:
        -> A0_S2_D1
    - dialogue_id == DIALOGUE_2:
        -> A0_S2_D2
    - dialogue_id == DIALOGUE_3:
        -> A0_S2_D3
}