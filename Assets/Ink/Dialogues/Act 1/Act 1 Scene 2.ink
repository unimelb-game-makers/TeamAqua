EXTERNAL checkQuestStatus(id, steps)
EXTERNAL TurnOffBarrier(id)
EXTERNAL SwapBGM(new_id, old_id, FadeSpeed)
EXTERNAL PlayBGM(id)
EXTERNAL AddQuest(id)
EXTERNAL SubmitQuest(questId)
EXTERNAL SetNextDialogue(dialogueId)

INCLUDE ../Globals/Globals.ink


// Variable Setup
CONST DIALOGUE_1 = "A1_S2_D1"
CONST DIALOGUE_2 = "A1_S2_D2"
CONST DIALOGUE_3 = "A1_S2_D3"
CONST DIALOGUE_4 = "A1_S2_D4"
CONST DIALOGUE_5 = "A1_S2_D5"
CONST QUEST_1 = "A1_S2_Q1"
CONST DIALOGUE_6 = "A1_S2_D6"
{
    - dialogue_id == DIALOGUE_1:
        -> dialogue_1
    - dialogue_id == DIALOGUE_2:
        -> dialogue_2
    - dialogue_id == DIALOGUE_3:
        -> dialogue_3
    - dialogue_id == DIALOGUE_4:
        -> dialogue_4
    - dialogue_id == DIALOGUE_5:
        -> dialogue_5
    - dialogue_id == QUEST_1:
        -> quest_1
    - dialogue_id == DIALOGUE_6:
        -> dialogue_6
}


// outline of main branches
===dialogue_1===
->A1_S2_D1

===dialogue_2===
->A1_S2_D2

===dialogue_3===
->A1_S2_D3

===dialogue_4===
->A1_S2_D4

===dialogue_5===
->A1_S2_D5

===quest_1===
{
    - quest_state == "ONGOING":
        -> ongoing_quest_1
    - quest_state == "COMPLETED":
        -> completed_quest_1
}

===dialogue_6===
-> A1_S2_D6

===dialogue_7===
-> PostquestSubmit



/*
SCENE 2 ✅
Walk into the forest area
Run into a puzzle > puzzle tutorial 
Cross into Viridi Settlement, meet Amaya
*/



// first orb on beach
===A1_S2_D1===
//follow the path and reach a certain point in the forest
So, what's your plan for getting out of here before the disaster? #speaker:Amelia #portrait:AmeliaNormal
My plan… Well, I guess the first step is trying to fix my ship. #speaker:Noelle #portrait:NoelleSceptical
Are you sure you can fix it? It looks pretty bad. #speaker:Amelia #portrait:AmeliaNormal
I mean I’ve never built a ship alone before, but I can try. My home island’s practices are all about wayfaring. #speaker:Noelle #portrait:NoelleSmallSmile
->DONE

===A1_S2_D2===
The mess I saw wasn’t looking very functional. #speaker:Amelia #portrait:AmeliaHostile
…We’re going to need a lot of supplies. Mainly wood, fabric for sails, iron, rope and glue. Do we have any of that around here? #speaker:Noelle #portrait:NoelleSceptical
There are plenty of settlements around. We should be able to get wood the easiest, since the forest’s close by. #speaker:Amelia #portrait:AmeliaNormal
->DONE

===A1_S2_D3===
//keep walking down the path
//once we reach a point on the road, dialogue triggers again and it rains for 60 seconds.
Ah! Gosh, the weather’s been getting worse lately. #speaker:Amelia #portrait:AmeliaHostile
Why's that? #speaker:Noelle #portrait:NoelleUm
The Great Disaster obviously. Do you live under a rock? #speaker:Amelia #portrait:AmeliaNormal
->DONE


===A1_S2_D4===
Um… #speaker:Noelle #portrait:NoelleUm
You seriously don’t know about the great floods? #speaker:Amelia #portrait:AmeliaHostile
I’ve only heard stories about them. We don’t talk much about the thing that causes it. #speaker:Noelle #portrait:NoelleUm
I’m sure someone else can do a better job of explaining it, but all you need to know is that the Great Disaster is the creature behind it all. #speaker:Amelia #portrait:AmeliaNormal
With the weather getting worse and the waves getting stronger… It’s a sign that the floods are about to come, soon.
(…Already?) #speaker:Noelle #portrait:NoelleShocked
(Dusk Island… their preparations aren’t completely done yet.)
How much longer do we have? #speaker:Noelle #portrait:NoelleSceptical
Two to four weeks, at most. You okay? #speaker:Amelia #portrait:AmeliaNormal
+[Yeah, just thinking.]
    ->DONE
+[Just counting the days.]
You’re so weird. #speaker:Amelia #portrait:AmeliaNormal
    ->DONE

/*after this point we need to walk further. Next dialogue is prompted when we reach the first puzzle.
Many creatures roam around the wild forest. Not interactable for now*/

// orb right outside puzzle entrance
===A1_S2_D5===
Upon seeing the ruin, Amelia gets frustrated. This place appears to be a dead end. #speaker:Narrator
Ugh, this thing again...  Looks like we’ll need to go around it. #speaker:Amelia #portrait:AmeliaHostile
+[Is it a puzzle?]
    ->quest
+[Let me take a look.]
    ->quest
===quest===
You? Solving these ancient ruins? Do you know how heavy those blocks are? #speaker:Amelia #portrait:AmeliaHostile
…Knock yourself out, I suppose. #portrait:AmeliaNormal
//puzzle tutorial appears here. Player gets to solve it. After which dialogue continues
~ AddQuest(QUEST_1)
->DONE


// quests   --- orb right outside puzzle 1 exit
===ongoing_quest_1===
You haven't solved the puzzle yet!
->DONE

===completed_quest_1===
~SubmitQuest(QUEST_1)
…? #speaker:Amelia #portrait:AmeliaSilly
Amelia studies you like you’re an alien. #speaker:Narrator 
It’d take twenty men to move that! How on earth did you—? #speaker:Amelia #portrait:AmeliaSilly
...You’re interesting. #portrait:AmeliaNormal
Thank you? #speaker:Noelle #portrait:NoelleWhat
The blocks were pretty heavy, but after putting your weight into it, they budged. #speaker:Narrator
To be honest, the hardest part was adjusting them to the right place since they’re so huge. 
But you’re happy to be of use.
…So, you said the settlement was through this way? #speaker:Noelle #portrait:NoelleBigSmile
Yeah, It's not far from here. Let’s go. #speaker:Amelia #portrait:AmeliaNormal
->DONE



// orb in between puzzle 1 and puzzle 2 / around where amaya is
===A1_S2_D6===
//same objective of finding the viridi settlement (doesn’t change). Player follows the path, terrain becomes more leafy and wild, entering viridi forest

We shouldn’t be too far off from the settlement now, I’m starting to recognise these plants. #speaker:Amelia #portrait:AmeliaNormal
That's good— #speaker:Noelle #portrait:NoelleSmallSmile
You hear rustling and see a figure amidst the trees. #speaker:Narrator
Who are you? What are you doing here!? #speaker:Amaya
Who are <b>you?</b> #speaker:Noelle #portrait:NoelleWhat
<b>I’m</b> the one asking questions. Why are the two of you here? #speaker:Amaya
+[Introduce yourself.]
    -> choiceintroyourself
+[Stay silent.]
    -> choicestaysilent

===choiceintroyourself===
I’m from the Tempest family; Noelle Tempest. #speaker:Noelle
Tempest…? That’s a strange name. #speaker:Amaya
Is it? #speaker:Noelle
    -> choicestaysilent

===choicestaysilent===
…You’re not from here. #speaker:Amaya
Why are you sneaking around? What do you want?
We’re looking for something. #speaker:Amelia
Both of you? #speaker:Amaya
The person seems to give you both a once-over. You try to explain. #speaker:Narrator
It’s about my ship— #speaker:Noelle
—Only to stop when they step out into the light. Hair the color of foliage. Eyes as sharp as iron. #speaker:Narrator
…Surely you’d rather spend your time preparing for the Great Disaster, instead of milling around our territory like this? #speaker:Amaya
I’m sorry— The great <i>what?</i> #speaker:Noelle
I’m just looking for a way to repair my boat.
And hey, who are <i>you?</i> You never told us! #speaker:Amelia
And a dragon. Interesting. #speaker:Amaya
Far from your pack, are you? You two are certainly suspicious.
We aren’t doing anything suspicious! #speaker:Amelia
…I think it’s better if we just go along here to avoid a fight. #speaker:Noelle
(It’s clear that this girl isn’t just going to let us off easy, but maybe we can get closer to what we need, this way.)
//new objective: follow the strange girl into the forest
->DONE


===PostquestSubmit===
Lets follow her!    #speaker:Amelia
->END