
//interactions: just basic dialogue continuing
//Global ink: not needed
//Dev review: complete

EXTERNAL SwapBGM(new_id, old_id, FadeSpeed)
EXTERNAL ChangeCutscene(SceneName)
EXTERNAL PlayBGM(id)

INCLUDE ../Globals/Globals.ink

~PlayBGM("BGM_CUTSCENE_CEREMONY")

//---------------------------------------------SCRIPT----------------------------------------

/*i think the typing sound for the prologue text works well! Makes it ominous. All the audio is in red font so that it's easy for you to ref and remove*/

A birthday tune is hummed as you sit around a campfire with your family, embers floating by your feet. #speaker:Narrator #cutscene:A0S1
“We are gathered here today to celebrate the coming of age of our dear Noelle. Next in line to the Tempest family name,” #cutscene:A0S2
“the finest of any family here on this little island.” #cutscene:A0S3
“A beacon of hope against any and all creatures that lurk in the shadow.” #cutscene:A0S4
… #cutscene:A0S5
“Are you ready to make all of us proud?”
You stare at the chalice, then at your grandfather and parents. Eager claps follow as you nod.
“Excellent!” #cutscene:A0S6
“Drink up! Awaken your inner potential!”
You take a gulp. The taste of iron spreads on your tongue. 
In seconds, you begin to choke and fall to your knees, struggling to catch your breath. #cutscene:A0S7
//steven yaps: old cutscene switched the bgm here
What…?
“Did someone poison the awakening potion?!” 
“You made it, Theodore. Are you trying to destroy the last of our legacy?!” #cutscene:A0S8
“How could you, to our own daughter—!”
“No no, of course not! I followed the recipe exactly! It shouldn’t be affecting her like this!”
Shadows swarm around you. Did your father really…? #cutscene:A0S9
Their outbursts dim as something rough and painful peels against your face.
And all of a sudden, the arguing stops. #cutscene:A0S10
You notice the claws on your hands. People gasp. #cutscene:A0S11
Your grandfather’s gnarled, bony finger points at your face.
“Monster!”
“No, no, I can't be—”
“And yet, you clearly are.” #cutscene:A0S12
Your mother’s expression twists into something deeper than anger. #cutscene:A0S13
“Well this is great— just great! Our only heir, a monster!”
“You didn’t tell me there was a monster on your side of the family.” She says to your father. #cutscene:A0S14
“No, that’s preposterous! That’s impossible!”
“At this point, anything is.”
A chill runs down your spine as your mother turns to you. #cutscene:A0S15
//track changes from royal to ominous here? Can fade out with the doom SFX
~SwapBGM("BGM_CUTSCENE_TRANSFORMATION","BGM_CUTSCENE_CEREMONY", 1) 
//change bgm right after this line
“Now listen here, this is what happened—” 
“Our darling Noelle was moments away from completing her coming of age ritual when a vicious creature attacked.”
“The creature was ultimately slain by yours truly, but at the tragic cost of our dear daughter’s life.”
“How terrible this day is, for the Tempest family to be robbed of its only heir, by a creature no less.” #cutscene:A0S16
"You can’t do this! I’m your daughter!”
“Not anymore. Not after what you've become.”
Your mother’s eyes grow manic. The knife in her hand glints.
It takes everything in you to not stumble as you run. Run, and run. #cutscene:A0S17
“You really had to go and ruin this for everyone, didn’t you?” Your mother snarls. 
Clouds are gathering in the sky by the time you reach the nearest boat. Your vision blurs with panic. #cutscene:A0S18
“I suppose we’ll need to find a replacement.” Your mother’s voice pursues you, not far behind.
“Anything to erase the mark of a vile creature in our midst.”
…The Island of Dusk lives in seclusion from the rest of the world. Priding itself on its natural ecosystem, void of dangerous creatures. #cutscene:A0S19
You watch as the very teachings cooed to you as a child now chase after you in a hunt.
You are no longer ‘Noelle,’ to them.
You are one of the monsters which your mother used to protect you from.
Monsters which have only existed in the imagination, until now.
Thunder and lightning flashes as if in warning as you flee atop a small ship. #cutscene:A0S20
…
“Goodbye Noelle, and may the gods have mercy on your soul.”
Rain is bucketing down from the sky. #cutscene:A0S21
“That’s right, you better run!”
“Run and run and run, and never come back!” #cutscene:A0S22
You aren’t sure if the voices you hear are from your island or simply figments of your imagination.
But something dark seems to swim beneath the ocean, curling and coiling closer, until your home is miles away.
A giant wave crashes down— #cutscene:A0S23
And everything turns black. #cutscene:A0S24
//fade out the BGM track on ‘and everything turns black’
~ChangeCutscene("DreamIsland")
->END