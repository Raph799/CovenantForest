using UnityEngine;

public class ProyectilMago : MonoBehaviour
{
    public int damage = 5;  // Da�o del proyectil al jugador
    public float destruccionTiempo = 5f;  // Tiempo despu�s del cual se destruir� el proyectil

    void Start()
    {
        // Invocar la funci�n DestroyProjectile despu�s del tiempo especificado
        Invoke("DestroyProjectile", destruccionTiempo);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Acceder al script de salud y mana del jugador
            PlayerHealthAndMana playerHealth = other.GetComponent<PlayerHealthAndMana>();

            // Aplicar el da�o al jugador
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            // Destruir el proyectil al colisionar con el jugador
            DestroyProjectile();
        }
    }

    // Funci�n para destruir el proyectil
    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
