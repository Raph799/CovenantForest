using UnityEngine;

public class Maguinho : MonoBehaviour
{
    public Transform player;
    public float distanciaDeseada = 5f;
    public float velocidadOrbita = 1f;
    public float intervaloDeDisparo = 2f;
    public GameObject proyectilPrefab;
    public int vidaInicial = 100;

    private int vida;
    private float tiempoUltimoDisparo;

    void Start()
    {
        vida = vidaInicial;
    }

    void Update()
    {
        if (player != null)
        {
            // Calcula la dirección hacia el jugador
            Vector3 direccionAlJugador = player.position - transform.position;
            direccionAlJugador.y = 0f; // Mantén el enemigo en el mismo plano que el jugador

            // Calcula la distancia actual al jugador
            float distanciaAlJugador = direccionAlJugador.magnitude;

            // Calcula la posición objetivo basada en la distancia deseada
            Vector3 posicionObjetivo = player.position - direccionAlJugador.normalized * distanciaDeseada;

            // Mueve al enemigo hacia la posición objetivo
            transform.position = Vector3.Lerp(transform.position, posicionObjetivo, velocidadOrbita * Time.deltaTime);

            // Gira alrededor del jugador
            transform.RotateAround(player.position, Vector3.up, velocidadOrbita * 30f * Time.deltaTime);

            // Disparo cada intervaloDeDisparo segundos
            if (Time.time - tiempoUltimoDisparo > intervaloDeDisparo)
            {
                Disparar();
                tiempoUltimoDisparo = Time.time;
            }
        }
    }

    void Disparar()
    {
        if (vida > 0)
        {
            // Instancia el proyectil en la posición del enemigo, mirando hacia el jugador
            GameObject proyectil = Instantiate(proyectilPrefab, transform.position, Quaternion.LookRotation(player.position - transform.position));
            // Ajusta la velocidad del proyectil según sea necesario
            proyectil.GetComponent<Rigidbody>().velocity = proyectil.transform.forward * 10f;
        }
    }

    // Función para recibir daño y reducir la vida del enemigo
    public void RecibirDanio(int cantidadDanio)
    {
        vida -= cantidadDanio;

        if (vida <= 0)
        {
            // Aquí puedes agregar lógica para manejar la muerte del enemigo (por ejemplo, reproducir una animación, desactivar el GameObject, etc.)
            Destroy(gameObject);
        }
    }
}
