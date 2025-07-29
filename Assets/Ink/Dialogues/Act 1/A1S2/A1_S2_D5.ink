===A1_S2_D5===
Upon seeing the ruin, Amelia gets frustrated. This place appears to be a dead end. #speaker:Narrator
Ugh, this thing again...  Looks like we’ll need to go around it. #speaker:Amelia #portrait:AmeliaHostile
+[Is it a puzzle?]
    ->quest
+[Let me take a look.]
    ->quest
    
// take quest here
===quest===
You? Solving these ancient ruins? Do you know how heavy those blocks are? #speaker:Amelia #portrait:AmeliaHostile
…Knock yourself out, I suppose. #portrait:AmeliaNormal
//puzzle tutorial appears here. Player gets to solve it. After which dialogue continues
EVENT:AddQuest:A1_S2_Q1
->DONE