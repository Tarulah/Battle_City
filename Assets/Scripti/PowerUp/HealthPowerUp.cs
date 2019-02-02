using UnityEngine;
using System.Collections;

public class HealthPowerUp : AbstractPowerUp
{
    public override bool EnemyDestroyPowerUp { get { return false; } }
    public override bool DestroyPowerUpAfterTrigger { get { return false; } }
    public override bool AlreadyUsed { get; set; }

    public float powerUpDurationSec = 10.0f; // Duration in seconds
    public int raiseHealthBy = 10;

    public override void PowerUpEffectPlayerTriggered()
    {
        Debug.Log("Player picked up health powerup");
        RaiseHealth();
    }

    public override void PowerUpEffectEnemyTriggered()
    {
        //Debug.Log("Enemy picked up health powerup");
        // Do nothing if enemy picks up
    }

    void RaiseHealth()
    {
        AlreadyUsed = true;
        GetComponent<Renderer>().enabled = false;
        PlayerHealth playerHealth = otherCollider.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.currentHealth += raiseHealthBy;
            Invoke("RestoreHealth", powerUpDurationSec);
        }
    }

    void RestoreHealth()
    {
        PlayerHealth playerHealth = otherCollider.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null && playerHealth.currentHealth > playerHealth.startingHealth)
        {
            playerHealth.currentHealth = playerHealth.startingHealth;
        }
        Debug.Log("Player health RESTORED!");
        Destroy(this.gameObject);
    }
}
