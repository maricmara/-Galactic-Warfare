using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Ponto de disparo")]
    public Transform firePoint;

    [Header("Prefabs dos 3 tiros")]
    public GameObject shot1;
    public GameObject shot2;
    public GameObject shot3;

    private int currentShot = 1; // começa no tiro 1
    public float fireRate = 0.25f;
    private float nextFireTime = 0f;

    void Update()
    {
        // Trocar tipo de tiro com Z
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ChangeShot();
        }

        // Atirar com Espaço
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void ChangeShot()
    {
        currentShot++;
        if (currentShot > 3)
            currentShot = 1;

        Debug.Log("Tiro selecionado: " + currentShot);
    }

    void Shoot()
    {
        GameObject selectedBullet = null;

        // Escolher qual tiro usar
        if (currentShot == 1) selectedBullet = shot1;
        if (currentShot == 2) selectedBullet = shot2;
        if (currentShot == 3) selectedBullet = shot3;

        // Instancia o prefab do tiro (o script da bala cuida do movimento)
        Instantiate(selectedBullet, firePoint.position, firePoint.rotation);
    }
}