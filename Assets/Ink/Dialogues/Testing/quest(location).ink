INCLUDE ../Globals/Globals.ink
EXTERNAL checkQuestStatus(id, step)

VAR quest_Steps = ""

~ checkQuestStatus(2,1)
quest step is {quest_state}
{
    - quest_Steps == "":
    quest step is {quest_state}
        -> GiveQuest
    - quest_Steps == "FINISHED":
    quest step is {quest_state}
        -> SubmitQuest
}


===GiveQuest===
quest step is {quest_state}
this gives the 2nd quest #questS:1
, step 1 LOCATION, step 2 TALK #questS:2
->DONE

===SubmitQuest===
quest step is {quest_state}
passed the check
->DONE