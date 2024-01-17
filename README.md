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

Niveles: 14


 </td>
    <td> <b>Hitos:</b>

1. Propuesta del concepto: 5 de octubre de 2023
2. Prototipo jugable: 7 de noviembre de 2023
3. Juego completo: 17 de enero de 2024


 </td>
        
    
</tr>

</table>

### Índice

1. [Objetivo educativo](#objetivoeducativo)
2. [Contexto de uso](#contextouso)
3. [Progresión del juego](#progresionjuego)
    1. [Nivel 1-3: Tutorial - Comprender las coordenadas X, Y](#tutorial-slider)
    2. [Nivel 4-6: Tutorial - Leyes de Newton](#tutorial)
    3. [Niveles 7-8: Aprendizaje Newton](#aprendizaje)
    4. [Niveles 9-12: Fuerza de Rozamiento y choque elástico](#nivelrozamiento)
    5. [Niveles 13-14: Plano Inclinado](#planoinclinado)
5. [Partida típica](#partidatipica)
6. [Controles](#controles)
7. [Gameplay](#gameplay)
    1. [Pelota](#pelota)
    2. [Flechas cinemáticas](#flechascinematicas)
    3. [Coche motorizado](#coche)
    4. [Dron](#dron)
    5. [Meta](#meta)
    6. [Fuerza de rozamiento](#rozamiento)
    7. [Gravedad](#gravedad)
8. [Menús](#menus)
    1. [Menú principal](#menuprincipal)
    2. [Configuración](#menuconfiguracion)
    3. [Selección de niveles](#menuniveles)
9. [Interfaz](#interfaz)
10. [Guardado](#guardado)
11. [Sonidos](#sonidos)
12. [Juegos educativos similares](#juegoseducativos)

## 1. Objetivo educativo <a name="objetivoeducativo"></a>
El objetivo principal es ayudar a los alumnos de instituto a visualizar vectores, para que puedan comprender el concepto que representan y entiendan que está sucediendo al sumarlos. Todos estos conceptos son útiles en la asignatura de física que se da en secundaria y bachillerato.

Hemos preguntado a una profesora de física de bachillerato qué tema es el que más cuesta a los alumnos. Resulta que uno de los mayores problemas es que los vectores suponen un pico de dificultad para los alumnos, y estos son vitales de entender para temas como cinemática, dinámica, gravitación y electromagnetismo.

Por todo esto, con Mini Golf pretendemos amenizar la introducción de vectores a los alumnos.

Además, el juego introduce progresivamente conceptos físicos como las Leyes de Newton, la fuerza de rozamiento, descomposición de fuerzas y problemas de plano y plano inclinado.

## 2. Contexto de uso <a name="contextouso"></a>
El juego está diseñado para jugarse en las salas de informática de los institutos, accediendo desde el navegador o descargando el juego.

El juego se puede jugar durante los 50 minutos que dura la hora, dando tiempo a una explicación inicial, a que los alumnos abran el juego y un conjunto de niveles que entre 15 y 30 minutos puedan completarse. Además, si algún alumno tiene interés puede repetir los niveles desde casa. 

## 3. Progresión del juego <a name="progresionjuego"></a>

Vector Golf consta de 14 niveles divididos en 4 bloques que añaden y exploran nuevas mecánicas. Al comenzar el primer nivel de cada bloque, se mostrará una interfaz a modo de tutorial que explicará las mecánicas empleadas en dicho nivel. Por ejemplo, si entramos al primer nivel, se nos explicará cómo jugar y se nos mostrará una breve definición de qué es un vector. Si entramos por primera vez al nivel 9, se explicará en la interfaz qué es la fuerza de rozamiento y cómo cambia la jugabilidad.
Cada nivel tiene 2 números de golpes objetivo para conseguir la bandera de oro y la de platino respectivamente.

El primer bloque se centra en el movimiento de la pelota. El objetivo es que el jugador se familiarice con las leyes de la cinemática en un entorno seguro, comprenda el flujo del juegolas coordenadas X,Y de los vectores  y pueda llegar al siguiente bloque con la información necesaria para continuar.

El segundo bloque profundiza en las leyes de la cinemática con el objetivo de que el jugador comprenda y ponga en práctica sus conocimientos, por primera vez de forma no trivial, reconociendo los vectores de la velocidad y de la fuerza de gravedad.

En el tercer bloque, se introduce la fuerza de rozamiento y los choques elásticos. Primero se aplicarán únicamente en la pelota, pero después se introducirán obstáculos que habrá que empujar y mover utilizando las fuerzas escalares y vectoriales de la interfaz. Estos obstáculos son principalmente cajas que se pueden mover con elementos como vehículos motorizados o cuerdas que tiran con poleas motorizadas.

En el último bloque se explorarán los planos inclinados, con su descomposición de fuerzas, y encontraremos niveles que pongan a prueba el conocimiento del jugador.

### Nivel 1-3: Tutorial - Comprender las coordenadas X, Y <a name="tutorial-slider"></a>
En los primeros niveles, los jugadores aprenderán los conceptos básicos de los vectores. No habrá ningún elemento complejo y el nivel solo contará con controles deslizantes para controlar la pelota (u otro objeto). 

#### Nivel 1: 
Nivel simple en el que solo disponemos del control deslizante de la pelota, y no cuenta con ningún obstáculo. 
La gravedad y la fuerza de rozamiento están  desactivadas. Usando los controles deslizantes modificaremos la velocidad de la flecha en tiempo real. 

![nivel1](https://github.com/javics2002/JuegosSerios/assets/75903737/533c7d19-55ad-4b05-acae-0ff6faefe965)

#### Nivel 2: 
Se introducen obstáculos que el jugador deberá evitar para meter la pelota en el hoyo. 

![nivel2](https://github.com/javics2002/JuegosSerios/assets/75903737/28015c7e-00f4-4312-98bd-80bb78d98013)

#### Nivel 3: 
Se introduce el control deslizable en otros objetos, en este caso una caja. El jugador deberá controlar la caja para empujar la pelota metiendola en el hoyo.

![nivel3](https://github.com/javics2002/JuegosSerios/assets/75903737/710a9ced-1b2a-40b5-b90d-3c6ed7e84116)

### Nivel 4-6: Tutorial - Leyes de Newton <a name="tutorial"></a>
En los primeros niveles, se aprenderán los conceptos básicos del movimiento y las mecánicas del juego. No habrá ningún elemento complejo y el nivel solo contará con fuerzas arrastrables desde la interfaz para mover la pelota. Este tutorial establecerá la base para comprender la física y las mecánicas que guiarán el juego.

Concepto de Vectores (explicado en el tutorial):
Un vector es una entidad matemática que tiene magnitud y dirección. En el contexto del juego, los vectores se utilizan para representar fuerzas que actúan sobre el pistón y determinan su movimiento. La magnitud del vector representa la fuerza, y su dirección indica la dirección del movimiento.

#### Nivel 4: 
Nivel simple, en el que solo disponemos de dos fuerzas vectoriales que aseguren que el nivel se complete. La gravedad y la fuerza de rozamiento comienzan desactivadas. En este nivel veremos el tutorial de la primera ley de newton y de cómo jugar, acompañados de imágenes y focos que dirijan la atención hacia un elemento concreto. Platino: 1

Todo cuerpo preserva su estado de reposo o movimiento uniforme y rectilíneo a no ser que sea obligado a cambiar su estado por fuerzas impresas sobre él.

![nivel4](https://github.com/javics2002/JuegosSerios/assets/75903737/4001b5a9-7ba4-4ebd-be1f-13dfd925cd95)

#### Nivel 5: 
Este nivel enseña la segunda Ley de Newton, e introduce la gravedad para mostrarla. Se disponen de 2 fuerzas vectoriales que ambas encajan la pelota en el primer golpe. Platino: 1

Cuando una fuerza actúa sobre un objeto este se pone en movimiento, acelera, desacelera o varía su trayectoria.

![nivel5](https://github.com/javics2002/JuegosSerios/assets/75903737/22b38c10-8d70-495c-95f1-016a7a3eb79b)


#### Nivel 6: 
Se introduce la tercera Ley de Newton con los primeros rebotes del juego. Al rebotar la pelota se produce una animación en la que se para el tiempo y el vector velocidad se voltea respecto a la superficie del rebote. Los rebotes son completamente elásticos. Platino: 1

Con toda acción ocurre siempre una reacción igual y contraria: o sea, las acciones mutuas de dos cuerpos siempre son iguales y dirigidas en direcciones opuestas.

![nivel6](https://github.com/javics2002/JuegosSerios/assets/75903737/3990fe0a-3710-4584-961e-e5471e5670cb)

### Niveles 7-8: Aprendizaje Newton <a name="aprendizaje"></a>

En este bloque no hay tutoriales, se aplican las 3 leyes de newton a la vez.

#### Nivel 7: 
Nivel sin gravedad en el que hay que hacer la primera suma de vectores, aplicando el segundo vector en el momento justo. Platino: 2

![nivel7](https://github.com/javics2002/JuegosSerios/assets/75903737/7415172a-31d0-4ec3-8255-63331a7e4965)


#### Nivel 8: 
Volvemos a activar la gravedad. En este nivel, la pelota, carente de fuerza de rozamiento y con rebotes completamente elásticos, bota indefinidamente hasta que nosotros la golpeemos en el momento justo para que pase por el agujero en la pared. Platino: 2

![nivel8](https://github.com/javics2002/JuegosSerios/assets/75903737/3c0990a1-d6f9-4c27-aa38-e0d91a0923f8)

### Niveles 9-11: Fuerza de Rozamiento y choque elástico <a name="nivelrozamiento"></a>

#### Nivel 9: 
Se introduce la fricción. En este nivel, hay que usar la fuerza justa para que la pelota no se frene antes de llegar al hoyo ni se caiga al vacío. Platino: 1

La fuerza de fricción es la fuerza que existe entre dos superficies en contacto, que se opone al deslizamiento.

![nivel9](https://github.com/javics2002/JuegosSerios/assets/75903737/52552227-02d7-4a6e-b66e-deefd1e1a160)


#### Nivel 10: 
Se introduce el choque elástico. Ahora la pelota pierde parte de la energía al rebotar, pudiendo quedarse completamente quieta tras un golpe. 

Este nivel utiliza estos choques para colocar la pelota en distintas plataformas de camino a la meta. Platino: 2. Oro: 4

![nivel10](https://github.com/javics2002/JuegosSerios/assets/75903737/9055a692-bfda-41c8-a143-d8c9f9b9e51b)


#### Nivel 11: 
Se introducen elementos de escenario. Hay una caja de masa m y puede ser empujada por un coche motorizado. La idea es apartar una caja para el camino de la pelota al hoyo quede despejado. Platino: 1. Oro: 2

![nivel11](https://github.com/javics2002/JuegosSerios/assets/75903737/b1ae1a18-5dee-4b76-89c3-89a28bb1988c)


#### Nivel 12: 
Se introduce el último elemento de escenario, el dron. Ahora que la pelota se puede quedar quieta, la tenemos que colocar en la plataforma del dron. Hay que usar uno de los vectores para mover el dron y volver a golpear la pelota desde otra posición. Platino: 2. Oro: 3.

![nivel12](https://github.com/javics2002/JuegosSerios/assets/75903737/119faa5a-195c-4727-9f96-bbfcdb159087)


### Niveles 12-14: Plano Inclinado <a name="planoinclinado"></a>

La inclusión de planos inclinados agrega una nueva dificultad al juego, ya que la pelota se moverá en la dirección del plano inclinado y el jugador deberá ajustar la velocidad y la dirección de la pelota para lograr superar estos nuevos obstáculos.

Un plano inclinado es una superficie inclinada con respecto a la horizontal, es decir, superficies que crean una pendiente. Estos planos inclinados pueden afectar significativamente el movimiento de la pelota, generando cambios en la aceleración.

Al descender por un plano inclinado, la gravedad efectiva se suma al componente horizontal, acelerando la pelota. Ascender por un plano inclinado puede reducir la gravedad efectiva, lo que afecta la velocidad de la pelota.

#### Nivel 13:
Para mostrar la descomposición de fuerzas sobre un plano inclinado, el primer nivel de este bloque es muy simple. Contaremos en pantalla con las flechas P, Px, Py, N. Platino: 1. Oro: 2.

![nivel13](https://github.com/javics2002/JuegosSerios/assets/75903737/a8fc131e-6359-4174-baad-bedfc9b1c4cf)


#### Nivel 14: 
Requiere lanzar la pelota aprovechando el plano inclinado. Platino: 1. Oro: 3

![nivel14](https://github.com/javics2002/JuegosSerios/assets/75903737/27f9cc75-ca23-4a97-88ae-be7659892f35)


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
