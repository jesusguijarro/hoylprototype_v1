INCLUDE ../globals.ink

¡Jugador, bienvenido de vuelta! #speaker:Capitan #portrait:captain_neutral #layout:right

-> main

=== main ===

- ¡Qué gran aventura has tenido! #portrait:captain_happy

- Ha sido una experiencia increíble, hice muchos amigos y conseguí una nueva pieza para el barco! #speaker:~PlayerUsername #portrait:male_player_happy #layout:left

- Se he puesto la situación un poco melancólica con tu ausencia ~PlayerUsername. #speaker:Capitan #portrait:captain_neutral #layout:right

- Ahora mismo me gustaría hablar de temas más personales #speaker:Capitan #portrait:captain_neutral #layout:right

- ¿Alguna vez tienes ganas de llorar? //10
+[Tengo ganas de llorar todos los dias]
    ~ question10 = 0
    Me da mucha tristeza escuchar eso #portrait:captain_sad
+[Tengo ganas de llorar muchos dias]
    ~ question10 = 1
    Me sa mucha tristeza escuchar eso #portrait:captain_sad
+[Tengo ganas de llorar de vez en cuando]
    ~ question10 = 2
    A veces es normal querer llorar de vez en cuando #portrait:captain_neutral

- Si alguna vez te pasará algo jugador, alguien se preocuparía por ti, ¿alguien te quiere? //25
+[Nadie me quiere]
    ~ question25 = 0
    Es triste escuchar eso #captain_sad
+[No estoy seguro de que alguien me quiera]
    ~ question25 = 1
    Muchas de las veces podemos pensar eso #captain_sad
+[Estoy seguro de que alguien me quiere]
    ~ question25 = 2
    Me alegra escuchar eso #captain_happy

- Te preguntarás Jugador, por qué re pregunto todo esto. #speaker:Capitan #portrait:captain_neutral #layout:right

- Mientras estuve solo esperando tu regreso contraje una enfermedad. 

- Muchas personas se preocupan por la enfermedad aunque no sea algo grave, ¿en tú caso cómo es Jugador? //19
+[No me preocupa el dolor ni la enfermedad]
    ~ question19 = 0
    Me alegra escuchar eso #captain_happy
+[Muchas veces me preocupa el dolor y la enfermedad]
    ~ question19 = 1
    Muchas de las veces es normal que eso nos preocupe #captain_sad
+[Siempre me preocupa el dolor y la enfermedad]
    ~ question20 = 2
    Es triste escuchar eso #captain_sad
    
- Sin importar la situación en la que te encuentres Jugador, ¿alguna vez has pensado en matarte? #speaker:Capitan #portrait:captain_neutral #layout:right //9
+[No pienso en matarme]
    ~ question9 = 0
    Me alegra mucho escuchar eso #captain_happy
+[Pienso en matarme pero no lo haria]
    ~ question9 = 0
    Que triste es escuchar eso #captain_sad
+[Quiero matarme]
    ~ question9 = 0
    Es demasiado triste escuchar eso #captain_sad
    
- En estos momentos lo unico que me podría salvar es una de las posiones del legendario Hannibal. #speaker:Capitan #portrait:captain_neutral #layout:right

- ¿Dónde puedo encontrar al legendario Hannibal? #speaker:~PlayerUsername #portrait:male_player_neutral #layout:left

- Lamentable no se le ha visto desde hace mucho tiempo, permanece en las sombras debido a su apariencia #speaker:Capitan #portrait:captain_neutral #layout:right

- Si alguien te dice qué hacer, ¿generalmente no haces lo que te dicen? //26
+[Generalmente]
    ~ question26 = 0
    Me alegra demasiado oir eso #portrait:captain_happy
+[Muchas veces]
    ~ question26 = 1
    Que bueno es oir eso #portrait:captain_happy
+[Nunca]
    ~ question26 = 2
    Escuchar eso es triste #portrait:captain_sad
    
- Tendrás que seguir tu instinto de aventurero y encontrarlo lo antes posible ~PlayerUsername. #speaker:Capitan #portrait:captain_neutral #layout:right 

- Dirigite al portal y elige el camino que desees ~PlayerUsername. La suerte te acompaña! #speaker:Capitan #portrait:captain_neutral #layout:right 

-> END

