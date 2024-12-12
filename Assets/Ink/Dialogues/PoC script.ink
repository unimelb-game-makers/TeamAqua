

//ideally, use just local variables but it seems easier to work with global variables
EXTERNAL checkQuestStatus(id, steps)     
//this checks the completion status of quest




INCLUDE globals.ink
//INCLUDE PoC post-quest.ink
VAR questSteps = ""         // <-- //delcaring the local var ends up reseting whatever change we did make to it at the start, hence justifies the need to declare a global variable
~ questSteps = quest_id1


//SCENE X: CRASHLANDING ON NOON ISLAND
//Comment1: as of rright now, we're using tags to let unity know when to display which portrait sprite, who is speak//etc and also when a choice is a quest-giving one
//Comment2: line breaks indicate that line of dialogue only loads when player clicks 'E' to continue
//in this script, im doing a line break every time a sentence ends or when it becomes too long, but for later scripts, //feel free to decide.

~checkQuestStatus(1, 1)
~ questSteps = quest_id1
current quest step is {questSteps} and current quest_id var is {quest_id1}
//conditional check, if var quest is empty, load main dialogue, if quest var < 10 (fishes), go to incomplete quest, else, go to submit quest
{ 
    - questSteps == "":     // if empty, go to main
        -> main 
    
    - questSteps == "NO":   //================================ failed here ==========
        quest step is {questSteps} and current quest_id var is {quest_id1}
        //~checkQuestStatus(1, 1)
        -> IncompleteQuest
    - questSteps == "YES":
        //~checkQuestStatus(1, 1)
        -> SubmitQuest 
}
        
===main===
<color=\#3A6DE3>colored text</color> normal <color=\#9EED8A><i><b>everything text</b></i>text</color> #speaker:Narrator #audio:2beep

<color=green>colored text</color> normal <color=\#9EED8A><i><b>everything text</b></i>text</color>
<color=red>colored text</color> normal <color=blue><i><b>everything text</b></i>text</color>
You find yourself on a beach. #speaker:Narrator  #audio:2beep
The sun glares over you, and your ship is in pieces around you. #audio:2beep
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
Hey! What are you doing just standing there? Get me out of here! #speaker:Amelia #portrait:AmeliaSad #audio:animal
What do you do? Do you help the creature? #speaker:Narrator #audio:2beep
+ [NO]
    ->choiceNO

+ [YES]
    ->choiceYES

 ===choiceNO===     //if Player does not help Amelia --> loops over and over again (fourth cycle gives slightly different dialogue)
You decide it’s probably best not to draw attention to yourself. #speaker:Narrator #audio:2beep
Besides, someone else might be able to help them out.
You take a step in the opposite direction, which catches the attention of the creature in need.
-> repeatNO0

===repeatNO0===
Hey! #speaker:Amelia #portrait:AmeliaHappy #audio:animal
Are you seriously going to ignore someone in peril? 
Get back here!
+ [NO]
    -> repeatNO1
+ [YES]
    -> choiceYES

===repeatNO1===
Hey! #speaker:Amelia #portrait:AmeliaSad #audio:animal
Are you seriously going to ignore someone in peril?
Get back here!
+ [NO]
    -> repeatNO2
+ [YES]
    -> choiceYES
    
===repeatNO2===
Hey! #speaker:Amelia #portrait:AmeliaHappy #audio:animal
Are you seriously going to ignore someone in peril?
Get back here!
+ [NO]
    -> repeatNO3
+ [YES]
    -> choiceYES
    
===repeatNO3===
... #speaker:Amelia #portrait:AmeliaSad #audio:animal
For real? 
You can't just ignore me!
Come back please! 
+ [NO]
    -> repeatNO0
+ [YES]
    -> choiceYES


===choiceYES=== //if player chooses to help Amelia
It’s what your gut is telling you, and it’s high time you started listening to your gut. #speaker:Narrator #audio:2beep
You rush in and with a surge of power that feels like it came out of nowhere, you move the tree off the creature. 
As it crashes to the ground next to you, the creature stands up slowly, face tensing in pain.

That’s more like it. Thought for sure I would be a goner there. #speaker:Amelia #audio:animal

Are you hurt? #speaker:Noelle #audio:default


I’m a little bruised, but I would be a lot worse for wear if it wasn’t for you. #speaker:Amelia #audio:animal
So thanks.  
Amelia looks up at you, and recoils in horror. #speaker:Narrator #audio:2beep

Hey, wait a minute? Are you a - #speaker:Amelia #audio:animal
human!
Amelia jumps back, and assumes a fighting stance, arms raised, ready to fight for her life. #speaker:Narrator #audio:2beep
Stay back! #speaker:Amelia #audio:animal
Wait! I’m not going to hurt you. #speaker:Noelle #audio:default 
//////============================BELOW, AUDIO TAG ISNT UPDATED YET===============================================

Ha! #speaker:Amelia #audio:2beep                  //<- test
Likely story. You humans are all the same. 
Can’t believe a lousy human was the one that came to my rescue. 
I must have the worst luck ever.
But I’m not human! #speaker:Noelle 
Lies! #speaker:Amelia 
Clearly you’re just trying to make me lower my guard down, 
and then when I have my back turned, 
bam! Insta kill. 
No way are you gonna pull the wool over my eyes. Now go! Back to wherever you came from!
But I can’t go back. #speaker:Noelle
Why not? You got here on a boat didn’t you? Just sail back. #speaker:Amelia
I can’t. The boat’s wrecked. And, well… #speaker:Noelle
Well what? Cat’s got your tongue? #speaker:Amelia
The words get caught up in your throat as you think back to the series of events that forced your way out of the nest not even 24 hours ago. #speaker:Narrator
Would a stranger you only just met understand, let alone care? Where would you even begin?
->choices2



===choices2===
+ [“it's a long story”]
It’s a long story #speaker:Noelle
Well I don’t have all day for you to regale me with your life story.#speaker:Amelia
But you still can’t stay here.
You’re a human, you’re not gonna last long out here alone.

-> contfromchoice2


+[I just can't, okay?]
I just can’t, okay? #speaker:Noelle
Fine, don’t tell me. #speaker:Amelia
But you still can’t stay here. 
You’re a human, you’re not gonna last long out here alone.
-> contfromchoice2

===contfromchoice2===
That’s the thing... #speaker:Noelle
You remove your scarf, revealing the rest of your face, and the tentacles that recently sprouted from around your mouth. #speaker:Narrator
I’m not a human. #speaker:Noelle
Huh. Well what are you doing, covering up like that? #speaker:Amelia
Are you on the run from humans or something? 
Something like that... #speaker:Noelle
Well whatever your reasons are, it’s really easy to mistake you for a human in that get-up. #speaker:Amelia
If I were you I would ditch the scarf.
I’ll take my chances.
Whatever. #speaker:Amelia
Amelia starts walking off towards the forest. #speaker:Narrator
Thanks again for saving me. #speaker:Amelia
Wait. Do you know where I can find another ship? #speaker:Noelle
Not really. I know where you might be able to get more stuff to build a brand new one, but well... #speaker:Amelia
Well what? #speaker:Noelle
They’re all deeper in-land. #speaker:Amelia
And deeper in-land is pretty dangerous.
Especially for a human.
Just point me in the right direction then.#speaker:Noelle
Hang on, you can’t go out there on your own! #speaker:Amelia
Have you even ventured out there before? 
A greenhorn like you would surely die on your own.
So you’re saying I should form a party then? #speaker:Noelle
Isn’t that obvious? #speaker:Amelia
Then join me. Help me build a ship out of here. #speaker:Noelle
What?! No no no - I only just met you, #speaker:Amelia
and you may not be a human, but you definitely reek of one!
Uh uh, no way!
Come on, please? #speaker:Noelle
You said so yourself I would most likely die on my own, and I don’t see anyone else around here. 
That’s because most creatures are further in-land, and there’s absolutely no humans at all! #speaker:Amelia
Except for the ones living in the treetops I guess...
So... does that mean you’ll come along with me? #speaker:Noelle
I didn’t say that! #speaker:Amelia
Look, I’m grateful you saved me from that tree and all, and I do owe you for that, but it’s a bit much to just ask a random stranger to tag along with you. 
Take care, and try not to die, okay?
Amelia turns her back on Noelle, and starts walking off in the direction of the rainforests. #speaker:Narrator

Before Amelia can leave however, her stomach rumbles.
When was the last time you ate? #speaker:Noelle
<i>sheepishly</i> It’s been a little while. #speaker:Amelia  
//dev note might be able to add some sfx here for that sheepishly line? need more experimenting
Hard to find food on this island when everything is trying to kill you.
Would be pretty convenient if someone were to find some food for you. #speaker:Noelle
Would save you a lot of effort of doing it yourself. #speaker:Amelia

<b><i>sighs</i></b> Fine. #speaker:Amelia
//same devnote 
~ temp examGrade = FLOAT(RANDOM(50, 100)) / 100         //testing
Catch me some fish.
VAR fish = 10
VAR remainingFish = 0//remaining var should actually be 0, currently set to 1 for testing purposes, this var will be updated in code, likely in MoveKnots()
{fish} of them, and maybe I will consider joining your party.
You look around for something to help you catch some fish. #speaker:Narrator
In the wreckage of your old ship you find an old fishing rod.
It’s nothing fancy, but it will do the trick.
*/
VAR id = 1
You find a spot on the beach where there are dark shapes of various sizes slowly moving about. 
You cast the fishing rod into the waters several times, hoping to catch the fish you need for your potential party member.    #questS:1
~ quest_id1 = "NO"
current quest step is {questSteps}
POC quest has been added by the previous line of dialogue (id1)
Below is the usual choice-based quest giver (id2)
+[Catch the fishes #quest:2 #done]
    ->DONE      //first chunk of dialogue ends here
+[Nah thanks #done]
    ->END     //first chunk of dialogue ends here
//the hashtag done is to exit dialogue mode upon click since DONE or END leads u to an empty dialogue box and then takes 1 more click to acutally exit

//IF YOU TALK TO AMELIA BEFORE YOU GET THE 10 REQUIRED FISH:

//if var quest <10 go here
===IncompleteQuest===
//~checkQuestStatus(1, 1)
this line of dialogue should play when player interacts with Amelia before completeing the quest. #speaker:silly dev
I still need 10 more fish. #speaker:Amelia
You better hurry before I change my mind. 
//~ quest_id1 = "YES"
->DONE

//=====================================After quest completion===========================================    
//if var quest >= 10 go here
===SubmitQuest===
//~checkQuestStatus(1, 1)
Would you like to finish this quest? #speaker:Narrator
text
clicking on yes should remove quest with id 1 while no should do nothing
    +[Finish quest? #finish:{id}] -> CompleteQuest
    +[Not yet #done]
    -> DONE
    

===CompleteQuest=== 
this line of dialogue should play when player interacts with Amelia after completeing the quest. #speaker:silly dev
After a few tries, you get the 10 fish you need. #speaker:Narrator
You return back to Amelia, who’s sitting on the sand next to the wreckage of your ship.
Please tell me you caught some salmon. #speaker:Amelia
I think so? #speaker:Noelle
The fish are a lot different around here compared to home
Huh. And where is your home? #speaker:Amelia
Dusk Island. #speaker:Noelle
It was until recently anyway.
Sorry to hear about that. #speaker:Amelia
Is that why you were on the ship when the storms <i>stomach rumbles again</i> //add sound effects here
While I would love to keep chatting, gimme the fish, quickly!
You hand over the fish, and Amelia hastily gobbles it all down, bones, scales and all. #speaker:Narrator
You must have been pretty hungry... #speaker:Noelle
That hit the spot! Anyway, see ya— #speaker:Amelia
Wait! I thought you said— #speaker:Noelle
I know what I said. #speaker:Amelia
But the reality is I still barely know you. 
And if you can catch that many fish in a short span of time, I’m sure you’ll be able to survive fine on your own. 
And I don’t really travel in groups anyway.
But we had an agreement— #speaker:Noelle
The /*add sfx here*/ haunting roar of a monstrous beast echoes throughout the island. Whatever you had to say is forgotten in the moment. 
Rain begins to fall on the island. 
Amelia looks up in horror.
The Great Disaster... #speaker:Amelia
What— #speaker:Noelle
How long do you think it will take for us to build a new ship and get off of this island? #speaker:Amelia
I dunno - I haven’t really built a ship on my own before and— #speaker:Noelle
However long you think we need, cut that time in half. #speaker:Amelia
We need to hurry.
We? #speaker:Noelle
Yep. #speaker:Amelia
Change of plans, I’m coming with you.
But why? #speaker:Noelle
Isn’t that obvious? #speaker:Amelia
To outrun the floods. 
Now come on, we need to go.
Amelia takes Noelle’s hand, and drags her towards the rainforests deeper in-land. #speaker:Narrator
->DONE



/*

*/