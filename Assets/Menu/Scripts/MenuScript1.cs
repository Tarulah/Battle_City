using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScript1 : MonoBehaviour
{
    public Canvas onlineMenu;
    public Canvas roomListMenu;

	public Canvas helpMenu;
    public Canvas helpMenuNextPage;
    public Canvas helpMenuNextPage1;

    public Canvas exitMenu;

    public Button onlineText;
    public Button createRoomText;
    public Button roomListText;
	public Button startText;
	public Button helpText;
	public Button exitText;
	
	void Start ()
	{
	    onlineMenu = onlineMenu.GetComponent<Canvas>();
		helpMenu = helpMenu.GetComponent<Canvas> ();
        helpMenuNextPage = helpMenuNextPage.GetComponent<Canvas>();
        helpMenuNextPage1 = helpMenuNextPage1.GetComponent<Canvas>();
        exitMenu = exitMenu.GetComponent<Canvas> ();
        roomListMenu = roomListMenu.GetComponent<Canvas>();

        startText = startText.GetComponent<Button> ();
	    onlineText = onlineText.GetComponent<Button>();
		helpText = helpText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
        createRoomText = createRoomText.GetComponent<Button>();
        roomListText = roomListText.GetComponent<Button>();

        onlineMenu.enabled = false;
		helpMenu.enabled = false;
        helpMenuNextPage.enabled = false;
        helpMenuNextPage1.enabled = false;
        exitMenu.enabled = false;
	    roomListMenu.enabled = false;
	}

    // Helpin eka sivu
    public void HelpPress(){    
	    onlineMenu.enabled = false;
        helpMenu.enabled = true;
        helpMenuNextPage.enabled = false;
        helpMenuNextPage1.enabled = false;
        exitMenu.enabled = false;
        roomListMenu.enabled = false;

		startText.enabled = false;
	    onlineText.enabled = false;
        helpText.enabled = false;
		exitText.enabled = false;
        createRoomText.enabled = false;
        roomListText.enabled = false;
    }

    // Helpin toinen sivu
    public void HelpNextPage() {    
        onlineMenu.enabled = false;
        helpMenu.enabled = false;
        helpMenuNextPage.enabled = true;
        helpMenuNextPage1.enabled = false;
        exitMenu.enabled = false;
        roomListMenu.enabled = false;

        startText.enabled = false;
        onlineText.enabled = false;
        helpText.enabled = false;
        exitText.enabled = false;
        createRoomText.enabled = false;
        roomListText.enabled = false;
    }

    // Helpin kolmas sivu
    public void HelpNextPage1() {
        onlineMenu.enabled = false;
        helpMenu.enabled = false;
        helpMenuNextPage.enabled = false;
        helpMenuNextPage1.enabled = true;
        exitMenu.enabled = false;
        roomListMenu.enabled = false;

        startText.enabled = false;
        onlineText.enabled = false;
        helpText.enabled = false;
        exitText.enabled = false;
        createRoomText.enabled = false;
        roomListText.enabled = false;
    }

    // Alkuvalikko
    public void BackAndNoPress(){   
        onlineMenu.enabled = false;
        helpMenu.enabled = false;
        helpMenuNextPage.enabled = false;
        helpMenuNextPage1.enabled = false;
        exitMenu.enabled = false;
        roomListMenu.enabled = false;

        startText.enabled = true;
        onlineText.enabled = true;
        helpText.enabled = true;
        exitText.enabled = true;
        createRoomText.enabled = false;
        roomListText.enabled = false;
    }

    // Exit valikko
	public void ExitPress(){
        onlineMenu.enabled = false;
        helpMenu.enabled = false;
        helpMenuNextPage.enabled = false;
        helpMenuNextPage1.enabled = false;
        exitMenu.enabled = true;
        roomListMenu.enabled = false;

        startText.enabled = false;
        onlineText.enabled = false;
        helpText.enabled = false;
        exitText.enabled = false;
        createRoomText.enabled = false;
        roomListText.enabled = false;
    }

    // Online valikko
    /*
    public void OnlinePress()
    {
        onlineMenu.enabled = true;
        helpMenu.enabled = false;
        helpMenuNextPage.enabled = false;
        helpMenuNextPage1.enabled = false;
        exitMenu.enabled = false;
        roomListMenu.enabled = false;

        startText.enabled = false;
        onlineText.enabled = false;
        helpText.enabled = false;
        exitText.enabled = false;
        createRoomText.enabled = false;
        roomListText.enabled = false;
    }


    // Jos painetaan Create Room-painiketta tai Online-painiketta
    public void CreateRoomPress()
    {
        onlineMenu.enabled = true;
        helpMenu.enabled = false;
        helpMenuNextPage.enabled = false;
        helpMenuNextPage1.enabled = false;
        exitMenu.enabled = false;
        roomListMenu.enabled = false;

        startText.enabled = false;
        onlineText.enabled = false;
        helpText.enabled = false;
        exitText.enabled = false;
        createRoomText.enabled = true;
        roomListText.enabled = true;
    }

    // Room List-painike
    public void RoomList()
    {
        onlineMenu.enabled = false;
        helpMenu.enabled = false;
        helpMenuNextPage.enabled = false;
        helpMenuNextPage1.enabled = false;
        exitMenu.enabled = false;
        roomListMenu.enabled = true;

        startText.enabled = false;
        onlineText.enabled = false;
        helpText.enabled = false;
        exitText.enabled = false;
        createRoomText.enabled = true;
        roomListText.enabled = true;
    }
    */

    public void PressOnline()
    {
        Application.LoadLevel(2);
    }

    // kun painetaan play-painiketta
    public void StartLevel(){	
		Application.LoadLevel (1);
	}

    // kun painetaan exit-painiketta
    public void ExitGame(){		
		Application.Quit ();
	}
}
