using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerDeEnemigos : MonoBehaviour
{
    public int cantidadDeOleadas = 3;  // Cantidad de oleadas
    public int enemigosPorOleada = 10;  // Cantidad de enemigos por oleada
    public float tiempoEntreOleadas = 5f;  // Tiempo entre oleadas
    public float tiempoEntreEnemigos = 3f;  // Tiempo de espera entre enemigos
    public List<GameObject> enemigosPrefabs;  // Lista de Prefabs de enemigos
    public List<Transform> puntosDeSpawn;  // Lista de puntos de spawn
    private int oleadasCompletadas = 0;
    private int enemigosEliminados = 0;

    void Start()
    {
        StartCoroutine(GenerarOleadas());
    }

    IEnumerator GenerarOleadas()
    {
        while (oleadasCompletadas < cantidadDeOleadas)
        {
            for (int i = 0; i < enemigosPorOleada; i++)
            {
                GenerarEnemigo();
                yield return new WaitForSeconds(tiempoEntreEnemigos);  // Espera el tiempo definido entre enemigos
            }

            // Espera hasta que todos los enemigos de la oleada actual sean eliminados
            while (enemigosEliminados < enemigosPorOleada)
            {
                yield return null;
            }

            enemigosEliminados = 0; // Reinicia la cuenta de enemigos eliminados

            // Espera entre oleadas
            yield return new WaitForSeconds(tiempoEntreOleadas);

            oleadasCompletadas++;
        }

        // Todas las oleadas han sido completadas, puedes realizar alguna acción de victoria aquí.
    }

    void GenerarEnemigo()
    {
        if (enemigosPrefabs.Count == 0)
        {
            Debug.LogError("No se han asignado Prefabs de enemigos en la lista.");
            return;
        }

        if (puntosDeSpawn.Count == 0)
        {
            Debug.LogError("No se han asignado puntos de spawn en la lista.");
            return;
        }

        // Selecciona un Prefab de enemigo al azar de la lista
        GameObject enemigoPrefab = enemigosPrefabs[Random.Range(0, enemigosPrefabs.Count)];
        // Selecciona un punto de spawn al azar de la lista
        Transform puntoSpawn = puntosDeSpawn[Random.Range(0, puntosDeSpawn.Count)];

        // Lógica para instanciar un enemigo desde el Prefab de enemigo seleccionado en el punto de spawn seleccionado
        GameObject enemigo = Instantiate(enemigoPrefab, puntoSpawn.position, puntoSpawn.rotation);
        enemigo.tag = "Enemy";  // Asegúrate de asignar el tag "Enemy" al enemigo
    }

    public void EnemigoEliminado()
    {
        enemigosEliminados++;
    }
}
