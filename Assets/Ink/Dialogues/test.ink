-> main

=== main ===
this a test!  #speaker: tester
rahhhh
Do you take the quest

    + [CHOICE 0]
        -> chosen("refused.")
    + [CHOICE 1]
        -> chosen("accepted!")
        
=== chosen(quest) ===
You {quest}
-> END