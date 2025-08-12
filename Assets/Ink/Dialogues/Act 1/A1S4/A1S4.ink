INCLUDE ../../Globals/Globals.ink
INCLUDE A1_S4_D1.ink
INCLUDE A1_S4_D2.ink
INCLUDE A1_S4_D3.ink

// Variable Setup
CONST DIALOGUE_1 = "A1_S4_D1"
CONST DIALOGUE_2 = "A1_S4_D2"
CONST DIALOGUE_3 = "A1_S4_D3"

{
    - dialogue_id == DIALOGUE_1:
        -> A1_S4_D1
    - dialogue_id == DIALOGUE_2:
        -> A1_S4_D2
    - dialogue_id == DIALOGUE_3:
        -> A1_S4_D3
}
