INCLUDE ../globals.ink

- Lo lograste ~PlayerUsername!, Gracias a ti todo volverá a la normalidad #speaker:Gato #portrait:bastet_happy #layout:right

-> main

=== main ===

- Aunque no lo creas estás gemas mantienen la paz en nuestras tierras

- Fue una aventura que nunca olvidaré #speaker:~PlayerUsername #portrait:male_player_happy #layout:left

- y saber que fui de gran ayuda me pone aun mas contento

- Vayamos de regreso por donde venimos. Escuché que su barco se averió, en agradecimiento los escarabajos prepararon velas, espero que sea de gran ayuda #speaker:Gato #portrait:bastet_neutral #layout:right

- Muchos de estos problemas como el que acabamos de vivir surgen de tener enemigos sin sentido. #speaker:Gato #portrait:bastet_neutral #layout:right

- Normalmente Jugador, ¿te llevas bien con la gente #speaker:Gato #portrait:bastet_neutral #layout:right //27

+[Me llevo bien con la gente]
~q27 = 0
    Que bien es ecuchar eso #portrait:bastet_happy
+[Me peleo muchas veces]
~q27 = 1
    Es triste escuchar eso #portrait:bastet_sad
+[Me peleo siempre]
~q27 = 2
    Que triste es escuchar eso #portrait:bastet_sad
    
- Los amigos son muy importantes en estas situaciones, ¡ahora te consideramos uno!, ¿tienes amigos Jugador? #speaker:Gato #portrait:bastet_neutral #layout:right //22

+[Tengo muchos amigos]
~q22 = 0
    Que bien es ecuchar eso #portrait:bastet_happy
->DONE
+[Tengo muchos amigos pero me gustaria tener mas ]
~q22 = 1
    Eso es bueno #portrait:bastet_neutral
->DONE
+[No tengo amigos]
~q22 = 2
    Que triste es escuchar eso #portrait:bastet_sad
->DONE

->END