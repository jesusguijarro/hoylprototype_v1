INCLUDE ../globals.ink

¡Bienvenido a bordo del Navegante de los Sueños, ~PlayerUsername!, ¿Qué te trae a este barco hoy? #speaker:Capitán #portrait:captain_neutral #layout:right

-> main

=== main ===

- ¡Gracias Capitán! Estoy buscando respuestas y aventuras. #speaker:~PlayerUsername #portrait:male_player_neutral #layout:left

- Antes de continuar, me gustaría hacerte unas preguntas ~PlayerUsername. #speaker:Capitán #portrait:captain_neutral #layout:right

- ¿Alguna vez te sientes triste ~PlayerUsername? // pregunta 1
+ [A veces me siento triste]
    ~ question1 = 0
    Está bien, eso pasa a veces! #portrait:captain_neutro
    -> chosen
+ [Muchas veces me siento triste]
    ~ question1 = 1
    Eso me pone triste también! #portrait:captain_sad
    -> chosen
+ [Siempre estoy triste]
    ~ question1 = 2
    Eso me pone triste también! #portrait:captain_sad
    -> chosen

=== chosen ===
-> END

=== already_chose ===

- Antes de una aventura es importante saber sobre tus habilidades ~PlayerUsername, ¿Crees que te saldrán bien las cosas? #portrait:captain_neutro // pregunta 2
+ [Nunca me saldrá nada bien]
    Eso me pone triste! #portrait:captain_sad
+ [No estoy seguro de si las cosas me saldrán bien]
    Es normal sentirse así algunas veces! #portrait:captain_neutro
+ [Las cosas me saldrán bien]
    Genial, eso me pone feliz! #portrait:captain_happy
- Dejando de lado lo que creas, es muy diferente como crees que te salen las cosas y como las haces, ¿no crees? #portrait:captain_neutro

- En tu día a día, ¿crees que haces bien la mayoría de las cosas?
+ [Hago bien la mayoría de las cosas]
    Genial, eso me pone feliz! #portrait:captain_happy
+ [Hago mal muchas cosas]
    Es normal algunas veces! #portrait:captain_neutro
+ [Todo lo hago mal]
    Siempre podemos mejorar, no lo olvides! #portrait:captain_sad
    
- Antes de seguir con nuestra aventura, debo saber en que tipo de persona voy a confiar, ¿te consideras una persona mala, ~PlayerUsername? #portrait:captain_neutro
+ [Soy malo siempre]
    Eso no esta bien! #portrait:captain_sad
+ [Soy malo muchas veces]
    Eso no esta bien! #portrait:captain_sad
+ [Soy malo algunas veces]
    Es normal algunas veces! #portrait:captain_neutro
    
- Te mentiría si te dijera que todos estamos libres de maldad ~PlayerUsername #portrait:captain_neutro

- Estamos por llegar a nuestro destino, ¿listo para la diversión?, espero te guste! 
+ [Me divierten muchas cosas]
    Genial, es bueno divertirse! #portrait:captain_happy
    -> DONE
+ [Me divierten algunas cosas]
    Es normal algunas veces! #portrait:captain_neutro
    -> DONE
+ [Nada me divierte]
    Eso me pone triste! #portrait:captain_sad
    -> DONE
    
-> END