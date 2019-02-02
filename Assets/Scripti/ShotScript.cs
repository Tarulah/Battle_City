using UnityEngine;
using System.Collections;

public class ShotScript : MonoBehaviour
{
	
	// 1 - Designer variables
	
	/// <summary>
	/// Damage inflicted
	/// </summary>
	public int damage = 1;
	
	/// <summary>
	/// Projectile damage player or enemies?
	/// </summary>
	public bool isEnemyShot = false;
	
	void Start()
	{
		// 2 - Limited time to live to avoid any leak
		Destroy(gameObject, 10); // 10 sec
	}

	// Trigger
	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		string name = otherCollider.gameObject.name.ToLower();

		if (name.Contains ("seinä") || name.Contains ("wall") || name.Contains("MyPlayer")) 
		{
			//Debug.Log ("Destroying shot: " + name);
			Destroy(gameObject); // Destroy always shot if hits the walls
		}

	}



}
