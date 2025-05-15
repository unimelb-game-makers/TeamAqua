EXTERNAL checkQuestStatus(id, steps)
EXTERNAL TurnOffBarrier(id)
EXTERNAL SwapBGM(new_id, old_id, FadeSpeed)
EXTERNAL PlayBGM(id)
EXTERNAL AddQuest(id)
EXTERNAL SubmitQuest(questId)
EXTERNAL SetNextDialogue(dialogueId)

INCLUDE ../Globals/Globals.ink


// Variable Setup
CONST DIALOGUE_1 = "A1_S3_D1"
CONST QUEST_1 = "A1_S3_Q1"
CONST DIALOGUE_2 = "A1_S3_D2"

{
    - dialogue_id == DIALOGUE_1:
        -> dialogue_1
    - dialogue_id == QUEST_1:
        -> quest_1
    - dialogue_id == DIALOGUE_2:
        -> dialogue_2
}


// outline of main branches
===dialogue_1===
->A1_S3_D1_1

===quest_1===
{
    - quest_state == "ONGOING":
        -> ongoing_quest_1
    - quest_state == "COMPLETED":
        -> completed_quest_1
}

===dialogue_2===
-> PostquestSubmit


===A1_S3_D1_1===
/*
SCENE 3 ✅
Follow amaya (invisible wall to not let us get away) > come across puzzle > solve puzzle
Reach the settlement, meet chione 
Explore settlement and ‘listen to oren’s story’
*/
The three of you make your way further into the forest, where the trees grow thicker and another ruin appears ahead. #speaker:narrator.
Amaya stops walking and gestures for you to go forward.
You got past a puzzle before we ran into each other, right? Show me how you did it. #speaker:amaya
+[I just pushed them?]
    -> secondpuzzle
+[It’s a secret.]
    -> secondpuzzle

===secondpuzzle===
Those are solid stone. It takes ten average men to move it manually. #speaker:amaya
(It <i>was</i> quite heavy, but how does that make sense?) #speaker:noelle
You whisper to Amelia. #speaker:narrator
I guess someone like me must not be common around here? #speaker:noelle
No, but this lady’s pretty strange, too! #speaker:amelia
What are you two whispering about? #speaker:amaya
Nothing! #speaker:amelia
->TakeQuest


===TakeQuest===
Well, go on then. #speaker:amaya
->DONE

//solve puzzle, which then prompts the screen to fade and everyone’s now on the other side

===ongoing_quest_1===
I'm waiting! #speaker:amaya
->DONE

===completed_quest_1===
So you really can move them on your own… #speaker:amaya
I know you Krakenfolk are strong, but I didn’t realize it’d be to this extent. 
(There’s that term again!) #speaker:noelle
Krakenfolk?
Yeah. I might be a Viridi, but I do talk to others too. #speaker:amaya
(She doesn’t seem fazed at all.) #speaker:noelle
(Maybe… there are others like me? Ones that also look like ‘monsters?’)
You said that you’re here for a boat? #speaker:amaya
Ah, yes! We were just looking for wood and other materials to fix it up. #speaker:noelle
Truth be told, while you were familiar with building ships and the like, you’ve never chopped down whole trees, before. #speaker:narrator
I… guess you might be able to tell, but I’m not from this island. #speaker:noelle
So any help would be greatly appreciated.
…Are you just recruiting anyone you see? #speaker:amelia
You hush the dragon. It doesn’t hurt to ask! #speaker:narrator
And intimidating as she may appear, the fact that this person hasn’t attacked you directly should indicate something. 
I won’t make any promises, but to make sure you aren’t a threat, I’ll need you to come with me to our settlement. #speaker:amaya
Whether or not any of us will ‘help,’ depends on our leader’s judgement. 

//quest objective, follow amaya into the settlement (environment will need to show it clearly. Maybe if not a path then a trail of flowers)
//upon reaching, speak to chione to proceed (she’s standing in the middle of the settlement)

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

//quest objective: talk to villagers (0/3)
//you can talk to them in any order but if it’s easier you can keep the same objective but make each convo linear

The weather has really gotten worse lately, it’s been raining so much! #speaker:carti
I hope it stops soon before it affects the forest.
I wonder where Chione has been going off to lately; she says she’s been searching for herbs? #speaker:bri

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
   ->DONE
   
===PostquestSubmit===
Let's go!
->END


