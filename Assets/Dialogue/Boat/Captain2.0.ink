INCLUDE ../globals.ink

¡~PlayerUsername, hijo! ¡Haz vuelto! #speaker:Capitan #portrait:captain_neutral #layout:right

-> main

=== main ===

- ¡Qué enorme aventura has tenido! Conseguiste la primer pieza faltante #speaker:Capitan #portrait:captain_happy #layout:right

- Ha sido una experiencia increíble, Capitán! #speaker:~PlayerUsername #portrait:male_player_neutral #layout:right

- Antes de que te pongas cómodo, déjame hacerte una pregunta más. #speaker:Capitan #portrait:captain_neutral #layout:right

- Sin importar si es de aventura, en general ¿Te gusta estar con la gente, o prefieres estar solo? #speaker:Capitan #portrait:captain_neutral #layout:right //12

+[Me gusta estar con la gente]
    ~ q12 = 0
    Que bueno a mi tambien me gusta mucho #portrait:captain_happy
+[Muy a menudo no me gusta estar con la gente] 
    ~ q12 = 1
    Suele pasar hay veces que no queremos estar con la gente #portrait:captain_sad
+[No quiero en absoluto estar con la gente]
    ~ q12 = 2
    Eso me entristece #portrait:captain_sad
    

- En tu mundo donde hay escuela ¿te diviertes ~PlayerUsername? #speaker:Capitan #portrait:captain_neutral #layout:right //21
+[Nunca] 
    ~ q21 = 0
    Me entristece oir eso #portrait:captain_sad
+[De vez en cuando]
    ~ q21 = 1
    A veces es normal que no nos divirtamos #portrait:captain_sad
+[Muchas veces]
    ~ q21 = 2
    Eso me alegra demasiado#portrait:captain_happy
    
- Sigamos reparando el barco ~PlayerUsername, ¿a qué lugar te gustaría ir ahora? Dirigite al portal y elige tu destino! #speaker:Capitan #portrait:captain_happy #layout:right

->END