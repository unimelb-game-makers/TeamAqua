INCLUDE ../../Globals/Globals.ink
INCLUDE A4_S3_D1.ink
INCLUDE A4_S3_D2.ink
INCLUDE A4_S3_D3.ink
INCLUDE A4_S3_Q1_ONGOING.ink
INCLUDE A4_S3_Q1_COMPLETED.ink

// Variable Setup
CONST DIALOGUE_1 = "A4_S3_D1"
CONST DIALOGUE_2 = "A4_S3_D2"
CONST DIALOGUE_3 = "A4_S3_D3"
CONST QUEST_1 = "A4_S3_Q1"

{
    - dialogue_id == DIALOGUE_1:
        -> A4_S3_D1
    - dialogue_id == DIALOGUE_2:
        -> A4_S3_D2
    - dialogue_id == DIALOGUE_3:
        -> A4_S3_D3
    - dialogue_id == QUEST_1:
        -> A4_S3_Q1
}

===A4_S3_Q1===
{
    - quest_state == "ONGOING":
        -> A4_S3_Q1_ONGOING
    - quest_state == "COMPLETED":
        -> A4_S3_Q1_COMPLETED
}