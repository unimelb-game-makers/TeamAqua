INCLUDE ../globals.ink
EXTERNAL checkQuestStatus(id, step)
VAR questSteps = ""


~checkQuestStatus(2, 1)
~ questSteps = quest_id2
current quest step is {questSteps} and current quest_id var is {quest_id1}
{ 
    - questSteps == "":     // if empty, go to main
        -> main 
    
    - questSteps == "NO":
        -> IncompleteQuest
    - questSteps == "YES":)
        -> SubmitQuest 
}

===main===
this gives the 2nd quest, step 1 LOCATION, step 2 TALK #questS:2
~ quest_id2 = "NO"
->DONE

===IncompleteQuest===
quest not completed yet, quest step is {questSteps} ; and quest_id is
->DONE

===SubmitQuest===
do u want to finish this quest?
    + [Submit #finish:2] ->DONE
    + [No #done]
->DONE