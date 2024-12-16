    INCLUDE ../globals.ink
    
Hola de vuelta ~PlayerUsername, veo que estas de vuelta luego de esta situación inesperada! #speaker:Gato #portrait:bastet_happy #layout:right

-> main

=== main ===

- En situaciones como esta, ¿cómo manejas la calma ~PlayerUsername?. #speaker:Gato #portrait:bastet_neutral #layout:right //11
+[Las cosas me preocupan siempre]
~q11 = 2
    Es triste cuando suele pasar eso #portrait:bastet_sad
+[Las cosas me preocupan muchas veces]
~q11 = 1
    Es normal que en ocasiones algo nos preocupe #portrait:bastet_sad
+[Las cosas me preocupan de cuando en cuando]
~q11 = 0
    Que alegria me da escuchar eso #portrait:bastet_happy
    
- Hace mucho tiempo yo vivía con el miedo constante de que me pasarán cosas malas.
 #speaker:Gato #portrait:bastet_neutral #layout:right
 
 - Y desde que ya no vivo con ese miedo, este tipo de situaciones las manejo con calma
 #speaker:Gato #portrait:bastet_neutral #layout:right
 
 - Ese miedo constante hace que pienses diferente, ¿cómo lo llevas tú ~PlayerUsername? //6
  
+[A veces pienso que me pueden ocurrir cosas malas]
~q06 = 0 
    Eso me suele pasar a mi tambien #portrait:bastet_neutral
+[Me preocupa que me ocurran cosas malas]
~q06 = 1
    A veces es normal preocuparnos #portrait:bastet_sad
+[Estoy seguro de que me van a ocurrir cosas malas]
~q06 = 2
    Me da tristeza escuchar eso #portrait:bastet_sad

- Y llegado a un punto, ¿tú cómo piensas Jugador?#speaker:Gato #portrait:bastet_neutral #layout:right //8

+[Todas las cosas malas son mi culpa]
~q08 = 2
    Escuchar eso me pone triste #portrait:bastet_sad
+[Muchas cosas malas son culpa mia]
~q08 = 1
    A veces podemos llegar a pensar eso #portrait:bastet_sad
+[Generalmente no tengo la culpa de que ocurran cosas malas]
~q08 = 0
    Me da mucha alegria escuchar eso #portrait:bastet_happy

- ¡Finalmente llegamos! #speaker:~PlayerUsername #portrait:male_player_neutral #layout:left

- Así es ~PlayerUsername, me temo que nuestros caminos se separan, solo sería un estorbo a tu lado, te esperaré aquí afuera #speaker:Gato #portrait:bastet_neutral #layout:right

- ¡Traeré de vuelta tus gemas, tenlo por seguro! #speaker:~PlayerUsername #portrait:male_player_happy #layout:left

->END