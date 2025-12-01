using UnityEngine;

public class BulletMovement2D : MonoBehaviour
{
    [Header("Configuração da Bala")]
    public float speed = 25f;
    public float lifetime = 3f;
    public int dano = 1;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // MOVIMENTO CORRETO EM 2D
        rb.linearVelocity = transform.right * speed;

        // DESAPARECE APÓS ALGUNS SEGUNDOS
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // ===============================
        //     DANO NO STARFIGHTER
        // ===============================
        StarFighter sf = other.GetComponent<StarFighter>();
        if (sf != null)
        {
            sf.TomarDano(dano);
            Destroy(gameObject);
            return;
        }

        // ===============================
        //     DANO NA NAVE DE ATAQUE
        // ===============================
        AttackShip aship = other.GetComponent<AttackShip>();
        if (aship != null)
        {
            aship.TomarDano(dano);
            Destroy(gameObject);
            return;
        }

        // ===============================
        //     DANO EM QUALQUER OUTRO INIMIGO
        // ===============================
        if (other.CompareTag("Enemy"))
        {
            // Se quiser usar um genérico no futuro
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
