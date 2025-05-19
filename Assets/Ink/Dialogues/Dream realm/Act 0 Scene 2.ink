EXTERNAL checkQuestStatus(id, steps)
EXTERNAL TurnOffBarrier(id)
EXTERNAL SwapBGM(new_id, old_id, FadeSpeed)
EXTERNAL PlayBGM(id)
EXTERNAL AddQuest(id)
EXTERNAL SubmitQuest(questId)
EXTERNAL SetNextDialogue(dialogueId)

INCLUDE ../Globals/Globals.ink


// Variable Setup
CONST DIALOGUE_1 = "A0_S2_D1"
CONST DIALOGUE_2 = "A0_S2_D2"
CONST DIALOGUE_3 = "A0_S2_D3"
{
    - dialogue_id == DIALOGUE_1:
        -> dialogue_1
    - dialogue_id == DIALOGUE_2:
        -> dialogue_2
    - dialogue_id == DIALOGUE_3:
        -> dialogue_3
}

// fades into Noelle standing in the dream realm
// movement tutorial
// click first orb to start dialogue below

===dialogue_1===
->A0_S2_O1

===dialogue_2===
->A0_S2_O2

===dialogue_3===
->A0_S2_O3


===A0_S2_O1===
…Your body and head ache. #speaker:Narrator
In just one night; your family, your home…
…Who knows what happened to me in that storm? #speaker:Noelle
It might be better if I just…
->DONE


===A0_S2_O2===
// next orb to continue
You try to push the memories of your family’s betrayal aside. #speaker:Narrator
The glow of these strange objects seem to calm your mind a little.
That is, until familiar voices float from them.
->DONE


==A0_S2_O3===
// next orb
<i>Noelle, one day, you’ll take over as the head of the family.</i> #speaker:Mother
<i>You’ll be the greatest wayfinder amongst us and lead us through the floods.</i>
What happened to you? #speaker:Narrator
The ceremony you’d been looking forward to as a child — a special day your mother told you about
braiding your hair while reminding you how special it’d be.
She was always strict when it came to the family and its leadership.
But you never expected that the ceremony could take such a turn.
<i>The Tempest name has guided and led our island to safety for many centuries.</i> #speaker:Mother
<i>Once you come of age, you too…</i>
Now, you’ll cease to exist in records. Turned into the very kind of monster that your island was blessed to have none of. #speaker:Narrator
<i>How do we escape the floods?</i> #speaker:Young Noelle
<i>We build arks — dozens of them.</i> #speaker:Mother
<i>Does the giant monster cause the floods because it’s evil?</i> #speaker:Young Noelle
<i>Most creatures are wild, dangerous things.</i> #speaker:Mother
<i>Which is why we never leave our Island of Dusk.</i> 
You’ve only seen drawings of such creatures in books; <i>animals,</i> as they were also called, with threatening features and the ability to speak like humans do. #speaker:Narrator
And now… you are neither. A hybrid of the two that is even more monstrous than any child could imagine.
->END
//final sun exit icon
//fade into main scene and act 1
