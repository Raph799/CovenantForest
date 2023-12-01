using UnityEngine;

public class Hachinha : MonoBehaviour
{
    public int damageAmount = 10; // Cantidad de daño que causa el objeto

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto ha colisionado con el jugador
        if (other.CompareTag("Player"))
        {
            // Obtener el componente PlayerHealthAndMana del jugador
            PlayerHealthAndMana playerHealth = other.GetComponent<PlayerHealthAndMana>();

            // Aplicar el daño al jugador
            playerHealth.TakeDamage(damageAmount);
        }
    }
}
