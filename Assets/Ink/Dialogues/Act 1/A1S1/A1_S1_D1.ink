===A1_S1_D1===
EVENT:SwapBGM:BGM_ISLAND_FLUTTERING_CRITTER, BGM_CUTSCENE_TRANSFORMATION  
Narrator: You find yourself on a beach. The sun glares over you, and your ship is in pieces around you. In stark contrast to the raging waters that overwhelmed you and your ship last night, calm waves now wash over shore.  
Narrator: The storms of yesterday seem to have died down for now. It seems you’ve landed on an island with large trees and forestry beyond the beach. Nearby, you see a collapsed tree… and a tail?
You’ve never been outside of Dusk Island before, so you aren’t quite sure what creature it is. Before you can think of leaving, however, it spots you and cries out in pain.  
Amelia: Hey! What are you doing just standing there? Get me out of here!  
Narrator: What do you do? Do you help the creature?  
    +[Yes]  
        
        -> choiceYessaveAmelia  
    +[No]  
        -> choiceNodont  

===choiceNodont===  
Narrator: You decide it's probably best not to draw attention to yourself. Besides, someone else might be able to help them out. You take a step in the opposite direction, which catches the attention of the creature in need.  
Amelia: Hey! Are you seriously going to ignore someone in peril? Get back here!  
    +[Help them]  
        -> choiceYessaveAmelia  
    +[No thanks]  
        -> choiceNothankslol  

===choiceNothankslol===  
Amelia: For real? You can't just ignore me!  
    +[Help them]  
        -> choiceYessaveAmelia  
    +[No thanks]  
        -> choiceNothankslol  

===choiceYessaveAmelia===  
Narrator: It's what your gut is telling you, and it's high time you started listening to your gut.  
Narrator: You rush in and with a surge of power that feels like it came out of nowhere, you move the tree off the creature. As it crashes to the ground next to you, the creature stands up slowly, face tensing in pain.  
Noelle: (How did I…?)  
Amelia: That's more like it. Thought for sure I would be a goner there.  
Noelle: Are you hurt?  
Amelia: I'm a little bruised, but I would be a lot worse for wear if it wasn't for you. So thanks.  
Narrator: Amelia looks up at you, and recoils in horror.  
Amelia: Hey, wait a minute? Are you a - human!  
Narrator: Amelia jumps back, and assumes a fighting stance, arms raised, ready to fight for her life.  
Amelia: Stay back!  
Noelle: Wait! I'm not going to hurt you.  
Amelia: Ha! Likely story. Can't believe a lousy human was the one that came to my rescue. I must have the worst luck ever.  
Noelle: But I'm not human!  
Amelia: Lies! Clearly you're just trying to make me lower my guard down, and then when I have my back turned, bam! No way are you gonna pull the wool over my eyes.  
Amelia: Now go! Back to wherever you came from!  
Noelle: But I can't go back.  
Amelia: Why not? You got here on a boat didn't you? Just sail back.  
Noelle: I can't. The boat's wrecked. And, well…  
Amelia: Well what? Cat's got your tongue?  
Narrator: The words get caught up in your throat as you think back to the series of events that forced your way out of the nest not even twenty-four hours ago.  
Narrator: Would a stranger you only just met understand, let alone care? Where would you even begin?  
    +[It’s a long story.]  
        -> choiceitsalongstory  
    +[I just can’t, okay?]  
        -> choiceijustcantokay  

===choiceitsalongstory===  
Noelle: It's a long story…  
Amelia: Well I don't have all day for you to regale me with your life story. But you still can't stay here. You're a human, you're not gonna last long out here alone.  
    ->afterchoice  

===choiceijustcantokay===  
Noelle: I just can't, okay?  
Amelia: Fine, don't tell me. But you still can't stay here. You're a human, you're not gonna last long out here on your own.  
    ->afterchoice  

===afterchoice===  
Noelle: That's the thing…  
Narrator: You move your hair around, willing their individual tentacles to wrap around your mouth like a mask.  
Noelle: I'm not a human.  
Amelia: Huh. Well what are you doing in a get-up like that? Are you on the run or something?  
Noelle: Something like that… But what do you mean, ‘get-up’?  
Amelia: None of the kraken-folk would ever dress like that. Way too constricting for their jobs.  
Noelle: Kraken… what?  
Amelia: I’m no expert on them, but you still smell like an outsider and that boat is definitely not local.  
Narrator: Amelia starts walking off towards the forest.  
Amelia: Anyways, thanks for saving me. I’m gonna—  
Noelle: Wait! Do you know where I can find another ship?  
Amelia: Not really. I know where you might be able to get more stuff to build a brand new one, but…  
Noelle: But what?  
Amelia: They're all deeper in-land. And deeper in-land is pretty dangerous. Especially for an outsider.  
Noelle: Just point me in the right direction then.  
Amelia: Hang on, you can't go out there all by yourself! Have you even ventured out there before? A greenhorn like you would surely die on your own.  
Noelle: So you're saying I should form a party?  
Amelia: Isn't that obvious?  
Noelle: Then join me. Help me build a ship and get out of here.  
Amelia: What?! No no no— I only just met you, and you may be a kraken-folk, but you’re definitely suspicious!  
Noelle: Come on, please? You said so yourself I would most likely die on my own, and I don't see anyone else around here.  
Amelia: That's because everyone is further in-land, and not even the humans would blindly welcome a rando like you.  
Noelle: (<i>So there are other humans around.</i>)  
Noelle: But you aren’t like the others, right? Does that mean you'll come along with me?  
Amelia: I didn't say that! Look, I'm grateful you saved me from that tree and all, and I do owe you for that, but it's a bit much to just ask a random stranger to tag along with you. Take care, and try not to die, okay?  
Narrator: Amelia turns her back on you and starts walking off in the direction of the forest.  
Noelle: Before she can leave however, her stomach rumbles.  
Noelle: When was the last time you ate?  
Amelia: It's been a little while. Hard to survive on this island when you’re as small as me.  
Noelle: …It’d be pretty convenient if someone were to find food for you.  
    ->TakeQuest  

===TakeQuest===  
Amelia: …Fine. Find me some berries. Ten of them, and maybe I will consider joining your party.  
Noelle: (I’ll need to find out more about this island if I want to survive.)  
Noelle: (<i>But for the first ‘monster’ I’ve met… This creature doesn’t seem too bad at all.</i>)  
// EVENT:AddQuest:{QUEST_1} 
EVENT:AddQuest:A1_S1_Q1 
    ->DONE
