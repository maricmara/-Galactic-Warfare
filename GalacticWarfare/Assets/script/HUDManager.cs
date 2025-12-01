using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;

    [Header("Event Channels")]
    [SerializeField] private IntEventChannel scoreEvent;
    [SerializeField] private IntEventChannel livesEvent;

    private void OnEnable()
    {
        if (scoreEvent != null)
            scoreEvent.Register(UpdateScore);

        if (livesEvent != null)
            livesEvent.Register(UpdateLives);
    }

    private void OnDisable()
    {
        if (scoreEvent != null)
            scoreEvent.Unregister(UpdateScore);

        if (livesEvent != null)
            livesEvent.Unregister(UpdateLives);
    }

    // REMOVIDO Start() QUE SOBRESCREVIA OS VALORES

    private void UpdateScore(int score)
    {
        scoreText.text = score.ToString("000000");
    }

    private void UpdateLives(int lives)
    {
        livesText.text = "x" + lives.ToString("00");
    }
}
