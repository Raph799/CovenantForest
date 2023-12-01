using UnityEngine;

public class Ogro : MonoBehaviour
{
    public float velocidad = 3f; // Velocidad de movimiento del enemigo
    public int vidaMaxima = 100; // Vida m�xima del ogro
    private int vidaActual; // Vida actual del ogro
    private Transform objetivo; // Referencia al jugador
    private bool esInvulnerable = false; // Indica si el ogro es invulnerable
    public GameObject enemigoPrefab; // Prefab del enemigo a invocar
    public Transform[] puntosSpawn; // Puntos de spawn de los enemigos
    public float tiempoEntreInvocaciones = 2f; // Tiempo entre cada invocaci�n
    private float tiempoUltimaInvocacion; // Tiempo de la �ltima invocaci�n
    private int enemigosInvocados = 0; // Contador de enemigos invocados

    void Start()
    {
        // Inicializa la vida actual al valor m�ximo
        vidaActual = vidaMaxima;

        // Encuentra el GameObject del jugador al comienzo del juego
        objetivo = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Verifica si hay un jugador antes de moverse y si no es invulnerable
        if (objetivo != null && !esInvulnerable)
        {
            // Calcula la direcci�n hacia el jugador
            Vector3 direccion = (objetivo.position - transform.position).normalized;

            // Calcula la rotaci�n hacia el jugador (opcional, depende de tu juego)
            Quaternion rotacion = Quaternion.LookRotation(direccion);
            transform.rotation = rotacion;

            // Mueve al enemigo hacia el jugador
            transform.Translate(direccion * velocidad * Time.deltaTime, Space.World);
        }

        // Verifica si es el momento de invocar otro enemigo
        if (esInvulnerable && Time.time - tiempoUltimaInvocacion > tiempoEntreInvocaciones)
        {
            InvocarEnemigo();
            tiempoUltimaInvocacion = Time.time;
        }
    }

    // Funci�n para recibir da�o
    public void RecibirDanio(int cantidad)
    {
        // Verifica si el ogro es invulnerable
        if (esInvulnerable)
        {
            return; // Si es invulnerable, no recibe da�o
        }

        vidaActual -= cantidad;

        // Verifica si la vida llega a la mitad
        if (vidaActual <= vidaMaxima / 2)
        {
            // El ogro se vuelve invulnerable y comienza a invocar enemigos
            esInvulnerable = true;
            velocidad = 0f;
            tiempoUltimaInvocacion = Time.time;
        }

        // Verifica si el ogro ha quedado sin vida
        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    // Funci�n para manejar la muerte del ogro
    void Morir()
    {
        // Puedes agregar aqu� cualquier l�gica adicional cuando el ogro muere
        // Por ejemplo, reproducir una animaci�n de muerte, reproducir un sonido, etc.

        // Finalmente, destruye el GameObject del ogro
        Destroy(gameObject);
    }

    // Funci�n para invocar otro enemigo
    void InvocarEnemigo()
    {
        // Verifica si hay puntos de spawn y el prefab est� asignado
        if (puntosSpawn.Length > 0 && enemigoPrefab != null)
        {
            // Selecciona un punto de spawn aleatorio
            Transform puntoSpawn = puntosSpawn[Random.Range(0, puntosSpawn.Length)];

            // Instancia el enemigo en el punto de spawn
            Instantiate(enemigoPrefab, puntoSpawn.position, puntoSpawn.rotation);

            // Incrementa el contador de enemigos invocados
            enemigosInvocados++;

            // Si se han invocado 10 enemigos, el ogro vuelve a moverse y pierde la invulnerabilidad
            if (enemigosInvocados >= 10)
            {
                esInvulnerable = false;
                velocidad = 3f; // Puedes ajustar la velocidad a la que se mueve el ogro aqu�
            }
        }
    }
}
