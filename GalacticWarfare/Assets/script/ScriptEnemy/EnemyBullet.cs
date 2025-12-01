using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("Configuração do Tiro")]
    public float speed = 10f;       // Velocidade do projétil
    public float lifetime = 3f;     // Tempo até o tiro desaparecer
    public float dano = 10f;        // Dano que o tiro causa ao player

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        // Para colisão de tiros rápidos
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        // Move o tiro na direção do transform.right
        rb.linearVelocity = transform.right * speed;

        // Destrói automaticamente após lifetime
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Procura PlayerHealth no objeto ou nos pais
        PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(dano); // Aplica o dano
            Destroy(gameObject);           // Destroi o tiro
            return;
        }

        // Caso queira destruir o tiro em limites da tela
        if (other.CompareTag("LimiteTiro"))
        {
            Destroy(gameObject);
        }
    }
}
