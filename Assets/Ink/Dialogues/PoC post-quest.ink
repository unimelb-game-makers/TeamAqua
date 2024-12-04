//=====================================After quest completion===========================================    
//if var quest >= 10 go here
/*
===SubmitQuest===
//~checkQuestStatus(1, 1)
Would you like to finish this quest? #speaker:Narrator
text
clicking on yes should remove quest with id 1 while no should do nothing
    +[Finish quest? #finish:{id}] 
    //~ CompleteEntireQuestAndRemove()
    -> CompleteQuest
    +[Not yet #done]
    -> DONE
    

===CompleteQuest=== 
this line of dialogue should play when player interacts with Amelia after completeing the quest. #speaker:silly dev
After a few tries, you get the 10 fish you need. #speaker:Narrator
You return back to Amelia, who’s sitting on the sand next to the wreckage of your ship.
Please tell me you caught some salmon. #speaker:Amelia
I think so? #speaker:Noelle
The fish are a lot different around here compared to home
Huh. And where is your home? #speaker:Amelia
Dusk Island. #speaker:Noelle
It was until recently anyway.
Sorry to hear about that. #speaker:Amelia
Is that why you were on the ship when the storms <i>stomach rumbles again</i> //add sound effects here
While I would love to keep chatting, gimme the fish, quickly!
You hand over the fish, and Amelia hastily gobbles it all down, bones, scales and all. #speaker:Narrator
You must have been pretty hungry... #speaker:Noelle
That hit the spot! Anyway, see ya— #speaker:Amelia
Wait! I thought you said— #speaker:Noelle
I know what I said. #speaker:Amelia
But the reality is I still barely know you. 
And if you can catch that many fish in a short span of time, I’m sure you’ll be able to survive fine on your own. 
And I don’t really travel in groups anyway.
But we had an agreement— #speaker:Noelle
The /*add sfx here*/ haunting roar of a monstrous beast echoes throughout the island. Whatever you had to say is forgotten in the moment. 
Rain begins to fall on the island. 
Amelia looks up in horror.
The Great Disaster... #speaker:Amelia
What— #speaker:Noelle
How long do you think it will take for us to build a new ship and get off of this island? #speaker:Amelia
I dunno - I haven’t really built a ship on my own before and— #speaker:Noelle
However long you think we need, cut that time in half. #speaker:Amelia
We need to hurry.
We? #speaker:Noelle
Yep. #speaker:Amelia
Change of plans, I’m coming with you.
But why? #speaker:Noelle
Isn’t that obvious? #speaker:Amelia
To outrun the floods. 
Now come on, we need to go.
Amelia takes Noelle’s hand, and drags her towards the rainforests deeper in-land. #speaker:Narrator
->DONE



*/