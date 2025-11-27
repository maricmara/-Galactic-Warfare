using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    
    public PlayerSpaceshipController player;

    [Header("HUD Textos")]
    public TextMeshProUGUI weaponText;
    public TextMeshProUGUI rocketAmmoText;
    public TextMeshProUGUI laserEnergyText;
    public TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        ScoreManager.OnScoreChanged += UpdateScore;
    }

    private void OnDisable()
    {
        ScoreManager.OnScoreChanged -= UpdateScore;
    }

    void Update()
    {
        if (player == null) return;

        // --- ARMA ATUAL ---
        string w = player.CurrentShot switch
        {
            1 => "Padrão",
            2 => "Foguete",
            3 => "Laser",
            _ => "Desconhecido"
        };
        weaponText.text = "Arma: " + w;

        // --- MUNIÇÃO DE FOGUETES ---
        rocketAmmoText.text = "Foguete: " + player.ammoRocket;

        // --- ENERGIA DO LASER ---
        laserEnergyText.text = "Laser: " + player.laserEnergy.ToString("0") + "%";
    }

    private void UpdateScore(int newScore)
    {
        scoreText.text = "Score: " + newScore;
    }
}
