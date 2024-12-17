INCLUDE ../globals.ink
EXTERNAL gotAnchor()

¡Lo lograste,  ~PlayerUsername! #speaker:Hada #portrait:fairy_happy #layout:right

-> main

=== main ===

- !Fue una dura batalla! Sin considerar esta batalla ¿como te sientes normalmente tú ~PlayerUsername? #speaker:Hada #portrait:fairy_neutral #layout:right //pregunta 17

+[Estoy cansado de cuando en cuando]
    ~ q17 = 0
    Esta bien, algunas veces nos encontramos cansados! #Portrait:fairy_neutral
+[Estoy cansado muchos dias]
    ~ q17 = 1
    Ohh, eso debe ser duro! #portrait:fairy_sad
+[Estoy cansado siempre]
    ~ q17 = 2
    Ohh, eso debe ser duro! #portrait:fairy_sad
    
- Que tranquilidad se ha quedado, ¿cómo te sientes normalmente ~PlayerUsername? #speaker:Hada #portrait:fairy_neutro #layout:right //pregunta 20

+[Nunca me siento solo]
    ~ q20 = 0
    Eso me alegra #portrait:fairy_happy
+[Me siento solo muchas veces]
    ~ q20 = 1
    Ohh, eso me entristece! #portrait:fairy_sad
+[Me siento solo siempre]
    ~ q20 = 2
    Ohh, eso me entristece! #portrait:fairy_sad

- Hay que aprovechar y comamos algo, ¿siempre tienes hambre en tu día a día? #speaker:Hada #portrait:fairy_neutro #layout:right //pregunta 18

+[La mayoria de los dias no tengo ganas de comer]
    ~ q18 = 2
    Eso me pone triste! #portrait:fairy_sad
+[Muchos dias no tengo ganas de comer]
    ~ q18 = 1
    Eso me pone triste! #portrait:fairy_sad
+[Como muy bien]
    ~ q18 = 0
    Ohh, eso me alegra! #portrait:fairy_happy

- En estas situaciones como la que acaba de pasar, ¿en tu caso ~PlayerUsername, cómo lo llevas? #speaker:Hada #portrait:fairy_neutral #layout:right //pregunta 13

+[No puedo decidirme]
    ~ q13 = 2
    Que dificil cuando no podemos decidir! #portrait:fairy_sad
+[Me cuesta decidirme]
    ~ q13 = 1
    Eso suele paasarme tambien! #portrait:fairy_sad
+[Me decido fácilmente]
    ~ q13 = 0
    Me alegra escuchar eso! #portrait:fairy_happy

- A a mí me cuesta mucho decidirme! #portrait:fairy_sad
    
- Mucho por hoy ~PlayerUsername, es hora de que descansemos, ¡tienes que dormir para la siguiente aventura! #speaker:Hada #portrait:fairy_neutral #layout:right //pregunta 16

+[Todas las noches me cuesta dormirme]
    ~ q16 = 2
    Eso debe ser muy complicado! #portrait:fairy_sad
+[Muchas noches me cuesta dormirme]
    ~ q16 = 1
    Eso suele paasarme tambien! #portrait:fairy_sad
+[Duermo muy bien]
    ~ q16 = 0
    Me alegra escuchar eso! #portrait:fairy_happy

- Escuché que su barco se averió, en agradecimiento los herreros del bosque prepararon un ancla, espero que sea de gran ayuda #speaker:Hada #portrait:fairy_neutral #layout:right

- Dirigete al barco tomando el portal al final del puente, el capitán te espera! #portrait:fairy_happy

~ gotAnchor()

-> END
