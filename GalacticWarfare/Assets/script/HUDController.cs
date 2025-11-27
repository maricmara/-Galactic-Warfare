using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
    public Image playerHPFill;

    public TextMeshProUGUI weaponText;
    public TextMeshProUGUI rocketAmmoText;
    public TextMeshProUGUI laserEnergyText;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;

    public GameObject bossHPPanel;
    public Image bossHPFill;

    public void UpdatePlayerHP(float current, float max)
    {
        playerHPFill.fillAmount = current / max;
    }

    public void UpdateWeapon(string weaponName)
    {
        weaponText.text = weaponName;
    }

    public void UpdateRocketAmmo(int value)
    {
        rocketAmmoText.text = value.ToString();
    }

    public void UpdateLaserEnergy(float percent)
    {
        laserEnergyText.text = percent.ToString("F0") + "%";
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void UpdateLives(int lives)
    {
        livesText.text = "x" + lives;
    }

    public void ShowBossHP(bool state)
    {
        bossHPPanel.SetActive(state);
    }

    public void UpdateBossHP(float current, float max)
    {
        bossHPFill.fillAmount = current / max;
    }
}
