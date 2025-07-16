INCLUDE ../../Globals/Globals.ink
INCLUDE A1_S2_D1.ink
INCLUDE A1_S2_D2.ink
INCLUDE A1_S2_D3.ink
INCLUDE A1_S2_D4.ink
INCLUDE A1_S2_D5.ink
INCLUDE A1_S2_D6.ink
INCLUDE A1_S2_Q1_ONGOING.ink
INCLUDE A1_S2_Q1_COMPLETED.ink

// Variable Setup
CONST DIALOGUE_1 = "A1_S2_D1"
CONST DIALOGUE_2 = "A1_S2_D2"
CONST DIALOGUE_3 = "A1_S2_D3"
CONST DIALOGUE_4 = "A1_S2_D4"
CONST DIALOGUE_5 = "A1_S2_D5"
CONST QUEST_1 = "A1_S2_Q1"
CONST DIALOGUE_6 = "A1_S2_D6"
{
    - dialogue_id == DIALOGUE_1:
        -> A1_S2_D1
    - dialogue_id == DIALOGUE_2:
        -> A1_S2_D2
    - dialogue_id == DIALOGUE_3:
        -> A1_S2_D3
    - dialogue_id == DIALOGUE_4:
        -> A1_S2_D4
    - dialogue_id == DIALOGUE_5:
        -> A1_S2_D5
    - dialogue_id == QUEST_1:
        -> A1_S2_Q1
    - dialogue_id == DIALOGUE_6:
        -> A1_S2_D6
}

===A1_S2_Q1===
{
    - quest_state == "ONGOING":
        -> A1_S2_Q1_ONGOING
    - quest_state == "COMPLETED":
        -> A1_S2_Q1_COMPLETED
}
