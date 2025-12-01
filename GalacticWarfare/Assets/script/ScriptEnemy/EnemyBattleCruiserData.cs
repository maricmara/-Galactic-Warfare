using UnityEngine;

[CreateAssetMenu(fileName = "BattleCruiserData", menuName = "ScriptableObjects/BattleCruiser Data")]
public class EnemyBattleCruiserData : ScriptableObject
{
    [Header("Status")]
    public int vida = 30;          // maior vida para não morrer em 1 tiro
    public int escudo = 10;        // escudo extra
    public float speed = 1f;       // nave lenta

    [Header("Arma Principal")]
    public float fireRatePrincipal = 2f; // tempo entre rajadas
    public int tirosPorRajada = 5;       // quantidade de tiros por rajada
    public float spreadAngle = 120f;     // abertura do tiro

    [Header("Arma Secundária")]
    public float fireRateSecundaria = 3f; // tempo entre disparos dos canhões menores

    [Header("Pontuação")]
    public int pontosAoMorrer = 50;
}
