INCLUDE ../globals.ink

¡Lo logramos,  ~PlayerUsername! #speaker:Hada #portrait:fairy_happy #layout:right

-> main

=== main ===

- !Fue una dura batalla! Sin considerar esta batalla ¿como te sientes normalmente tú ~PlayerUsername? #speaker:Hada #portrait:fairy_neutral #layout:right //pregunta 17

+[Estoy cansado de cuando en cuando]
    ~ question17 = 0
    Esta bien, algunas veces nos encontramos cansados! #Portrait:fairy_neutral
+[Estoy cansado muchos dias]
    ~ question17 = 1
    Ohh, eso debe ser duro! #portrait:fairy_sad
+[Estoy cansado siempre]
    ~ question17 = 2
    Ohh, eso debe ser duro! #portrait:fairy_sad
    
- Que tranquilidad se ha quedado, ¿cómo te sientes? #speaker:Hada #portrait:fairy_neutro #layout:right //pregunta 20

+[Nunca me siento solo]
    ~ question20 = 0
    Eso me alegra #portrait:fairy_happy
+[Me siento solo muchas veces]
    ~ question20 = 1
    Ohh, eso me entristece! #portrait:fairy_sad
+[Me siento solo siempre]
    ~ question20 = 2
    Ohh, eso me entristece! #portrait:fairy_sad

- Hay que aprovechar y comamos algo, ¿siempre tienes hambre en tu día a día? #speaker:Hada #portrait:fairy_neutro #layout:right //pregunta 18

+[La mayoria de los dias no tengo ganas de comer]
    ~ question18 = 0
    Eso me pone triste! #portrait:fairy_sad
+[Muchos dias no tengo ganas de comer]
    ~ question18 = 1
    Eso me pone triste! #portrait:fairy_sad
+[Como muy bien]
    ~ question18 = 2
    Ohh, eso me entristece! #portrait:fairy_happy

- Personalmente en estas situaciones como la que acaba de pasar, ¿en tu caso ~PlayerUsername, cómo lo llevas? #speaker:Hada #portrait:fairy_neutral #layout:right //pregunta 13

+[No puedo decidirme]
    ~ question13 = 0
    Que dificil cuando no podemos decidir! #portrait:fairy_sad
+[Me cuesta decidirme]
    ~ question13 = 1
    Eso suele paasarme tambien! #portrait:fairy_sad
+[Me decido fácilmente]
    ~ question13 = 2
    Me alegra escuchar eso! #portrait:fairy_happy

- A a mí me cuesta mucho decidirme! #portrait:fairy_sad
    
- Mucho por hoy ~PlayerUsername, es hora de que descansemos, ¡tienes que dormir para la siguiente aventura! #speaker:Hada #portrait:fairy_neutro #layout:right //pregunta 16

+[Todas las noches me cuesta dormirme]
    ~ question16 = 0
    Eso debe ser muy complicado! #portrait:fairy_sad
+[Muchas noches me cuesta dormirme]
    ~ question16 = 1
    Eso suele paasarme tambien! #portrait:fairy_sad
+[Duermo muy bien]
    ~ question16 = 2
    Me alegra escuchar eso! #portrait:fairy_happy

- Escuché que su barco se averió, en agradecimiento los herreros del bosque prepararon un ancla, espero que sea de gran ayuda #speaker:Hada #portrait:fairy_neutro #layout:right

- Dirigete al barco tomando el portal al final del puente, el capitán te espera!

    -> END
