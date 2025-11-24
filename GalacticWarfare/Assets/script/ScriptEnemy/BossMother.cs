using UnityEngine;

public class BossMother : MonoBehaviour
{
    [Header("Stats")]
    public float maxHealth = 500f;
    private float currentHealth;
    public int shield = 100;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public Transform[] movePoints;
    private int currentPoint = 0;

    [Header("Attacks")]
    public GameObject laserPrefab;
    public GameObject missilePrefab;
    public GameObject energyBeamPrefab; // raio que segue o jogador
    public Transform firePoint;
    public float laserRate = 1f;
    private float nextLaser;
    public float missileRate = 3f;
    private float nextMissile;
    public float energyBeamRate = 5f;
    private float nextEnergyBeam;

    [Header("Stage Thresholds")]
    public float stage2Health = 333f; // 2/3 da vida
    public float stage3Health = 166f; // 1/3 da vida
    private int stage = 1;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        HandleStages();
        MoveBoss();
        Attack();
    }

    void HandleStages()
    {
        if(currentHealth <= stage3Health && stage < 3)
        {
            stage = 3;
            Debug.Log("Stage 3 - Raio de energia ativado!");
        }
        else if(currentHealth <= stage2Health && stage < 2)
        {
            stage = 2;
            Debug.Log("Stage 2 - Mísseis ativados!");
        }
    }

    void MoveBoss()
    {
        if(movePoints.Length == 0) return;

        Transform targetPoint = movePoints[currentPoint];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            currentPoint = (currentPoint + 1) % movePoints.Length;
        }
    }

    void Attack()
    {
        // Fase 1+ → laser frontal
        if(Time.time >= nextLaser)
        {
            nextLaser = Time.time + laserRate;
            Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
        }

        // Fase 2+ → lança mísseis guiados
        if(stage >= 2 && Time.time >= nextMissile)
        {
            nextMissile = Time.time + missileRate;
            Instantiate(missilePrefab, firePoint.position, firePoint.rotation);
        }

        // Fase 3 → raio de energia que segue o jogador
        if(stage >= 3 && Time.time >= nextEnergyBeam)
        {
            nextEnergyBeam = Time.time + energyBeamRate;
            GameObject beam = Instantiate(energyBeamPrefab, firePoint.position, firePoint.rotation);
            if(beam.TryGetComponent<LaserFollow>(out var follow))
            {
                follow.target = GameObject.FindGameObjectWithTag("Player").transform;
                follow.speed = 5f;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if(shield > 0)
        {
            shield -= (int)damage;
            if(shield < 0)
            {
                currentHealth += shield; // aplica excesso de dano na vida
                shield = 0;
            }
        }
        else
        {
            currentHealth -= damage;
        }

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Boss destruído!");
        Destroy(gameObject);
        // Aqui você pode chamar pontuação, tela de vitória, etc.
    }
}
