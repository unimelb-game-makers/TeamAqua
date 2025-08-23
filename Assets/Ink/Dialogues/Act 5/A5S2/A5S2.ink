INCLUDE ../../Globals/Globals.ink
INCLUDE A5_S2_D1.ink
INCLUDE A5_S2_D2.ink
INCLUDE A5_S2_D3.ink


// Variable Setup
CONST DIALOGUE_1 = "A5_S2_D1"
CONST DIALOGUE_2 = "A5_S2_D2"
CONST DIALOGUE_3 = "A5_S2_D3"

{
    - dialogue_id == DIALOGUE_1:
        -> A5_S2_D1
    - dialogue_id == DIALOGUE_2:
        -> A5_S2_D2
    - dialogue_id == DIALOGUE_3:
        -> A5_S2_D3
}