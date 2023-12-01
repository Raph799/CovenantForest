using UnityEngine;
using System;

public class MagoController : MonoBehaviour
{
    public Transform player;
    public float distanciaDeseada = 5f;
    public float velocidadOrbita = 1f;
    public float intervaloDeDisparo = 2f;
    public GameObject proyectilPrefab;
    public int vidaInicial = 100;
    public GameObject[] enemigosSecundarios; // Asigna los enemigos secundarios desde el Inspector
    bool activeOnce = false;
    public GameObject indicadorRecibirDanio; // Asigna el GameObject desde el Inspector

    private int vida;
    private float tiempoUltimoDisparo;
    private bool puedeRecibirDanio = true;

    void Start()
    {
        vida = vidaInicial;
        ActualizarIndicadorRecibirDanio();
    }

    void Update()
    {
        if (player != null)
        {
            bool oneAlive = false;
            foreach (GameObject enemigoSecundario in enemigosSecundarios)
            {
                if (enemigoSecundario != null && enemigoSecundario.activeInHierarchy)
                {
                    oneAlive = true;
                }
            }
            puedeRecibirDanio = !oneAlive;
            ActualizarIndicadorRecibirDanio();

            // Calcula la direcci�n hacia el jugador
            Vector3 direccionAlJugador = player.position - transform.position;
            direccionAlJugador.y = 0f; // Mant�n el enemigo en el mismo plano que el jugador

            // Calcula la distancia actual al jugador
            float distanciaAlJugador = direccionAlJugador.magnitude;

            // Calcula la posici�n objetivo basada en la distancia deseada
            Vector3 posicionObjetivo = player.position - direccionAlJugador.normalized * distanciaDeseada;

            // Mueve al enemigo hacia la posici�n objetivo si puede recibir da�o
            if (puedeRecibirDanio)
            {
                transform.position = Vector3.Lerp(transform.position, posicionObjetivo, velocidadOrbita * Time.deltaTime);
            }

            // Gira alrededor del jugador
            transform.RotateAround(player.position, Vector3.up, velocidadOrbita * 30f * Time.deltaTime);

            // Verifica si la vida est� por debajo del 50%
            if (vida <= vidaInicial * 0.5f)
            {
                // Activa los enemigos secundarios si no han sido activados a�n
                ActivarEnemigosSecundarios();
            }

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
            // Instancia el proyectil en la posici�n del enemigo, mirando hacia el jugador
            GameObject proyectil = Instantiate(proyectilPrefab, transform.position, Quaternion.LookRotation(player.position - transform.position));
            // Ajusta la velocidad del proyectil seg�n sea necesario
            proyectil.GetComponent<Rigidbody>().velocity = proyectil.transform.forward * 10f;
        }
    }

    void ActivarEnemigosSecundarios()
    {
        if(activeOnce == false) { 
            foreach (GameObject enemigoSecundario in enemigosSecundarios)
            {
                // Activa cada enemigo secundario si no est� activado
                if (!enemigoSecundario.activeSelf)
                {
                    enemigoSecundario.SetActive(true);
                }
            }
            activeOnce = true;
            // Desactiva la capacidad de recibir da�o del enemigo principal solo si hay enemigos secundarios activos
            puedeRecibirDanio = false;
            ActualizarIndicadorRecibirDanio();
        }
    }

    // Funci�n para recibir da�o y reducir la vida del enemigo
    public void RecibirDanio(int cantidadDanio)
    {
        if (puedeRecibirDanio)
        {
            vida -= cantidadDanio;

            if (vida <= 0)
            {
                // Aqu� puedes agregar l�gica para manejar la muerte del enemigo (por ejemplo, reproducir una animaci�n, desactivar el GameObject, etc.)
                Destroy(gameObject);
            }
        }
    }

    void ActualizarIndicadorRecibirDanio()
    {
        if (indicadorRecibirDanio != null)
        {
            indicadorRecibirDanio.SetActive(!puedeRecibirDanio);
        }
    }
}
