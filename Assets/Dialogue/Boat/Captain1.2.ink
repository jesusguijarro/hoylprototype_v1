INCLUDE ../globals.ink

El futuro es incierto, pero estamos juntos en esta aventura. #speaker:Capitán #portrait:captain_neutral #layout:right

-> main

=== main ===

- ¿Cómo te sientes al respecto? // 6
+ [A veces pienso que me pueden ocurrir cosas malas]
    ~ q06 = 0
    Está bien, eso pasa a veces! #portrait:captain_happy
+ [Me preocupa que me ocurran cosas malas]
    ~ q06 = 1
    Es normal sentirse así algunas veces! #portrait:captain_neutro
+ [Estoy seguro de que me van a ocurrir cosas terribles]
    ~ q06 = 2
    Eso me pone triste! #portrait:captain_sad

- Probablemente sientas que es una carga buscar las piezas faltantes ¿te cuesta hacer este tipo de deberes en tu día a día? #portrait:captain_neutro // 15
+[Siempre me cuesta]
    ~ q15 = 2
    Eso me pone triste! #portrait:captain_sad
+[Muchas veces me cuesta]
    ~ q15 = 1
    Eso me pone triste! #portrait:captain_sad
+[No me cuesta]
    ~ q15 = 0
    Genial, eso me pone feliz! #portrait:captain_happy

- Dicho esto jugador, dirigete al portal que esta en el barco. #portrait:captain_neutro

- Elige el camino que quieras, es tu elección! #portrait:captain_happy

- En cualquiera encontrarás alguna de las piezas faltantes, mucha suerte ~PlayerUsername!. #portrait:captain_happy

-> END