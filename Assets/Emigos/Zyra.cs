using UnityEngine;

public class Zyra : MonoBehaviour
{
    public int healthDamage = 10; // Cantidad de da�o a la vida que causar� al jugador
    public int manaCost = 10; // Cantidad de man� que se restar� al jugador
    public float destroyDelay = 5f; // Tiempo en segundos antes de destruir el objeto

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Asegurarse de que el objeto de da�o tenga una masa elevada para minimizar las desviaciones por colisiones
        rb.mass = 1000f; // Puedes ajustar este valor seg�n tus necesidades
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // Obtener la instancia del script PlayerHealthAndMana en el jugador
            PlayerHealthAndMana playerHealth = collision.collider.GetComponent<PlayerHealthAndMana>();

            // Verificar si se encontr� el script en el jugador
            if (playerHealth != null)
            {
                // Aplicar da�o a la vida y consumir man� al jugador
                playerHealth.TakeDamage(healthDamage);
                playerHealth.UseMana(manaCost);
            }

            // Programar la destrucci�n del objeto despu�s del tiempo especificado
            Destroy(gameObject, destroyDelay);
        }
    }
}
