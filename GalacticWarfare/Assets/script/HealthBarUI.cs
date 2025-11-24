using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [Header("Imagem que enche/esvazia")]
    public Image fillImage;

    // Espera receber um valor entre 0 e 1
    public void UpdateHealthBar(float fillAmount)
    {
        fillImage.fillAmount = fillAmount;
    }
}
