using UnityEngine;

public class PlayerSpaceshipController : MonoBehaviour
{
    [Header("Movimentação")]
    public float speed = 10f;
    public float tilt = 30f;

    [Header("Limites")]
    public float xLimit = 8f;
    public float yLimit = 4f;

    [Header("Tiros")]
    public Transform firePoint;
    public GameObject bullet1;   // tiro normal
    public GameObject bullet2;   // tiro duplo
    public GameObject bullet3;   // tiro laser

    public float fireRate = 0.2f;
    private float nextFire = 0f;

    private int currentShot = 1;   // tipo de tiro inicial

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;   // nave não cai
    }

    void Update()
    {
        // Trocar o tipo de tiro (1 / 2 / 3)
        if (Input.GetKeyDown(KeyCode.Alpha1)) currentShot = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) currentShot = 2;
        if (Input.GetKeyDown(KeyCode.Alpha3)) currentShot = 3;

        // Atirar
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFire)
        {
            nextFire = Time.time + fireRate;

            switch (currentShot)
            {
                case 1:
                    Instantiate(bullet1, firePoint.position, firePoint.rotation);
                    break;

                case 2:
                    Instantiate(bullet2, firePoint.position, firePoint.rotation);
                    break;

                case 3:
                    Instantiate(bullet3, firePoint.position, firePoint.rotation);
                    break;
            }
        }
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        rb.linearVelocity = new Vector3(moveX, moveY, 0f) * speed;

        // Limitar posição
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, -xLimit, xLimit),
            Mathf.Clamp(rb.position.y, -yLimit, yLimit),
            rb.position.z
        );

        // Inclinando nave ao mover
        rb.rotation = Quaternion.Euler(-moveY * tilt, moveX * 10f, 0f);
    }
}
