INCLUDE ../globals.ink
EXTERNAL gotRudder()

- Muchos de estos problemas como el que acabamos de vivir surgen de tener enemigos sin sentido #speaker:Joven #portrait:youngwoman_neutral #layout:right

-> main

=== main ===

- Normalmente ~PlayerUsername, ¿cómo te llevas con la gente? #speaker:Joven #portrait:youngwoman_neutral #layout:right //27

+[Me llevo bien con la gente]
    ~q27 = 0
    Que alegria me da escuchar eso #portrait:youngwoman_happy
+[Me peleo muchas veces]
    ~q27 = 1
    Eso puedes ser normal #portrait:youngwoman_neutral
+[Me peleo siempre]
    ~q27 = 2
    Es triste cuando suele pasar eso #portrait:youngwoman_sad

- Los amigos son muy importantes en estas situaciones, ¿tienes amigos ~PlayerUsername? #speaker:Joven #portrait:youngwoman_neutral #layout:right//22

+[Tengo muchos amigos]
    ~q22 = 0
    Que alegría me da escuchar eso #portrait:youngwoman_happy 
+[Tengo muchos amigos pero me gustaría tener más]
    ~q22 = 1
    Es bueno oír eso #portrait:youngwoman_neutral
+[No tengo amigos]
    ~q22 = 2
    Es triste escuchar eso #portrait:youngwoman_happy

- ¡Ahora toda la Tierra Nevada te consideramos un gran amigo! #portrait:youngwoman_happy

- Escuché que su barco se averió, en agradecimiento los habitantes de las Tierras Nevadas prepararon velas, espero que sea de gran ayuda.

- Dirígete al portal al final del puente, ¡el capitán te espera!.

~ gotRudder()

->END