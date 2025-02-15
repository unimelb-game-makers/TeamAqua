
//interactions: just basic dialogue continuing
//Global ink: not needed
//Dev review: complete

//chores: add speaker portraits.
EXTERNAL SwapBGM(new_id, old_id, FadeSpeed)
EXTERNAL ChangeCutscene(SceneName)
EXTERNAL PlayBGM(id)
~PlayBGM("BGM_CUTSCENE_CEREMONY")



//~SwapBGM("BGM_CUTSCENE_INTO_THE_STORM", "BGM_CUTSCENE_CEREMONY", 1) 

#cutscene:A0_DEFAULT
A birthday tune is hummed as you sit around a campfire with your family, embers floating by your feet. #speaker: Narrator #cutscene:A0S1
We are gathered here today to celebrate the coming of age of our dear Noelle. #speaker: Noelle's Mother #cutscene:A0S2  
Next in line to the Tempest family name, the finest of any family here on this little island. #cutscene: A0S3
A beacon of hope against any and all creatures that lurk in the shadow. #cutscene: A0S4
//Cutscene art of chalice being handed over. Maybe water sloshing sound effects?
… #speaker: Narrator //Add some water sloshing sfx here
Are you ready to make all of us proud? #speaker: Noelle's Mother #cutscene: A0S5
You stare at the chalice, then at your grandfather and parents. Eager claps follow as you nod. #speaker: Narrator
Excellent! #speaker: Noelle's Mother 
What are you waiting for then? Drink up! Awaken your inner potential! #cutscene: A0S6
You stare at the chalice with a deep breath and take a gulp. #speaker: Narrator

~SwapBGM("BGM_CUTSCENE_INTO_THE_STORM", "BGM_CUTSCENE_CEREMONY", 1) 
//~SwapBGM("BGM_CUTSCENE_INTO_THE_STORM", 1)      //doesnt work rn
//change bgm right after this line

The taste of iron spreads on your tongue. In seconds, you begin to choke and fall to your knees, struggling to catch your breath. #cutscene: A0S7
Did someone poison the awakening potion?! #speaker: Noelle's Mother #cutscene: A0S8

//Cutscene art of Noelle’s mother turning to her father, a cold expression on her face.
You made it, my dear. Are you seriously trying to destroy the last of the Tempest legacy? 
No no, of course not! I followed the recipe exactly! It shouldn’t be affecting her like this! #speaker: Noelle's Father 
Distantly, you realize that you’ve begun to scream. Something rough and painful sprouts over your face, #cutscene: A0S9
and by the time the sensation subsides, you notice your reflection in the spilled potion on the ground. #cutscene: A0S10

Your grandfather stands up with his cane and glares daggers at you. A gnarled, bony finger points at your face—
Monster! #speaker: Noelle's Grandfather
No, no, I can't be— #speaker: Noelle #cutscene: A0S11
And yet, you clearly are. #speaker: Noelle's Mother
Your mother’s face twists with anger. She whirls to your father. #speaker: Narrator
Well this is great, just great. Our only heir, a monster! #speaker: Noelle's Mother
You didn’t tell me there was a monster on your side of the family.
No, that’s preposterous. That can’t be possible! #speaker: Noelle’s Father
At this point anything is. #speaker: Noelle's Mother #cutscene: A0S12
A chill runs down your spine as your mother turns to you. #speaker: Narrator
Now listen here, this is what happened— #speaker: Noelle's Mother
//Cutscene: Vera takes out a knife. 
Our darling Noelle was moments away from completing her coming of age ritual #cutscene: A0S13
when a vicious creature attacked her out of nowhere.
The creature was ultimately slain by yours truly, but at the tragic cost of our dear daughter’s life.
How terrible this day is, for the Tempest family to be robbed of its only heir, by a creature no less. 
You can’t do this! I’m your daughter! #speaker: Noelle
Not anymore. Not after what you've become. #speaker: Noelle's Mother
But what about the family? You have no other heirs. #speaker: Noelle
I’m sure we can make one up. #speaker: Noelle's Mother #cutscene: A0S14
Her eyes grow manic. The knife in her hand glints. #speaker: Narrator
How's this? ‘A long lost child who miraculously has Tempest blood.’ #speaker: Noelle's Mother
It will be a bit of a stretch, but not as much as a creature becoming the next head of the family.
Oh to think, all the time and resources we wasted!
You really had to go and ruin this for everyone, didn’t you? 
You scramble to your feet and make a run for it, just as your mother closes in on you. #speaker: Narrator #cutscene: A0S15
Goodbye Noelle, and may the gods have mercy on your soul. #speaker: Noelle's Mother
//Black screen? Or another dramatic cutscene of noelle running away, whatever ibbi has prepped
#cutscene: A0S16
Monster! #speaker: Noelle’s Grandfather
//Sound effect of footsteps running?
You run towards the waters, your parents’ pursuit not far behind. #speaker: Narrator #cutscene: A0S17
Clouds start gathering in the sky and thunder rumbles as rain starts bucketing down. You spot a small boat on the docks and run towards it.
Untie the knots. #cutscene: A0S18
Come on! #speaker: Noelle
Right before your parents can catch you, you grab an oar and start paddling for your life. #speaker: Narrator
That’s right, you better run! #speaker: Noelle’s Mother #cutscene: A0S19
Run and run and run, and never come back! #cutscene: A0S20
You continue to flee, further and further out, until your family members are specks in the distance. You do not stop until your home is miles away. #cutscene: A0S21
The storm above intensifies while your boat begins to spin out of control.
A number of shadows move underneath the surface, #cutscene: A0S22

but before you can get a better look, a giant wave crashes— and everything goes dark. #cutscene: A0S23

END #cutscene: A0S24
~ChangeCutscene("NoonIsland")