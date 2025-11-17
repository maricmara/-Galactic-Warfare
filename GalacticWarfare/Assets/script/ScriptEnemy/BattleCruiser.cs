using UnityEngine;

public class BattleCruiser : MonoBehaviour
{
    [Header("Status")]
    public int vida = 15;
    public int escudo = 6;
    public float speed = 1f;

    [Header("Arma Principal")]
    public GameObject bulletPrincipalPrefab;
    public Transform firePointPrincipal;
    public float fireRatePrincipal = 2.5f;
    public int tirosPorRajada = 5;
    public float spreadAngle = 120f;
    private float timerPrincipal;

    [Header("Canhões Secundários")]
    public GameObject bulletSecundariaPrefab;
    public Transform[] canhoesSecundarios;
    public float fireRateSecundario = 1f;
    private float timerSecundario;

    void Update()
    {
        Movimento();
        AtirarPrincipal();
        AtirarSecundario();
    }

    void Movimento()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
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
        if (escudo > 0)
            escudo -= dano;
        else
            vida -= dano;

        if (vida <= 0)
            Destroy(gameObject);
    }
}
