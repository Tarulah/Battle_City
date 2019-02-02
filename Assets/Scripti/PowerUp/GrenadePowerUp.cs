using UnityEngine;
using System.Collections;

public class GrenadePowerUp : AbstractPowerUp
{
    public override bool EnemyDestroyPowerUp { get { return true; } }
    public override bool DestroyPowerUpAfterTrigger { get { return true; } }
    public override bool AlreadyUsed { get; set; }

    public int damageToPlayerOnEnemyPickUp = 2;

    public override void PowerUpEffectPlayerTriggered()
    {
        Debug.Log("Player picked up grenade powerup");
        AlreadyUsed = true;

        GameObject[] enemyTanks = GameObject.FindGameObjectsWithTag(GlobalVars.enemyTankTag);
        foreach (GameObject enemyTank in enemyTanks)
        {
            // Destory all tanks
            EnemyHealth enemyTankHealth = enemyTank.GetComponent<EnemyHealth>();
            if (enemyTankHealth != null)
                enemyTankHealth.TakeDamage(999999);
        }
    }

    public override void PowerUpEffectEnemyTriggered()
    {
        Debug.Log("Enemy picked up grenade powerup");

        AlreadyUsed = true;
        // Damage player if enemy picks up a grenade
        GameObject playerObject = GameObject.FindGameObjectWithTag(GlobalVars.playerTankTag);
        PlayerHealth playerHealth = playerObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
            playerHealth.TakeDamage(damageToPlayerOnEnemyPickUp);
    }
}
