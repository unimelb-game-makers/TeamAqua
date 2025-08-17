INCLUDE ../../Globals/Globals.ink
INCLUDE A5_S1_D1.ink
INCLUDE A5_S1_D2.ink

// Variable Setup
CONST DIALOGUE_1 = "A5_S1_D1"
CONST DIALOGUE_2 = "A5_S1_D2"

{
    - dialogue_id == DIALOGUE_1:
        -> A5_S1_D1
    - dialogue_id == DIALOGUE_2:
        -> A5_S1_D2
}