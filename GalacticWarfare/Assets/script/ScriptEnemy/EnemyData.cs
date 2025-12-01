using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Status")]
    public float speed = 5f;
    public int vida = 1;

    [Header("Movimento Errático")]
    public float zigzagAmplitude = 1f;
    public float zigzagFrequency = 5f;

    [Header("Pontuação")]
    public int pontosAoMorrer = 10;
}
