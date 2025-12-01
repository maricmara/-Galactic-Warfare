using UnityEngine;

public class StarFighter : MonoBehaviour
{
    public EnemyData data; // ← Dados do ScriptableObject

    private float startY;
    private int vidaAtual;

    void Start()
    {
        vidaAtual = data.vida;
        startY = transform.position.y;
    }

    void Update()
    {
        MovimentoErratico();
    }

    void MovimentoErratico()
    {
        float y = startY + Mathf.Sin(Time.time * data.zigzagFrequency) * data.zigzagAmplitude;

        transform.position = new Vector3(
            transform.position.x - data.speed * Time.deltaTime,
            y,
            transform.position.z
        );
    }

    public void TomarDano(int dano)
    {
        vidaAtual -= dano;

        if (vidaAtual <= 0)
        {
            // Agora usa o GameManager para pontuar
            GameManager.Instance.AddScore(data.pontosAoMorrer);

            Destroy(gameObject);
        }
    }
}
