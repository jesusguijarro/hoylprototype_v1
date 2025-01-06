INCLUDE ../globals.ink
EXTERNAL gotAnchor()
    
¡Lo lograste ~PlayerUsername, el pueblo de Las Tierras Nevadas te estará eternamente agredecido! #speaker:Bjorn #portrait:bjorn_neutral #layout:right 

-> main

=== main ===

- !Fue una dura batalla! Sin considerar esta batalla ¿cómo te sientes normalmente tú ~PlayerUsername?//pregunta 17

+[Estoy cansado de cuando en cuando]
    ~q17 = 0
    Esta bien, ¡algunas veces nos encontramos cansados!#portrait:bjorn_neutral
+[Estoy cansado muchos días]
    ~q17 = 1
    ¡Oh, eso debe ser duro! #portrait:bjorn_sad
+[Estoy cansado siempre]
    ~q17 = 2
    ¡Oh, eso debe ser duro! #portrait:bjorn_sad
    
- Que tranquilidad se ha quedado, ¿cómo te sientes normalmente ~PlayerUsername? #speaker:Bjorn #portrait:bjorn_neutral #layout:right //pregunta 20

+[Nunca me siento solo]
    ~q20 = 0
    Eso me alegra #portrait:bjorn_happy
+[Me siento solo muchas veces]
    ~q20 = 1
    ¡Oh, eso me entristece! #portrait:bjorn_sad
+[Me siento solo siempre]
    ~q20 = 2
    ¡Oh, eso me entristece! #portrait:bjorn_sad

- Hay que aprovechar y comamos algo, ¿siempre tienes hambre en tu día a día? #speaker:Bjorn #portrait:bjorn_neutral #layout:right //pregunta 18

+[La mayoría de los días no tengo ganas de comer]
    ~q18 = 2
    ¡Eso me pone triste! #portrait:bjorn_sad
+[Muchos días no tengo ganas de comer]
    ~q18 = 1
    ¡Eso me pone triste! #portrait:bjorn_sad
+[Como muy bien]
    ~q18 = 0
    ¡Oh, eso me alegra! #portrait:bjorn_happy

- En estas situaciones como la que acaba de pasar, ¿en tu caso ~PlayerUsername, cómo lo llevas? #speaker:Bjorn #portrait:bjorn_neutral #layout:right //pregunta 13

+[No puedo decidirme]
    ~q13 = 2
    ¡Que difícil cuando no podemos decidir! #portrait:bjorn_sad
+[Me cuesta decidirme]
    ~q13 = 1
    ¡Eso suele pasarme también! #portrait:bjorn_sad
+[Me decido fácilmente]
    ~q13 = 0
    ¡Me alegra escuchar eso! #portrait:bjorn_happy
    
- ¡A mí me cuesta mucho decidirme! #portrait:bjorn_sad    
    
- Mucho por hoy ~PlayerUsername, es hora de que descansemos, ¡tienes que dormir para la siguiente aventura! #speaker:Bjorn #portrait:bjorn_neutral #layout:right //pregunta 16

+[Todas las noches me cuesta dormirme]
    ~q16 = 2
    ¡Eso debe ser muy complicado! #portrait:bjorn_sad
+[Muchas noches me cuesta dormirme]
    ~q16 = 1
    ¡Eso suele paasarme tambien! #portrait:bjorn_sad
+[Duermo muy bien]
    ~q16 = 0
    ¡Me alegra escuchar eso! #portrait:bjorn_happy

- Escuché que su barco se averió, en agradecimiento los herreros del bosque prepararon un ancla, espero que sea de gran ayuda #speaker:Bjorn #portrait:bjorn_neutral #layout:right

- ¡Dirígete de vuelta al barco por el portal de la isla del norte, el capitán te espera! #portrait:bjorn_happy

~ gotAnchor()

->END