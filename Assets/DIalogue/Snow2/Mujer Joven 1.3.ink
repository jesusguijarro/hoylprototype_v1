INCLUDE ../globals.ink


- Muchos de estos problemas como el que acabamos de vivir surgen de tener enemigos sin sentido #speaker:Youngwoman #portrait:youngwoman_neutral #layout:right

-> main

=== main ===

- Normalmente Jugador, ¿te llevas bien con la gente? #speaker:Youngwoman #portrait:youngwoman_neutral #layout:right //27

+[Me llevo bien con la gente]
~q27 = 0
    Que alegria me da escuchar eso #portrait:youngwoman_happy
+[Me peleo muchas veces]
~q27 = 1
    Eso puedes ser normal #portrait:youngwoman_neutral
+[Me peleo siempre]
~q27 = 2
    Es triste cuando suele pasar eso #portrait:youngwoman_sad

- Los amigos son muy importantes en estas situaciones, ¡ahora te consideramos uno!, ¿tienes amigos Jugador? #speaker:Youngwoman #portrait:youngwoman_neutral #layout:right//22

+[Tengo muchos amigos]
~q22 = 0
-> DONE
    Que alegria me da escuchar eso #portrait:youngwoman_happy 
+[Tengo muchos amigos pero me gustaría tener más]
~q22 = 1
-> DONE
    Es bueno oir eso #portrait:youngwoman_neutral
+[No tengo amigos]
~q22 = 2
-> DONE
    Es triste escuchar eso #portrait:youngwoman_happy


->END