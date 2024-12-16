INCLUDE ../globals.ink
EXTERNAL gotRudder()

Hola de nuevo ~PlayerUsername! Muchos de estos problemas como el que acabamos de vivir surgen de tener enemigos sin sentido. #speaker:Furry #portrait:furry_sad #layout:right

-> main

=== main ===

- Normalmente ~PlayerUsername, ¿cómo te llevas con la gente? #speaker:Furry #portrait:furry_neutral #layout:right
//pregunta 11

+[Me llevo bien con la gente]
    ~ q11 = 0
    Me alegra escuchar eso #portrait:furry_happy
+[Me peleo muchas veces]
    ~ q11 = 1
    Oh no puede ser! #portrait:furry_sad
+[Me peleo siempre]
    ~ q11 = 2
    Es triste escuchar eso #portrait:furry_sad

- Los amigos son muy importantes en estas situaciones ¿tienes amigos ~PlayerUsername? #portrait:furry_neutral //22 

+[Tengo muchos amigos]
    ~ q22 = 0
    Me alegra demasiado escuchar eso #portrait:furry_happy
+[Tengo muchos amigos pero me gustaria tener mas]
    ~ q22 = 1
    Es bueno escuchar eso #portrait:furry_happy
+[No tengo amigos]
    ~ q22 = 2
    Es triste escuchar eso #portrait:furry_sad

- ¡Ahora todo el bosque te consideramos un gran amigo! #portrait:furry_happy

- Escuché que su barco se averió, en agradecimiento los habitantes del bosque prepararon velas, espero que sea de gran ayuda. #speaker:Furry #portrait:furry_happy #layout:right

- Dirigite al portal al final del puente, el capitán te espera!.

~ gotRudder()

-> END