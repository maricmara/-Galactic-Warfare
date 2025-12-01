using UnityEngine;

public class AttackShip : MonoBehaviour
{
    public AttackShipData data;

    public Transform leftGun;
    public Transform rightGun;

    public GameObject enemyBulletPrefab;

    private int vidaAtual;
    private int escudoAtual;
    private float fireTimer;

    [Header("Movimento vertical aleatório")]
    public float verticalSpeed = 2f;
    public float minY = -3f;
    public float maxY = 3f;
    private float targetY;
    private Vector3 startPos;

    void Start()
    {
        vidaAtual = data.vida;
        escudoAtual = data.escudo;
        startPos = transform.position;
        targetY = transform.position.y;
    }

    void Update()
    {
        Mover();
        Atirar();
    }

    void Mover()
    {
        // Movimento horizontal constante
        transform.position += Vector3.left * data.speed * Time.deltaTime;

        // Escolhe um novo alvo vertical ao chegar perto do atual
        if (Mathf.Abs(transform.position.y - targetY) < 0.1f)
            targetY = Random.Range(minY, maxY);

        // Move verticalmente em direção ao targetY
        float newY = Mathf.MoveTowards(transform.position.y, targetY, verticalSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void Atirar()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= data.fireRate)
        {
            fireTimer = 0f;

            for (int i = 0; i < data.tirosPorRajada; i++)
            {
                float angle = Random.Range(-data.spreadAngle / 2f, data.spreadAngle / 2f);

                // Canhão esquerdo
                Disparar(leftGun, angle);

                // Canhão direito
                Disparar(rightGun, angle);
            }
        }
    }

    void Disparar(Transform gun, float angle)
    {
        GameObject tiro = Instantiate(enemyBulletPrefab, gun.position, gun.rotation);
        tiro.transform.Rotate(0, 0, angle);
    }

    public void TomarDano(int dano)
    {
        // Primeiro desconta do escudo
        if (escudoAtual > 0)
        {
            escudoAtual -= dano;
            return;
        }

        // Depois desconta da vida
        vidaAtual -= dano;

        if (vidaAtual <= 0)
        {
            GameManager.Instance.AddScore(data.pontosAoMorrer);
            Destroy(gameObject);
        }
    }
}
