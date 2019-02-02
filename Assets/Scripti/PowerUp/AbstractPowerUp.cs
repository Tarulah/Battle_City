using UnityEngine;
using System.Collections;

public abstract class AbstractPowerUp : MonoBehaviour
{
    public abstract bool EnemyDestroyPowerUp { get; }
    public abstract bool DestroyPowerUpAfterTrigger { get; }
    public abstract bool AlreadyUsed { get; set; }

    protected Collider2D otherCollider;

    public AudioClip powerupPickedSound;

    // Abstract class for powerup effect
    public abstract void PowerUpEffectPlayerTriggered();
    public abstract void PowerUpEffectEnemyTriggered();

    // Trigger the powerup effect and destroy the object
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (AlreadyUsed)
            return;

        this.otherCollider = otherCollider;

        string tag = otherCollider.gameObject.tag;
        if (tag.Equals(GlobalVars.playerTankTag))
        {
            PowerUpEffectPlayerTriggered();
            if (DestroyPowerUpAfterTrigger)
            {
                AudioSource.PlayClipAtPoint(powerupPickedSound, transform.position, 1f);
                Destroy(this.gameObject);
            }
        }
        else if (tag.Equals(GlobalVars.enemyTankTag))
        {
            PowerUpEffectEnemyTriggered();
            if (EnemyDestroyPowerUp)
            {
                AudioSource.PlayClipAtPoint(powerupPickedSound, transform.position, 1f);
                Destroy(this.gameObject);
            }
        }
    }

}
