using UnityEngine;

public class Hachinha : MonoBehaviour
{
    public int damageAmount = 10; // Cantidad de da�o que causa el objeto

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto ha colisionado con el jugador
        if (other.CompareTag("Player"))
        {
            // Obtener el componente PlayerHealthAndMana del jugador
            PlayerHealthAndMana playerHealth = other.GetComponent<PlayerHealthAndMana>();

            // Aplicar el da�o al jugador
            playerHealth.TakeDamage(damageAmount);
        }
    }
}
