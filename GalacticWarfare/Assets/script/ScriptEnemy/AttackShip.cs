using UnityEngine;

public class AttackShip : MonoBehaviour
{
    [Header("Status")]
    public int vida = 4;
    public int escudo = 2;
    public float speed = 2f;

    [Header("Tiro")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1.5f;
    private float fireTimer;

    [Header("Rajada")]
    public int tirosPorRajada = 3;
    public float spreadAngle = 60f;

    void Update()
    {
        Movimento();
        Atirar();
    }

    void Movimento()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    void Atirar()
    {
        fireTimer -= Time.deltaTime;

        if (fireTimer <= 0f)
        {
            DispararRajada();
            fireTimer = fireRate;
        }
    }

    void DispararRajada()
    {
        float anguloInicial = -spreadAngle / 2f;   
        float incremento = spreadAngle / (tirosPorRajada - 1);

        for (int i = 0; i < tirosPorRajada; i++)
        {
            float anguloAtual = anguloInicial + incremento * i;
            Quaternion rot = firePoint.rotation * Quaternion.Euler(0, 0, anguloAtual);
            Instantiate(bulletPrefab, firePoint.position, rot);
        }
    }

    public void TomarDano(int dano)
    {
        if (escudo > 0)
            escudo -= dano;
        else
            vida -= dano;

        if (vida <= 0)
            Destroy(gameObject);
    }
}
