using UnityEngine;

public class BulletMovement2D : MonoBehaviour
{
    [Header("Configuração da Bala")]
    public float speed = 25f;
    public float lifetime = 3f;
    public int dano = 1;

    [Header("Partícula de Impacto (apenas no contato com inimigo)")]
    public GameObject impactoPrefab;   // arraste aqui o prefab da faísca

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Use rb.velocity (mais compatível)
        rb.linearVelocity = transform.right * speed;

        // A bala some depois de alguns segundos
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // === Primeiro, detectar TIPO concreto de inimigo e aplicar dano corretamente ===
        // StarFighter
        StarFighter sf = other.GetComponent<StarFighter>();
        if (sf != null)
        {
            sf.TomarDano(dano);
            CriarImpacto(transform.position);
            Destroy(gameObject);
            return;
        }

        // AttackShip (Nave de Ataque)
        AttackShip aship = other.GetComponent<AttackShip>();
        if (aship != null)
        {
            aship.TomarDano(dano);
            CriarImpacto(transform.position);
            Destroy(gameObject);
            return;
        }

        // BattleCruiser (Cruzador)
        BattleCruiser bc = other.GetComponent<BattleCruiser>();
        if (bc != null)
        {
            bc.TomarDano(dano);
            CriarImpacto(transform.position);
            Destroy(gameObject);
            return;
        }

        // Se bater num obstáculo (cenário) — só destruir a bala (sem criar faísca se não quiser)
        if (other.CompareTag("Obstacle") || other.CompareTag("Walls") || other.CompareTag("Ground"))
        {
            Destroy(gameObject);
            return;
        }

        // Se bater em algo que não é inimigo (player, UI, etc.) — ignore ou destrua só a bala
        // Removi o bloco genérico que destrói "other.gameObject" com tag "Enemy" — isso fazia matar inimigos instant.
        // Se quiser que algum objeto "frágil" morra, trate isso em um script específico daquele objeto.
    }

    void CriarImpacto(Vector3 pos)
    {
        if (impactoPrefab != null)
        {
            GameObject fx = Instantiate(impactoPrefab, pos, Quaternion.identity);

            // Se o prefab não tiver StopAction=Destroy por segurança destruímos em 0.6s
            Destroy(fx, 0.6f);
        }
    }
}
