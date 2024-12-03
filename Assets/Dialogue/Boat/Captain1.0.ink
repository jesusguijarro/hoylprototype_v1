INCLUDE ../globals.ink

¡Bienvenido a bordo del Navegante de los Sueños, ~PlayerUsername!, ¿Qué te trae a este barco hoy? #speaker:Capitán #portrait:captain_neutral #layout:right

-> main

=== main ===

- ¡Gracias Capitán! Estoy buscando respuestas y aventuras. #speaker:~PlayerUsername #portrait:male_player_neutral #layout:left

- Antes de continuar, me gustaría hacerte unas preguntas ~PlayerUsername. #speaker:Capitán #portrait:captain_neutral #layout:right

- ¿Alguna vez te sientes triste ~PlayerUsername? #speaker:Capitán #portrait:captain_neutral #layout:right // pregunta 1
+ [A veces me siento triste]
    ~ question1 = 0
    Está bien, eso pasa a veces! #speaker:Capitán #portrait:captain_neutro #layout:right
    //-> DONE
+ [Muchas veces me siento triste]
    ~ question1 = 1
    Eso me pone triste también! #speaker:Capitán #portrait:captain_sad #layout:right
    //-> DONE
+ [Siempre estoy triste]
    ~ question1 = 2
    Eso me pone triste también! #speaker:Capitán #portrait:captain_sad #layout:right
    //-> DONE

- Antes de una aventura es importante saber sobre tus habilidades ~PlayerUsername, ¿Crees que te saldrán bien las cosas? #speaker:Capitán #portrait:captain_neutro #layout:right // pregunta 2
+ [Nunca me saldrá nada bien]
    ~ question2 = 0
    Eso me pone triste! #speaker:Capitán #portrait:captain_sad #layout:right
    //-> DONE
+ [No estoy seguro de si las cosas me saldrán bien]
    ~ question2 = 1
    Es normal sentirse así algunas veces! #speaker:Capitán #portrait:captain_neutro #layout:right
    //-> DONE
+ [Las cosas me saldrán bien]
    ~ question2 = 2
    Genial, eso me pone feliz! #speaker:Capitán #portrait:captain_happy #layout:right
    //-> DONE

- Dejando de lado lo que creas, es muy diferente como crees que te salen las cosas y como las haces, ¿no crees? #speaker:Capitán #portrait:captain_neutro #layout:right

- En tu día a día, ¿crees que haces bien la mayoría de las cosas? #speaker:Capitán #portrait:captain_neutro #layout:right //3
+ [Hago bien la mayoría de las cosas]
    ~ question3 = 0
    Genial, eso me pone feliz! #speaker:Capitán #portrait:captain_happy #layout:right
+ [Hago mal muchas cosas]
    ~ question3 = 1
    Es normal algunas veces! #speaker:Capitán #portrait:captain_neutro #layout:right
+ [Todo lo hago mal]
    ~ question3 = 2
    Siempre podemos mejorar, no lo olvides! #speaker:Capitán #portrait:captain_sad #layout:right
    
- Antes de seguir con nuestra aventura, debo saber en que tipo de persona voy a confiar, ¿te consideras una persona mala? ~PlayerUsername? #speaker:Capitán #portrait:captain_neutro #layout:right // 5
+ [Soy malo siempre]
    ~ question5 = 0
    Eso no esta bien! #speaker:Capitán #portrait:captain_sad #layout:right
+ [Soy malo muchas veces]
    ~ question5 = 1
    Eso no esta bien! #speaker:Capitán #portrait:captain_sad #layout:right
+ [Soy malo algunas veces]
    ~ question5 = 2
    Es normal algunas veces! #speaker:Capitán #portrait:captain_neutro #layout:right
    
- Te mentiría si te dijera que todos estamos libres de maldad ~PlayerUsername #speaker:Capitán #portrait:captain_neutro #layout:right

- Estamos por llegar a nuestro destino, ¿listo para la diversión?, espero te guste! #speaker:Capitán #portrait:captain_neutro #layout:right // 4
+ [Me divierten muchas cosas]
    ~ question4 = 0
    Genial, es bueno divertirse! #speaker:Capitán #portrait:captain_happy #layout:right
    -> DONE
+ [Me divierten algunas cosas]
    ~ question4 = 1
    Es normal algunas veces! #speaker:Capitán #portrait:captain_neutro #layout:right
    -> DONE
+ [Nada me divierte]
    ~ question4 = 2
    Eso me pone triste! #speaker:Capitán #portrait:captain_sad #layout:right
    -> DONE
    
-> END