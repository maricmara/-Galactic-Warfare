using UnityEngine;

public class BossMother : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 150;
    private int currentHealth;
    public int shield = 50;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public Transform[] movePoints;
    private int currentPoint = 0;

    [Header("Movement Boundaries")]
    public float minX = -6f, maxX = 6f, minY = 2f, maxY = 5f;

    [Header("Attacks")]
    public GameObject laserPrefab;
    public GameObject missilePrefab;
    public GameObject energyBeamPrefab;
    public Transform firePoint;
    public float attackRange = 10f;

    public float laserRate = 1f;
    private float nextLaser;
    public float missileRate = 3f;
    private float nextMissile;
    public float energyBeamRate = 5f;
    private float nextEnergyBeam;

    [Header("Stage Thresholds")]
    public int stage2Health = 100;
    public int stage3Health = 50;
    private int stage = 1;

    [Header("HUD")]
    public IntEventChannel bossHpEvent;

    [Header("Explosion")]
    public ParticleSystem explosion;

    private Transform player;

    void Start()
    {
        currentHealth = maxHealth;
        bossHpEvent?.Raise(currentHealth);

        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        // Começa no último ponto do cenário
        if (movePoints.Length > 0)
            transform.position = movePoints[movePoints.Length - 1].position;
    }

    void Update()
    {
        HandleStages();
        MoveBoss();
        Attack();
    }

    void HandleStages()
    {
        if (currentHealth <= stage3Health && stage < 3) stage = 3;
        else if (currentHealth <= stage2Health && stage < 2) stage = 2;
    }

    void MoveBoss()
    {
        if (movePoints.Length == 0) return;

        Transform targetPoint = movePoints[currentPoint];
        Vector3 newPos = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);
        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        newPos.y = Mathf.Clamp(newPos.y, minY, maxY);
        transform.position = newPos;

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.05f)
            currentPoint = (currentPoint + 1) % movePoints.Length;
    }

    void Attack()
    {
        if (player == null) return;
        if (Vector3.Distance(transform.position, player.position) > attackRange) return;

        switch (stage)
        {
            case 1:
                if (Time.time >= nextLaser && laserPrefab != null)
                {
                    nextLaser = Time.time + laserRate;
                    Instantiate(laserPrefab, firePoint.position, Quaternion.Euler(0, 0, 180));
                }
                break;

            case 2:
                if (Time.time >= nextMissile && missilePrefab != null)
                {
                    nextMissile = Time.time + missileRate;
                    Instantiate(missilePrefab, firePoint.position, Quaternion.Euler(0, 0, 180));
                }
                break;

            case 3:
                if (Time.time >= nextEnergyBeam && energyBeamPrefab != null)
                {
                    nextEnergyBeam = Time.time + energyBeamRate;
                    GameObject beam = Instantiate(energyBeamPrefab, firePoint.position, firePoint.rotation);
                    if (beam.TryGetComponent<LaserFollow>(out LaserFollow lf))
                    {
                        lf.target = player;
                        lf.speed = 5f;
                    }
                }
                break;
        }
    }

    public void TakeDamage(int damage)
    {
        int damageToHealth = damage;

        if (shield > 0)
        {
            int shieldDamage = Mathf.Min(shield, damage);
            shield -= shieldDamage;
            damageToHealth -= shieldDamage;
        }

        currentHealth -= damageToHealth;
        bossHpEvent?.Raise(currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        if (explosion != null)
            Instantiate(explosion, transform.position, Quaternion.identity);

        // Chama a vitória corretamente
        GameManager.Instance.TriggerVictory();

        Destroy(gameObject);
    }
}
