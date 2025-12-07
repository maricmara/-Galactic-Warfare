using UnityEngine;

public class BossLaser : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        // Move o laser para frente
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica colisão com jogador
        if(other.CompareTag("Player"))
        {
            var playerHealth = other.GetComponent<PlayerHealth>();
            if(playerHealth != null)
                playerHealth.TakeDamage(10); // dano do laser

            Destroy(gameObject); // destrói o laser ao atingir
        }
    }
}
