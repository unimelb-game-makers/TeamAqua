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