using System;
using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
	System.Random random = new System.Random();

	public float changeDirectionTime = 5f; // Time in seconds
	public float stayingCheckTime = 1.0f; // Time in seconds
	public GameObject rajahdysAnimation;

    private WeaponScript weapon;
	private Vector3 enemyPosition;
	private float sightCheckDelta = 0.3f;

    private float floatCalculationsTolerance = 0.001f;

    void Start ()
	{
		enemyPosition = transform.position;
		ChangeMoveDirectionRandom(); // Change direction 
		// Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
		InvokeRepeating("CheckForStaying", stayingCheckTime, stayingCheckTime);
	}

    void Awake()
    {
        // Retrieve the weapon only once
        weapon = GetComponentInChildren<WeaponScript>();
    }

    void Update()
    {
        // Auto-fire
        if (weapon != null && weapon.CanAttack && (this.PlayerOnSight() || this.BaseOnSight()) )
        {
            weapon.Attack(true);
        }
    }

	bool PlayerOnSight() 
	{
		Vector3 playerPosition = GameObject.Find(GlobalVars.playerTankName).transform.position;
		Vector3 ownPosition = transform.position;
	    GlobalVars.RotationSides rotatedTo = GetComponent<MoveScript>().RotatedTo();

	    if (rotatedTo == GlobalVars.RotationSides.Up)
	    {
	        if (playerPosition.y > ownPosition.y &&
                    Math.Abs(playerPosition.x - ownPosition.x) < sightCheckDelta) // Looking up and player above the enemy
	            return true;
	    }
        else if (rotatedTo == GlobalVars.RotationSides.Down)
        {
            if (playerPosition.y < ownPosition.y &&
                    Math.Abs(playerPosition.x - ownPosition.x) < sightCheckDelta) // Looking down and player lower than enemy
                return true;
        }
        else if (rotatedTo == GlobalVars.RotationSides.Left)
        {
            if (playerPosition.x < ownPosition.x
                    && Math.Abs(playerPosition.y - ownPosition.y) < sightCheckDelta) // Looking left and player on the left side
                return true;
        }
        else if (rotatedTo == GlobalVars.RotationSides.Right)
        {
            if (playerPosition.x > ownPosition.x &&
                    Math.Abs(playerPosition.y - ownPosition.y) < sightCheckDelta) // Looking right and player on the right side
                return true;
        }

        return false;
	}

    // TODO: REFACTOR
    bool BaseOnSight()
    {
        Vector3 basePosition = GameObject.Find(GlobalVars.playerBase).transform.position;
        Vector3 ownPosition = transform.position;
        GlobalVars.RotationSides rotatedTo = GetComponent<MoveScript>().RotatedTo();

        if (rotatedTo == GlobalVars.RotationSides.Up)
        {
            if (basePosition.y > ownPosition.y &&
                Math.Abs(basePosition.x - ownPosition.x) < sightCheckDelta) // Looking up and player above the enemy
                return true;
        }
        else if (rotatedTo == GlobalVars.RotationSides.Down)
        {
            if (basePosition.y < ownPosition.y &&
                Math.Abs(basePosition.x - ownPosition.x) < sightCheckDelta) // Looking down and player lower than enemy
                return true;
        }
        else if (rotatedTo == GlobalVars.RotationSides.Left)
        {
            if (basePosition.x < ownPosition.x
                && Math.Abs(basePosition.y - ownPosition.y) < sightCheckDelta) // Looking left and player on the left side
                return true;
        }
        else if (rotatedTo == GlobalVars.RotationSides.Right)
        {
            if (basePosition.x > ownPosition.x &&
                Math.Abs(basePosition.y - ownPosition.y) < sightCheckDelta) // Looking right and player on the right side
                return true;
        }

        return false;
    }

    // Collision Trigger
    void OnCollisionEnter2D(Collision2D collision)
	{
		this.ChangeMoveDirectionRandom();
	}

	// Trigger
	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		//Debug.Log("ENEMY OnTriggerEnter2D");
		if (otherCollider.tag == "PlayerBullet") {
			PlayExplosion();

		}
	}

	void ChangeMoveDirectionRandom()
	{
		int directionX = random.Next(0, 2);
		int directionY = random.Next(0, 2);

		if (directionX == 1 && random.Next(0, 2) == 1)
			directionX = -1;
		if (directionY == 1 && random.Next(0, 2) == 1)
			directionY = -1;

		GetComponent<MoveScript>().direction = new Vector2(directionX, directionY);
	}

	private void CheckForStaying() 
	{
		Vector3 newPosition = transform.position;

		if (Math.Abs(enemyPosition.x - newPosition.x) < floatCalculationsTolerance 
                && Math.Abs(enemyPosition.y - newPosition.y) < floatCalculationsTolerance)
			ChangeMoveDirectionRandom ();

		enemyPosition = newPosition;
	}
	void PlayExplosion(){
		GameObject explosion = (GameObject)Instantiate (rajahdysAnimation);

		explosion.transform.position = transform.position;
	}
}
