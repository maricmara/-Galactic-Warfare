using UnityEngine;

public class BattleCruiser : MonoBehaviour
{
    [Header("Data (ScriptableObject)")]
    public EnemyBattleCruiserData data;

    // Status internos
    private int vida;
    private int escudo;
    private float speed;

    // Tiro principal
    public GameObject bulletPrincipalPrefab;
    public Transform firePointPrincipal;
    private float fireRatePrincipal;
    private int tirosPorRajada;
    private float spreadAngle;
    private float timerPrincipal;

    // Tiro secundário
    public GameObject bulletSecundariaPrefab;
    public Transform[] canhoesSecundarios;
    private float fireRateSecundario;
    private float timerSecundario;

    [Header("Explosão")]
    public GameObject explosionPrefab;

    private Animator anim;
    private bool isDead = false;

    void Start()
    {
        anim = GetComponent<Animator>();

        // Carrega os valores do ScriptableObject
        vida = data.vida;
        escudo = data.escudo;
        speed = data.speed;

        fireRatePrincipal = data.fireRatePrincipal;
        tirosPorRajada = data.tirosPorRajada;
        spreadAngle = data.spreadAngle;

        fireRateSecundario = data.fireRateSecundaria;

        timerPrincipal = fireRatePrincipal;
        timerSecundario = fireRateSecundario;
    }

    void Update()
    {
        if (isDead) return;

        Movimento();
        AtirarPrincipal();
        AtirarSecundario();
    }

    void Movimento()
    {
        transform.Translate(Vector2.left * (speed * Time.deltaTime));
    }

    void AtirarPrincipal()
    {
        timerPrincipal -= Time.deltaTime;

        if (timerPrincipal <= 0f)
        {
            DispararRajadaPrincipal();
            timerPrincipal = fireRatePrincipal;
        }
    }

    void DispararRajadaPrincipal()
    {
        float anguloInicial = -spreadAngle / 2f;
        float incremento = spreadAngle / (tirosPorRajada - 1);

        for (int i = 0; i < tirosPorRajada; i++)
        {
            float anguloAtual = anguloInicial + incremento * i;
            Quaternion rot = firePointPrincipal.rotation * Quaternion.Euler(0, 0, anguloAtual);
            Instantiate(bulletPrincipalPrefab, firePointPrincipal.position, rot);
        }
    }

    void AtirarSecundario()
    {
        timerSecundario -= Time.deltaTime;

        if (timerSecundario <= 0f)
        {
            foreach (Transform canhao in canhoesSecundarios)
                Instantiate(bulletSecundariaPrefab, canhao.position, canhao.rotation);

            timerSecundario = fireRateSecundario;
        }
    }

    public void TomarDano(int dano)
    {
        if (isDead) return;

        // escudo primeiro
        if (escudo > 0)
            escudo -= dano;
        else
            vida -= dano;

        if (vida <= 0)
        {
            isDead = true;
            anim.SetBool("isFalling", true);

            // Atualiza pontuação
            GameManager.Instance.AddScore(data.pontosAoMorrer);

            this.enabled = false;
            Invoke(nameof(Explodir), 1.5f);
        }
    }

    void Explodir()
    {
        if (explosionPrefab != null)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
