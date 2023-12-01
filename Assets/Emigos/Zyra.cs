using UnityEngine;

public class Zyra : MonoBehaviour
{
    public int healthDamage = 10; // Cantidad de daño a la vida que causará al jugador
    public int manaCost = 10; // Cantidad de maná que se restará al jugador
    public float destroyDelay = 5f; // Tiempo en segundos antes de destruir el objeto

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Asegurarse de que el objeto de daño tenga una masa elevada para minimizar las desviaciones por colisiones
        rb.mass = 1000f; // Puedes ajustar este valor según tus necesidades
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // Obtener la instancia del script PlayerHealthAndMana en el jugador
            PlayerHealthAndMana playerHealth = collision.collider.GetComponent<PlayerHealthAndMana>();

            // Verificar si se encontró el script en el jugador
            if (playerHealth != null)
            {
                // Aplicar daño a la vida y consumir maná al jugador
                playerHealth.TakeDamage(healthDamage);
                playerHealth.UseMana(manaCost);
            }

            // Programar la destrucción del objeto después del tiempo especificado
            Destroy(gameObject, destroyDelay);
        }
    }
}
