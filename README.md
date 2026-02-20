## Primero

Primeramente cree el Plano el cual seria el mapa del juego. 

Luego cree la pelota con un Tag player y con su Script que se llama PlayerController

´´´
public float speed = 15f; 
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    
    private int count;
    private Rigidbody rb;
´´´

- En esta primera sección definimos todos los "ingredientes" que el script necesita. speed controla la velocidad del jugador. Luego tenemos dos variables fundamentales para la interfaz: countText (que usaremos para mostrar el número en pantalla usando el sistema avanzado TextMeshPro) y winTextObject (que es el mensaje grande que aparecerá al final). Por último, creamos dos variables privadas invisibles en el Inspector: count funcionará como nuestra calculadora mental para llevar la cuenta de las monedas, y rb almacenará el motor de físicas de nuestro personaje

´´´
void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        count = 0;
        if (winTextObject != null) winTextObject.SetActive(false);
        SetCountText();
    }
´´´

- El método Start prepara el terreno justo antes de que el jugador empiece a moverse. Primero, atrapa el Rigidbody del jugador y lo guarda en rb. Después, se asegura de que la partida empiece de forma justa: pone el contador de monedas a 0, apaga el cartel de victoria/derrota para que no estorbe en la pantalla (usando SetActive(false)) y llama a la función SetCountText() para que el marcador de la esquina diga "0" desde el primer milisegundo

´´´
void SetCountText() 
    {
        if (countText != null) 
        {
            countText.text = "Count: " + count.ToString();
        }
        
        if (count >= 3)
        {
            if (winTextObject != null) winTextObject.SetActive(true);
            
            GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
            if (enemy != null) Destroy(enemy);
        }
    }
´´´
- Esta función personalizada es el "árbitro" del juego. Primero, actualiza el texto de la pantalla para mostrar la puntuación actual. Acto seguido, comprueba si has alcanzado la condición de victoria: tener 3 o más monedas. Si es así, enciende el texto central de la pantalla mostrando el mensaje que hayas configurado (usualmente "You Win!") y, como premio extra, busca en el mapa a cualquier objeto que tenga la etiqueta "Enemy" y lo destruye instantáneamente para que deje de molestarte

´´´
void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }
´´´
- Como en el script original, FixedUpdate se encarga de las físicas puras. Lee las teclas que estás pulsando de forma horizontal y vertical, y convierte esos datos en un Vector3, que no es más que una dirección en el espacio 3D (X, Y, Z). Al dejar la "Y" en cero, el jugador se mantiene pegado al suelo. Finalmente, le aplica esa fuerza de empuje al Rigidbody del jugador, multiplicada por la velocidad.

´´´
private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Moneda")) 
        {
            other.gameObject.SetActive(false);

            count++;
            SetCountText();
            
            Debug.Log("¡Moneda recogida correctamente! Llevas: " + count);
        }
    }
´´´

- Aquí controlamos la interacción "suave" (objetos fantasma que se pueden atravesar). Cuando el jugador entra en una zona de detección (Trigger), el script comprueba si la etiqueta de ese objeto es "Moneda". Si es así, apaga la moneda para simular que la has recogido, suma 1 a la calculadora interna (count++), y llama al árbitro (SetCountText()) para que actualice la pantalla y compruebe si ya has ganado. Además, deja un mensaje en la consola para ayudarte a comprobar que todo va bien


´´´
private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (winTextObject != null)
            {
                winTextObject.SetActive(true);
                winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
            }
            
            Destroy(gameObject);
        }
    }
´´´

- A diferencia de las monedas, el enemigo tiene colisiones sólidas. Cuando rebotamos físicamente contra un objeto, el código pregunta: "¿Este objeto tiene la etiqueta 'Enemy'?". Si la respuesta es afirmativa, ocurre la tragedia: el juego toma el texto central (el mismo que usábamos para ganar), lo enciende, le cambia el mensaje a la fuerza por un cruel "You Lose!", y finalmente destruye al jugador (Destroy(gameObject)), terminando la partida de forma abrupta.


## Segundo
Tambien cree el enemigo que es un cuadrado y cree otro Script llamado EnemigoIa. Para designar que este persiga al player en las opciones del Script dentro de Unity hay que poner bola: Player(Transform). (Enemigo no tiene Tag)

´´´
public Transform bola; 
    public float velocidad = 3f;
    public float distanciaLimite = 5f;
´´´

- En estas primeras líneas definimos las características principales de nuestro enemigo para que podamos ajustarlas desde el Inspector de Unity. La variable bola es el objetivo al que el enemigo debe prestar atención (tendrás que arrastrar a tu jugador a este hueco en Unity). La velocidad determina lo rápido que se moverá al atacar. Finalmente, distanciaLimite crea una esfera invisible de detección alrededor del enemigo (de 5 metros en este caso); si el jugador cruza esa frontera invisible, el enemigo despertará

´´´
void Update() {
        float distancia = Vector3.Distance(transform.position, bola.position);
´´´

- Entramos en el método Update(), que es el motor de la IA y se ejecuta en cada fotograma del juego. En esta línea específica, el enemigo usa una herramienta matemática de Unity llamada Vector3.Distance. Lo que hace es trazar una línea recta imaginaria desde su propia posición (transform.position) hasta la posición exacta del jugador (bola.position). El resultado de esa medida lo guarda en una cajita de memoria llamada distancia

´´´
if (distancia < distanciaLimite) {
            Debug.Log("Estado: CERCA - Persiguiendo");
            transform.position = Vector3.MoveTowards(transform.position, bola.position, velocidad * Time.deltaTime);
        } else {
            Debug.Log("Estado: LEJOS - Esperando");
        }
    }

´´´

- Aquí es donde el enemigo toma decisiones. Comprueba si la distancia que acaba de medir es más pequeña que su distanciaLimite. Si es así (el jugador ha entrado en su territorio), avisa por consola de que está persiguiendo y usa la función Vector3.MoveTowards. Esta instrucción mueve físicamente al enemigo un poquito más cerca del jugador de forma suave y fluida (gracias al multiplicador Time.deltaTime, que adapta el movimiento a los FPS del ordenador). Si, por el contrario, el jugador está lejos, el código entra en el else, el enemigo se queda completamente quieto y avisa de que está esperando

´´´
private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            Debug.Log("¡CONTACTO DETECTADO! La bola ha tocado al cubo.");
        }
    }
´´´

- Para terminar, el método OnCollisionEnter actúa como el sentido del tacto. Solo se activa cuando el enemigo choca físicamente contra un objeto sólido (es decir, rebotan, no se atraviesan). Cuando detecta un golpe, lee la etiqueta del objeto contra el que ha chocado. Si la etiqueta es "Player", confirma que ha atrapado al jugador lanzando un mensaje de alerta a la consola de Unity


## Tercero
Luego meti monedas, creando una esfera y dandole forma, ademas de crear una carpeta materiales y designandole un color. Aqui tambien cree un Script llamado Rotator donde le digo que pueda girar. Tambien dentro del Unity le asigno como Trigger para que cuadno lo toque desaparezca. Tambien cree un Tag nuego llamado Moneda

´´´
void Update() {
´´´

- el método Update es una función que Unity ejecuta sin parar, una vez por cada fotograma (frame) que dibuja en pantalla. Al poner el código de rotación aquí dentro, nos aseguramos de que el objeto esté girando constantemente, fotograma a fotograma, dándole ese aspecto dinámico continuo

´´´
  // Hace que la moneda rote 45 grados en cada eje por segundo
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
´´´

## Cuarto
Luego cree un Canvan donde hice un UI con un Text para signar en la Interfaz una Puntiacion. Para que pudiera contar las monedas le meti un metodo dentro del Script PlayerController donde le asigno que cada vez que el jugador agarre una moneda este cuente uno (como maximo le asigne 3). Tambien le asigne una variable count

Hice lo mismo con WIN y PERDER. Cree otro text y dentro del mismo Script le asigne otro metodo que en caso de conseguir todas las monedas te salga en pantalla WIN! y en caso de que el Enemigo te pille te salga perder.

Para ajustar en la pantalla el text en cuestion tienes que ir a Unity y moverlo manualmente o darle shift a una opcion para que te lo centre a la izquierda, derecha, ect...

(Todo lo que es la velocidad del enemigo, del jugador y todo eso lo puedes modificar dentro del Script o desde Unity, como prefieras)


Dentro de cada Script se hace un Log para que dentro de Console salga los mensajes de las acciones
