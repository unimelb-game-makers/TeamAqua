INCLUDE ../Globals/Globals.ink
EXTERNAL checkQuestStatus(id, steps) 
EXTERNAL TurnOffBarrier(id)

VAR id = 1

//~checkQuestStatus(2,1)    -> calling this here throws an index error in most cases, hence a check down below


{
    - quest_state == "":     // if empty, go to main
        -> main 
    - quest_state == "NOT_FINISHED":   //================================ failed here ==========
        //quest step is {questSteps} and current quest_id var is {quest_id1}
        ~checkQuestStatus(2, 1)
}


{ 
    - quest_state == "":     // if empty, go to main
        -> main 
    - quest_state == "NOT_FINISHED":   //================================ failed here ==========
        //quest step is {questSteps} and current quest_id var is {quest_id1}
        //~checkQuestStatus(2, 1)
        c
        -> IncompleteQuest
    - quest_state == "FINISHED":
        //~checkQuestStatus(2, 1)
        -> SubmitQuest 
}

===main===
//#questS:1
#questS:2
~quest_state = "NOT_FINISHED"
psss #speaker:The Suspicious Avian
solve this puzzle
trust
->DONE

===checkquest===
~checkQuestStatus(2,1)
->DONE
===IncompleteQuest===
Not done yet lil squid #speaker:The Suspicious Avian
->DONE

===SubmitQuest===
Would you like to finish this quest? #speaker:Narrator
~ checkQuestStatus(2, 1)
    +[Finish #finish:2  #done] 
    ~TurnOffBarrier(0)
    -> DONE
    +[Not yet #done]    
    -> DONE

