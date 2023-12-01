using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthAndMana : MonoBehaviour
{
    public int maxHealth = 100;
    public int maxMana = 100;
    private int currentHealth;
    private float currentMana;
    private bool isInvulnerable = false;
    private bool isInvulnerabilityActive = false;

    public int manaCostPerSecond = 10;
    public int manaRegenPerSecond = 2;

    public TextMeshProUGUI healthText; // Referencia al elemento de texto de la vida.
    public TextMeshProUGUI manaText; // Referencia al elemento de texto del maná.

    private void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
        UpdateUI(); // Actualizar la interfaz de usuario al inicio.
    }

    private void Update()
    {
        // Verificar si la tecla Espacio está presionada para activar/desactivar la invulnerabilidad
        if (Input.GetKeyDown(KeyCode.Space) && currentMana >= manaCostPerSecond)
        {
            isInvulnerabilityActive = true;
            isInvulnerable = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isInvulnerabilityActive = false;
            isInvulnerable = false;
        }
        if (currentMana < manaCostPerSecond)
        {
            isInvulnerabilityActive = false;
            isInvulnerable = false;
        }

        // Consumir maná si la invulnerabilidad está activa
        if (isInvulnerabilityActive)
        {
            currentMana -= (manaCostPerSecond * Time.deltaTime);
        }
        else
        {
            // Regenerar maná con el tiempo
            RegenerateManaOverTime();
        }
        

        // Actualizar la interfaz de usuario en cada fotograma.
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        // Verificar si el jugador es invulnerable antes de aplicar daño
        if (!isInvulnerable)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                //Die(); // Puedes implementar tu propia función "Die" aquí.
            }
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public float GetMana()
    {
        return currentMana;
    }

    public void UseMana(int manaCost)
    {
        if (currentMana >= manaCost)
        {
            currentMana -= manaCost;
            // Aquí puedes agregar la lógica para realizar una acción que requiera mana.
        }
    }

    private void RegenerateManaOverTime()
    {
        // Regenerar el maná con el tiempo
        currentMana += manaRegenPerSecond * Time.deltaTime;

        // Asegurarse de que el maná no supere el valor máximo
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
    }

    private void UpdateUI()
    {
        // Actualizar los elementos de texto de vida y maná con los valores actuales.
        healthText.text = "Vida: " + currentHealth + " / " + maxHealth;
        manaText.text = "Maná: " + (int)currentMana + " / " + maxMana;
    }

    private void Die()
    {
        // Agregar la lógica para la muerte del jugador aquí.
        // Por ejemplo, mostrar una pantalla de game over, reiniciar el nivel, etc.
    }
}
