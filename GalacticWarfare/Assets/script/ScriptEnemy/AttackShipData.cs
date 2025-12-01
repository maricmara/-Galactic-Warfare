using UnityEngine;

[CreateAssetMenu(
    fileName = "AttackShipData",
    menuName = "ScriptableObjects/Attack Ship Data"
)]
public class AttackShipData : ScriptableObject
{
    [Header("Status")]
    public int vida = 6;
    public int escudo = 3;
    public float speed = 2f;

    [Header("Tiros Laterais")]
    public float fireRate = 1.5f;
    public int tirosPorRajada = 3;
    public float spreadAngle = 60f;

    [Header("Pontuação")]
    public int pontosAoMorrer = 20;
}
