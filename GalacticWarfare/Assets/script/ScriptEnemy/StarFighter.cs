using UnityEngine;

public class StarFighter : MonoBehaviour
{
    [Header("Status")]
    public float speed = 5f;
    public int vida = 1;

    [Header("Movimento Errático")]
    public float zigzagAmplitude = 1f;
    public float zigzagFrequency = 5f;

    private float startY;

    void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        MovimentoErratico();
    }

    void MovimentoErratico()
    {
        // Movimento para a esquerda com zig-zag vertical
        float y = startY + Mathf.Sin(Time.time * zigzagFrequency) * zigzagAmplitude;

        transform.position = new Vector3(
            transform.position.x - speed * Time.deltaTime,
            y,
            transform.position.z
        );
    }

    public void TomarDano(int dano)
    {
        vida -= dano;
        if (vida <= 0)
            Destroy(gameObject);
    }
}
