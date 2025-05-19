EXTERNAL checkQuestStatus(id, steps)
EXTERNAL TurnOffBarrier(id)
EXTERNAL SwapBGM(new_id, old_id, FadeSpeed)
EXTERNAL PlayBGM(id)
EXTERNAL AddQuest(id)
EXTERNAL SubmitQuest(questId)
EXTERNAL SetNextDialogue(dialogueId)

INCLUDE ../Globals/Globals.ink


// Variable Setup
CONST DIALOGUE_1 = "A1_S3_5_D1"
CONST QUEST_1 = "A1_S3_5_Q1"
CONST DIALOGUE_2 = "A1_S3_5_D2"

{
    - dialogue_id == DIALOGUE_1:
        -> dialogue_1
}


// outline of main branches
===dialogue_1===
->A1_S3_D1_1


// place this near the center of the settlement


===A1_S3_D1_1===
The Viridi settlement is hidden amidst the forest, with small houses that make up a lively village. #speaker:narrator
It’s certainly not as large as the community on your home island, but the appearance of so many people—Viridi—in one place, helps you relax a little.
It’s a small piece of familiarity within the craziness of the past twenty-four hours, yet foreign at the same time.  
Amaya, you’re back! #speaker:chione
Who are these people with you?
I found them outside, after they passed through the ruins. #speaker:amaya
(...I guess the ruins were supposed to keep outsiders away.) #speaker:noelle
Their methods and backstory seemed suspicious, so I brought them here. #speaker:amaya
I need to speak to the Elder, so can you keep watch, Chione?
Hey! We’re not some kind of dangerous animal! #speaker:amelia
The two smoothly move past the comment and continue. #speaker:narrator
That’s alright! Go ahead. #speaker:chione
//amaya’s sprite walks away?
Once Amaya has made her leave, Chione introduces herself to break the ice. #speaker:narrator
Hello! I’m Chione! #speaker:chione
#speaker:amaya
+[Stay silent.]
    -> silenttochione
+[Introduce yourself.]
    -> introtochione

===silenttochione===
…Oh, it’s alright! You don’t need to be afraid to talk. #speaker:chione
Amaya’s actually a really nice person! She’s just protective of us.
Trailing off, the curiosity in Chione’s eyes pushes her forward. #speaker:narrator 
What brought you to this island, if you don’t mind me asking? #speaker:chione
   -> meetchione

===introtochione===
I’m Noelle Tempest, and this is Amelia. #speaker:noelle
Tempest? That’s an interesting last name; so you’re not from this island? #speaker:chione
(So instead of my appearance, it’s my last name that gives that away?) #speaker:noelle
   -> meetchione

===meetchione===
I got shipwrecked here this morning, and need to find a way to fix my boat. #speaker:noelle
Fix your boat? Well you couldn’t have come at a better or worse time. #speaker:chione 
What do you mean? #speaker:noelle
Well… on one hand, we have plenty of wood you could use around here. But on the other, the Great Disaster’s almost ready to approach. #speaker:chione
The stories you’ve heard about the Great Floods from your home have always been quite vague. #speaker:narrator
All you know is that they’re the reason your people have specialised in building arks; to sail through the floods, rather than allowing them to demolish everything you’ve built. 
But over here, everyone’s been referring to not the floods, but the creature behind it?
(The Great Disaster…) #speaker:noelle
Well… fixing my boat is my own way of preparing for the floods.
If there’s a way to avoid it, shouldn’t that be done?
The phrasing of your words causes concern to crease on Chione’s brow. #speaker:narrator
I understand where you’re coming from, but please be careful of what you say around the village. #speaker:chione
It’s a brave thing to want to stop fate, but our way of life prioritises nature. 
Therefore, we shouldn’t mess with the natural order of things, or attempt to place our personal feelings above them.
Our elder Silas has always taught us to respect the land; from the largest creatures to the smallest plants.
Chione catches herself, trying to lighten the mood. #speaker:narrator
…Which is why even allowing you to harvest wood in our area requires a green-light from him. #speaker:chione
I hope you understand; we only take as much as we need, and regularly give back to the island just as it helps us. 
But enough about that— I can still ‘keep an eye’ on you even when we aren’t talking, so feel free to explore around.
Maybe even listen to Oren’s stories, while you’re at it! I’ll catch you later once arrangements are confirmed with Amaya and Silas.
//~AddQuest(QUEST_1)
->END

