using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	public Vector2 speed = new Vector2(50, 50);

	private Vector2 movement;
	private Vector3 rotationVector;

	void Update()
	{
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");

		if (inputX != 0)
		{
			movement = new Vector2(speed.x * inputX, 0);
		}
		else
		{
			movement = new Vector2(0, speed.y * inputY);
		}
		Rotate(inputX, inputY); // Get rotation vector
		transform.eulerAngles = rotationVector; // Apply rotation

		//Debug.Log("inputX: " + inputX);
		//Debug.Log("inputY: " + inputY);

		bool shoot = Input.GetButtonDown("Fire1");
		shoot |= Input.GetButtonDown("Fire2");
		// Careful: For Mac users, ctrl + arrow is a bad idea

		if (shoot)
		{
			WeaponScript weapon = GetComponent<WeaponScript>();
			if (weapon != null)
			{
                //Debug.Log("Shooting");
                // false because the player is not an enemy
                weapon.Attack(false);
			}
		}
	}

	void FixedUpdate()
	{
		GetComponent<Rigidbody2D>().velocity = movement;
	}

	void Rotate(float inputX, float inputY)
	{
		float x = rotationVector.x,
			y = rotationVector.y,
			z = rotationVector.z;

		// Looking right
		if (inputX > 0)
		{
			x = 0;
			y = 0;
			z = 270;
		}
		// Looking left
		else if (inputX < 0)
		{
			x = 0;
			y = 0;
			z = 90;
		}

		// Looking up
		else if (inputY > 0)
		{
			x = 0;
			y = 0;
			z = 0;
		}
		// Looking down
		else if (inputY < 0)
		{
			x = 0;
			y = 0;
			z = 180;
		}

		rotationVector = new Vector3(x, y, z);
	}

}
