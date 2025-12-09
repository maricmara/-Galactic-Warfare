using UnityEngine;

public class LaserFollow : MonoBehaviour
{
    public Transform target;
    public float speed = 10f;

    void Update()
    {
        if (target == null) return;

        Vector2 dir = (target.position - transform.position).normalized;
        transform.position += (Vector3)(dir * speed * Time.deltaTime);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var ph = other.GetComponent<PlayerHealth>();
            if (ph != null)
                ph.TakeDamage(20);

            Destroy(gameObject);
        }
    }
}
