using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vida do Jogador")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Barra de Vida")]
    public Image healthFill;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            PlayerDied();
        }
    }

    void PlayerDied()
    {
        // só avisa o GameManager que uma vida foi perdida
        GameManager.Instance.LoseLife(1);

        // reseta HP para próxima vida (apenas se ainda tiver vidas)
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        if (healthFill != null)
            healthFill.fillAmount = currentHealth / maxHealth;
    }
}
