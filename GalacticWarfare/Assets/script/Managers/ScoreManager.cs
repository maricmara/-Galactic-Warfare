using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    // --- OBSERVER EVENTO ---
    public delegate void ScoreChanged(int newScore);
    public static event ScoreChanged OnScoreChanged;

    private int score = 0;
    public int Score => score;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;

        // disparamos o evento para o HUD
        OnScoreChanged?.Invoke(score);
    }
}
