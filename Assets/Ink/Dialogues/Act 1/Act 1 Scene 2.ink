EXTERNAL checkQuestStatus(id, steps)
EXTERNAL TurnOffBarrier(id)
EXTERNAL SwapBGM(new_id, old_id, FadeSpeed)
EXTERNAL PlayBGM(id)
EXTERNAL AddQuest(id)
EXTERNAL SubmitQuest(questId)
EXTERNAL SetNextDialogue(dialogueId)

INCLUDE ../Globals/Globals.ink


// Variable Setup
CONST DIALOGUE_1 = "A1_S1_D1"
CONST DIALOGUE_2 = "A1_S1_D2"

CONST QUEST_1 = "A1_S1_Q1"

/*
{
    - dialogue_id == DIALOGUE_1:
        -> dialogue_1
    - dialogue_id == QUEST_1:
        -> quest_1
    - dialogue_id == DIALOGUE_2:
        -> dialogue_2
}*/
/*
===dialogue_1===
->main

===quest_1===
{
    - quest_state == "ONGOING":
        -> ongoing_quest_1
    - quest_state == "COMPLETED":
        -> completed_quest_1
}

===dialogue_2===
-> PostquestSubmit
*/
SCENE 2 ✅
Walk into the forest area
Run into a puzzle > puzzle tutorial 
Cross into Viridi Settlement, meet amaya

//follow the path and reach a certain point in the forest
So, what's your plan for getting out of here before the disaster? #speaker:amelia #portrait:amelianormal
My plan… Well, I guess the first step is trying to fix my ship. #speaker:noelle #portrait:noellesceptical
Are you sure you can fix it? It looks pretty bad. #speaker:amelia #portrait:amelianormal
I mean I’ve never built a ship alone before, but I can try. My home island’s practices are all about wayfaring. #speaker:noelle #portrait:noellesmallsmile
The mess I saw wasn’t looking very functional. #speaker:amelia #portrait:ameliahostile
…We’re going to need a lot of supplies. Mainly wood, fabric for sails, iron, rope and glue. Do we have any of that around here? #speaker:noelle #portrait:noellesceptical
There are plenty of settlements around. We should be able to get wood the easiest, since the forest’s close by. #speaker:amelia #portrait:amelianormal
//keep walking down the path
//once we reach a point on the road, dialogue triggers again and it rains for 60 seconds.
Ah! Gosh, the weather’s been getting worse lately. #speaker:amelia #portrait:ameliahostile
Why's that? #speaker:noelle #portrait:noelleum
The Great Disaster obviously. Do you live under a rock? #speaker:amelia #portrait:amelianormal
Um… #speaker:noelle #portrait:noelleum
You seriously don’t know about the great floods? #speaker:amelia #portrait:ameliahostile
I’ve only heard stories about them. We don’t talk much about the thing that causes it. #speaker:noelle #portrait:noelleum
I’m sure someone else can do a better job of explaining it, but all you need to know is that the Great Disaster is the creature behind it all. #speaker:amelia #portrait:amelianormal
With the weather getting worse and the waves getting stronger… It’s a sign that the floods are about to come, soon.
(…Already?) #speaker:noelle #portrait:noelleshocked
(Dusk Island… their preparations aren’t completely done yet.)
How much longer do we have? #speaker:noelle #portrait:noellesceptical
Two to four weeks, at most. You okay? #speaker:amelia #portrait:amelianormal
+[Yeah, just thinking.]
+[Just counting the days.]
You’re so weird. #speaker:amelia #portrait:amelianormal

/*after this point we need to walk further. Next dialogue is prompted when we reach the first puzzle.
Many creatures roam around the wild forest. Not interactable for now*/

Upon seeing the ruin, Amelia gets frustrated. This place appears to be a dead end. #speaker:narrator
Ugh, this thing again...  Looks like we’ll need to go around it. #speaker:amelia #portrait:ameliahostile
+[Is it a puzzle?]
+[Let me take a look.]
You? Solving these ancient ruins? Do you know how heavy those blocks are? #speaker:amelia #portrait:ameliahostile
…Knock yourself out, I suppose. #portrait:amelianormal
//puzzle tutorial appears here. Player gets to solve it. After which dialogue continues
…? #speaker:amelia #portrait:ameliasilly
Amelia studies you like you’re an alien. #speaker:narrator 
It’d take twenty men to move that! How on earth did you—? #speaker:amelia #portrait:ameliasilly
...You’re interesting. #portrait:amelianormal
Thank you? #speaker:noelle #portrait:noellewhat
The blocks were pretty heavy, but after putting your weight into it, they budged. #speaker:narrator
To be honest, the hardest part was adjusting them to the right place since they’re so huge. 
But you’re happy to be of use.
…So, you said the settlement was through this way? #speaker:noelle #portrait:noellebigsmile
Yeah, It's not far from here. Let’s go. #speaker:amelia #portrait:amelianormal

//same objective of finding the viridi settlement (doesn’t change). Player follows the path, terrain becomes more leafy and wild, entering viridi forest

We shouldn’t be too far off from the settlement now, I’m starting to recognise these plants. #speaker:amelia #portrait:amelianormal
That's good— #speaker:noelle #portrait:noellesmallsmile
You hear rustling and see a figure amidst the trees. #speaker:narrator
Who are you? What are you doing here!? #speaker:amaya
Who are <b>you?</b> #speaker:noelle #portrait:noellewhat
<b>I’m</b> the one asking questions. Why are the two of you here? #speaker:amaya
+[Introduce yourself.]
    -> choiceintroyourself
+[Stay silent.]
    -> choicestaysilent

===choiceintroyourself===
I’m from the Tempest family; Noelle Tempest. #speaker:noelle
Tempest…? That’s a strange name. #speaker:amaya
Is it? #speaker:noelle
    -> choicestaysilent

===choicestaysilent===
…You’re not from here. #speaker:amaya
Why are you sneaking around? What do you want?
We’re looking for something. #speaker:amelia
Both of you? #speaker:amaya
The person seems to give you both a once-over. You try to explain. #speaker:narrator
It’s about my ship— #speaker:noelle
—Only to stop when they step out into the light. Hair the color of foliage. Eyes as sharp as iron. #speaker:narrator
…Surely you’d rather spend your time preparing for the Great Disaster, instead of milling around our territory like this? #speaker:amaya
I’m sorry— The great <i>what?</i> #speaker:noelle
I’m just looking for a way to repair my boat.
And hey, who are <i>you?</i> You never told us! #speaker:amelia
And a dragon. Interesting. #speaker:amaya
Far from your pack, are you? You two are certainly suspicious.
We aren’t doing anything suspicious! #speaker:amelia
…I think it’s better if we just go along here to avoid a fight. #speaker:noelle
(It’s clear that this girl isn’t just going to let us off easy, but maybe we can get closer to what we need, this way.)

//new objective: follow the strange girl into the forest
->END
