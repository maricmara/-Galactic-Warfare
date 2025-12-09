using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;

[System.Serializable]
public class GameData
{
    public int score;
    public int lives;
    public int playerAmmoRocket;
    public float playerLaserEnergy;
}

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

    [Header("Tela de Vitória")]
    public GameObject victoryScreen;

    [Header("Player reference")]
    public PlayerSpaceshipController player;

    private string saveFilePath;

    private void Awake()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "savegame.json");
    }

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
        else
            Debug.LogError("GameOverUI não foi configurado no GameManager!");
    }

    // Substituí TriggerVictory por coroutine para delay da explosão
    public void TriggerVictory()
    {
        StartCoroutine(ShowVictoryCoroutine());
    }

    private IEnumerator ShowVictoryCoroutine()
    {
        // Espera 1 segundo (para a explosão ser vista)
        yield return new WaitForSecondsRealtime(1f);

        // Salva o progresso do jogador
        SaveGame();

        // Mostra a tela de vitória
        if (victoryScreen != null)
            victoryScreen.SetActive(true);
        else
            Debug.LogError("VictoryScreen não configurada no GameManager!");

        // Pausa o jogo
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    #region Salvamento JSON

    private void SaveGame()
    {
        if (player == null)
        {
            Debug.LogWarning("Player não atribuído no GameManager. Não será possível salvar.");
            return;
        }

        GameData data = new GameData()
        {
            score = score,
            lives = lives,
            playerAmmoRocket = player.ammoRocket,
            playerLaserEnergy = player.laserEnergy
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Jogo salvo em: " + saveFilePath);
    }

    public void LoadGame()
    {
        if (!File.Exists(saveFilePath))
        {
            Debug.LogWarning("Arquivo de save não encontrado!");
            return;
        }

        string json = File.ReadAllText(saveFilePath);
        GameData data = JsonUtility.FromJson<GameData>(json);

        score = data.score;
        lives = data.lives;

        if (player != null)
        {
            player.ammoRocket = data.playerAmmoRocket;
            player.laserEnergy = data.playerLaserEnergy;
        }

        scoreEvent?.Raise(score);
        livesEvent?.Raise(lives);
    }

    #endregion
}
