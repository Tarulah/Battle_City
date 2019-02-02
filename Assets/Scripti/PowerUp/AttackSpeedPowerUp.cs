using UnityEngine;
using System.Collections;

public class AttackSpeedPowerUp : AbstractPowerUp
{
    public override bool EnemyDestroyPowerUp { get { return false; } }
    public override bool DestroyPowerUpAfterTrigger { get { return false; } }
    public override bool AlreadyUsed { get; set; }

    public float powerUpDurationSec = 5.0f; // Duration in seconds
    public float attackSpeed = 0.1f;

    private float oldAttackSpeed;

    public override void PowerUpEffectPlayerTriggered()
    {
        Debug.Log("Player picked up attack speed powerup");
        // Raise players attack speed on pick up
        RaiseAttackSpeed();
    }

    public override void PowerUpEffectEnemyTriggered()
    {
        Debug.Log("Enemy picked up attack speed powerup");
        // Raise enemies attack speed on pickup
        RaiseAttackSpeed();
    }

    void RaiseAttackSpeed()
    {
        AlreadyUsed = true;
        GetComponent<Renderer>().enabled = false;
        WeaponScript weapon = otherCollider.gameObject.GetComponent<WeaponScript>();
        if (weapon != null)
        {
            oldAttackSpeed = weapon.shootingRate;
            weapon.shootingRate = attackSpeed;
            Invoke("RestoreAttackSpeed", powerUpDurationSec);
        }
    }

    void RestoreAttackSpeed()
    {
        WeaponScript weapon = otherCollider.gameObject.GetComponent<WeaponScript>();
        if (weapon != null)
        {
            weapon.shootingRate = oldAttackSpeed;
        }
        Destroy(this.gameObject);
    }
}
