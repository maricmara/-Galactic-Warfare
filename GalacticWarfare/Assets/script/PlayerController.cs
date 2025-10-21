using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    [Header("Movimentação")]
    public float speed = 5f;             // Velocidade de movimento
    public float tilt = 5f;              // Inclinação nas curvas
    public float xLimit = 8f;            // Limite horizontal
    public float yLimit = 4.5f;          // Limite vertical

    [Header("Tiro")]
    public GameObject bulletPrefab;      // Prefab do projétil
    public Transform firePoint;          // Ponto de saída do tiro
    public float fireRate = 0.25f;       // Intervalo entre tiros
    private float nextFire = 0f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // impede o avião de cair
    }

    void Update()
    {
        // Disparo
        if (Input.GetKey(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    void FixedUpdate()
    {
        // Entrada de movimento
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Movimento
        Vector2 movement = new Vector2(moveHorizontal, moveVertical) * speed;
        rb.linearVelocity = movement;

        // Limitar posição na tela
        float xPos = Mathf.Clamp(rb.position.x, -xLimit, xLimit);
        float yPos = Mathf.Clamp(rb.position.y, -yLimit, yLimit);
        rb.position = new Vector2(xPos, yPos);

        // Inclinação (efeito de rotação quando vira)
        rb.rotation = -moveHorizontal * tilt;
    }
}