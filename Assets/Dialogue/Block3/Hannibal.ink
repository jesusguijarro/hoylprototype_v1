INCLUDE ../globals.ink
EXTERNAL gotRudder()

¡¿Quién eres tú?! #speaker:Hannibal #portrait:hannibal_neutral #layout:right

-> main

=== main ===

- ¡¿Te mandaron a capturarme?! #speaker:Hannibal #portrait:hannibal_sad #layout:right

- ¡Para nada, estoy en busca de una de sus pócimas legendarias! #speaker:~PlayerUsername #portrait:player_neutral #layout:left

- Eres el primero en encontrarme, ¿no serás el aventurero del que se ha corrido la voz que ha ayudado a las tierras cercanas? #speaker:Hannibal #portrait:hannibal_happy #layout:right

- ¡Aunque no lo creas tus hazañas me han ayudado hasta a mi!

- Me siento alagado, pero ahora mismo me encuentro en una situación complicada #speaker:~PlayerUsername #portrait:player_sad #layout:left

- En tu mundo ¿qué tal es tu trabajo en la escuela ~PlayerUsername? #speaker:Hannibal #portrait:hannibal_neutral #layout:right //23

+[Es bueno]
    ~ q23 = 0
    Me alegra demasiado escuchar eso #portrait:hannibal_happy
+[No es tan bueno como antes]
    ~ q23 = 1
    Eso puede ser normal solo es cuestion de mejorar #portrait:hannibal_neutral
+[Lo llevo muy mal]
    ~ q23 = 2
    Que bueno es escuchar eso #portrait:hannibal_happy

- En este mundo te has desempeñado muy bien en tus aventuras! #speaker:Hannibal #portrait:hannibal_happy #layout:right

- Necesito una de tus pociones magicas para salvar al Capitán! #speaker:~PlayerUsername #portrait:player_neutral #layout:left

- Tranquilo, tu nombre es ~PlayerUsername, verdad...  #speaker:Hannibal #portrait:hannibal_neutral #layout:right

- Toma asiento, mientras preparo la poción que necesitas. La gente normalmente me tiene miedo por mi apariencia.

- Por eso permanezco aquí oculto, pero al parecer tu no  sientes miedo.

- Ahora mismo no me había pasado eso por la cabeza, siendo sincero #speaker:~PlayerUsername #portrait:player_neutral #layout:left

- Conforme tu aspecto ~PlayerUsername, ¿cómo te sientes? #speaker:Hannibal #portrait:hannibal_neutral #layout:right //14

+[Tengo buen aspecto]
    ~ q14 = 0
    Me algra escuchar que pienses eso #portrait:hannibal_happy
+[Hay algunas cosas de mi aspecto que no me gustan]
    ~ q14 = 1
    A veces esta bien que pensemos eso #portrait:hannibal_neutral
+[Soy feo]
    ~ q14 = 2
    Es muy triste escuchar eso #portrait:hannibal_sad
    
- Y en general ~PlayerUsername, ¿te gusta cómo eres? #speaker:Hannibal #portrait:hannibal_neutral #layout:right //7

+[Me odio]
    ~ q07 = 2
    Es triste escuchar eso #portrait:hannibal_sad
+[No me gusta como soy] 
    ~ q07 = 1
    Es triste escuchar eso #portrait:hannibal_sad
+[Me gusta como soy]
    ~ q07 = 0
    Me alegra escuchar eso #portrait:hannibal_happy

- De sus pociones se habla en todos lados, ¿cómo llego a ser tan bueno en ello? #speaker:~PlayerUsername #portrait:player_neutral #layout:left

- Primero te preguntaré, ¿cómo te ves comparado con las demás personas ~PlayerUsername? #speaker:Hannibal #portrait:hannibal_neutral #layout:right //14
+[Nunca podré ser tan bueno como otros]
    ~ q24 = 2
    Que triste es escuchar eso #portrait:hannibal_sad
+[Si quiero puedo ser tan bueno como otros]
    ~ q24 = 1
    Me alegra que pienses asi #portrait:hannibal_happy
+[Soy tan bueno como otros]
    ~ q24 = 0
    Que bueno es ver que pienses asi #portrait:hannibal_happy

- La clave está en dejar de darle importancia a lo que digan los demás. #speaker:Hannibal #portrait:hannibal_neutral #layout:right

- Y no sentirse mal por qué a los demás les vaya bien y a ti no.

- Cada uno va a su propio ritmo y tarde o temprano podrías llegar a ser el mejor en lo que te apasiona.

- Por cierto, ya casi esta la poción...

- Justo a tiempo para regresar al barco, antes de que la salud del Capitán empeore... #speaker:~PlayerUsername #portrait:player_sad #layout:left

- Toma ~PlayerUsername, parte ahora mismo, hay un camino mas corto que yo uso normalmente  #speaker:Hannibal #portrait:hannibal_neutral #layout:right

- Además, escuché que les hacen falta partes para el barco, te daré algo más dirigite al final de la cueva y encontrarás la salida.

~ gotRudder()

-> END

