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
    public GameObject bullet1; // tiro rápido
    public GameObject bullet2; // foguete
    public GameObject bullet3; // laser
    public float fireRate = 0.2f;
    private float nextFire = 0f;
    private int currentShot = 1;

    private Rigidbody2D rb;

    [Header("Recursos do jogador")]
    public int ammoRocket = 50;       // Foguetes
    public float laserEnergy = 100f;  // Energia do laser 0-100%
    public bool superShotActive = false;
    public float shield = 0f;         // Escudo

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    void Update()
    {
        // Troca direta de tiro
        if (Input.GetKeyDown(KeyCode.Alpha1)) currentShot = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) currentShot = 2;
        if (Input.GetKeyDown(KeyCode.Alpha3)) currentShot = 3;

        // Ciclo de tiro com Z
        if (Input.GetKeyDown(KeyCode.Z))
        {
            currentShot++;
            if (currentShot > 3) currentShot = 1;
            Debug.Log("Tiro atual: " + currentShot);
        }

        // Atirar
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFire)
        {
            nextFire = Time.time + fireRate;

            GameObject prefab = null;

            switch(currentShot)
            {
                case 1:
                    prefab = bullet1; // tiro rápido
                    break;
                case 2:
                    if(ammoRocket > 0) 
                    {
                        prefab = bullet2; // foguete
                        ammoRocket--;
                    }
                    break;
                case 3:
                    if(laserEnergy > 0f) 
                    {
                        prefab = bullet3; // laser
                        laserEnergy -= 5f; // consome energia por tiro
                        if(laserEnergy < 0f) laserEnergy = 0f;
                    }
                    break;
            }

            if(prefab != null)
                Instantiate(prefab, firePoint.position, firePoint.rotation);
        }
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        rb.linearVelocity = new Vector2(moveX, moveY) * speed;

        rb.position = new Vector2(
            Mathf.Clamp(rb.position.x, -xLimit, xLimit),
            Mathf.Clamp(rb.position.y, -yLimit, yLimit)
        );

        transform.rotation = Quaternion.Euler(-moveY * tilt, 0f, -moveX * 10f);
    }
}
