using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	public GameObject pauseMenu;
	private bool paused = false;

    public GameObject resume;
    public GameObject restart;
    public GameObject mainMenu;
    public GameObject exit;
    public GameObject soundToggle;

    // Use this for initialization
    void Start () {
		pauseMenu.SetActive (false);
        resume.SetActive(false);
        restart.SetActive(false);
        mainMenu.SetActive(false);
        exit.SetActive(false);
        soundToggle.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		if ((Input.GetKeyDown (KeyCode.Escape)) || (Input.GetKeyDown(KeyCode.P))){
			paused = !paused;
		}
		if (paused) {
			pauseMenu.SetActive(true);
            resume.SetActive(true);
            restart.SetActive(true);
            mainMenu.SetActive(true);
            exit.SetActive(true);
            soundToggle.SetActive(true);
            Time.timeScale = 0;
		}
		if (!paused) {
			pauseMenu.SetActive(false);
            resume.SetActive(false);
            restart.SetActive(false);
            mainMenu.SetActive(false);
            exit.SetActive(false);
            soundToggle.SetActive(false);
            Time.timeScale = 1f;
		}
	}

	public void Resume(){
		paused = false;
	}

	public void Restart(){
		Application.LoadLevel (Application.loadedLevel);
	}

	public void MainMenu(){
		Application.LoadLevel (0);
	}

	public void Quit(){
		Application.Quit ();
	}

}
