
//interactions: dialogue cont, choices, 
//global ink:
//dev approval: in progress
You and Amelia walk through the thickets of the rainforests. #for programmer
Ahead you spot a clearing. #for programmer
I don’t know about this - a lot of humans live in these parts. #speaker: Amelia
I would honestly rather chance it with the plants, #speaker: Amelia
but if you think we might be able to find some ship parts, I won’t stop. #speaker: Amelia
Do you want to head into the clearing? #speaker: Narrator
+[Yes, we can't afford to waste time.]
    -> choiceYeswecantaffordtowastetime


===choiceYeswecantaffordtowastetime=== //if player chooses for Noelle and Amelia to pass through clearing
Augh, fine. #speaker: Amelia
You and Amelia enter the clearing, and spot several treetops. #for programmer
Stop! # speaker: Elder
An elderly figure jumps down from the tallest tree to block your way. #for programmer
State your name and your purpose. Now. #speaker: Elder
We don’t want any trouble, we’re just passing through. speaker: Amelia
Wouldn’t be the first time an outsider came through with that story. #speaker: Elder
I will ask you again: #speaker: Elder
Several cloaked figures in masks appear from the trees and crowd around Noelle and Amelia, spears raised. #for programmer
Name and purpose. Now. #Elder
It appears that you’re in a tight spot. How do you wish to deescalate the situation:
+[Say nothing.]
    ->choiceSaynothing
+[Run.]
    -> choiceRun
+[Say the truth]
    -> choiceSaythetruth

===choiceSaynothing===
You heard her the first time. Now let us leave. #speaker: Amelia
I can’t have outsiders knowing where we live. #speaker: Elder
->DONE
===choiceRun===
Noelle glances at Amelia, and Amelia nods understandingly. #for programmer
You both make a run for the thickets. #for programmer
After them! #speaker: Elder
->DONE

===choiceSaythetruth
My name is Noelle Tempest, and this is Amelia. #speaker: Noelle
My ship capsized on shores not far from this settlement, #speaker: Noelle
and we only wish to gather parts necessary to #speaker: Noelle
build a new ship capable of escaping the approaching floods. #speaker: Noelle 
Please, I beg you, let us leave, and we will never disturb your peace again. #speaker: Noelle
Elder motions to the cloaked figures. They lower their weapons. #for programmer
I see. What a noble quest, #speaker: Elder
and you have my sympathy for being marooned the way you have been. #speaker: Elder
However, I can’t let you leave. #speaker: Elder
I’m sorry, could you repeat that? #speaker: Noelle
Don’t think I heard you properly the first time. #speaker: Noelle
Oh it’s no mistake. #speaker: Elder
Simply put, you are both outsiders, #speaker: Elder
and you both know where we live now. #speaker: Elder
Therefore, neither of you can leave. #speaker: Elder
You are free to contribute to our community, #speaker: Elder
and to co-exist with us. #speaker: Elder
But your life outside of this settlement ends today. #speaker: Elder
You don’t understand though—the floods are coming! #speaker: Amelia
The water’s rising around here as we speak! #speaker: Amelia 
Surely someone as supposedbly in tune with the land as you are #speaker: Amelia
would have already realised that? #speaker: Amelia
Oh I am already aware of the floods. #speaker: Elder
But fret not—we live high enough in the treetops that our home is at no risk of being washed away. #speaker: Elder
Do you really think being a little bit higher off the ground is gonna save you? #speaker: Amelia
I am quite confident. #speaker: Elder
And even if we all ultimately perish, such is the circle of life. #speaker: Elder
And you have no issue about putting everyone’s lives on the line? #speaker: Noelle
People you’re supposed to protect. #speaker: Noelle
I don’t see the harm in trusting in the natural order. #speaker: Elder 
Whether we live or die isn’t our choice after all. #speaker: Elder
Besides, we live with the land, we only take what we need. #speaker: Elder
To try to uproot everything we hold dear just to resist the call of The Great Disaster is sacrilege. #speaker: Elder
Do you even hear yourselves?! #speaker: Amelia
The Great Disaster is going to kill us all! #speaker: Amelia
All I hear is two outsiders shouting about things they do not understand. #speaker: Elder
But that will fade in time I’m sure. #speaker: Elder
For now, take a look around, and get to know us. #speaker: Elder 
After all, whether you like it or not, this is your home now. #speaker: Elder
The cloaked figures jump up the rope ladders—all but one. Instead they move to the entrance of the settlement, blocking the only exit out. #for programmer 
The elder slowly climbs up the middle rope ladder, leaving Amelia and Noelle alone. #for programmer
    -> END