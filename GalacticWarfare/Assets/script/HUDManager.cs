using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [Header("Score UI")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;

    [Header("Event Channels")]
    [SerializeField] private IntEventChannel scoreEvent;
    [SerializeField] private IntEventChannel livesEvent;

    [Header("Boss UI")]
    public GameObject bossPanel;
    public Slider bossSlider;

    [Header("Boss Event")]
    public IntEventChannel bossHpEvent;

    private void Start()
    {
        // Iniciar score/vidas normalmente

        // Configurar barra do boss
        bossPanel.SetActive(false);
        bossSlider.maxValue = 500;
        bossSlider.value = 500;
    }

    private void OnEnable()
    {
        if (scoreEvent != null)
            scoreEvent.Register(UpdateScore);

        if (livesEvent != null)
            livesEvent.Register(UpdateLives);

        if (bossHpEvent != null)
            bossHpEvent.Register(UpdateBossHP);
    }

    private void OnDisable()
    {
        if (scoreEvent != null)
            scoreEvent.Unregister(UpdateScore);

        if (livesEvent != null)
            livesEvent.Unregister(UpdateLives);

        if (bossHpEvent != null)
            bossHpEvent.Unregister(UpdateBossHP);
    }

    private void UpdateScore(int s)
    {
        scoreText.text = s.ToString("000000");
    }

    private void UpdateLives(int l)
    {
        livesText.text = "x" + l.ToString("00");
    }

    private void UpdateBossHP(int hp)
    {
        bossPanel.SetActive(true);
        bossSlider.value = hp;
    }
}
