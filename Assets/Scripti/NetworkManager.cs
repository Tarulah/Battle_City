using UnityEngine;
using System.Collections;

public class NetworkManager : Photon.PunBehaviour
{
    private const string roomName = "RoomName";
    private RoomInfo[] roomsList;
    public Camera MyCam;
    //public Transform[] spawnPoints;
    public Transform spawnPoint;
    GameObject player;

	// Use this for initialization
	void Start ()
	{
	    PhotonNetwork.logLevel = PhotonLogLevel.ErrorsOnly;
	    PhotonNetwork.ConnectUsingSettings("0.1");
	}

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());

        if (PhotonNetwork.room == null)
        {
            //Create room
            if (GUI.Button(new Rect(50, 50, 200, 50), "Start Server"))
            {
                PhotonNetwork.CreateRoom(roomName + System.Guid.NewGuid().ToString("N"));
            }

            //Join room
            if (roomsList != null)
            {
                for (int i = 0; i < roomsList.Length; i++)
                {
                    if(GUI.Button(new Rect(50, 110 + (10 * i), 200, 50), "Join" + roomsList[i].name))
                    {
                        RoomOptions ro = new RoomOptions() { isVisible = true, maxPlayers = 4 };
                        PhotonNetwork.JoinOrCreateRoom(roomsList[i].name, ro, TypedLobby.Default);
                    }
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public override void OnJoinedLobby()
    {
        //Debug.Log("Tultiin lobbyyn!");
    }

    //Tämä päivittää huonelistan muuttujaa olemassa olevilla huoneilla
    void OnReceivedRoomListUpdate()
    {
        roomsList = PhotonNetwork.GetRoomList();
    }

    private void OnJoinedRoom()
    {
        //Tultiin huoneeseen, tässä kohtaa pitää istansioida pelihahmo
        //Application.LoadLevel(1);
        //PhotonNetwork.Instantiate("MyPlayer", spawnPoint.position, spawnPoint.rotation, 0);
        //Debug.Log("Pelaaja pelissä!");
        //GameObject player = PhotonNetwork.Instantiate("MyPlayer", new Vector3(0, 2, 0), Quaternion.Euler(0, 0, 0), 0);
    }

    private void OnCreateRoom()
    {
        // Tässä voidaan luoda juttuja, jotka tarvitaan pelin alussa
        // Rakennutkset, viholliset, tms.

    }

}











