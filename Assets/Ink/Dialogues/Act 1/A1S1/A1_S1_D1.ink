
===main===
EVENT:SwapBGM:BGM_ISLAND_FLUTTERING_CRITTER, BGM_CUTSCENE_TRANSFORMATION
// ~SwapBGM("BGM_ISLAND_FLUTTERING_CRITTER", "BGM_CUTSCENE_TRANSFORMATION", 4)
You find yourself on a beach. The sun glares over you, and your ship is in pieces around you. In stark contrast to the raging waters that overwhelmed you and your ship last night, calm waves now wash over shore. #speaker:Narrator
The storms of yesterday seem to have died down for now. It seems you’ve landed on an island with large trees and forestry beyond the beach. Nearby, you see a collapsed tree… and a tail?
//will later place cutscene art here (image on top of text box) of Amelia stuck under tree
You’ve never been outside of Dusk Island before, so you aren’t quite sure what creature it is. Before you can think of leaving, however, it spots you and cries out in pain. 
Hey! What are you doing just standing there? Get me out of here! #speaker:Amelia #portrait:AmeliaHostile
What do you do? Do you help the creature? #speaker:Narrator
    +[Yes]
        -> choiceYessaveAmelia
    +[No]
        -> choiceNodont

// cutscene art disappears after you click the choice

===choiceNodont===
You decide it's probably best not to draw attention to yourself. Besides, someone else might be able to help them out. You take a step in the opposite direction, which catches the attention of the creature in need. #speaker:Narrator
Hey! Are you seriously going to ignore someone in peril? Get back here! #speaker:Amelia #portrait:AmeliaHostile
    +[Help them]
        -> choiceYessaveAmelia
    +[No thanks]
        -> choiceNothankslol

===choiceNothankslol===
For real? You can't just ignore me! #speaker:Amelia #portrait:AmeliaHostile
    +[Help them]
        -> choiceYessaveAmelia
    +[No thanks]
        -> choiceNothankslol

===choiceYessaveAmelia===
It's what your gut is telling you, and it's high time you started listening to your gut. #speaker:Narrator #portrait:NoelleSceptical
You rush in and with a surge of power that feels like it came out of nowhere, you move the tree off the creature. As it crashes to the ground next to you, the creature stands up slowly, face tensing in pain.
(How did I…?) #speaker:Noelle #portrait:NoelleShocked
That's more like it. Thought for sure I would be a goner there. #speaker:Amelia #portrait:AmeliaHappy
Are you hurt? #speaker:Noelle #portrait:NoelleUm
I'm a little bruised, but I would be a lot worse for wear if it wasn't for you. So thanks. #speaker:Amelia #portrait:AmeliaNormal
Amelia looks up at you, and recoils in horror. #speaker:Narrator 
Hey, wait a minute? Are you a - human! #speaker:Amelia #portrait:AmeliaHostile
Amelia jumps back, and assumes a fighting stance, arms raised, ready to fight for her life. #speaker:Narrator
Stay back! #speaker:Amelia #portrait:AmeliaHostile
Wait! I'm not going to hurt you. #speaker:Noelle #portrait:NoelleSad
Ha! Likely story. Can't believe a lousy human was the one that came to my rescue. I must have the worst luck ever. #speaker:Amelia #portrait:AmeliaHostile
But I'm not human! #speaker:Noelle #portrait:NoelleSad
Lies! Clearly you're just trying to make me lower my guard down, and then when I have my back turned, bam! No way are you gonna pull the wool over my eyes. #speaker:Amelia #portrait:AmeliaHostile
Now go! Back to wherever you came from! 
But I can't go back. #speaker:Noelle #portrait:NoelleUm
Why not? You got here on a boat didn't you? Just sail back. #speaker:Amelia #portrait:AmeliaNormal
I can't. The boat's wrecked. And, well… #speaker:Noelle #portrait:NoelleUm
Well what? Cat's got your tongue? #speaker:Amelia #portrait:AmeliaHostile
The words get caught up in your throat as you think back to the series of events that forced your way out of the nest not even twenty-four hours ago. #speaker:Narrator
Would a stranger you only just met understand, let alone care? Where would you even begin?
+[It’s a long story.]
    -> choiceitsalongstory
+[I just can’t, okay?]
    -> choiceijustcantokay
    
===choiceitsalongstory===
It's a long story… #speaker:Noelle #portrait:NoelleUm
Well I don't have all day for you to regale me with your life story. But you still can't stay here. You're a human, you're not gonna last long out here alone. #speaker:Amelia #portrait:AmeliaNormal
    ->afterchoice

===choiceijustcantokay===
I just can't, okay? #speaker:Noelle #portrait:NoelleAngry
Fine, don't tell me. But you still can't stay here. You're a human, you're not gonna last long out here on your own. #speaker:Amelia #portrait:AmeliaNormal

//not sure how to make this part go to the next section, but basically either choice joins back to the stuff below, continuously
    ->afterchoice
    
===afterchoice===
That's the thing… #speaker:Noelle #portrait:NoelleMask
You move your hair around, willing their individual tentacles to wrap around your mouth like a mask. #speaker:Narrator
I'm not a human. #speaker:Noelle #portrait:NoelleMask
Huh. Well what are you doing in a get-up like that? Are you on the run or something?  #speaker:Amelia #portrait:AmeliaNormal
Something like that… But what do you mean, ‘get-up’? #speaker:Noelle #portrait:NoelleMask
None of the kraken-folk would ever dress like that. Way too constricting for their jobs. #speaker:Amelia #portrait:AmeliaNormal
Kraken… what? #speaker:Noelle #portrait:NoelleWhat
I’m no expert on them, but you still smell like an outsider and that boat is definitely not local. #speaker:Amelia #portrait:AmeliaNormal
Amelia starts walking off towards the forest. #speaker:Narrator
Anyways, thanks for saving me. I’m gonna— #speaker:Amelia #portrait:AmeliaNormal
Wait! Do you know where I can find another ship? #speaker:Noelle #portrait:NoelleSad
Not really. I know where you might be able to get more stuff to build a brand new one, but… #speaker:Amelia #portrait:AmeliaNormal 
But what? #speaker:Noelle #portrait:NoelleUm
They're all deeper in-land. And deeper in-land is pretty dangerous. Especially for an outsider. #speaker:Amelia #portrait:AmeliaNormal
Just point me in the right direction then. #speaker:Noelle #portrait:NoelleSmallSmile
Hang on, you can't go out there all by yourself! Have you even ventured out there before? A greenhorn like you would surely die on your own. #speaker:Amelia #portrait:AmeliaHostile
So you're saying I should form a party? #speaker:Noelle #portrait:NoelleSmallSmile
Isn't that obvious? #speaker:Amelia #portrait:AmeliaNormal
Then join me. Help me build a ship and get out of here. #speaker:Noelle #portrait:NoelleBigSmile
What?! No no no— I only just met you, and you may be a kraken-folk, but you’re definitely suspicious! #speaker:Amelia #portrait:AmeliaSilly
Come on, please? You said so yourself I would most likely die on my own, and I don't see anyone else around here. #speaker:Noelle #portrait:NoelleSad
That's because everyone is further in-land, and not even the humans would blindly welcome a rando like you. #speaker:Amelia #portrait:AmeliaHostile
(<i>So there are other humans around.</i>) #speaker:Noelle #portrait:NoelleSceptical
But you aren’t like the others, right? Does that mean you'll come along with me? #portrait:NoelleSmallSmile
I didn't say that! Look, I'm grateful you saved me from that tree and all, and I do owe you for that, but it's a bit much to just ask a random stranger to tag along with you. Take care, and try not to die, okay? #speaker:Amelia #portrait:AmeliaNormal
Amelia turns her back on you and starts walking off in the direction of the forest. #speaker:Narrator  
Before she can leave however, her stomach rumbles.
When was the last time you ate? #speaker:Noelle #portrait:NoelleUm
It's been a little while. Hard to survive on this island when you’re as small as me. #speaker:Amelia #portrait:AmeliaNormal
…It’d be pretty convenient if someone were to find food for you. #speaker:Noelle #portrait:NoelleSmallSmile
->TakeQuest

===TakeQuest===
…Fine. Find me some berries. Ten of them, and maybe I will consider joining your party. #speaker:Amelia #portrait:AmeliaNormal
(I’ll need to find out more about this island if I want to survive.) #speaker:Noelle #portrait:NoelleSceptical
(<i>But for the first ‘monster’ I’ve met… This creature doesn’t seem too bad at all.</i>) #portrait:NoelleSmallSmile
EVENT:AddQuest:QUEST_1 //~ AddQuest(QUEST_1)
    ->DONE