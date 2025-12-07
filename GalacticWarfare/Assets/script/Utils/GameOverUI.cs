using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameObject painelGameOver;

    private void Start()
    {
        painelGameOver.SetActive(false);
    }

    public void MostrarGameOver()
    {
        painelGameOver.SetActive(true);
    }

    public void ReiniciarJogo()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
