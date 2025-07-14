

==completed_quest_1===
Would you like to finish this quest? #speaker:Narrator
    +[Finish quest?]
    -> CompleteQuest 
    +[Not yet #done]
    -> DONE
->DONE

===CompleteQuest===
EVENT:SubmitQuest:QUEST_1  //~ SubmitQuest(QUEST_1)
//UPON SUBMISSION OF 10 BERRIES TO AMELIA, next part continues.
After you get the ten berries you need, you return back to Amelia, who's sitting on the sand next to the wreckage of your ship. #speaker:Narrator
Please tell me you got me something edible. #speaker:Amelia #portrait:AmeliaNormal
I think so? The fruit's a lot different around here compared to home. #speaker:Noelle #portrait:NoelleSmallSmile
Huh. And where is your home? #speaker:Amelia #portrait:AmeliaNormal
Dusk Island. It was until recently anyway… #speaker:Noelle #portrait:NoelleUm
Sorry to hear about that. Is that why you were on the ship when the storms— #speaker:Amelia #portrait:AmeliaSilly
…While I would love to keep chatting, gimme the berries, quickly! #speaker:Amelia #portrait:AmeliaNormal
You hand over the berries, and Amelia hastily gobbles it all down, seeds, stems, skin and all. #speaker:Narrator
You must have been pretty hungry… #speaker:Noelle #portrait:NoelleSmallSmile
That hit the spot! Anyway, see ya— #speaker:Amelia #portrait:AmeliaHappy
Wait! I thought you said— #speaker:Noelle #portrait:NoelleSad
I know what I said. But the reality is, I still barely know you. And if you can find that many berries in a short span of time, I'm sure you'll be able to survive fine on your own. I don't really travel in groups. Like ever. Period. #speaker:Amelia #portrait:AmeliaNormal
But we had an agreement—  #speaker:Noelle #portrait:NoelleSad
//add in a flash across the screen like lightning, start rain for 1 minute. audio lightningflash and ambience-audio rain at the same time
The haunting roar of a monstrous beast echoes throughout the island. Whatever you had to say is forgotten in the moment. Rain begins to fall. Amelia looks up in horror. #speaker:Narrator
The Great Disaster… #speaker:Amelia #portrait:AmeliaHostile
What— #speaker:Noelle #portrait:NoelleShocked
How long do you think it will take for us to build a new ship and escape the flood? #speaker:Amelia #portrait:AmeliaHostile
I dunno— I haven't really built a ship on my own before. #speaker:Noelle #portrait:NoelleUm
However long you think we need, cut that time in half. We need to hurry. Now. #speaker:Amelia #portrait:AmeliaHostile
We? #speaker:Noelle #portrait:NoelleShocked
Yep. Change of plans, I'm coming with you. #speaker:Amelia #portrait:AmeliaHostile
But why? #speaker:Noelle #portrait:NoelleShocked
Isn't that obvious? To survive the floods. Now come on, we need to go. #speaker:Amelia #portrait:AmeliaHostile
Amelia nudges your hand, and leads you toward the forests deeper in-land. #speaker:Narrator
 ~ quest_state = "SUBMITTED"  //remove this, change in EVENT:SubmitQuest

->DONE
//ends here. Next objective is to ‘find the forest settlement’. 