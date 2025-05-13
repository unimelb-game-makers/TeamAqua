INCLUDE ../Globals/Globals.ink
EXTERNAL checkQuestStatus(id, step)
VAR questSteps = ""


~checkQuestStatus(4, 1)     
//indexing does not support just using the actual id of the quest
current quest step is {quest_state}
{ 
    - quest_state == "":     // if empty, go to main
        -> main 
    
    - quest_state == "NO":
        -> IncompleteQuest
    - quest_state == "YES":
        -> SubmitQuest 
}

===main===
this gives the 2nd quest, step 1 LOCATION, step 2 TALK #questS:2
~ quest_state = "NO"
->DONE

===IncompleteQuest===
quest not completed yet, quest step is {quest_state}
->DONE

===SubmitQuest===
do u want to finish this quest?
    + [Submit #finish:4] ->DONE
    + [No #done]
->DONE