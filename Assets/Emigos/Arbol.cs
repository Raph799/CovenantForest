using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arbol : MonoBehaviour
{
    public GameObject enemigoPrefab;
    public Transform[] spawners;
    public float tiempoEntreSpawns = 5f;
    public int maxHealth = 100;
    private int currentHealth;

    [SerializeField] EnemyHealthUI healthBar;
    public GameObject emptyObject; // Asigna el emptyObject en el Inspector
    List<GameObject> spawnedEnemies = new List<GameObject>();

    private void Awake()
    {
        healthBar = GetComponentInChildren<EnemyHealthUI>();
    }

    private void Start()
    {
        StartCoroutine(SpawnearEnemigos());
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    IEnumerator SpawnearEnemigos()
    {
        while (true)
        {
            yield return new WaitForSeconds(tiempoEntreSpawns);

            foreach (Transform spawner in spawners)
            {
                SpawnearEnemigo(spawner.position);
            }
        }
    }

    void SpawnearEnemigo(Vector3 posicion)
    {
        GameObject spawned = Instantiate(enemigoPrefab, posicion, Quaternion.identity);

        spawnedEnemies.Add(spawned);
    }

    public void TakeDamage(int damage)
    {

        spawnedEnemies.RemoveAll(obj => obj == null);

        if (spawnedEnemies.Count > 0) return;

        currentHealth -= damage;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        // Verificar si la vida ha llegado a la mitad
        if (currentHealth <= maxHealth / 2)
        {
            // Activar el emptyObject si la vida está a la mitad o menos
            if (emptyObject != null)
            {
                emptyObject.SetActive(true);
            }
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Agregar aquí la lógica de muerte del enemigo si es necesario
        Destroy(gameObject);
    }
}
