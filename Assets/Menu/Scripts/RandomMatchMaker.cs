using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class RandomMatchMaker : Photon.MonoBehaviour
{

    private List<GameObject> currentRoom = new List<GameObject>();

    public GameObject RoomListPanel;
    public Transform roomOrigionObject;
    //Declare Variable for Photon View
    private PhotonView myPhotonView;
    //Declare Boolean for Shouting Sounds Across Network
    private bool shoutMarco;

    //Declare String for room lobby names
    private string roomName = "Room01";
    //Declare for room status.....
    private string roomStatus = "";

    private string password = "balls22!";

    private Hashtable pass = new Hashtable();

    [HideInInspector]
    public Text roomText;
    //Variables for max amount of players 0-20 "you can change this inside gui"
    private int maxPlayer = 1;
    private string maxPlayerString = "1";


    //Declare list of Room array.
    private Room[] game;

    //For GUI Scroll Bar *for large room list*
    private Vector2 scrollPosition;

    public bool oldGUI = false;

    private int roomAmount = 0;

    public GameObject roomListButton;

    // Use this for initialization
    void Start()
    {

        //Init PUN with version of you're game.
        PhotonNetwork.ConnectUsingSettings("0.1");

        /*
		pass [0] = password;

		RoomInfo[] dog;
		dog[0].customProperties
		*/
        //Grab RoomInfo[].customProperties = Grab the HashCode then thus I can recieve table 0 = password created.... if that room password is not null then we can output if room has password.

        //Debug.Log (pass[0].ToString());
    }


    void OnJoinedLobby()
    {
        //PhotonNetwork.JoinRandomRoom ();
    }


    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Cant Join random room!");

        //If we failed to join room create one *Might want to comment this one out*
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom()
    {
        //Create Our monster assign it to a game object variable disable it's controllers and make sure to turn the control system for third person script on if I'm connected. * joined room * 
        GameObject monster = PhotonNetwork.Instantiate("Prefabs/monster", Vector3.zero, Quaternion.identity, 0);
        CharacterController controller = monster.GetComponent<CharacterController>();
        controller.enabled = true;
        monster.GetComponent<ThirdPersonController>().isControllable = true;
        myPhotonView = monster.GetComponent<PhotonView>();


    }


    public void HostName(GameObject inputFieldName)
    {
        roomName = inputFieldName.GetComponent<InputField>().text;

        Debug.Log(roomName.ToString());

    }

    public void HostPlayer(GameObject inputFieldPlayers)
    {
        maxPlayer = int.Parse(inputFieldPlayers.GetComponent<InputField>().text);

        Debug.Log(maxPlayer.ToString());
    }

    public void HostPassword(GameObject inputFieldPassword)
    {
        password = inputFieldPassword.GetComponent<InputField>().text;

        Debug.Log(password.ToString());
    }

    public void CreateButton(GameObject button)
    {
        //Create The Room with the given values
        //if the room name is not equal to null also players are greater then 0
        if (roomName != "" && maxPlayer > 0)
        {
            //PhotonNetwork.CreateRoom(roomName, true, true, maxPlayer);

            Destroy(GameObject.FindGameObjectWithTag("LobbyCanvas").gameObject);
        }

    }

    public void ListRooms()
    {

        //Vector3 startPosition = new Vector3 (roomOrigionObject.position.x,roomOrigionObject.position.y,roomOrigionObject.position.z);

        /*
		for (int i =0; i < 10; i++) {


				currentRoom.Add( (GameObject) Instantiate(roomListButton));
				currentRoom[i].transform.SetParent(RoomListPanel.transform);
				currentRoom[i].GetComponent<RectTransform>().sizeDelta = new Vector3(1,1,1);
				currentRoom[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(-6,117 - (i * 30));

				}
		*/


        foreach (RoomInfo game in PhotonNetwork.GetRoomList()) // Each RoomInfo "game" in the amount of games created "rooms" display the fallowing.
        {


            //GUILayout.Box(game.name + " " + game.playerCount + "/" + game.maxPlayers); //Thus we are in a for loop of games rooms display the game.name provide assigned above, playercount, and max players provided. EX 2/20


            currentRoom.Add((GameObject)Instantiate(roomListButton));
            //GameObject currentRoom = Instantiate(roomListButton,startPosition,Quaternion.identity) as GameObject;
            currentRoom[roomAmount].transform.SetParent(RoomListPanel.transform);
            currentRoom[roomAmount].GetComponent<RectTransform>().transform.localScale = new Vector3(1, 1, 1);
            currentRoom[roomAmount].GetComponent<RectTransform>().localPosition = new Vector3(-6, 117 - (roomAmount * 30), 0);




            //RoomButton roomButton = currentRoom.GetComponent<RoomButton>();
            currentRoom[roomAmount].GetComponent<RoomButton>().name = game.name;

            currentRoom[roomAmount].GetComponent<RoomButton>().players = game.maxPlayers;


            roomAmount++;

            Debug.Log(currentRoom.Count.ToString());
            //roomButton.name = game.name;

            //PhotonNetwork.JoinRoom(game.name); // Next to each room there is a button to join the listed game.name in the current loop.

        }
    }



    void OnGUI()
    {



        //Show Detail of connection to master server
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());

        //GUILayout.Label (PhotonNetwork.GetPing ().ToString ());

        //Connection to master server lobby if joined 
        if (PhotonNetwork.connectionStateDetailed == PeerState.Joined)
        {

            // Assign the it player to the appropriate player ID 
            shoutMarco = GameLogic.playerWhoIsIt == PhotonNetwork.player.ID;

            //If I'm tagged then allow to say marco and send that threw an rpc using the method "Marco" to all targets with network view.
            if (shoutMarco && GUILayout.Button("Marco!"))
            {
                this.myPhotonView.RPC("Marco", PhotonTargets.All);
            }
            //I'f im not tagged then I can now say POLO to all network view.. with method RpC "Polo"
            if (!shoutMarco && GUILayout.Button("Polo"))
            {
                this.myPhotonView.RPC("Polo", PhotonTargets.All);
            }
        }


        //If I'm connected and inside lobby
        if (PhotonNetwork.insideLobby == true)
        {
            if (oldGUI)
            {

                //Display the lobby connection list and room creation.
                GUI.Box(new Rect(Screen.width / 2.5f, Screen.height / 3, 400, 550), "");
                GUILayout.BeginArea(new Rect(Screen.width / 2.5f, Screen.height / 3f, 400, 500));
                GUI.color = Color.red;
                GUILayout.Box("Lobby");
                GUI.color = Color.white;

                GUILayout.Label("Room Name:");
                roomName = GUILayout.TextField("Room Name"); //For network room name ask and recieve
                GUILayout.Label("Max Amount of player 1-20:");
                maxPlayerString = GUILayout.TextField(maxPlayerString, 2); //How man players with a max character set of 2 allowing no more then 2 digit player size.

                if (maxPlayerString != "")
                { // if there is a character of max players 

                    maxPlayer = int.Parse(maxPlayerString); // parse the max player text field into a string.

                    if (maxPlayer > 20) maxPlayer = 20; // if I enter above 20 reset the max to 20 .
                    if (maxPlayer == 0) maxPlayer = 1; // if i'm below 1 reset min of 1

                }
                else
                {
                    maxPlayer = 1; // 1
                }

                if (GUILayout.Button("Create Room"))
                {

                    if (roomName != "" && maxPlayer > 0) // if the room name has a name and max players are larger then 0
                    {
                        //PhotonNetwork.CreateRoom(roomName, true, true, maxPlayer); // then create a photon room visible , and open with the maxplayers provide by user.

                    }
                }

                GUILayout.Space(20);
                GUI.color = Color.red;
                GUILayout.Box("Game Rooms");
                GUI.color = Color.white;
                GUILayout.Space(20);

                scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true, GUILayout.Width(400), GUILayout.Height(300));

                foreach (RoomInfo game in PhotonNetwork.GetRoomList()) // Each RoomInfo "game" in the amount of games created "rooms" display the fallowing.
                {

                    GUI.color = Color.green;
                    GUILayout.Box(game.name + " " + game.playerCount + "/" + game.maxPlayers); //Thus we are in a for loop of games rooms display the game.name provide assigned above, playercount, and max players provided. EX 2/20
                    GUI.color = Color.white;

                    if (GUILayout.Button("Join Room"))
                    {

                        PhotonNetwork.JoinRoom(game.name); // Next to each room there is a button to join the listed game.name in the current loop.
                    }
                }

                GUILayout.EndScrollView();
                GUILayout.EndArea();

            }


        }
    }
}