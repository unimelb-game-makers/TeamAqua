INCLUDE ../globals.ink
EXTERNAL checkQuestStatus(id, step)

VAR quest_Steps = ""

quest step is {quest_Steps} and quest_id2 is {quest_id2}
~ quest_Steps = quest_id2
~ checkQuestStatus(2,1)
quest step is {quest_Steps} and quest_id2 is {quest_id2}
{
    - quest_Steps == "":
    quest step is {quest_Steps} and quest_id2 is {quest_id2}
        -> GiveQuest
    - quest_Steps == "yes":
    quest step is {quest_Steps} and quest_id2 is {quest_id2}
        -> SubmitQuest
}


===GiveQuest===
quest step is {quest_Steps} and quest_id2 is {quest_id2}
this gives the 2nd quest #questS:1
, step 1 LOCATION, step 2 TALK #questS:2
->DONE

===SubmitQuest===
quest step is {quest_Steps} and quest_id2 is {quest_id2}
passed the check
->DONE