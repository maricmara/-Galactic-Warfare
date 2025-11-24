using UnityEngine;

public enum PowerUpType { Health, Shield, Ammo, SuperShot }

public class PowerUp : MonoBehaviour
{
    public PowerUpType type;
    public float amount = 20f; // valor que o power-up aumenta

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerSpaceshipController player = other.GetComponent<PlayerSpaceshipController>();
            PlayerHealth health = other.GetComponent<PlayerHealth>();

            switch(type)
            {
                case PowerUpType.Health:
                    if(health != null)
                        health.Heal(amount);
                    break;

                case PowerUpType.Shield:
                    if(player != null)
                        player.shield += amount;
                    break;

                case PowerUpType.Ammo:
                    if(player != null)
                        player.ammoRocket += (int)amount;
                    break;

                case PowerUpType.SuperShot:
                    if(player != null)
                        player.superShotActive = true;
                    break;
            }

            Destroy(gameObject);
        }
    }
}
