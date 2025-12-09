using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryUI : MonoBehaviour
{
    public GameObject painelVitoria;

    private void Start()
    {
        painelVitoria.SetActive(false);
    }

    public void MostrarVitoria()
    {
        painelVitoria.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
