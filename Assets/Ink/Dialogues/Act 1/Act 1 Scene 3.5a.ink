EXTERNAL checkQuestStatus(id, steps)
EXTERNAL TurnOffBarrier(id)
EXTERNAL SwapBGM(new_id, old_id, FadeSpeed)
EXTERNAL PlayBGM(id)
EXTERNAL AddQuest(id)
EXTERNAL SubmitQuest(questId)
EXTERNAL SetNextDialogue(dialogueId)

INCLUDE ../Globals/Globals.ink


// Variable Setup
CONST DIALOGUE_1 = "A1_S3_5A_D1"
CONST DIALOGUE_2 = "A1_S3_5A_D2"
CONST DIALOGUE_3 = "A1_S3_5A_D3"
{
    - dialogue_id == DIALOGUE_1:
        -> dialogue_1
    - dialogue_id == DIALOGUE_2:
        -> dialogue_2
    - dialogue_id == DIALOGUE_3:
        -> dialogue_3
}


// outline of main branches
===dialogue_1===
->A1_S3_5A_D1

===dialogue_2===
->A1_S3_5A_D2

===dialogue_3===
->A1_S3_5A_D3


//quest objective: talk to villagers (0/3)
//you can talk to them in any order but if it’s easier you can keep the same objective but make each convo linear

===A1_S3_5A_D1===
//one
The weather has really gotten worse lately, it’s been raining so much! #speaker:carti
I hope it stops soon before it affects the forest.
I wonder where Chione has been going off to lately; she says she’s been searching for herbs? #speaker:bri
->DONE


===A1_S3_5A_D2===
//next one
Where were you yesterday? You missed the finale of Oren’s story. #speaker:aspho
Wait, that was yesterday?! #speaker:faile
Yeah! You missed the best part! #speaker:aspho
What happens? Do they get together in the end? #speaker:faile
They do, and get this— he ended up confessing to Noah! #speaker:aspho
<i>No.</i> #speaker:faile
(Judging by how invested they sound, maybe it’d be a good idea to listen to this ‘Oren’s stories, too.) #speaker:noelle
A settlement’s tales always reveal their culture, after all.
Uh… but what was that about a confession?
->DONE

===A1_S3_5A_D3===
//next
I overheard Oren talking to Silas earlier. I think today’s session will be about the Great Disaster! #speaker:dion
Before you an eavesdrop more, the two viridi villagers notice you. #speaker:narrator
Woah, you’re not from around here! #speaker:este
What’s a Krakenfolk doing so deep in the woods?
+[A Krakenfolk?]
    -> whatskraken
+[I’m just looking for some supplies.]
    -> justsupplies

===whatskraken===
Don’t be silly, your features are pretty obvious. #speaker:dion
What are you doing in our village? You guys don’t tend to visit these parts. #speaker:este
I was just looking for some simple supplies. #speaker:noelle
Ah, I see… #speaker:dion
   -> justsupplies

===justsupplies===
Well, you’ve come to the right place! Have you talked to Chione yet? #speaker:dion
Yes, I did. #speaker:noelle
Lovely! Oh and make sure to have a listen of Oren’s stories while you’re here. I think the session’s about to start. #speaker:dion
Right… I’ve been hearing about him. Who <i>is</i> Oren? #speaker:noelle
He’s our local storyteller! Hosts shows very often based on some history and other made-up things. #speaker:este
(Oren, huh…?) #speaker:noelle
Sure, I’ll definitely have a look.
   //-> scenefour
   ->END