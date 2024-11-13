INCLUDE globals.ink

{ mat == "": -> main | -> alreadychosen}
//if variable mat is empty, go to knot main, else go to knot alreadychosen

=== main ===
this a test!  #speaker: tester 
rahhhh
Do you take wood or water?

    + [WOOD]
        -> chosen("wood")
    + [WATER]
        -> chosen("water")
        
=== chosen(material) ===
~mat = material
You chose {mat}
-> DONE

==alreadychosen===
you already chose {mat}
-> END