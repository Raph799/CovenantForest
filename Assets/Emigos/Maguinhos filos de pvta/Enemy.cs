using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public int damage = 10;

    public float speed = 5f;
    private Transform player;

    [SerializeField] EnemyHealthUI healthBar;

    private void Awake()
    {
        healthBar = GetComponentInChildren<EnemyHealthUI>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    private void Update()
    {
        // Moverse hacia el jugador
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verificar si ha chocado con el jugador
        if (collision.transform.CompareTag("Player"))
        {
            // Aplicar daño al jugador
            collision.transform.GetComponent<PlayerHealthAndMana>().TakeDamage(damage);

            // Opcional: Destruir al enemigo al tocar al jugador
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);

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
