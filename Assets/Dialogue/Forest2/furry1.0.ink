INCLUDE ../globals.ink

El Hada Aurora ha sido capturada por el rey de las sombras. Necesitamos tu ayuda para rescatarla y devolver la luz al Bosque Encantado. #speaker:Furry #portrait:furry_sad #layout:right

-> main

=== main ===

- ¡Por supuesto que ayudaré! El hada Aurora necesita nuestra ayuda y no permitiré que el rey de las sombras se salga con la suya. #speaker:~PlayerUsername #portrait:male_player_neutral #layout:left

- En situaciones como esta, ¿cómo manejas la calma ~PlayerUsername? #speaker:Furry #portrait:furry_neutral #layout:right //pregunta 11
+[Las cosas me preocupan siempre]
    ~ q11 = 2
    Eso es muy triste #portrait:furry_sad
+[Las cosas me preocupan muchas veces]
    ~ q11 = 1
    Eso me pone muy triste #portrait:furry_sad
+[Las cosas me preocupan de cuando en cuando]
    ~ q11 = 0
    Es normal preocuparnos algunas veces#portrait:furry_neutral

- Hace mucho tiempo yo vivía con el miedo constante de que me pasarán cosas malas. #speaker:Furry #portrait:furry_neutral #layout:right

- Y desde que ya no vivo con ese miedo, este tipo de situaciones las manejo con calma. #portrait:furry_happy

- Ese miedo constante hace que pienses diferente, ¿cómo lo llevas tú ~PlayerUsername? #portrait:furry_neutral //6

+[A veces pienso que me pueden ocurrir cosas malas]
    ~ q06 = 0
    Es normal pensar eso algunas veces#portrait:furry_neutral
+[Me preocupa que me ocurran cosas malas]
    ~ q06 = 1
    Eso me pone muy triste #portrait:furry_sad
+[Estoy seguro de que me van a ocurrir cosas malas]
    ~ q06 = 2
    Es es muy triste pensar eso #portrait:furry_sad

- ¿Tú qué piensas sobre las cosas malas que pasan a tu al rededor ~PlayerUsername? #portrait:furry_neutral //8

+[Todas las cosas malas son culpa mia]
    ~ q08 = 2
    Es triste escuchar eso. #portrait:furry_sad
    -> DONE
+[Muchas cosas malas son culpa mia]
    ~ q08 = 1
    Eso me pone muy triste. #portrait:furry_sad
    -> DONE
+[Generalmente no tengo la culpa de que ocurran cosas malas]
    ~ q08 = 0
    Que bueno que pienses eso. #portrait:furry_happy
    -> DONE
    
-> END