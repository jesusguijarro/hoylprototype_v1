INCLUDE ../globals.ink
EXTERNAL gotAnchor()    
    
!Fue una dura batalla! Sin considerar esta batalla ¿cómo te sientes normalmente tú ~PlayerUsername? #speaker:Escarabajo #portrait:purplebeetle_neutral #layout:right //pregunta 17

-> main

=== main ===

+[Estoy cansado de vez en cuando]
~q17 = 0
    Está bien, algunas veces nos encontramos cansados! #portrait:purplebeetle_neutral
    
+[Estoy cansado muchos días]
~q17 = 1
    ¡Oh, eso debe ser duro! #portrait:purplebeetle_sad
    
+[Estoy cansado siempre]
~q17 = 2
    ¡Oh, eso debe ser duro! #portrait:purplebeetle_sad
    
- Que tranquilidad se ha quedado, ¿cómo te sientes normalmente ~PlayerUsername? #speaker:Escarabajo #portrait:purplebeetle_neutral #layout:right //pregunta 20

+[Nunca me siento solo]
~q20 = 0
    Eso me alegra #portrait:purplebeetle_happy
+[Me siento solo muchas veces]
~q20 = 1
    ¡Oh, eso me entristece! #portrait:purplebeetle_sad
+[Me siento solo siempre]
~q20 = 2    
    ¡Oh, eso me entristece! #portrait:purplebeetle_sad

- Hay que aprovechar y comamos algo, ¿siempre tienes hambre en tu día a día? #speaker:Escarabajo #portrait:purplebeetle_neutral #layout:right //pregunta 18

+[La mayoria de los días no tengo ganas de comer]
~q18 = 2
    Eso me pone triste! #portrait:purplebeetle_sad
+[Muchos días no tengo ganas de comer]
~q18 = 1
    Eso me pone triste! #portrait:purplebeetle_sad
+[Como muy bien]
~q18 = 0
    ¡Oh, eso me alegra! #portrait:purplebeetle_happy

- En estas situaciones como la que acaba de pasar, ¿en tu caso ~PlayerUsername, cómo lo llevas? #speaker:Escarabajo #portrait:purplebeetle_neutral #layout:right //pregunta 13

+[No puedo decidirme]
~q13 = 2
    ¡Que difícil cuando no podemos decidir! #portrait:purplebeetle_sad
+[Me cuesta decidirme]
~q13 = 1
    ¡Eso suele pasarme también! #portrait:purplebeetle_sad
+[Me decido fácilmente]
~q13 = 0
    ¡Me alegra escuchar eso! #portrait:purplebeetle_happy
    
- ¡A mí me cuesta mucho decidirme! #portrait:purplebeetle_sad    
    
- Mucho por hoy ~PlayerUsername, es hora de que descansemos, ¡tienes que dormir para la siguiente aventura! #speaker:Escarabajo #portrait:purplebeetle_neutral #layout:right //pregunta 16

+[Todas las noches me cuesta dormirme]
~q16 = 2
    ¡Eso debe ser muy complicado! #portrait:purplebeetle_sad
+[Muchas noches me cuesta dormirme]
~q16 = 1
    ¡Eso suele pasarme tambien! #portrait:purplebeetle_sad
+[Duermo muy bien]
~q16 = 0
    ¡Me alegra escuchar eso! #portrait:purplebeetle_happy

- Escuché que su barco se averió, en agradecimiento los herreros del desierto prepararon un ancla, espero que sea de gran ayuda #speaker:Escarabajo #portrait:purplebeetle_happy #layout:right

- ¡Dirígete a la puerta, podrás llegar con el capitán! 

~ gotAnchor()

->END
