INCLUDE ../../Globals/Globals.ink
INCLUDE A3_S4_D1.ink
INCLUDE A3_S4_D2.ink
INCLUDE A3_S4_D3.ink
INCLUDE A3_S4_Q1_ONGOING.ink
INCLUDE A3_S4_Q1_COMPLETED.ink

// Variable Setup
CONST DIALOGUE_1 = "A3_S4_D1"
CONST DIALOGUE_2 = "A3_S4_D2"
CONST DIALOGUE_3 = "A3_S4_D3"
CONST QUEST_1 = "A3_S4_Q1"
{
    - dialogue_id == DIALOGUE_1:
        -> A3_S4_D1
    - dialogue_id == QUEST_1:
        -> A3_S4_Q1
    - dialogue_id == DIALOGUE_2:
        -> A3_S4_D2
    - dialogue_id == DIALOGUE_3:
        -> A3_S4_D3
}

===A3_S4_Q1===
{
    - quest_state == "ONGOING":
        -> A3_S4_Q1_ONGOING
    - quest_state == "COMPLETED":
        -> A3_S4_Q1_COMPLETED
}