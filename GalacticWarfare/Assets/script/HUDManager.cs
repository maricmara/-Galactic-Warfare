using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [Header("Score e Vidas")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;

    [Header("Boss UI")]
    public GameObject bossPanel;
    public Slider bossSlider;
    public IntEventChannel bossHpEvent;

    [Header("Weapon Panel")]
    public TextMeshProUGUI weaponText;
    public TextMeshProUGUI rocketAmmoText;
    public TextMeshProUGUI laserEnergyText;
    public PlayerSpaceshipController player;

    [Header("Eventos")]
    [SerializeField] private IntEventChannel scoreEvent;
    [SerializeField] private IntEventChannel livesEvent;

    private void Start()
    {
        // Inicializa boss panel
        if (bossPanel != null) bossPanel.SetActive(false);
        if (bossSlider != null) bossSlider.maxValue = 500;
        if (bossSlider != null) bossSlider.value = 500;

        // Atualiza WeaponPanel
        UpdateWeaponUI();
    }

    private void OnEnable()
    {
        if (scoreEvent != null) scoreEvent.Register(UpdateScore);
        if (livesEvent != null) livesEvent.Register(UpdateLives);
        if (bossHpEvent != null) bossHpEvent.Register(UpdateBossHP);
    }

    private void OnDisable()
    {
        if (scoreEvent != null) scoreEvent.Unregister(UpdateScore);
        if (livesEvent != null) livesEvent.Unregister(UpdateLives);
        if (bossHpEvent != null) bossHpEvent.Unregister(UpdateBossHP);
    }

    private void Update()
    {
        UpdateWeaponUI();
    }

    private void UpdateScore(int s)
    {
        if (scoreText != null)
            scoreText.text = s.ToString("000000");
    }

    private void UpdateLives(int l)
    {
        if (livesText != null)
            livesText.text = "x" + l.ToString("00");
    }

    private void UpdateBossHP(int hp)
    {
        if (bossPanel != null) bossPanel.SetActive(true);
        if (bossSlider != null) bossSlider.value = hp;
    }

    private void UpdateWeaponUI()
    {
        if (player == null) return;

        if (weaponText != null)
            weaponText.text = player.CurrentShot.ToString();

        if (rocketAmmoText != null)
            rocketAmmoText.text = player.ammoRocket.ToString();

        if (laserEnergyText != null)
            laserEnergyText.text = Mathf.RoundToInt(player.laserEnergy).ToString();
    }
}
