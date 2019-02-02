using UnityEngine;

public class GameOverManager : MonoBehaviour
{
  //  public GameObject playerHealth;       // Reference to the player's health.
    public float restartDelay = 5f;         // Time to wait before restarting the level

	public PlayerHealth playerHealth;
	//public BaseHealth baseHealth;
    Animator anim;                          // Reference to the animator component.
    float restartTimer;                     // Timer to count up to restarting the level


    void Awake()
    {
        // Set up the reference.
        anim = GetComponent<Animator>();
    }

	void Start()
    {
		playerHealth = GameObject.Find(GlobalVars.playerTankName).GetComponent<PlayerHealth> ();
        //baseHealth = GameObject.Find(GlobalVars.playerBase).GetComponent<BaseHealth>();
    }

    void Update()
    {

        //PlayerHealth playerHealth = GetComponent<PlayerHealth>();

        // If the player has run out of health...
        //if (playerHealth.currentHealth <= 0 || baseHealth.currentHealth <= 0)
        if (playerHealth.currentHealth <= 0)
        {

			Debug.Log("KUOLIT");
            // ... tell the animator the game is over.
            anim.SetTrigger("GameOver");

            // .. increment a timer to count up to restarting.
            restartTimer += Time.deltaTime;

            // .. if it reaches the restart delay...
            if (restartTimer >= restartDelay)
            {
                // .. then reload the currently loaded level.
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

}
