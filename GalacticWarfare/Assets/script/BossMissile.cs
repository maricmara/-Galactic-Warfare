using UnityEngine;

public class BossMissile : MonoBehaviour
{
    public float speed = 4f;
    public float rotateSpeed = 200f;
    public float lifetime = 8f;

    private Transform target;

    private void Start()
    {
        // Encontra o jogador na cena
        target = GameObject.FindGameObjectWithTag("Player")?.transform;

        // Destruir depois de 8 segundos
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        if (target == null) return;

        // Direção do míssil para o jogador
        Vector2 direction = (target.position - transform.position).normalized;

        // Rotaciona suavemente na direção do player
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        transform.Rotate(0, 0, -rotateAmount * rotateSpeed * Time.deltaTime);

        // Move sempre para frente (para onde aponta)
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var health = other.GetComponent<PlayerHealth>();
            if (health != null)
                health.TakeDamage(25f);

            // Explode (ou só destrói)
            Destroy(gameObject);
        }
        else if (other.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
