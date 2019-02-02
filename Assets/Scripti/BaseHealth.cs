using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BaseHealth : MonoBehaviour
{
	public int startingHealth = 5;                            // The amount of health the player starts the game with.
	public int currentHealth;                                   // The current health the player has.
	public Slider healthSlider;                                 // Reference to the UI's health bar.
	public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
	public AudioClip deathClip;                                 // The audio clip to play when the player dies.
	public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.


	Animator anim;                                              // Reference to the Animator component.
	//AudioSource playerAudio;                                    // Reference to the AudioSource component.
	//PlayerMovement playerMovement;                              // Reference to the player's movement.
	//PlayerShooting playerShooting;                              // Reference to the PlayerShooting script.
	bool isDead;                                                // Whether the player is dead.
	bool damaged;                                               // True when the player gets damaged.

	public bool isEnemy = false;


	void Awake ()
	{
		//     Setting up the references.
		anim = GetComponent <Animator> ();
		//playerAudio = GetComponent <AudioSource> ();
		//playerMovement = GetComponent <PlayerMovement> ();
		//playerShooting = GetComponentInChildren <PlayerShooting> ();

		//     Set the initial health of the player.
		currentHealth = startingHealth;
	}



	void Update ()
	{
		//      If the player has just been damaged...
		if(damaged)
		{
			//     ... set the colour of the damageImage to the flash colour.
			//damageImage.color = flashColour;
		}
		//     Otherwise...
		else
		{
			//      ... transition the colour back to clear.
			//damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}

		//      Reset the damaged flag.
		damaged = false;
	}

	// Trigger
	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		// Is this a shot?
		ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
		if (shot != null)
		{
			// Avoid friendly fire
			if (shot.isEnemyShot)
			{
				TakeDamage(shot.damage);
				Debug.Log ("VIHOLLINEN OSUI TUKIKOHTAAN"+ currentHealth);
				Destroy(shot.gameObject);
			}
		}
	}


	public void TakeDamage (int damageCount)
	{
		damaged = true;

		//healthSlider.value = currentHealth;

		//playerAudio.Play ();

		currentHealth -= damageCount;

		if(currentHealth <= 0 && !isDead)
		{
			Death ();
		}
	}


	void Death ()
	{
		//         Set the death flag so this function won't be called again.
		isDead = true;

		//       Turn off any remaining shooting effects.
		//playerShooting.DisableEffects ();

		//       Tell the animator that the player is dead.
		if (anim != null)
			anim.SetTrigger ("Die");

		//       Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
		//playerAudio.clip = deathClip;
		//playerAudio.Play ();

		//        Turn off the movement and shooting scripts.
		//playerMovement.enabled = false;
		//playerShooting.enabled = false;
	}
}
