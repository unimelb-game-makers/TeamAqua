INCLUDE ../../Globals/Globals.ink
INCLUDE A3_S3_D1.ink
INCLUDE A3_S3_D2.ink

// Variable Setup
CONST DIALOGUE_1 = "A3_S3_D1"
CONST DIALOGUE_2 = "A3_S3_D2"

{
    - dialogue_id == DIALOGUE_1:
        -> A3_S3_D1
    - dialogue_id == DIALOGUE_2:
        -> A3_S3_D2
}