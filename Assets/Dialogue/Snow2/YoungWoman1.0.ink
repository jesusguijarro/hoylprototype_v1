INCLUDE ../globals.ink

Bienvenido, ~PlayerUsername. Hemos escuchado los rumores sobre sus valientes hazañas en las otras tierras #speaker:Joven #portrait:youngwoman_neutral #layout:right

-> main

=== main ===

- Nos encontramos en una situación complicada y ¡necesitamos tu ayuda! #portrait:youngwoman_sad

- Un oso grizzly ha regresado a nuestro bosques, más feroz y peligroso que nunca. Muchos han intentado detenerlo, pero ninguno ha tenido éxito.  #speaker:Joven #portrait:youngwoman_neutral #layout:right

- Necesitamos tu ayuda ~PlayerUsername, para proteger nuestro pueblo y restaurar la paz en nuestras tierras  #speaker:Joven #portrait:youngwoman_neutral #layout:right

- ¡Haré todo lo que esté en mi poder para ayudar! #speaker:~PlayerUsername #portrait:player_neutral #layout:left

- En situaciones como esta, ¿cómo manejas la calma ~PlayerUsername? #speaker:Joven #portrait:youngwoman_neutral #layout:right //pregunta 11
+[Las cosas me preocupan siempre]
    ~ q11 = 2
    Eso es muy triste #portrait:youngwoman_sad
+[Las cosas me preocupan muchas veces]
    ~ q11 = 1
    Eso me pone muy triste #portrait:youngwoman_sad
+[Las cosas me preocupan de cuando en cuando]
    ~ q11 = 0
    Es normal preocuparnos algunas veces#portrait:youngwoman_neutral
    
- Hace mucho tiempo yo vivía con el miedo constante de que me pasarán cosas malas. #portrait:youngwoman_neutral

- Y desde que ya no vivo con ese miedo, este tipo de situaciones las manejo con calma. #portrait:youngwoman_happy

- Ese miedo constante hace que pienses diferente, ¿cómo lo llevas tú ~PlayerUsername? 


+[A veces pienso que me pueden ocurrir cosas malas]
    ~ q06 = 0
    Es normal pensar eso algunas veces#portrait:youngwoman_neutral
+[Me preocupa que me ocurran cosas malas]
    ~ q06 = 1
    Eso me pone muy triste #portrait:youngwoman_sad
+[Estoy seguro de que me van a ocurrir cosas malas]
    ~ q06 = 2
    Es es muy triste pensar eso #portrait:youngwoman_sad

- ¿Tú qué piensas sobre las cosas malas que pasan a tu al rededor ~PlayerUsername?

+[Todas las cosas malas son culpa mía]
    ~ q08 = 2
    Es triste escuchar eso. #portrait:youngwoman_sad
+[Muchas cosas malas son culpa mía]
    ~ q08 = 1
    Eso me pone muy triste. #portrait:youngwoman_sad
+[Generalmente no tengo la culpa de que ocurran cosas malas]
    ~ q08 = 0
    Que bueno que pienses eso. #portrait:youngwoman_happy
    
- A partir de este punto no te puedo acompañar, mucha suerte ~PlayerUsername! #speaker:Joven #portrait:youngwoman_happy #layout:right

- Puedes encontrar al oso en su guarida en la isla de la izquierda! #speaker:Joven #portrait:youngwoman_neutral #layout:right

-> END