INCLUDE ../globals.ink

¡~PlayerUsername, bienvenido de vuelta! #speaker:Capitan #portrait:captain_neutral #layout:right

-> main

=== main ===

- ¡Qué gran aventura has tenido! #portrait:captain_happy

- Ha sido una experiencia increíble, hice muchos amigos y conseguí una nueva pieza para el barco! #speaker:~PlayerUsername #portrait:player_happy #layout:left

- Se he puesto la situación un poco melancólica con tu ausencia ~PlayerUsername. #speaker:Capitan #portrait:captain_neutral #layout:right

- Ahora mismo me gustaría hablar de temas más personales #speaker:Capitan #portrait:captain_neutral #layout:right

- ¿Alguna vez tienes ganas de llorar? //10
+[Tengo ganas de llorar todos los dias]
    ~ q10 = 2
    Me da mucha tristeza escuchar eso #portrait:captain_sad
+[Tengo ganas de llorar muchos dias]
    ~ q10 = 1
    Me sa mucha tristeza escuchar eso #portrait:captain_sad
+[Tengo ganas de llorar de vez en cuando]
    ~ q10 = 0
    A veces es normal querer llorar de vez en cuando #portrait:captain_neutral

- Si alguna vez te pasará algo ~PlayerUsername, alguien se preocuparía por ti, ¿alguien te quiere? //25

+[Nadie me quiere]
    ~ q25 = 2
    Es triste escuchar eso #portrait:captain_sad
+[No estoy seguro de que alguien me quiera]
    ~ q25 = 1
    Muchas de las veces podemos pensar eso #portrait:captain_sad
+[Estoy seguro de que alguien me quiere]
    ~ q25 = 0
    Me alegra escuchar eso #portrait:captain_happy

- Te preguntarás ~PlayerUsername, por qué re pregunto todo esto. #speaker:Capitan #portrait:captain_neutral #layout:right

- Mientras estuve solo esperando tu regreso contraje una enfermedad. 

- Muchas personas se preocupan por la enfermedad aunque no sea algo grave, ¿en tú caso cómo es ~PlayerUsername? //19

+[No me preocupa el dolor ni la enfermedad]
    ~ q19 = 0
    Me alegra escuchar eso #captain_happy
+[Muchas veces me preocupa el dolor y la enfermedad]
    ~ q19 = 1
    Muchas de las veces es normal que eso nos preocupe #captain_sad
+[Siempre me preocupa el dolor y la enfermedad]
    ~ q19 = 2
    Es triste escuchar eso #captain_sad
    
- Sin importar la situación en la que te encuentres ~PlayerUsername, ¿alguna vez has pensado en matarte? #speaker:Capitan #portrait:captain_neutral #layout:right //9

+[No pienso en matarme]
    ~ q09 = 0
    Me alegra mucho escuchar eso #captain_happy
+[Pienso en matarme pero no lo haria]
    ~ q09 = 1
    Que triste es escuchar eso #captain_sad
+[Quiero matarme]
    ~ q09 = 2
    Es demasiado triste escuchar eso #captain_sad
    
- En estos momentos lo unico que me podría salvar es una de las posiones del legendario Hannibal. #speaker:Capitan #portrait:captain_neutral #layout:right

- ¿Dónde puedo encontrar al legendario Hannibal? #speaker:~PlayerUsername #portrait:player_neutral #layout:left

- Lamentable no se le ha visto desde hace mucho tiempo, permanece en las sombras debido a su apariencia #speaker:Capitan #portrait:captain_neutral #layout:right

- Si alguien te dice qué hacer, ¿generalmente no haces lo que te dicen? //26

+[Generalmente, hago lo que me dicen]
    ~ q26 = 0
    Me alegra demasiado oir eso #portrait:captain_happy
+[Muchas veces no lo hago]
    ~ q26 = 1
    Que bueno es oir eso #portrait:captain_happy
+[Nunca hago lo que me dicen]
    ~ q26 = 2
    Escuchar eso es triste #portrait:captain_sad
    
- Tendrás que seguir tu instinto de aventurero y encontrarlo lo antes posible ~PlayerUsername. #speaker:Capitan #portrait:captain_neutral #layout:right 

- Dirigite al portal y elige el camino que desees ~PlayerUsername. La suerte te acompaña! #speaker:Capitan #portrait:captain_neutral #layout:right 

-> END

