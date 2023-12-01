using UnityEngine;
using System.Collections;

public class Hacha : MonoBehaviour
{
    public float velocidadRotacion = 100.0f;
    public float duracionCorte = 0.2f;
    public Transform puntoCorte;
    public float distanciaActivacion = 2.0f; // Distancia a la que se activa la acción

    private bool cortando = false;
    private Transform player; // Referencia al jugador

    Quaternion initialLocalRotation;

    private void Start()
    {
        initialLocalRotation = this.transform.localRotation;
        transform.GetChild(0).gameObject.SetActive(false);

        // Buscar el jugador en la escena (asegúrate de que el jugador tenga un tag "Player")
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Verificar si el jugador está cerca y presiona el botón izquierdo del ratón
        if (Vector3.Distance(transform.position, player.position) < distanciaActivacion && !cortando)
        {
            StartCoroutine(RealizarCorte());
        }
    }

    IEnumerator RealizarCorte()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        cortando = true;

        Quaternion rotacionInicial = transform.rotation;

        Quaternion rotacionFinal = transform.rotation * Quaternion.Euler(0, 0, 90);
        float tiempoPasado = 0.0f;

        while (tiempoPasado < duracionCorte)
        {
            tiempoPasado += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(rotacionInicial, rotacionFinal, tiempoPasado / duracionCorte);
            yield return null;
        }

        transform.rotation = rotacionInicial;

        cortando = false;
        transform.GetChild(0).gameObject.SetActive(false);
        this.transform.localRotation = initialLocalRotation;
    }
}
