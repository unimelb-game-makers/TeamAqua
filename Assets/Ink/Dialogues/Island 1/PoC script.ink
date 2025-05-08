

//ideally, use just local variables but it seems easier to work with global variables
EXTERNAL checkQuestStatus(id, steps)

//this checks the completion status of quest



//chore: add portrait tags, merge audio sfx into speaker tag



INCLUDE Global var storage/globals.ink
//INCLUDE PoC post-quest.ink
VAR questSteps = ""         // <-- //delcaring the local var ends up reseting whatever change we did make to it at the start, hence justifies the need to declare a global variable
~ questSteps = quest_id1



~checkQuestStatus(1, 1)
~ questSteps = quest_id1
//current quest step is {questSteps} and current quest_id var is {quest_id1}
//conditional check, if var quest is empty, load main dialogue, if quest var < 10 (berries), go to incomplete quest, else, go to submit quest
{ 
    - questSteps == "":     // if empty, go to main
        -> main 
    - questSteps == "NOT_ACCEPTED":
        -> TakeQuest
    - questSteps == "NOT_FINISHED":   //================================ failed here ==========
        //quest step is {questSteps} and current quest_id var is {quest_id1}
        //~checkQuestStatus(1, 1)
        -> IncompleteQuest
    - questSteps == "FINISHED":
        //~checkQuestStatus(1, 1)
        -> SubmitQuest 
}
        
===main===
//<color=\#3A6DE3>colored text</color> normal <color=\#9EED8A><i><b>everything text</b></i>text</color> #speaker:Narrator 

//<color=green>colored text</color> normal <color=\#9EED8A><i><b>everything text</b></i>text</color>
//<color=red>colored text</color> normal <color=blue><i><b>everything text</b></i>text</color>
You find yourself on a beach. #speaker:Narrator 
The sun glares over you, and your ship is in pieces around you. 
/*
In stark contrast to the raging waters that overwhelmed you and your ship last night,
calm waves now wash over shore. 
The storms of yesterday seem to have died down for now. 
In front of you are large tropical trees that seem to stretch on forever.
One tree seems to have fallen down, and underneath it...
... a creature!
The creature has spotted you and they cry out in pain.
->Amelia_encounter

===Amelia_encounter===
Hey! What are you doing just standing there? Get me out of here! #speaker:Amelia #portrait:AmeliaAngry
What do you do? Do you help the creature? #speaker:Narrator 
+ [NO]
    ->choiceNO

+ [YES]
    ->choiceYES

 ===choiceNO===     //if Player does not help Amelia --> loops over and over again (fourth cycle gives slightly different dialogue)
You decide it’s probably best not to draw attention to yourself. #speaker:Narrator 
Besides, someone else might be able to help them out.
You take a step in the opposite direction, which catches the attention of the creature in need.
-> repeatNO0

===repeatNO0===
Hey! #speaker:Amelia #portrait:AmeliaNeutral 
Are you seriously going to ignore someone in peril? 
Get back here!
+ [NO]
    -> repeatNO1
+ [YES]
    -> choiceYES

===repeatNO1===
Hey! #speaker:Amelia #portrait:AmeliaAngry
Are you seriously going to ignore someone in peril?
Get back here!
+ [NO]
    -> repeatNO2
+ [YES]
    -> choiceYES
    
===repeatNO2===
Hey! #speaker:Amelia #portrait:AmeliaJoyful 
Are you seriously going to ignore someone in peril?
Get back here!
+ [NO]
    -> repeatNO3
+ [YES]
    -> choiceYES
    
===repeatNO3===
... #speaker:Amelia #portrait:AmeliaSilly
For real? 
You can't just ignore me!
Come back please! 
+ [NO]
    -> repeatNO0
+ [YES]
    -> choiceYES


===choiceYES=== //if player chooses to help Amelia
It’s what your gut is telling you, and it’s high time you started listening to your gut. #speaker:Narrator 
You rush in and with a surge of power that feels like it came out of nowhere, you move the tree off the creature. 
As it crashes to the ground next to you, the creature stands up slowly, face tensing in pain.

That’s more like it. Thought for sure I would be a goner there. #speaker:Amelia #portrait:AmeliaSilly

Are you hurt? #speaker:Noelle #portrait:NoelleShock


I’m a little bruised, but I would be a lot worse for wear if it wasn’t for you. #speaker:Amelia #portrait:AmeliaJoyful
So thanks.  
Amelia looks up at you, and recoils in horror. #speaker:Narrator 

Hey, wait a minute? Are you a - #speaker:Amelia #portrait:AmeliaNeutral
human!
Amelia jumps back, and assumes a fighting stance, arms raised, ready to fight for her life. #speaker:Narrator 
Stay back! #speaker:Amelia #portrait:AmeliaAngry
Wait! I’m not going to hurt you. #speaker:Noelle #portrait:NoelleSad 
//////============================BELOW, AUDIO TAG ISNT UPDATED YET===============================================

Ha! #speaker:Amelia #portrait:AmeliaAngry               //<- test
Likely story. You humans are all the same. 
Can’t believe a lousy human was the one that came to my rescue. 
I must have the worst luck ever.
But I’m not human! #speaker:Noelle #portrait:NoelleAngry
Lies! #speaker:Amelia 
Clearly you’re just trying to make me lower my guard down, 
and then when I have my back turned, 
bam! Insta kill. 
No way are you gonna pull the wool over my eyes. Now go! Back to wherever you came from!
But I can’t go back. #speaker:Noelle #portrait:NoelleSad
Why not? You got here on a boat didn’t you? Just sail back. #speaker:Amelia #portrait:AmeliaNeutral
I can’t. The boat’s wrecked. And, well… #speaker:Noelle #portrait:NoelleDispleased
Well what? Cat’s got your tongue? #speaker:Amelia #portrait:AmeliaAngry
The words get caught up in your throat as you think back to the series of events that forced your way out of the nest not even 24 hours ago. #speaker:Narrator
Would a stranger you only just met understand, let alone care? Where would you even begin?
->choices2



===choices2===
+ [“it's a long story”]
It’s a long story #speaker:Noelle #portrait:NoelleDistant
Well I don’t have all day for you to regale me with your life story.#speaker:Amelia #portrait:AmeliaNeutral
But you still can’t stay here.
You’re a human, you’re not gonna last long out here alone.

-> contfromchoice2


+[I just can't, okay?]
I just can’t, okay? #speaker:Noelle #portrait:NoelleSad
Fine, don’t tell me. #speaker:Amelia #portrait:AmeliaNeutral
But you still can’t stay here. 
You’re a human, you’re not gonna last long out here alone.
-> contfromchoice2

===contfromchoice2===
That’s the thing... #speaker:Noelle #portrait:NoelleSad
You remove your scarf, revealing the rest of your face, and the tentacles that recently sprouted from around your mouth. #speaker:Narrator
I’m not a human. #speaker:Noelle #portrait:NoelleDistant
Huh. Well what are you doing, covering up like that? #speaker:Amelia #portrait:AmeliaNeutral
Are you on the run from humans or something? 
Something like that... #speaker:Noelle #portrait:NoelleDispleased
Well whatever your reasons are, it’s really easy to mistake you for a human in that get-up. #speaker:Amelia #portrait:AmeliaNeutral
If I were you I would ditch the scarf.
I’ll take my chances.
Whatever. #speaker:Amelia #portrait:AmeliaNeutral
Amelia starts walking off towards the forest. #speaker:Narrator
Thanks again for saving me. #speaker:Amelia #portrait:AmeliaJoyful
Wait. Do you know where I can find another ship? #speaker:Noelle #portrait:NoelleConfused
Not really. I know where you might be able to get more stuff to build a brand new one, but well... #speaker:Amelia #portrait:AmeliaNeutral
Well what? #speaker:Noelle #portrait:NoelleConfused
They’re all deeper in-land. #speaker:Amelia #portrait:AmeliaNeutral
And deeper in-land is pretty dangerous.
Especially for a human.
Just point me in the right direction then.#speaker:Noelle #portrait:NoellePleased
Hang on, you can’t go out there on your own! #speaker:Amelia #portrait:AmeliaAngry
Have you even ventured out there before? 
A greenhorn like you would surely die on your own.
So you’re saying I should form a party then? #speaker:Noelle #portrait:NoelleConfused
Isn’t that obvious? #speaker:Amelia #portrait:AmeliaNeutral
Then join me. Help me build a ship out of here. #speaker:Noelle #portrait:NoellePleased
What?! No no no - I only just met you, #speaker:Amelia #portrait:AmeliaSilly
and you may not be a human, but you definitely reek of one!
Uh uh, no way!
Come on, please? #speaker:Noelle #portrait:NoellePleased
You said so yourself I would most likely die on my own, and I don’t see anyone else around here. 
That’s because most creatures are further in-land, and there’s absolutely no humans at all! #speaker:Amelia #portrait:AmeliaAngry
Except for the ones living in the treetops I guess...
So... does that mean you’ll come along with me? #speaker:Noelle #portrait:NoellePleased
I didn’t say that! #speaker:Amelia #portrait:AmeliaAngry
Look, I’m grateful you saved me from that tree and all, and I do owe you for that, but it’s a bit much to just ask a random stranger to tag along with you. 
Take care, and try not to die, okay?
Amelia turns her back on Noelle and starts walking off in the direction of the rainforests. #speaker:Narrator
Before Amelia can leave however, her stomach rumbles.
When was the last time you ate? #speaker:Noelle #portrait:NoelleConfused
<i>sheepishly</i> It’s been a little while. #speaker:Amelia  #portrait:AmeliaSilly
//dev note might be able to add some sfx here for that sheepishly line? need more experimenting
Hard to find food on this island when everything is trying to kill you.
Would be pretty convenient if someone were to find some food for you. #speaker:Noelle #portrait:NoellePleased
Would save you a lot of effort of doing it yourself. #speaker:Amelia #portrait:AmeliaNeutral

<b><i>sighs</i></b> Fine. #speaker:Amelia #portrait:AmeliaAngry
//~ temp examGrade = FLOAT(RANDOM(50, 100)) / 100         //testing
Find me some berries.
*/
10 of them, and maybe I will consider joining your party.
You look around for some bushes 
VAR id = 1
You find an area nearby the beach where shrubs of lush, golden berries shine invitingly. You reach out to pick some fresh, juicy ones for your potential party member.    
->TakeQuest

===TakeQuest===
+[Find berries #quest:1 #done]
~ quest_id1 = "NOT_FINISHED"
    ->DONE  
+[Nah thanks #done]
~ quest_id1 = "NOT_ACCEPTED"
    ->END     
//the hashtag done is to exit dialogue mode upon click since DONE or END leads u to an empty dialogue box and then takes 1 more click to acutally exit


//IF YOU TALK TO AMELIA BEFORE YOU GET THE 10 REQUIRED berries:

//if var quest <10 go here
===IncompleteQuest===
//~checkQuestStatus(1, 1)
I still need more berries. #speaker:Amelia #portrait:AmeliaJoyous
You better hurry before I change my mind. 
->DONE

//=====================================After quest completion===========================================    
===SubmitQuest===
//~checkQuestStatus(1, 1)
Would you like to finish this quest? #speaker:Narrator
    +[Finish quest? #finish:1] -> CompleteQuest
    +[Not yet #done]
    -> DONE
    

===CompleteQuest=== 
After a few tries, you get the 10 berries you need. #speaker:Narrator
You return back to Amelia, who’s sitting on the sand next to the wreckage of your ship.
Please tell me you found berries. #speaker:Amelia #portrait:AmeliaJoyous
I think so? #speaker:Noelle #portrait:NoelleConfused
The berries are a lot different around here compared to home
Huh. And where is your home? #speaker:Amelia #portrait:AmeliaNeutral
Dusk Island. #speaker:Noelle #portrait:Noelle
It was until recently anyway.
Sorry to hear about that. #speaker:Amelia #portrait:AmeliaNeutral
Is that why you were on the ship when the storms <i>stomach rumbles again</i> //add sound effects here
While I would love to keep chatting, gimme the berries, quickly!
You hand over the berries, and Amelia hastily gobbles it all down, bones, scales and all. #speaker:Narrator
You must have been pretty hungry... #speaker:Noelle #portrait:NoellePleased
That hit the spot! Anyway, see ya— #speaker:Amelia #portrait:AmeliaNeutral
Wait! I thought you said— #speaker:Noelle #portrait:NoelleShock
I know what I said. #speaker:Amelia #portrait:AmeliaNeutral
But the reality is I still barely know you. 
And if you can catch that many berries in a short span of time, I’m sure you’ll be able to survive fine on your own. 
And I don’t really travel in groups anyway.
But we had an agreement— #speaker:Noelle #portrait:NoelleDispleased
The /*add sfx here*/ haunting roar of a monstrous beast echoes throughout the island. Whatever you had to say is forgotten in the moment. 
Rain begins to fall on the island. 
Amelia looks up in horror.
The Great Disaster... #speaker:Amelia #portrait:AmeliaAngry
What— #speaker:Noelle #portrait:NoelleConfused
How long do you think it will take for us to build a new ship and get off of this island? #speaker:Amelia #portrait:AmeliaAngry
I dunno - I haven’t really built a ship on my own before and— #speaker:Noelle #portrait:Noelle
However long you think we need, cut that time in half. #speaker:Amelia #portrait:AmeliaNeutral
We need to hurry.
We? #speaker:Noelle #portrait:Noelle
Yep. #speaker:Amelia #portrait:AmeliaJoyous
Change of plans, I’m coming with you.
But why? #speaker:Noelle #portrait:NoelleConfused
Isn’t that obvious? #speaker:Amelia #portrait:AmeliaSilly
To outrun the floods. 
Now come on, we need to go.
Amelia takes Noelle’s hand, and drags her towards the rainforests deeper in-land. #speaker:Narrator
->DONE
