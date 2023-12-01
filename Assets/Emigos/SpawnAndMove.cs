using UnityEngine;

public class SpawnAndMove : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public float speed = 5f;
    public float tiempoEntreSpawns = 2f;

    void Start()
    {
        // Iniciar la invocaci�n peri�dica de SpawnAndMovePrefab
        InvokeRepeating("SpawnAndMovePrefab", 0f, tiempoEntreSpawns);
    }

    void SpawnAndMovePrefab()
    {
        // Obtener la posici�n actual del jugador
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector3 playerPosition = player.transform.position;

            // Calcular la direcci�n hacia el jugador (solo en los ejes X y Z)
            Vector3 direction = new Vector3(playerPosition.x - transform.position.x, 0f, playerPosition.z - transform.position.z).normalized;

            // Spawnear el prefab
            GameObject spawnedPrefab = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);

            // Obtener el componente Rigidbody del prefab
            Rigidbody prefabRigidbody = spawnedPrefab.GetComponent<Rigidbody>();

            // Mover el prefab en la direcci�n del jugador (solo en los ejes X y Z)
            if (prefabRigidbody != null)
            {
                prefabRigidbody.velocity = new Vector3(direction.x * speed, 0f, direction.z * speed);
            }
            else
            {
                Debug.LogError("El prefab no tiene un componente Rigidbody.");
            }
        }
        else
        {
            Debug.LogError("No se encontr� un objeto con la etiqueta 'Player'. Aseg�rate de etiquetar al jugador correctamente.");
        }
    }
}
