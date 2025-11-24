using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Game state")]
    public int score;
    public int lives = 3;

    [Header("Event Channels")]
    public IntEventChannel scoreEvent;
    public IntEventChannel livesEvent;
    public IntEventChannel enemiesDestroyedEvent;

    private void Start()
    {
        // Raise initial values so HUD updates immediately
        scoreEvent?.Raise(score);
        livesEvent?.Raise(lives);
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreEvent?.Raise(score);
    }

    public void AddEnemyDestroyed(int count = 1)
    {
        enemiesDestroyedEvent?.Raise(count);
    }

    public void LoseLife(int amount = 1)
    {
        lives -= amount;
        if (lives < 0) lives = 0;
        livesEvent?.Raise(lives);

        if (lives == 0)
            GameOver();
    }

    public void GameOver()
    {
        // Pause game and show UI (connect to UI scene)
        Time.timeScale = 0f;
        Debug.Log("Game Over");
    }

    public void SaveProgress()
    {
        SaveData data = new SaveData
        {
            highScore = Mathf.Max(SaveSystem.Load().highScore, score),
            totalEnemiesDestroyed = SaveSystem.Load().totalEnemiesDestroyed // keep previous tally or add logic
        };
        SaveSystem.Save(data);
    }

    public void LoadProgress()
    {
        SaveData d = SaveSystem.Load();
        // e.g. load high score to show on menus
        Debug.Log("Loaded highscore: " + d.highScore);
    }
}
