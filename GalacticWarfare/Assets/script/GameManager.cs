using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int score;
    public int playerLives = 3;
    public bool isPaused;

    public GameEvent onScoreChanged;
    public GameEvent onLivesChanged;

    private void Start()
    {
        onScoreChanged?.Raise(score);
        onLivesChanged?.Raise(playerLives);
    }

    public void AddScore(int amount)
    {
        score += amount;
        onScoreChanged?.Raise(score);
    }

    public void LoseLife()
    {
        playerLives--;
        onLivesChanged?.Raise(playerLives);

        if (playerLives <= 0)
            GameOver();
    }

    public void GameOver()
    {
        isPaused = true;
        Time.timeScale = 0;
        // Mostrar tela de game over
    }
    public void SaveProgress()
    {
        SaveData data = new SaveData();
        data.highScore = score;
        SaveSystem.Save(data);
    }

    public void LoadProgress()
    {
        SaveData data = SaveSystem.Load();
        score = data.highScore;
        onScoreChanged?.Raise(score);
    }

}