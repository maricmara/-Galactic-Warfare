using UnityEngine;

[CreateAssetMenu(menuName = "GameData/BossData")]
public class BossData : ScriptableObject
{
    public string bossName;
    public int maxHealth = 1000;
    public float speed = 2f;
    public int damageLaser = 10;
    public int damageMissile = 20;
    public int damageRay = 30;

    [Header("Thresholds de Stages")]
    [Range(0f, 1f)] public float stage2Threshold = 0.7f; // <70% HP muda para mísseis
    [Range(0f, 1f)] public float stage3Threshold = 0.3f; // <30% HP muda para raio
}
