Bonjour ! #speaker:npc #portrait:npc_base
-> main

=== main ===
How do u feel today?
+ [Am happy]
    Awesome bro #portrait:npc_happy
+ [need sleeb] #portrait:npc_sad
    same bro

- I hate calc2 #speaker:silly dev #portrait:silly_dev_base #layout:right
do u take da quest? #speaker:npc #portrait:npc_base #layout:left
+ [Yes, i accept the quest]
    -> main
+ [Nah, too busy]
    see u then.
    -> END