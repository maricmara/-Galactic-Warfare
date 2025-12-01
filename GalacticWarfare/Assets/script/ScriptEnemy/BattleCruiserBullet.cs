using UnityEngine;

public class BattleCruiserBullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;
    public int dano = 1;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Move para a frente do objeto (eixo X local)
        rb.linearVelocity = transform.right * speed;

        // Destrói o tiro após 'lifetime' segundos
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                Debug.Log("Achou PlayerHealth! Aplicando dano...");
                playerHealth.TakeDamage(dano);
            }
            Destroy(gameObject);
        }
    }
}
