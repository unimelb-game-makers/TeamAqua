INCLUDE globals.ink

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
    +[Finish quest 2? #finish:{id}] -> DONE
    +[Not yet]
    ->DONE

