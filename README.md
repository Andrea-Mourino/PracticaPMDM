Primeramente cree el Plato el cual seria el mapa del juego. 

Luego cree la pelota con un Tag player y con su Script que se llama PlayerController

(meter script)

Tambien cree el enemigo que es un cuadrado y cree otro Script llamado EnemigoIa. Para designar que este persiga al player en las opciones del Script dentro de Unity hay que poner bola: Player(Transform).

(meter script)

Luego meti monedas, creando una esfera y dandole forma, ademas de crear una carpeta materiales y designandole un color. Aqui tambien cree un Script llamado Rotator donde le digo que pueda girar. Tambien dentro del Unity le asigno como Trigger para que cuadno lo toque desaparezca

(meter script)

Luego cree un Canvan donde hice un UI con un Text para signar en la Interfaz una Puntiacion. Para que pudiera contar las monedas le meti un metodo dentro del Script PlayerController donde le asigno que cada vez que el jugador agarre una moneda este cuente uno (como maximo le asigne 3). Tambien le asigne una variable count

Hice lo mismo con WIN y PERDER. Cree otro text y dentro del mismo Script le asigne otro metodo que en caso de conseguir todas las monedas te salga en pantalla WIN! y en caso de que el Enemigo te pille te salga perder.

Para ajustar en la pantalla el text en cuestion tienes que ir a Unity y moverlo manualmente o darle shift a una opcion para que te lo centre a la izquierda, derecha, ect...

(meter script)

Todo lo que es la velocidad del enemigo, del jugador y todo eso lo puedes modificar dentro del Script o desde Unity, como prefieras.
