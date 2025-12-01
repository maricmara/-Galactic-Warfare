using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Game state")]
    public int score;
    public int lives = 3;

    [Header("Event Channels")]
    public IntEventChannel scoreEvent;
    public IntEventChannel livesEvent;

    private void Start()
    {
       
        // Atualiza vidas no início
        livesEvent?.Raise(lives);
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreEvent?.Raise(score);
    }

    public void LoseLife(int amount = 1)
    {
        lives -= amount;
        if (lives < 0) lives = 0;

        livesEvent?.Raise(lives);
    }
}
