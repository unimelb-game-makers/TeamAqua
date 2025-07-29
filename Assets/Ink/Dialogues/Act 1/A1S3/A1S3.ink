INCLUDE ../../Globals/Globals.ink
INCLUDE A1_S3_D1.ink
INCLUDE A1_S3_D2.ink

// Variable Setup
CONST DIALOGUE_1 = "A1_S3_D1"
CONST DIALOGUE_2 = "A1_S3_D2"


{
    - dialogue_id == DIALOGUE_1:
        -> A1_S3_D1
    - dialogue_id == DIALOGUE_2:
        -> A1_S3_D2
}
