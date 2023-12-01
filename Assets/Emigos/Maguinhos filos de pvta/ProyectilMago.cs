using UnityEngine;

public class ProyectilMago : MonoBehaviour
{
    public int damage = 5;  // Daño del proyectil al jugador
    public float destruccionTiempo = 5f;  // Tiempo después del cual se destruirá el proyectil

    void Start()
    {
        // Invocar la función DestroyProjectile después del tiempo especificado
        Invoke("DestroyProjectile", destruccionTiempo);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Acceder al script de salud y mana del jugador
            PlayerHealthAndMana playerHealth = other.GetComponent<PlayerHealthAndMana>();

            // Aplicar el daño al jugador
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            // Destruir el proyectil al colisionar con el jugador
            DestroyProjectile();
        }
    }

    // Función para destruir el proyectil
    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
