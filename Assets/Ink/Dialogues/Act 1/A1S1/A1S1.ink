INCLUDE ../../Globals/Globals.ink
INCLUDE A1_S1_D1.ink
INCLUDE A1_S1_D2.ink
INCLUDE A1_S1_Q1_ONGOING.ink
INCLUDE A1_S1_Q1_COMPLETED.ink

// Variable Setup
CONST DIALOGUE_1 = "A1_S1_D1"
CONST DIALOGUE_2 = "A1_S1_D2"

CONST QUEST_1 = "A1_S1_Q1"

{
    - dialogue_id == DIALOGUE_1:
        -> A1_S1_D1
    - dialogue_id == QUEST_1:
        -> A1_S1_Q1
    - dialogue_id == DIALOGUE_2:
        -> A1_S1_D2
}

===A1_S1_Q1===
{
    - quest_state == "ONGOING":
        -> A1_S1_Q1_ONGOING
    - quest_state == "COMPLETED":
        -> A1_S1_Q1_COMPLETED
}