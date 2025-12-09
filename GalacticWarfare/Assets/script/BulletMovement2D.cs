using UnityEngine;

public class BulletMovement2D : MonoBehaviour
{
    [Header("Configuração da Bala")]
    public float speed = 25f;
    public float lifetime = 3f;
    public int dano = 1;               // dano padrão para inimigos normais
    public int bossDamageMultiplier = 5; // multiplicador de dano para a Nave Mãe

    [Header("Partícula de Impacto (apenas no contato com inimigo)")]
    public GameObject impactoPrefab;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed;
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // StarFighter
        StarFighter sf = other.GetComponent<StarFighter>();
        if (sf != null)
        {
            sf.TomarDano(dano);
            CriarImpacto(transform.position);
            Destroy(gameObject);
            return;
        }

        // AttackShip
        AttackShip aship = other.GetComponent<AttackShip>();
        if (aship != null)
        {
            aship.TomarDano(dano);
            CriarImpacto(transform.position);
            Destroy(gameObject);
            return;
        }

        // BattleCruiser
        BattleCruiser bc = other.GetComponent<BattleCruiser>();
        if (bc != null)
        {
            bc.TomarDano(dano);
            CriarImpacto(transform.position);
            Destroy(gameObject);
            return;
        }

        // BossMother (NAVE-MÃE)
        BossMother boss = other.GetComponent<BossMother>();
        if (boss != null)
        {
            boss.TakeDamage(dano * bossDamageMultiplier); // aplica dano maior
            CriarImpacto(transform.position);
            Destroy(gameObject);
            return;
        }

        // Obstáculos
        if (other.CompareTag("Obstacle") || other.CompareTag("Walls") || other.CompareTag("Ground"))
        {
            Destroy(gameObject);
            return;
        }
    }

    void CriarImpacto(Vector3 pos)
    {
        if (impactoPrefab != null)
        {
            GameObject fx = Instantiate(impactoPrefab, pos, Quaternion.identity);
            Destroy(fx, 0.6f);
        }
    }
}
