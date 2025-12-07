using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [Header("Game state")]
    public int score = 0;
    public int lives = 3;

    [Header("Event Channels")]
    public IntEventChannel scoreEvent;
    public IntEventChannel livesEvent;

    [Header("Tela de Game Over")]
    public GameOverUI gameOverUI;

    private void Start()
    {
        // Atualiza HUD inicial
        scoreEvent?.Raise(score);
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

        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER via GameManager");

        Time.timeScale = 0f; // Pausa o jogo

        if (gameOverUI != null)
            gameOverUI.MostrarGameOver();
        else
            Debug.LogError("GameOverUI não foi configurado no GameManager!");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Despausa
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
