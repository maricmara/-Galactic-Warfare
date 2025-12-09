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

    [Header("UI")]
    public GameOverUI gameOverUI;
    public GameObject victoryScreen;

    private void Start()
    {
        scoreEvent?.Raise(score);
        livesEvent?.Raise(lives);

        if (victoryScreen != null)
            victoryScreen.SetActive(false);
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
            GameOver();
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        if (gameOverUI != null)
            gameOverUI.MostrarGameOver();
    }

    public void TriggerVictory()
    {
        Debug.Log("VITÓRIA!");
        if (victoryScreen != null)
        {
            victoryScreen.SetActive(true);
            Time.timeScale = 0f; // Pausa o jogo
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
