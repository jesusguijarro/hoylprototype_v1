    INCLUDE ../global.ink
    
    !Fue una dura batalla! Sin considerar esta batalla ¿como te sientes normalmente tú Jugador? #speaker:EscarabajoMorado #portrait:purplebeetle_neutral #layout:right //pregunta 17

-> main

=== main ===

+[Estoy cansado de cuando en cuando]
~q17 = 0
    Esta bien, algunas veces nos encontramos cansados! #portrait:purplebeetle_neutral
+[Estoy cansado muchos dias]
~q17 = 1
    ohh, eso debe ser duro! #portrait:purplebeetle_sad
+[Estoy cansado siempre]
~q17 = 2
    ohh, eso debe ser duro! #portrait:purplebeetle_sad
    
- Que tranquilidad se ha quedado, ¿no te sientes un poco solo?#speaker:EscarabajoMorado #portrait:purplebeetle_neutral #layout:right //pregunta 20
+[Nunca me siento solo]
~q20 = 0
    Eso me alegra #portrait:purplebeetle_happy
+[Me siento solo muchas veces]
~q20 = 1
    ohh, eso me entristece! #portrait:purplebeetle_sad
+[Me siento solo siempre]
~q20 = 2    
    ohh, eso me entristece! #portrait:purplebeetle_sad

- Hay que aprovechar y comamos algo, ¿siempre tienes hambre en tu día a día? #speaker:EscarabajoMorado #portrait:purplebeetle_neutral #layout:right //pregunta 18
+[La mayoria de los dias no tengo ganas de comer]
~q18 = 2
    Eso me pone triste! #portrait:purplebeetle_sad
+[Muchos dias no tengo ganas de comer]
~q18 = 1
    Eso me pone triste! #portrait:purplebeetle_sad
+[Como muy bien]
~q18 = 0
    ohh, eso me entristece! #portrait:purplebeetle_happy

- Personalmente en estas situaciones como la que acaba de pasar, a mí me cuesta mucho decidirme, ¿en tu caso ~PlayerUsername, cómo lo llevas? #speaker:EscarabajoMorado #portrait:purplebeetle_neutral #layout:right //pregunta 13
+[No puedo decidirme]
~q13 = 2
    Que dificil cuando no podemos decidir! #portrait:purplebeetle_sad
+[Me cuesta decidirme]
~q13 = 1
    Eso suele paasarme tambien! #portrait:purplebeetle_sad
+[Me decido fácilmente]
~q13 = 0
    Me alegra escuchar eso! #portrait:purplebeetle_happy
    
- Mucho por hoy ~PlayerUsername, es hora de que descansemos, ¡tienes que dormir para la siguiente aventura! #speaker:EscarabajoMorado #portrait:purplebeetle_neutral #layout:right //pregunta 16
+[Todas las noches me cuesta dormirme]
~q16 = 2
    Eso debe ser muy complicado! #portrait:purplebeetle_sad
+[Muchas noches me cuesta dormirme]
~q16 = 1
    Eso suele paasarme tambien! #portrait:purplebeetle_sad
+[Duermo muy bien]
~q16 = 0
    Me alegra escuchar eso! #portrait:purplebeetle_happy

- Escuché que su barco se averió, en agradecimiento los herreros del desierto prepararon un ancla, espero que sea de gran ayuda #speaker:EscarabajoMorado #portrait:purplebeetle_neutral #layout:right


->END
