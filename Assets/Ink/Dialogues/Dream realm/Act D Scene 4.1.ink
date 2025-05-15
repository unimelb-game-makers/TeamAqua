EXTERNAL checkQuestStatus(id, steps)
EXTERNAL TurnOffBarrier(id)
EXTERNAL SwapBGM(new_id, old_id, FadeSpeed)
EXTERNAL PlayBGM(id)
EXTERNAL AddQuest(id)
EXTERNAL SubmitQuest(questId)
EXTERNAL SetNextDialogue(dialogueId)

INCLUDE ../Globals/Globals.ink



// Variable Setup
CONST DIALOGUE_1 = "A1_S4.1_D1"
CONST DIALOGUE_2 = "A1_S4.1_D2"

{
    - dialogue_id == "DIALOGUE_1":
        -> dialogue_1
    - dialogue_id == "DIALOGUE_2":
        -> dialogue_2
}


// outline of main branches
===dialogue_1===
->A1_S4_O1

===dialogue_2===
->A1_S4_O2


//instead of crossing into the next day, the player has to go to the guesthouse and click E to ‘rest for the night’. This is a DIALOGUE E, NOT a DAY TRANSITION
//rest area (sleeping function) is not yet unlocked so just attach this DIALOGUE E to the house
//player then loads into the dream realm where the below dialogue happens

===A1_S4_O1===
//first orb
Despite sleeping in a strange and unfamiliar place, you managed to sleep pretty quickly due to the exhaustion of today’s whirlwind events. #speaker:narrator
…It’d be a little better if Amelia didn’t keep rolling over and bumping you awake, but that’s alright. 
You’re just glad to have a roof over your head. 
->DONE

===A1_S4_O2===
//second orb
…Another memory. #speaker:narrator
<i>Creatures are dangerous, deceptive beings.</i> #speaker:Mother
<i>With their ability to speak, they can lure sailors into the water to attack. Or steal supplies from us humans who have spent so long foraging for food.</i>
<i>Which is why our home of Dusk is so successful, Noelle.</i>
<i>You must always remember that we Tempests always stride forward. Leading our own to survival despite the harrowing, outside world.</i>
(The floods were always regarded as a giant, natural disaster…) #speaker:noelle
(But turns out there is a creature that causes it?)
(Why would it destroy the archipelagos every five-hundred years?)
The creatures and Thavma that you’ve met are just as empathetic and intelligent as any human; not mindless monsters. #speaker:narrator
And they’re even able to live in coexistence.
…
…If so, what about the Great Disaster?

//go to sun icon to wake up and begin scene five
->END