using UnityEngine;

public class BossShot : MonoBehaviour
{
    public float speed = 5f;
    public float lifeTime = 5f;
    public int damage = 10;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // AGORA O TIRO VAI NA DIREÇÃO QUE O FIREPOINT ESTIVER APONTANDO
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth hp = collision.GetComponent<PlayerHealth>();
            if (hp != null)
            {
                hp.TakeDamage(damage);
            }

            Destroy(gameObject);
        }

        if (collision.CompareTag("Limit"))
        {
            Destroy(gameObject);
        }
    }
}
