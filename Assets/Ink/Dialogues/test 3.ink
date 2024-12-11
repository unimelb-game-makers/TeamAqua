INCLUDE globals.ink
EXTERNAL checkQuestStatus(id, steps) 

VAR id = 2
VAR questSteps = ""
~ questSteps = quest_id2

{ mat == "": -> empty | -> hi}

===empty===
u havent picked anything.
->DONE

===hi===
u picked {mat}?
->testquest


===testquest===
after this line of dialogue, u should be taken to SubmitQuest knot and complete the quest id 2
->SubmitQuest

===SubmitQuest===
Would you like to finish this quest? #speaker:Narrator
text
clicking on yes should remove quest with id 2 while no should do nothing
~ checkQuestStatus(2, 1)
    +[Finish quest 2?  #done] -> DONE
    +[Not yet #done]
    ->DONE

