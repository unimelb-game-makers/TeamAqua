INCLUDE ../globals.ink
EXTERNAL SetOffDial2ndVarTrig()




/*
Noelle lights up a fire. Her parents exit from the family house, and gather around a fire, humming a birthday tune. Her grandfather hobbles in not too far behind. He takes a seat on a chair on a small mound overlooking the fire, his cane at his side. From his cloak he takes out a chalice, and passes it to Noelle’s mother. Noelle’s mother smiles at her only daughter. #for programmers 
*/
// accesses the associated ink global trigger variable, starts as empty,
// at end of main, set it to sth else, then go to other knot
VAR trig = ""
~ trig = cutscene0

a b c
{trig == "": -> main | -> Chase }

===main===
We are gathered here today to celebrate the coming of age of our dear Noelle. #speaker:Noelle's Mother //Vera

c
~ cutscene0 = "YES"
d
~ SetOffDial2ndVarTrig()
jjj
->DONE
//Noelle’s mother closes in on Noelle. Noelle stands up and starts running. #for programmer
//Her father joins in the pursuit. #for programmer
===Chase===
Monster! #speaker: Noelle's grandfather
Monster! #spaker: Noelle's mother  // repeated multiple times??? how to implement???
Monster! #speaker: Noelle's father

//Noelle runs towards the waters, her parents not far behind. Clouds start gathering in the sky and thunder rumbles as rain starts bucketing down. A storm is brewing. #for programmer
// ^all above can prob be done either manually or through tags/external function
//Noelle spots a small boat out on the docks, and runs towards it. She unties the knots binding it to the docks as her parents get closer and closer. Before they can join her on the boat and end her life, Noelle grabs an oar and starts paddling for her life. #for programmer

That’s right, you better run! #speaker:Noelle's Mother 
Run and run and run, and never come back! #speaker:Noelle's Mother

//Noelle keeps on paddling, further and further out, until her parents are specks in the distance. She keeps on going until not even her own island, her home is visible from the distance. #for programmer
//The storms intensifies, and Noelle’s boat starts spinning out of control. A number of shadows move underneath, but before Noelle can get a better look, waves crash in from all over, and overwhelm her and her boat. #for programmer
//Everything goes dark. #for programmer
-> DONE


/*Next in line to the Tempest family name, #speaker:Noelle's Mother
the finest of any family here on this little island. #speaker:Noelle's Mother
A beacon of hope against any and all creatures that lurk in the shadow. #speaker:Noelle's Mother

//Noelle’s mother hands her the chalice. #for programmers

Now Noelle, are you ready to make all of us proud? #speaker:Noelle's Mother

//Noelle stares at the cup, then at her grandfather, who stares intently at her, awaiting her decision. Noelle nods slowly. Noelle’s parents clap eagerly. # for programmers

Excellent! #speaker:Noelle's Mother
Well what are you waiting for then? Drink up! Awaken your inner potential! #speaker:Noelle's Mother

//Noelle stares back at her chalice, takes a deep breath, and takes a big gulp. Noelle starts choking and falls to the ground, clutching her heart. #for programmers

Did someone poison the awakening potion?! #speaker:Noelle's Mother

//Noelle’s mother turns to her father, a cold expression on her face. #for programmers   
// -----this part is going to need some thoughts on how it can be implemented

You made it, my dear. Are you seriously trying to destroy 
the last of the Tempest legacy? #speaker: Noelle's Mother
No no, of course not! I followed the recipe exactly!  #speaker: Noelle's Father //Theodore
It shouldn’t be affecting our daughter like this!

//Noelle begins to scream, as she starts to transform. Tentacles sprout all over her face, and her eyes turn to a ruby red colour. #for programmer
//Noelle’s parents stare at her, in disbelief. Noelle’s grandfather stands up, and stares daggers at Noelle. He points a gnarled withered bony finger at her. #for programmer

Monster! #speaker:Noelle's grandfather      // Amrbose

No, no, I can't be— #speaker:Noelle
And yet you clearly are, Noelle. #speaker:Noelle's Mother
Well this is great, just great. Our only heir, a monster! #speaker:Noelle's Mother

//Noelle’s mother stares daggers at Noelle’s father. #for programmer

You didn’t tell me there was a monster on your side of the family. #speaker:Noelle's Mother
No that’s presposterous. That can’t be possible. #speaker:Noelle's Father
At this point anything is. #speaker:Noelle's Mother

//Turns to Noelle. #for programmer.

Now listen here, this is what happened. #speaker:Noelle's Mother

//Noelle's Mother takes out a knife. #for programmer

Our darling Noelle was moments away from completing her coming of age ritual #speaker:Noelle's Mother
when a vicious creature attacked her out of nowhere. 
The creature was ultimately slain by yours truly, 
but at the tragic cost of our dear daughter. 
How terrible this day is, for the Tempest family to be robbed of its only heir, 
by a creature no less.
You can’t do this! I’m your daughter! #speaker:Noelle
Not anymore, not after what you have become. #speaker:Noelle's Mother
But what about the family? I’m your only daughter, you have no other heirs. #speaker:Noelle
I’m sure we can make one up. #speaker:Noelle's Mother
How's this? A long lost child who miraculously has Tempest blood. #speaker:Noelle's Mother
Oh it will be a bit of a stretch, #speaker:Noelle's Mother
but not as much as a creature becoming the next head of the Tempest family. #speaker:Noelle's Mother
Oh to think, all the time and resources we wasted, #speaker:Noelle's Mother
into a creature of all things! #speaker:Noelle's Mother 
You really had to go and ruin this for everyone, didn’t you? #speaker:Noelle's Mother
Goodbye Noelle, and may the gods have mercy on your soul. #speaker:Noelle's Mother
//end of noraml dialogue, start chase sequence*/

