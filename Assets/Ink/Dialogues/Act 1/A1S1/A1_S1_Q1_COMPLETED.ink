==A1_S1_Q1_COMPLETED===
Narrator: Would you like to finish this quest?  
    +[Finish quest?]  
    -> CompleteQuest   
    +[Not yet #done]  
    -> DONE  
->DONE  

===CompleteQuest===  
EVENT:SubmitQuest:A1_S1_Q1  //~ SubmitQuest(QUEST_1)  
//UPON SUBMISSION OF 10 BERRIES TO AMELIA, next part continues.  
Narrator: After you get the ten berries you need, you return back to Amelia, who's sitting on the sand next to the wreckage of your ship.  
Amelia: Please tell me you got me something edible.  
Noelle: I think so? The fruit's a lot different around here compared to home.  
Amelia: Huh. And where is your home?  
Noelle: Dusk Island. It was until recently anyway…  
Amelia: Sorry to hear about that. Is that why you were on the ship when the storms—  
Amelia: …While I would love to keep chatting, gimme the berries, quickly!  
Narrator: You hand over the berries, and Amelia hastily gobbles it all down, seeds, stems, skin and all.  
Noelle: You must have been pretty hungry…  
Amelia: That hit the spot! Anyway, see ya—  
Noelle: Wait! I thought you said—  
Amelia: I know what I said. But the reality is, I still barely know you. And if you can find that many berries in a short span of time, I'm sure you'll be able to survive fine on your own. I don't really travel in groups. Like ever. Period.  
Noelle: But we had an agreement—  
//add in a flash across the screen like lightning, start rain for 1 minute. audio lightningflash and ambience-audio rain at the same time  
Narrator: The haunting roar of a monstrous beast echoes throughout the island. Whatever you had to say is forgotten in the moment. Rain begins to fall. Amelia looks up in horror.  
Amelia: The Great Disaster…  
Noelle: What—  
Amelia: How long do you think it will take for us to build a new ship and escape the flood?  
Noelle: I dunno— I haven't really built a ship on my own before.  
Amelia: However long you think we need, cut that time in half. We need to hurry. Now.  
Noelle: We?  
Amelia: Yep. Change of plans, I'm coming with you.  
Noelle: But why?  
Amelia: Isn't that obvious? To survive the floods. Now come on, we need to go.  
Narrator: Amelia nudges your hand, and leads you toward the forests deeper in-land.  
~ quest_state = "SUBMITTED"
->DONE  
