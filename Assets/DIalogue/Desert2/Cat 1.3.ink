INCLUDE ../globals.ink
EXTERNAL gotRudder()

Lo lograste ~PlayerUsername!, Gracias a ti todo volverá a la normalidad #speaker:Gato #portrait:bastet_happy #layout:right

-> main

=== main ===

- Aunque no lo creas estás gemas mantienen la paz en nuestras tierras

- Fue una aventura que nunca olvidaré #speaker:~PlayerUsername #portrait:player_happy #layout:left

- Muchos de estos problemas como el que acabamos de vivir surgen de tener enemigos sin sentido. #speaker:Gato #portrait:bastet_neutral #layout:right

+[Me llevo bien con la gente]
    ~q27 = 0
    Que bien es ecuchar eso #portrait:bastet_happy
+[Me peleo muchas veces]
    ~q27 = 1
    Es triste escuchar eso #portrait:bastet_sad
+[Me peleo siempre]
    ~q27 = 2
    Que triste es escuchar eso #portrait:bastet_sad
    
- Los amigos son muy importantes en estas situaciones, ¡ahora te consideramos uno!, ¿tienes amigos ~PlayerUsername? #speaker:Gato #portrait:bastet_neutral #layout:right //22

+[Tengo muchos amigos]
    ~q22 = 0
    Que bien es ecuchar eso #portrait:bastet_happy
+[Tengo muchos amigos pero me gustaria tener mas ]
    ~q22 = 1
    Eso es bueno #portrait:bastet_neutral
+[No tengo amigos]
    ~q22 = 2
    Que triste es escuchar eso #portrait:bastet_sad

- ¡Ahora todo el desierto te consideramos un gran amigo! #portrait:bastet_happy

- Escuché que su barco se averió, en agradecimiento los habitantes del desierto prepararon velas, espero que sea de gran ayuda. #portrait:bastet_happy

- Dirigite al portal al final del puente, el capitán te espera!.

~ gotRudder()

->END