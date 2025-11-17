using UnityEngine;

public class StarFighter : MonoBehaviour
{
    [Header("Status")]
    public float speed = 5f;
    public int vida = 1;

    [Header("Movimento Errático")]
    public float zigzagAmplitude = 1f;
    public float zigzagFrequency = 5f;

    private float startX;

    void Start()
    {
        startX = transform.position.x;
    }

    void Update()
    {
        MovimentoErratico();
    }

    void MovimentoErratico()
    {
        // Desce enquanto faz zig-zag
        float x = startX + Mathf.Sin(Time.time * zigzagFrequency) * zigzagAmplitude;
        transform.position = new Vector3(x, transform.position.y - speed * Time.deltaTime, transform.position.z);
    }

    public void TomarDano(int dano)
    {
        vida -= dano;
        if (vida <= 0)
            Destroy(gameObject);
    }
}
