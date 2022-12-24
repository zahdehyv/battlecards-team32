# Battle Cards

## Introducción

Bienvenido a nuestro juego de cartas Battle Cards(Estamos buscando un nombre original así que si se le ocurre alguno no dude en decirnos).

## ¿Qué es Battle Cards?

Battle Cards es un juego de cartas coleccionables super original que simula una lucha entre dos ejércitos de un mundo de fantasía génerico
número 52.

## ¿Cómo abrir Battle Cards?

Para ejecutar Battle Cards necesitas escribir dotnet run en una terminal abierta en la carpeta del juego. 

## Main Menu

Cuando abras el juego y recibas la bienvenida entrarás en el menú principal dónde tendrás dos opciones BuildDecks y Exit. Estoy seguro que no
necesitas explicación sobre lo que hace Exit. La otra opción de función no tan obvia es la encargada de generar los mazos de cartas para el juego
(Explicación más detallada en el informe del proyecto). Para empezar a jugar primero debes generar los mazos. Luego de pulsar esta opción se compilarán los mazos y tendrás la opción de Jugar. Selecciona esta para tener toneladas de diversión ;)

## Creando los jugadores 

Lo primero antes de empezar a jugar es crear los jugadores para esto te voy a pedir que me des el nombre del Player 1, selecciones su deck y me digas si es un jugador virtual(O sea, controlado por la computadora. Detalles de su funcionamiento en el informe del proyecto). Luego haces lo mismo para el Player 2.

## Game Flow

El juego alterna entre los turnos de cada jugador. Cada turno consiste de una fase de invocación y una fase de acción. El juego termina cuando uno de los jugadores pierda todas las cartas.

### Cartas 

Las cartas tienen 4 estadísticas Vida, Ataque, Defensa y Velocidad. La vida determina si la carta continua en juego o no, el ataque determina cuanta vida quita esa carta con sus acciones, la defensa determina cuanto de este ataque es desvíado y no sufre la vida y la velocidad determina el orden de ejecución de los ataques.

### Invocación de Cartas

El juego comienza ahora. Cada jugador tiene un campo con 4 espacios para cartas, solo existe un tipo de carta en el juego. Los jugadores están obligados a ocupar todos los espacios de su campo si tienen suficientes cartas en su mazo, al inicio de cada turno los jugadores colocan sus cartas elegidas directamente de su mazo, luego van a la elección de acciones(Excepto en la primera ronda que es sólo de invocación).

### Elección de acciones 

Después de colocar las cartas es la hora de atacar para esto eliges la carta que realizará la acción y se te mostrarán todas las posibles acciones que esta carta puede tomar.Todas las cartas tienen 4 acciones predeterminadas: 
-Atacar: Que le quita vida a la carta objetivo
-Defender: Que aumenta la defensa de la carta objetivo
-Debilitar: Que disminuye la defensa de la carta objetivo
-No hacer nada: No hace nada
Todas las cartas del juego poseen estas acciones. Pero algunas cartas pueden tener otras acciones que hacen todo tipo de efectos.
Cuando elijas una acción debes escoger hacia dónde va a estar dirigida (Hacia tí o el contrario) y hacia que carta está orientada. Todas las cartas tienen posibilidad de tomar una acción por turno, así que no las desperdicies.

## Despedida 

Espero que haya entendido correctamente el funcionamiento de nuestro juego y lo disfrute. Muchas Gracias.



