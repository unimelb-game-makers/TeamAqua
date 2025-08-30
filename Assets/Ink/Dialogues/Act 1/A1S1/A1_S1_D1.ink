===A1_S1_D1===
//load into noon island starting beach, dialogue automatically starts
Narrator: You find yourself on a beach. The sun glares over you, and your ship is in pieces around you.
Narrator: In contrast to the storm that swallowed you last night, calm waves now wash over the shore.
Narrator: It seems like you've landed on a foreign island of some sort...
Narrator: And nearby is a collapsed tree... and a tail?
???: <b>—You!</b> Whoever's standing there!

???: Don't think I can't see you!
Narrator: You've never been outside of Dusk before, so you aren't sure <i>what</i> that is.
Narrator: But if it isn't human, and it can speak... That means…

???: Help me get out of here!#speaker:Amelia #portrait:AmeliaHostile 
Narrator: What do you do? Do you help the creature?

+[Yes]
    -> choiceYessaveAmelia
+[No]
    -> choiceNodont

===choiceNodont===
Narrator: You take a step in the opposite direction.
???: <b>Hey!</b> Are you seriously going to ignore someone in peril?! Get back here! #speaker:Amelia #portrait:AmeliaHostile
+[Help them]
    -> choiceYessaveAmelia
+[No thanks]
    -> choiceNodont
    
    ===choiceYessaveAmelia===
Narrator: You rush in, and with a surge of power that is unfamiliar to your limbs, you lift the tree off the creature.
Narrator: As it crashes onto the ground, the creature shakes itself off.
Noelle: (How did I…?) #portrait:NoelleShocked
Amelia: That's more like it. Thought I'd be a goner for a second. #portrait:AmeliaHappy
Noelle: Are you hurt? #portrait:NoelleUm
Narrator: Amelia looks up at you, finally taking in your appearance and gaining a complicated expression in her eyes.
Amelia: <i>(Great. Of course it's a human that saves me.)</i> #portrait:AmeliaShut
Amelia: Well, thanks and all. But this is where our meeting ends. #portrait:AmeliaNeutral
Noelle: Wait! #portrait:NoelleUm
Noelle: I don't have anywhere to go. #portrait:NoelleUm
Amelia: What? Just go back the way you came. #portrait:AmeliaNeutral
Noelle: I'm not from here. My boat's wrecked, and, well... #portrait:NoelleUm
Narrator: The words are caught in your throat. Would a stranger you just met understand, let alone care?

+[It’s a long story.]
    -> choiceitsalongstory
+[I just can’t, okay?]
    -> choiceijustcantokay
    
===choiceitsalongstory===
Noelle: It's a long story… #portrait:NoelleUm
Amelia: Well, you can't just stay here. Your settlement’s probably looking for you. #portrait:AmeliaNeutral
->afterchoicea1s1

===choiceijustcantokay===
Noelle: I just can't, okay? #portrait:NoelleAngry
Amelia: Fine, don't tell me. But you can't just stay here. Your settlement’s probably looking for you. #portrait:AmeliaNeutral
-> afterchoicea1s1

===afterchoicea1s1===
Noelle: That's the thing… #portrait:NoelleMask
Narrator: You move your hair around, willing the tentacles to form a mask.
Noelle: I'm not a human, and I can’t go back to my homeland like this. #portrait:NoelleMask
Amelia: Huh. Are you on the run or something? #portrait:AmeliaNeutral
Narrator: Amelia gives you an odd look, before her gaze trails down to the style of your clothes.
Amelia: Ah, so you're an outsider. I guess it's true that some islands aren't used to Thavma. #portrait:AmeliaNeutral
Noelle: <i>(Thavma?</i> Are there others like me?) #portrait:NoelleUm
Noelle: Can I ask— #portrait:NoelleSad
Amelia: Sorry! Not really a tour guide kind of dragon. You're better off wandering in-land to find the Viridi— #portrait:AmeliaHappy
Noelle: —Come on, <i>please?</i> I promise I won't take up too much of your time! #portrait:NoelleSad
Narrator: Before either of you can say more, a rumble emits from Amelia's stomach.
// insert stomach rumble SFX
Amelia: ... #portrait:AmeliaHostile
Amelia: ... #portrait:AmeliaShut
Amelia: Look, no one in their right mind would trust a stranger, just like that. #portrait:AmeliaShut
Amelia: It's not like I can provide you with anything, either. #portrait:AmeliaNeutral
Noelle: Then why don't we work together? #portrait:NoelleSmallSmile
Noelle: You share your knowledge of this island, and I'll handle things like food. #portrait:NoelleSmallSmile
Narrator: Amelia takes a moment to size you up. She holds your gaze for a moment as if to access your reliability, before sighing.
Amelia: ...Alright. But before I agree, show me that you can actually do as you say. #portrait:AmeliaNeutral
Amelia: There should be some berry shrubs nearby. Get me 10 berries and I'll consider it. #portrait:AmeliaNeutral
EVENT:AddQuest:A1_S1_Q1
->DONE