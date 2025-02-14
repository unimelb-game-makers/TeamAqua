
//interactions: dialogue cont, choices, 
//global ink:
//dev approval: in progress



START:
//to start this file: walk further into forest


You and Amelia walk through the thickets of the rainforests. 
Ahead you spot a clearing. 
I don’t know about this - a lot of humans live in these parts. #speaker:Amelia #portrait:AmeliaAngry
I would honestly rather chance it with the plants, #speaker:Amelia #portrait:AmeliaNeutral
but if you think we might be able to find some ship parts, I won’t stop. 
Do you want to head into the clearing? #speaker:Narrator
+[Yes, we can't afford to waste time.]
    -> choiceYeswecantaffordtowastetime
+[No better to be safe than sorry]
    -> choiceNobettertobesafethansorry
    
===choiceNobettertobesafethansorry=== //if player chooses for Noelle & Amelia to move on past clearing
Thank goodness. Now come on! #speaker:Amelia #portrait:AmeliaJoyous
You and Amelia move on past the clearing, and deeper into the woods. #speaker:Narrator
Ahead of you lies some barbed vines blocking your way. Venus flytrap-like plants stand on either side of the vines, menacingly.
Do you try to hack through them?
+[Sure, why not?]
    -> choiceSurewhynot
+[No thanks.]
    -> choiceNothanks
    
===choiceNothanks===
It would most likely be futile anyway. #speaker:Narrator
->DONE
    
===choiceSurewhynot===
You and Amelia attempt to hack and slash and bite through the vines, but to no avail. #speaker:Narrator

The venus fly trap plants start to growl. // start sound effect

It would likely be futile—and hazardous—to keep going. #speaker:Narrator
Oh come on! #speaker:Amelia #portrait:AmeliaAngry
Well it looks like we can’t go any further in that way. #speaker:Amelia
Not unless we had fire.
I don’t suppose you can— #speaker:Noelle #portrait:NoelleDispleased
Breathe fire? No. Maybe an ember or two, but that’s about it. #speaker:Amelia #portrait:AmeliaSilly
I would have to practice for like a gajillion hours #speaker:Amelia #portrait:Silly
to be able to breathe even one large flame. #speaker:Amelia #portrait:Neutral
And we don't have the time for that right now.
So can we please go somewhere else? #speaker:Amelia #portrait:AmeliaNeutral
->DONE


===choiceYeswecantaffordtowastetime=== //if player chooses for Noelle and Amelia #portrait:AmeliaJoyful to pass through clearing
Augh, fine. #speaker:Amelia #portrait:AmeliaJoyful 
You and Amelia enter the clearing, and spot several treetops. 
Stop! # speaker:Elder
An elderly figure jumps down from the tallest tree to block your way. 
State your name and your purpose. Now. #speaker:Elder
We don’t want any trouble, we’re just passing through. speaker: Amelia #portrait:AmeliaJoyful
Wouldn’t be the first time an outsider came through with that story. #speaker:Elder
I will ask you again: #speaker:Elder
Several cloaked figures in masks appear from the trees and crowd around Noelle and Amelia, spears raised. 
Name and purpose. Now. #speaker:Elder
It appears that you’re in a tight spot. How do you wish to deescalate the situation: #speaker:Narrator
+[Say nothing.]
    ->choiceSaynothing
//+[Run.]
   // -> choiceRun
+[Say the truth]
    -> choiceSaythetruth

===choiceSaynothing===
You heard her the first time. Now let us leave. #speaker:Amelia #portrait:AmeliaNeutral
I can’t have outsiders knowing where we live. #speaker:Elder
->DONE

/*
===choiceRun===
//NOTE: delayed to alpha
Noelle glances at Amelia, and Amelia nods understandingly. 
You both make a run for the thickets. 
After them! #speaker: Elder
->DONE
*/

===choiceSaythetruth
My name is Noelle Tempest, and this is Amelia. #speaker:Noelle #portrait:NoellePleased
My ship capsized on shores not far from this settlement,
and we only wish to gather parts necessary to build a new ship capable of escaping the approaching floods. #speaker:Noelle #portrait:NoelleDistant
Please, I beg you, let us leave, and we will never disturb your peace again. #speaker:Noelle #portrait:NoellePleased
Elder motions to the cloaked figures. #speaker:Narrator #portrait:ElderNeutral
They lower their weapons. 
I see. What a noble quest, #speaker:Elder #portrait:ElderNeutral
and you have my sympathy for being marooned the way you have been. 
However, I can’t let you leave. #speaker:Elder #portrait:ElderNeutral
I’m sorry, could you repeat that? #speaker:Noelle #portrait:NoelleConfused
Don’t think I heard you properly the first time.
Oh it’s no mistake. #speaker:Elder #portrait:ElderNeutral
Simply put, you are both outsiders,
and you both know where we live now.
Therefore, neither of you can leave.
You are free to contribute to our community, 
and to co-exist with us.
But your life outside of this settlement ends today.
You don’t understand though— the floods are coming! #speaker:Amelia #portrait:AmeliaAngry
The water’s rising around here as we speak! 
Surely someone as supposedbly in tune with the land as you are #speaker:Amelia #portrait:AmeliaNeutral 
would have already realised that? #speaker:Amelia #portrait:Angry
Oh I am already aware of the floods. #speaker:Elder #portrait:ElderNeutral
But fret not—
we live high enough in the treetops that our home is at no risk of being washed away. #speaker:Elder #portrait:ElderNeutral
Do you really think being a little bit higher off the ground is gonna save you? #speaker:Amelia #portrait:AmeliaAngry
I am quite confident. #speaker:Elder #portrait:ElderNeutral
And even if we all ultimately perish, such is the circle of life.
And you have no issue about putting everyone’s lives on the line? #speaker:Noelle #portrait:NoelleDispleased
People you’re supposed to protect. #speaker:Noelle #portrait:NoelleAngry
I don’t see the harm in trusting in the natural order. #speaker:Elder #portrait:ElderNeutral 
Whether we live or die isn’t our choice after all. #speaker:Elder #portrait:ElderNeutral
Besides, we live with the land, we only take what we need.
To try to uproot everything we hold dear just to resist the call of The Great Disaster is sacrilege.
Do you even hear yourselves?! #speaker:Amelia #portrait:AmeliaNeutral
The Great Disaster is going to kill us all! #speaker:Amelia #portrait:AmeliaAngry
All I hear is two outsiders shouting about things they do not understand. #speaker:Elder #portrait:ElderNeutral
But that will fade in time I’m sure.
For now, take a look around, and get to know us. 
After all, whether you like it or not, this is your home now. #speaker:Elder #portrait:ElderNeutral
The cloaked figures jump up the rope ladders—all but one. Instead they move to the entrance of the settlement, blocking the only exit out.  #speaker:Narrator
The elder slowly climbs up the middle rope ladder, leaving Amelia and Noelle alone. 

//function call to end prototype here
    -> END