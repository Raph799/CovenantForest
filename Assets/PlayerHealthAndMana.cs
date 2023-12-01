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
    public TextMeshProUGUI manaText; // Referencia al elemento de texto del man�.

    private void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
        UpdateUI(); // Actualizar la interfaz de usuario al inicio.
    }

    private void Update()
    {
        // Verificar si la tecla Espacio est� presionada para activar/desactivar la invulnerabilidad
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

        // Consumir man� si la invulnerabilidad est� activa
        if (isInvulnerabilityActive)
        {
            currentMana -= (manaCostPerSecond * Time.deltaTime);
        }
        else
        {
            // Regenerar man� con el tiempo
            RegenerateManaOverTime();
        }
        

        // Actualizar la interfaz de usuario en cada fotograma.
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        // Verificar si el jugador es invulnerable antes de aplicar da�o
        if (!isInvulnerable)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                //Die(); // Puedes implementar tu propia funci�n "Die" aqu�.
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
            // Aqu� puedes agregar la l�gica para realizar una acci�n que requiera mana.
        }
    }

    private void RegenerateManaOverTime()
    {
        // Regenerar el man� con el tiempo
        currentMana += manaRegenPerSecond * Time.deltaTime;

        // Asegurarse de que el man� no supere el valor m�ximo
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
    }

    private void UpdateUI()
    {
        // Actualizar los elementos de texto de vida y man� con los valores actuales.
        healthText.text = "Vida: " + currentHealth + " / " + maxHealth;
        manaText.text = "Man�: " + (int)currentMana + " / " + maxMana;
    }

    private void Die()
    {
        // Agregar la l�gica para la muerte del jugador aqu�.
        // Por ejemplo, mostrar una pantalla de game over, reiniciar el nivel, etc.
    }
}
