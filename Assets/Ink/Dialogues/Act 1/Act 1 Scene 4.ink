EXTERNAL checkQuestStatus(id, steps)
EXTERNAL TurnOffBarrier(id)
EXTERNAL SwapBGM(new_id, old_id, FadeSpeed)
EXTERNAL PlayBGM(id)
EXTERNAL AddQuest(id)
EXTERNAL SubmitQuest(questId)
EXTERNAL SetNextDialogue(dialogueId)

INCLUDE ../Globals/Globals.ink


// Variable Setup
CONST DIALOGUE_1 = "A1_S4_D1"


{
    - dialogue_id == DIALOGUE_1:
        -> dialogue_1

}

// outline of main branches
===dialogue_1===
->A1_S4_D1_1

/*
SCENE 4 ✅
Listen to Oren’s story
Dream realm 4.1
*/

===A1_S4_D1_1===
//oren is somewhere off to the side of the village. If there’s a river in the forest that’s what they’re next to
//he’s surrounded by a crowd

Everyone, everyone, welcome! #speaker:oren
It is time to hear a new tale — a tale from the past, a tale of bravery and betrayal, of struggles and battles which changed the course of the future!
A tale that began with just two people, and a boat.
// crowd murmur sfx
Gather around, from near and far, for tonight’s tale is one that’s been unheard of for years!
Amelia whispers to you. #speaker:narrator
This is that Oren guy? #speaker:amelia
I see some new faces amongst the crowd. Welcome, travelers from afar! #speaker:oren
My name is Oren, and I tell tales of many kinds. If you aren’t in a rush, I recommend that you stick around~
//cutscene of him bowing or just a dialogue sprite version
—It will be worth it.
The crowd settles down as eyes train on Oren, who smiles and continues as if he’s been on a stage all his life. #speaker:narrator
Long ago, when our island was still young. #speaker:oren
A boy with a yearning heart was born in a distant land.
//simple cutscene here
His heart called for the great beyond — but his people shamed him for such a foolish wish. 
For the Great Disaster was due to strike, and none could spare a moment for lands outside their island.
One shrouded in such mystery, that it is said no creatures live or speak within.
But surely, other humans lived beyond their barriers?
As time grew so did his restlessness, until all but one embarked on a journey at his side:
His closest ally and friend — and maybe even a little more.
Oren winks, causing the crowd to huff out a laugh under their breaths. It seems like they’re quite used to such comments and content. #speaker:narrator
Together they built a ship and sailed into the endless expanse beyond what they knew; mocked by their people, they built and built until their vessel was complete. #speaker:oren
//cutscene again?
They would sail until the first land they could find.
The journey could have taken days, or weeks — but the two friends managed to find their way to our very home right here. 
As the two of them stood on the beach, their hearts went aflutter at the proof that something larger <i>did</i> lie out there. 
They met many along their journey, including our humble Viridi ancestors — and learned of our vibrant strengths and differences!
//cutscene showing the species as a mysterious silhouette
But there was an imminent issue…
The Great Disaster was due to come within a month. 
The two, without the support of their home island, venture through the archipelago to learn more about the ancient beast behind the floods.
It took several days and trials — as they had to earn the trust of others <i>and</i> learn about the kraken’s existence!
For you see — their island held no creatures, and thus were unaware of what caused the great floods of every five-hundred years.
And yet! Be it through bravery or insanity, they decided to do what they could to stop the floods!
They gathered humans, thavma, and creatures alike, and set out to track the Great Disaster in hopes to shake its will.
Will they manage to make it past a beast as strong as the primordial waves?
Will they overcome the scars which haunt them from their homeland?
And what will this egregious quest make them become?
Heroes? Villains? Or perhaps the two friends would become—
Oren, child, you are getting carried away. #speaker:silas 
A masked man interrupts the story, holding a regal air. #speaker:narrator
I believe it is time to let the children go to bed. #speaker:silas
Ah, Chief Silas. Of course. #speaker:oren
Everyone, friends — this epic must be told in two parts!
The crowd doesn’t seem thrilled, but the children in the audience do rub their eyes, and are getting ushered home by their parents. #speaker:narrator
But you need not wait for long! As tomorrow night we will continue to the end of this hero’s tale! #speaker:oren
Until then, goodnight everyone!
//fade effect, the crowd standing around us are gone, only leaving noelle and amelia together
//chione + amaya will be off to the side
The crowd disperses, leaving you and Amelia to digest the story.
(A faraway island without creatures?) #speaker:noelle
(…No way. It must be a coincidence.)
But considering how everyone on this island has treated you up until now… #speaker:narrator
They weren’t afraid of your appearance, nor did they look like the regular humans you knew back home.
The outside world was completely different from what your mother had told you.
(Ugh… I was their only daughter, and after all these years they just—) #speaker:noelle
Your thoughts are interrupted by a now-familiar voice. #speaker:narrator
Hey, what’s with that long face? #speaker:amelia
Your attention snaps to the dragon, only realising now that a frown had been plastered on your face. #speaker:narrator
Come on, you heard him. The story doesn’t continue until tomorrow. #speaker:amelia
Ah, right. I just got a little lost in thought. #speaker:noelle
Are you tired already? Come on, the night’s still young! And we still need to catch up with the Chione girl. #speaker:amelia
Noticing Amelia’s attempt to cheer you up, you huff out a laugh. #speaker:narrator
You’re pretty sprightly for a dragon who was stuck under a tree hours ago. #speaker:noelle
Amelia rolls her eyes. #speaker:narrator
The storm last night had it out of me, I swear. #speaker:amelia
But looks like it brought you to our island too, huh?
(That’s right…) #speaker:noelle
Out of all places to have landed, this isn’t bad at all. #speaker:narrator

//speak to chione to continue

Hello you two! #speaker:chione
Amaya waves. #speaker:narrator
How did you find the show? #speaker:chione
+[It was so interesting, shame it got cut short…]
    -> cutshort
+[What a strange story…]
    -> strangesto

===cutshort===
I’m so glad you enjoyed it! #speaker:chione
I always make sure I’m back in time to hear Oren’s stories, even if I was tired from rummaging in the forest!
He’s got such a special way with words.
He’s been like this since he was young. #speaker:amaya
That’s right! He was always telling stories to anyone who’d listen, until Silas officially gave him a role to channel all that energy. #speaker:chione
And after getting more popular, we started to have nights like these by the river!
His characters seem very lively, too. #speaker:noelle
He can get carried away with portraying them… Which isn’t always taken that well. #speaker:chione
Silas, he…
He’s quite strict with Oren at times. #speaker:amaya
Right! But then again, today’s <i>was</i> about the traveler from afar. #speaker:chione
It’s a topic that Silas takes very seriously, so I’m sure he just wants to make sure Oren doesn’t twist it too far from the truth. 
   -> endscenefour

===strangesto===
Haha, did his dramatics confuse you a little? #speaker:chione
Oren likes to put a lot of focus on the characters. #speaker:amaya
Right! He gets into tangents all the time about the what-ifs and motivations. #speaker:chione
But that’s what makes his stories great! They always feel like it’s about real people.
…Sometimes at the cost of the story itself. #speaker:amaya
Oh, but you have to admit he does it well. #speaker:chione
I suppose. #speaker:amaya
And everyone knows about the story about that traveler from afar. #speaker:chione
   -> endscenefour

===endscenefour===
What’s with that look on your face? #speaker:amaya
I’ve been meaning to ask… I know about the great floods and all, but I’ve never heard about the people in Oren’s story or the creature behind it. #speaker:noelle
A furrow appears on Amaya and Chione’s brows. #speaker:narrator
…I was aware that you behaved oddly, but I didn’t know it was to this extent. #speaker:amaya
Just how far away are you from?
With all three pairs of eyes on you, you decide it’s best not to lie, lest it gives them reason to rescind their hospitality. #speaker:narrator
You tell them about how you escaped your home island recently due to personal matters.
And during the storm, your ship capsized, only to wash up on the shore this morning. 
Dusk Island? #speaker:amaya
Can’t say I’ve heard of an island by that name. Then again, most of the archipelago has been washed out over the years of the Great Disaster, so many names have changed and been lost. 
We’re only familiar with the others that live on Thavmia, since other than the time period of the story Oren just told,
we haven’t really had ships that could travel far away.
Thavmia? #speaker:noelle
That’s the official name of our island! It’s named after the unique evolution of humans on our land. #speaker:chione
Chione gestures to her own features and Amaya’s. #speaker:narrator
Due to how ancient Thavmia is, our human ancestors have since adapted to the regions they lived in over many generations. #speaker:chione
We Viridi live in the forest, while other settlements go by different names.
I see… so there are other types of… uh. #speaker:noelle
Thavma. #speaker:amaya
Thavma, on the island. #speaker:noelle
Along with creatures, regular humans, and krakenfolk like you. #speaker:amelia
Amelia nudges your leg with an exasperated sigh. #speaker:narrator
You’re lucky we ran into each other this morning. What were you gonna do if you were all alone? #speaker:amelia
Despite yourself, your lips quirk into a smile. #speaker:narrator
I guess I would’ve camped out with berries until the great floods came. #speaker:noelle
Well, you don’t have to do that now. #speaker:chione
But I wonder… if your homeland is different enough to not know the Great Disaster by that name, what else is different? 
What kind of animals live there? What about the climate? 
Chione’s sudden flurry of questions take you by surprise, but the excitement in her eyes makes you attempt to grapple for a satisfying answer. #speaker:narrator
Well, we have only humans there, I haven’t actually met or talked to an animal until today. #speaker:noelle
And I guess the climate’s similar? Temperature wise and all. But our land is flatter and made up of more buildings than nature. 
(Thavmia had a mysterious impression on me when I first got here.)
(But now that these people are around me… it doesn’t feel too bad.)
Just humans? That sounds boring. #speaker:amelia
I… I’m sure it’s not all boring! #speaker:chione
Ecosystem aside, you two are spending the night here, I assume? #speaker:amaya
Ah, yes! Elder Silas mentioned that we’ll need more time before permitting access to the supplies you need. #speaker:chione
So for now, I’ve found a place for you and Amelia to spend the night!
That is, unless you’re returning to your pack. #speaker:amaya
Amelia stares at Amaya with a deadpan expression, huffing. #speaker:narrator 
No, I’m staying here. I kind of promised Noelle I’d accompany her, so… #speaker:amelia
Thank you, Amelia. #speaker:noelle
Good thing I’ve prepared two beds just in case! #speaker:chione
Just head to the empty guest house further along the river once you’re ready!
We won’t police your bedtime, so feel free to look around before you turn in for the night.
Don’t do anything strange. #speaker:amaya
+[I won’t, don’t worry.]
    -> dwamaya
+[No promises.]
    -> lolamaya

===dwamaya===
Good. #speaker:amaya
    //-> scenefoursleep
    ->DONE

===lolamaya===
…If you do, I’ll find out. #speaker:amaya
    //-> scenefoursleep
    ->DONE

// no repeat post-dialgoue
