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
    public GameObject bullet1;
    public GameObject bullet2;
    public GameObject bullet3;

    public float fireRate = 0.2f;
    private float nextFire = 0f;

    private int currentShot = 1;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    void Update()
    {
        // Trocar tiros diretamente
        if (Input.GetKeyDown(KeyCode.Alpha1)) currentShot = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) currentShot = 2;
        if (Input.GetKeyDown(KeyCode.Alpha3)) currentShot = 3;

        // --- NOVO CÓDIGO: Troca de tiro em ciclo com a tecla Z ---
        if (Input.GetKeyDown(KeyCode.Z))
        {
            currentShot++; // Incrementa para o próximo tiro
            
            // Se passar de 3, volta para 1
            if (currentShot > 3)
            {
                currentShot = 1;
            }
            
            // Opcional: Debug para ver qual tiro está ativo
            Debug.Log("Tiro atual: " + currentShot);
        }
        // --- FIM DO NOVO CÓDIGO ---

        // Atirar
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFire)
        {
            nextFire = Time.time + fireRate;

            // Seleciona o prefab com base no tiro atual
            GameObject prefab = currentShot == 1 ? bullet1 :
                                currentShot == 2 ? bullet2 :
                                bullet3;

            Instantiate(prefab, firePoint.position, firePoint.rotation);
        }
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        rb.linearVelocity = new Vector2(moveX, moveY) * speed;

        // Limitar posição
        rb.position = new Vector2(
            Mathf.Clamp(rb.position.x, -xLimit, xLimit),
            Mathf.Clamp(rb.position.y, -yLimit, yLimit)
        );

        // Inclinação da nave
        transform.rotation = Quaternion.Euler(-moveY * tilt, 0f, -moveX * 10f);
    }
}