# Vector Golf
[GDD](https://docs.google.com/document/d/1Apajk1e-smBtr8f2iwCf4LUKQ7TcWrqK09_-8CG4wkI/edit?usp=sharing])

### Documento de diseño de videojuego
Autores:
* Javier Cano Salcedo, jacano01@ucm.es
* José Miguel Villacañas, josemv03@ucm.es
* Aura Román Cerezo, auraroman@ucm.es

<table>
<tr>
    <td colspan = "2"> <b>Descripción:</b> Juego serio que pretende facilitar la enseñanza de vectores y sus operaciones de cara a la física de nivel bachillerato mediante un juego de mini-golf. </td>
</tr>
<tr>
    <td> <b>Géneros:</b> Educativo, Puzles </td>
    <td> <b>Modos:</b> 1 jugador </td>
</tr>
<tr>
    <td> <b>Público objetivo: </b>  Estudiantes de secundaria y bachillerato de ciencias entre 15 y 18 años </td>
    <td> <b>Plataformas:</b> Windows, WebGL </td>
</tr>
<tr>
    <td> <b>Cantidades:</b>

Niveles: 12


 </td>
    <td> <b>Hitos:</b>

1. Propuesta del concepto: 5 de octubre de 2023
2. Prototipo jugable: 7 de noviembre de 2023


 </td>
        
    
</tr>

</table>

### Índice

1. [Objetivo educativo](#objetivoeducativo)
2. [Contexto de uso](#contextouso)
3. [Progresión del juego](#progresionjuego)
    1. [Nivel 1-3: Tutorial - Leyes de Newton](#tutorial)
    2. [Niveles 4-5: Aprendizaje Newton](#aprendizaje)
    3. [Niveles 6-9: Fuerza de Rozamiento y choque elástico](#nivelrozamiento)
    4. [Niveles 10-12: Plano Inclinado](#planoinclinado)
4. [Partida típica](#partidatipica)
5. [Controles](#controles)
6. [Gameplay](#gameplay)
    1. [Pelota](#pelota)
    2. [Flechas cinemáticas](#flechascinematicas)
    3. [Coche motorizado](#coche)
    4. [Dron](#dron)
    5. [Meta](#meta)
    6. [Fuerza de rozamiento](#rozamiento)
    7. [Gravedad](#gravedad)
7. [Menús](#menus)
    1. [Menú principal](#menuprincipal)
    2. [Configuración](#menuconfiguracion)
    3. [Selección de niveles](#menuniveles)
8. [Interfaz](#interfaz)
9. [Guardado](#guardado)
10. [Sonidos](#sonidos)
11. [Juegos educativos similares](#juegoseducativos)

## 1. Objetivo educativo <a name="objetivoeducativo"></a>
El objetivo principal es ayudar a los alumnos de instituto a visualizar vectores, para que puedan comprender el concepto que representan y entiendan que está sucediendo al sumarlos. Todos estos conceptos son útiles en la asignatura de física que se da en secundaria y bachillerato.

Hemos preguntado a una profesora de física de bachillerato qué tema es el que más cuesta a los alumnos. Resulta que uno de los mayores problemas es que los vectores suponen un pico de dificultad para los alumnos, y estos son vitales de entender para temas como cinemática, dinámica, gravitación y electromagnetismo.

Por todo esto, con Mini Golf pretendemos amenizar la introducción de vectores a los alumnos.

Además, el juego introduce progresivamente conceptos físicos como las Leyes de Newton, la fuerza de rozamiento, descomposición de fuerzas y problemas de plano y plano inclinado.

## 2. Contexto de uso <a name="contextouso"></a>
El juego está diseñado para jugarse en las salas de informática de los institutos, accediendo desde el navegador o descargando el juego.

El juego se puede jugar durante los 50 minutos que dura la hora, dando tiempo a una explicación inicial, a que los alumnos abran el juego y un conjunto de niveles que entre 15 y 30 minutos puedan completarse. Además, si algún alumno tiene interés puede repetir los niveles desde casa. 

## 3. Progresión del juego <a name="progresionjuego"></a>

El juego dispone de 12 niveles, separados en 4 bloques que añadirán y explorarán nuevas mecánicas. Cuando entremos al primer nivel de cada bloque, se mostrará una interfaz a modo de tutorial, que explicará las mecánicas que se empleen en dicho nivel.

Por ejemplo, si entramos al primer nivel, se nos explicará cómo jugar y se nos mostrará una breve definición de qué es un vector; y si entramos por primera vez al nivel 7, se explicará en la interfaz qué es la fuerza de rozamiento y cómo cambia la jugabilidad.

La primera mecánica que se presentará en el juego será el movimiento de la pelota. En este bloque, el objetivo es que el jugador se familiarice con las leyes de la cinemática en un entorno seguro, comprenda el bucle de juego y pueda llegar al siguiente bloque con la información necesaria para continuar.

En el segundo bloque, el objetivo es que el jugador aprenda a identificar los vectores básicos, como la velocidad y la fuerza, mientras se asientan los conocimientos de las leyes de Newton.

En el tercer bloque, se introduce la fuerza de rozamiento y los choques elásticos. Primero se aplicarán únicamente en la pelota, pero después introduciremos obstáculos que habrá que empujar, mover, utilizando las fuerzas escalares y vectoriales de la interfaz. Estos obstáculos son principalmente cajas que se pueden mover con elementos como cochecitos motorizados o cuerdas que tiren con poleas motorizadas.

En el último bloque se explorarán los planos inclinados, con su descomposición de fuerzas, y encontraremos niveles que pongan a prueba el conocimiento del jugador.

### Nivel 1-3: Tutorial - Leyes de Newton <a name="tutorial"></a>
En los primeros niveles, se aprenderán los conceptos básicos del movimiento y las mecánicas del juego. No habrá ningún elemento complejo y el nivel solo contará con fuerzas arrastrables desde la interfaz para mover la pelota. Este tutorial establecerá la base para comprender la física y las mecánicas que guiarán el juego.

Concepto de Vectores (explicado en el tutorial):
Un vector es una entidad matemática que tiene magnitud y dirección. En el contexto del juego, los vectores se utilizan para representar fuerzas que actúan sobre el pistón y determinan su movimiento. La magnitud del vector representa la fuerza, y su dirección indica la dirección del movimiento.

#### Nivel 1: 
Nivel simple, en el que solo disponemos de dos fuerzas vectoriales que aseguren que el nivel se complete. La gravedad y la fuerza de rozamiento comienzan desactivadas. En este nivel veremos el tutorial de la primera ley de newton y de cómo jugar, acompañados de imágenes y focos que dirijan la atención hacia un elemento concreto. Platino: 1

Todo cuerpo preserva su estado de reposo o movimiento uniforme y rectilíneo a no ser que sea obligado a cambiar su estado por fuerzas impresas sobre él.

![nivel1](https://github.com/javics2002/JuegosSerios/assets/75903737/350fb688-1d81-485a-b7a2-9e562766f3fa)

#### Nivel 2: 
Este nivel enseña la segunda Ley de Newton, e introduce la gravedad para mostrarla. Se disponen de 2 fuerzas vectoriales que ambas encajan la pelota en el primer golpe. Platino: 1

Cuando una fuerza actúa sobre un objeto este se pone en movimiento, acelera, desacelera o varía su trayectoria.

![nivel2](https://github.com/javics2002/JuegosSerios/assets/75903737/9729687f-b9bf-4b14-be9d-ab229b0cce61)


#### Nivel 3: 
Se introduce la tercera Ley de Newton con los primeros rebotes del juego. Al rebotar la pelota se produce una animación en la que se para el tiempo y el vector velocidad se voltea respecto a la superficie del rebote. Los rebotes son completamente elásticos. Platino: 1

Con toda acción ocurre siempre una reacción igual y contraria: o sea, las acciones mutuas de dos cuerpos siempre son iguales y dirigidas en direcciones opuestas.

![nivel3](https://github.com/javics2002/JuegosSerios/assets/75903737/27969ddb-e366-4eef-915e-2ac1e7541bc2)

### Niveles 4-5: Aprendizaje Newton <a name="aprendizaje"></a>

En este bloque no hay tutoriales, se aplican las 3 leyes de newton a la vez.

#### Nivel 4: 
Nivel sin gravedad en el que hay que hacer la primera suma de vectores, aplicando el segundo vector en el momento justo. Platino: 2

![nivel4](https://github.com/javics2002/JuegosSerios/assets/75903737/415678bb-66fc-48f0-a856-399814daab70)


#### Nivel 5: 
Volvemos a activar la gravedad. En este nivel, la pelota, carente de fuerza de rozamiento y con rebotes completamente elásticos, bota indefinidamente hasta que nosotros la golpeemos en el momento justo para que pase por el agujero en la pared. Platino: 2

![nivel5](https://github.com/javics2002/JuegosSerios/assets/75903737/51858fb9-0409-44ba-ae4c-f5edd69c7904)

### Niveles 6-9: Fuerza de Rozamiento y choque elástico <a name="nivelrozamiento"></a>

#### Nivel 6: 
Se introduce la fricción. En este nivel, hay que usar la fuerza justa para que la pelota no se frene antes de llegar al hoyo ni se caiga al vacío. Platino: 1

La fuerza de fricción es la fuerza que existe entre dos superficies en contacto, que se opone al deslizamiento.

![nivel6](https://github.com/javics2002/JuegosSerios/assets/75903737/9021235a-ab80-4e4e-b665-485a6fa02cc0)

#### Nivel 7: 
Se introduce el choque elástico. Ahora la pelota pierde parte de la energía al rebotar, pudiendo quedarse completamente quieta tras un golpe. 

Este nivel utiliza estos choques para colocar la pelota en distintas plataformas de camino a la meta. Platino: 2. Oro: 4

![nivel72](https://github.com/javics2002/JuegosSerios/assets/75903737/a7da822c-a8cf-446b-aead-ecb83b5db723)


#### Nivel 8: 
Se introducen elementos de escenario. Hay una caja de masa m y puede ser empujada por un coche motorizado. La idea es apartar una caja para el camino de la pelota al hoyo quede despejado. Platino: 1. Oro: 2

![nivel7](https://github.com/javics2002/JuegosSerios/assets/75903737/29f15c6c-ad98-434d-bda8-ed64f0f6d88f)


#### Nivel 9: 
Se introduce el último elemento de escenario, el dron. Ahora que la pelota se puede quedar quieta, la tenemos que colocar en la plataforma del dron. Hay que usar uno de los vectores para mover el dron y volver a golpear la pelota desde otra posición. Platino: 2. Oro: 3.

![nivel8](https://github.com/javics2002/JuegosSerios/assets/75903737/2dbd1693-1c65-4819-87e4-235d80ce6ad3)

### Niveles 10-12: Plano Inclinado <a name="planoinclinado"></a>

La inclusión de planos inclinados agrega una nueva dificultad al juego, ya que la pelota se moverá en la dirección del plano inclinado y el jugador deberá ajustar la velocidad y la dirección de la pelota para lograr superar estos nuevos obstáculos.

Un plano inclinado es una superficie inclinada con respecto a la horizontal, es decir, superficies que crean una pendiente. Estos planos inclinados pueden afectar significativamente el movimiento de la pelota, generando cambios en la aceleración.

Al descender por un plano inclinado, la gravedad efectiva se suma al componente horizontal, acelerando la pelota. Ascender por un plano inclinado puede reducir la gravedad efectiva, lo que afecta la velocidad de la pelota.

#### Nivel 10:
Para mostrar la descomposición de fuerzas sobre un plano inclinado, el primer nivel de este bloque es muy simple. Contaremos en pantalla con las flechas P, Px, Py, N. Platino: 1. Oro: 2.

![nivel9](https://github.com/javics2002/JuegosSerios/assets/75903737/e1daeeb8-b9c5-48bd-9389-6f3c06d388ce)

#### Nivel 11: 
Nivel complejo que requiera deslizar cajas por el plano inclinado. Platino: 2. Oro: 3

![nivel10](https://github.com/javics2002/JuegosSerios/assets/75903737/5393c006-b0e2-486d-ac53-f03498ef1951)

#### Nivel 12: 
Este nivel cuenta con vectores hacia la derecha, por lo que hay que usar los planos inclinados y la tercera ley de newton (rebotes) para conseguir moverse hacia la izquierda. Platino: 3. Oro: 5

![nivel11](https://github.com/javics2002/JuegosSerios/assets/75903737/fb54bf7a-80e5-47fe-b816-2bcc17ad4d3c)

## 3. Partida típica <a name="partidatipica"></a>
Al comenzar a jugar, el jugador empieza en el menú principal. Pulsaremos jugar y entraremos al menú de selección de niveles. Se empieza con todos los niveles bloqueados, excepto el primero. Pulsamos en cualquier nivel que tengamos desbloqueado para entrar en él e iremos desbloqueando niveles.

Para superar un nivel de ejemplo, arrastraremos las magnitudes desde la parte derecha de la pantalla hasta los elementos disponibles. Por ejemplo, asignaremos una magnitud escalar de “10” al coche motorizado, para que empuje una caja con una fuerza vectorial hacia adelante de magnitud 10. 

Después aplicaremos las magnitudes vectoriales a la pelota para intentar lograr hacer un hoyo y pasar al siguiente nivel. Cuando completemos todos los niveles, se volverá al menú de selección de niveles, todos marcados como completados con la mejor bandera obtenida.

## 4. Controles <a name="controles"></a>
Con el click izquierdo del ratón se podrán arrastrar objetos de la parte derecha de la pantalla, siendo estos las magnitudes escalares y vectoriales que se usarán a la hora de jugar. 

Los menús también se controlan con el click izquierdo del ratón.

## 5. Gameplay <a name="gameplay"></a>
Cada nivel tiene un par, que es el número de golpes objetivo con el que se debería superar un nivel.

### Pelota <a name="pelota"></a>
La pelota es el elemento que el jugador debe hacer llegar a la meta para completar el juego.

La pelota podrá ser golpeada por el jugador soltando una magnitud vectorial encima de ella, y esa magnitud se sumará a su velocidad instantáneamente. 
Cuando la magnitud vectorial se sitúe encima de la pelota, antes de soltarla se verá una preview de la velocidad que se sumará a la pelota. 

Al soltarla, si la pelota está en movimiento, se para el tiempo y la nueva la nueva magnitud comenzará a salir con una animación desde la punta de la velocidad actual.

Luego, una nueva flecha va desde la pelota hasta la suma de los 2, y se reanuda el tiempo. Entonces se suma el nuevo vector velocidad a la velocidad previa de la bola. En caso de que la pelota ya estuviera quieta, la animación se salta.

![image](https://github.com/javics2002/JuegosSerios/assets/75903737/319202ec-dce3-4465-a973-32d790a77fc3)

Las mecánicas descritas en el próximo párrafo pueden estar activas o no dependiendo del nivel en el que estemos. Ver progresión.

La pelota también se ve afectada por fuerzas como la gravedad, el rozamiento u otras producidas por el coche motorizado o el dron. La pelota colisiona con los elementos físicos del nivel, como el suelo o las mecánicas de escenario. La pelota rebotará al tocar el suelo o las paredes debido a que cuentan con un material físico que aplica el rebote de la fuerza de la pelota.

### Flechas cinématicas <a name="flechascinematicas"></a>
Las flechas cinemáticas son flechas que representan un vector cinemático. Las hay de 2 tipos: flechas de velocidad y flechas de fuerza. Las flechas de velocidad son azules y las flechas de fuerza son amarillas.

Siempre que la magnitud de la velocidad y fuerza de la pelota sean mayores de lo que ocupa la punta de la flecha, se dibujarán y actualizarán continuamente para representar los cambios en estas magnitudes. Si una de estas magnitudes es menor que lo que ocupa la punta de la flecha, la flecha deja de pintarse.

La malla de una flecha consta de 3 triángulos: 2 para la línea y 1 para la punta. Se puede modificar la anchura y longitud de estas 2 partes.

Otros elementos que modifiquen la velocidad o fuerzas de la pelota también contarán con su propia flecha del color correspondiente, como el coche, cuya flecha es amarilla; o el dron, cuya flecha es azul.

Al dibujar estas flechas, la flecha de la velocidad es más ancha que la de aceleración y se dibuja por encima de la de aceleración.

Las flechas pretenden hacer visual la relación entre fuerzas, velocidad y posición.

Contamos también con las flechas de preview o interfaz. Estas flechas sólo se dibujarán cuando arrastremos una fuerza del canvas hacia un elemento interactuable válido. Si arrastramos una fuerza vectorial hacia la pelota, se mostrará una flecha a modo de previsualización de la fuerza que hayamos cogido.

La flecha será del mismo color que la velocidad si arrastramos la magnitud a un elemento que modifique la velocidad de la pelota, o del mismo color de la fuerza si el elemento arrastrado modifica la aceleración aplicada a la pelota (como en un coche motorizado).

Si arrastramos una fuerza del canvas fuera de un elemento interactuable válido, la flecha de previsualización desaparecerá, volviendo a mostrar el elemento que habíamos empezado a arrastrar.

### Coche motorizado <a name="coche"></a>
El coche motorizado es un elemento interactuable del escenario. 

Al arrastrar al coche una magnitud escalar, se establecerá la velocidad con la que se moverá el coche. Además, la orientación del coche determina la dirección hacia la cual se moverá. 

El coche empujará de manera constante cualquier elemento en la dirección a la que está orientado. Por ejemplo, al posicionar el coche cerca de una caja y aplicar la fuerza en la dirección correcta, podrás empujar las cajas estratégicamente.

A pesar de su apariencia, internamente se comporta como un simple cubo, simplificando la física del juego. Además, el coche se integra con otras mecánicas del juego, como planos inclinados.

### Dron <a name="dron"></a>
El dron es un aparato volador y, al igual que el coche, es un elemento interactuable del escenario. 

El dron volará con su objeto de forma estática. Cuando se le aplica un vector, el dron toma esa velocidad y se mueve con su objeto de forma constante. El dron y el objeto no pueden sufrir cambios en la rotación.

El dron puede tener un elemento estático que servirá a modo de barrera para impedir el paso o una plataforma en la que la pelota podrá apoyarse y ser transportada por el mismo.

### Meta <a name="meta"></a>
La meta será tendrá el mismo ancho que un tile, que contará con un agujero donde entra la bola. La banderá de la meta podrá variar de color dependiendo de la situación:
- **Default** *(gris azulado oscuro con palo marrón)*: el nivel no ha sido completado.
- **Red** *(rojo con palo blanco)*: el nivel ha sido completado.
- **Gold** *(dorado con palo blanco)*: el nivel ha sido completado con un golpe adicional de la solución más óptima.
- **Platinum** *(gris con palo gris oscuro)*: el nivel ha sido completado con la solución óptima, es decir, el menor número de golpes.

|                                                         default                                                          |                                                       red                                                        |                                                        gold                                                        |                                                          platinum                                                          |
|:------------------------------------------------------------------------------------------------------------------------:|:----------------------------------------------------------------------------------------------------------------:|:------------------------------------------------------------------------------------------------------------------:|:--------------------------------------------------------------------------------------------------------------------------:|
| ![game-flag-default](https://raw.githubusercontent.com/javics2002/JuegosSerios/main/Assets/Images/game-flag-default.png) | ![game-flag-red](https://raw.githubusercontent.com/javics2002/JuegosSerios/main/Assets/Images/game-flag-red.png) | ![game-flag-gold](https://raw.githubusercontent.com/javics2002/JuegosSerios/main/Assets/Images/game-flag-gold.png) | ![game-flag-platinum](https://raw.githubusercontent.com/javics2002/JuegosSerios/main/Assets/Images/game-flag-platinum.png) |

Además, al volver a jugar un nivel, la meta tendrá que aparecer del mismo color con el que se superó con el mínimo número de intentos. 

El trigger que se encarga de mostrar los resultados se encuentra en el agujero propio de la meta. Cuando la pelota colisione con el trigger, se mostrarán los resultados y se podrá pasar al siguiente nivel. Es obligatorio que haya una meta en cada nivel y el comportamiento de la meta no varía dependiendo del nivel.

### Fuerza de rozamiento <a name="rozamiento"></a>
La fuerza de rozamiento depende del material de los objetos del escenario y de las fuerzas presentes en la pelota. 

Supone una fuerza que contrarresta la velocidad de la pelota constantemente hasta que ésta se para. Depende de la masa del objeto.

### Gravedad  <a name="gravedad"></a>
La fuerza de la gravedad depende de cada nivel, y se mantendrá igual durante el transcurso del mismo. 

Dicha gravedad afecta a todos los elementos no estáticos del juego, como la pelota o cualquier obstáculo dinámico.

Además, la fuerza de la gravedad se ve representada en todos los elementos no estáticos con una flecha amarilla, apuntando en la dirección en la que se aplique.

## 7. Menús <a name="menus"></a>
### Menú principal <a name="menuprincipal"></a>
El juego cuenta con un menú principal, donde podrás ir al menú de selección de nivel y al menú de configuración del juego.

![image](https://github.com/javics2002/JuegosSerios/assets/75903737/18cd94f5-f705-44aa-aa31-c96e44f3f4ec)

### Menú de configuración <a name="menuconfiguracion"></a>
Desde el menú de configuración del juego podrás modificar las opciones de audio (nivel de sonido, música).

![image](https://github.com/javics2002/JuegosSerios/assets/75903737/6ffe9e12-08df-4738-a9b4-115caec91cac)

### Menú de selección de niveles <a name="menuniveles"></a>
Desde el menú de selección de niveles podrás elegir el nivel que quieras jugar, aunque los niveles a los que no hayas llegado estarán bloqueados y no serán jugables.

Los niveles superados se marcan con una bandera. La bandera es roja para los niveles superados, dorada si se ha superado en par o mejor y de platino si se ha hecho hoyo en 1.

![image](https://github.com/javics2002/JuegosSerios/assets/75903737/54322c49-fb8c-46c8-92c5-0d45b10c388e)

## 8. Guardado <a name="guardado"></a>
El juego guarda las preferencias de settings y los niveles desbloqueados entre sesiones de juego usando player prefs.

Además, el juego guardará los datos de los niveles de volumen y efectos de sonido del usuario.

## 9. Interfaz <a name="interfaz"></a>
Durante el gameplay, habrá botones para entrar al menú de configuración, resetear el nivel, o salir del nivel al menú principal en la esquina superior izquierda. 

En la esquina superior derecha hay una cuadrícula con fuerzas escalares y vectoriales que el jugador puede arrastrar y aplicar a los elementos interactuables del nivel, como la pelota, el coche motorizado o el dron. Al usar estas fuerzas, desaparecen y si nos quedamos sin fuerzas, el nivel se resetea tras un tiempo de margen. 

En la esquina inferior derecha podemos personalizar la visualización de vectores marcando los siguientes ticks:

* Ver velocidad

![image](https://github.com/javics2002/JuegosSerios/assets/75903737/61706f04-37d7-4792-a718-61cd643d3ea1)

* Ver fuerzas

![image](https://github.com/javics2002/JuegosSerios/assets/75903737/3b1d86cb-89fd-4331-a35f-5d5e7daf9811)

* Ver nombres

![image](https://github.com/javics2002/JuegosSerios/assets/75903737/b8051e56-d5cd-4481-8792-4339f09fd9a0)

* Ver valores

![image](https://github.com/javics2002/JuegosSerios/assets/75903737/49c07825-7345-4736-96fc-94eef2a7d562)

* Ver animaciones:
  si esta desactivado se salta la corrutina de animación de suma de vectores.
  
* Descomposición de fuerzas

![image](https://github.com/javics2002/JuegosSerios/assets/75903737/a7d38bf1-8cce-44e1-bb20-5a43a9ca471e)

<hr>

![image](https://github.com/javics2002/JuegosSerios/assets/75903737/2a2ce946-5088-47fd-91fe-b86ffb1f3284)

Al completar un nivel sale un menú que muestra tu puntuación. El menú cuenta con botones para voler al menú de selección de niveles, reiniciar el nivel y continuar al próximo nivel.

![image](https://github.com/javics2002/JuegosSerios/assets/75903737/ddd06141-c728-4dd2-a4dc-76824cc6ea90)

## 10. Sonidos <a name="sonidos"></a>
https://www.youtube.com/watch?v=2DswTxYNSpU
https://www.youtube.com/watch?v=4Q55aLIZnwE 
https://www.youtube.com/playlist?list=OLAK5uy_k075Trz4q4Vv90OajKQ4tb87igK6XwQEc 

## 11. Juegos educativos similares <a name="juegoseducativos"></a>
En [the physics classroom](https://www.physicsclassroom.com/) hay disponibles una colección de juegos online que simulan un ejercicio físico. [Entre ellos](https://www.physicsclassroom.com/Physics-Interactives/1-D-Kinematics) encontramos juegos para determinar la relación entre espacio recorrido y tiempo, ya sea con el movimiento de un coche, una pelota que cae por una rampa o con gráficos posición-tiempo y velocidad-tiempo. [Otro gurpo de ellos](https://www.physicsclassroom.com/Physics-Interactives/Vectors-and-Projectiles) nos enseñan vectores, desde nombrar vectores y dibujarlos hasta sumarlos. Vector Golf pretende dar un paso más para convertir esta gamificación en un juego serio. Las siguientes capturas pertenecen a [Graphs and Ramps](https://www.physicsclassroom.com/Physics-Interactives/1-D-Kinematics/Graphs-and-Ramps/Graphs-and-Ramps-Interactive) y a [Vector Guessing Game](https://www.physicsclassroom.com/Physics-Interactives/Vectors-and-Projectiles/Vector-Guessing-Game/Vector-Guessing-Game-Interactive).

![image](https://github.com/javics2002/JuegosSerios/assets/49459590/8bf3201a-95ab-45dd-86df-68d5d324cb7b)
![image](https://github.com/javics2002/JuegosSerios/assets/49459590/0fb0f480-84c2-47e2-8ab6-a86c95ba52e7)

[Vector Unknown](https://stemforall2020.videohall.com/presentations/1871) es un juego serio de puzzles en el que tenemos que encontrar la suma de vectores que lleva al conejo hacia la cesta en un tablero cuadriculado, arrastrando vectores predeterminados en la interfaz como lo hace Vector Golf.

![image](https://github.com/javics2002/JuegosSerios/assets/49459590/83a8385d-fa19-47fb-af81-bc4889fd6925)
