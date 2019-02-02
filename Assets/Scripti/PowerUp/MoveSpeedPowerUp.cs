using UnityEngine;
using System.Collections;

public class MoveSpeedPowerUp : AbstractPowerUp
{

    public override bool EnemyDestroyPowerUp { get { return true; } }
    public override bool DestroyPowerUpAfterTrigger { get { return false; } }
    public override bool AlreadyUsed { get; set; }

    public float powerUpDurationSec = 5.0f; // Duration in seconds
    public float moveSpeed = 15.0f;

    private float oldMoveSpeedX;
    private float oldMoveSpeedY;

    public override void PowerUpEffectPlayerTriggered()
    {
        Debug.Log("Player picked up move speed powerup");
        // Raise players attack speed on pick up
        RaiseMoveSpeedPlayer();
    }

    public override void PowerUpEffectEnemyTriggered()
    {
        Debug.Log("Enemy picked up move speed powerup");
        // Raise enemies attack speed on pickup
        RaiseMoveSpeedEnemy();
    }

    void RaiseMoveSpeedPlayer()
    {
        AlreadyUsed = true;
        GetComponent<Renderer>().enabled = false;
        PlayerMove playerMove = otherCollider.gameObject.GetComponent<PlayerMove>();
        if (playerMove != null)
        {
            oldMoveSpeedX = playerMove.speed.x;
            oldMoveSpeedY = playerMove.speed.y;

            playerMove.speed = new Vector2(moveSpeed, moveSpeed);
            Invoke("RestoreMoveSpeedPlayer", powerUpDurationSec);
        }
    }

    void RaiseMoveSpeedEnemy()
    {
        AlreadyUsed = true;
        GetComponent<Renderer>().enabled = false;
        MoveScript moveScript = otherCollider.gameObject.GetComponent<MoveScript>();
        if (moveScript != null)
        {
            oldMoveSpeedX = moveScript.speed.x;
            oldMoveSpeedY = moveScript.speed.y;

            moveScript.speed = new Vector2(moveSpeed, moveSpeed);
            Invoke("RestoreMoveSpeedEnemy", powerUpDurationSec);
        }
    }

    void RestoreMoveSpeedPlayer()
    {
        PlayerMove playerMove = otherCollider.gameObject.GetComponent<PlayerMove>();
        if (playerMove != null)
            playerMove.speed = new Vector2(oldMoveSpeedX, oldMoveSpeedY);

        Destroy(this.gameObject);
    }

    void RestoreMoveSpeedEnemy()
    {
        MoveScript moveScript = otherCollider.gameObject.GetComponent<MoveScript>();
        if (moveScript != null)
            moveScript.speed = new Vector2(oldMoveSpeedX, oldMoveSpeedY);

        Destroy(this.gameObject);
    }
}
