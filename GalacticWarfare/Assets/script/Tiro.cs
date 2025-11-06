using UnityEngine;

public class Tiro : MonoBehaviour
{
    public float velocidade = 10f;
    public float tempoDeVida = 1.5f; // tempo até desaparecer

    void Start()
    {
        // destruir depois de um tempo
        Destroy(gameObject, tempoDeVida);
    }

    void Update()
    {
        // mover para a direita
        transform.Translate(Vector2.right * velocidade * Time.deltaTime);
    }
}