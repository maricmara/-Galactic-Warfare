using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vida do Jogador")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Barra de Vida")]
    public Image healthFill; // Arraste o HealthBarFill aqui!

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth < 0)
            currentHealth = 0;

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Debug.Log("Jogador morreu!");
            // Aqui depois colocamos animação, reinício, etc.
        }
    }

    void UpdateHealthUI()
    {
        if (healthFill != null)
        {
            healthFill.fillAmount = currentHealth / maxHealth;
        }
    }
    
}
