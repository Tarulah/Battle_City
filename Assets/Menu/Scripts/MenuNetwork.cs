//using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuNetwork : Photon.MonoBehaviour {

    [SerializeField] GameObject onlineWindow;
    [SerializeField] GameObject roomListWindow;
    [SerializeField] InputField roomName;
    [SerializeField] InputField nroOfPlayers;
    [SerializeField] Text connectionText1;
    [SerializeField] Text connectionText2;
    [SerializeField] Button createButton;
    [SerializeField] Text roomList;
    //[SerializeField] Transform[] spawnPoints;

    private int maxPlayers = 4;
    private RoomInfo[] roomsList;

    //public GameObject room;
    //public Camera myCam;

    //GameObject player;

    //GUIStyle textStyle;

    void Start () {
        PhotonNetwork.logLevel = PhotonLogLevel.ErrorsOnly;
        PhotonNetwork.ConnectUsingSettings("0.1");
        //setStyles();
    }
	
	void Update () {
        connectionText1.text = PhotonNetwork.connectionStateDetailed.ToString();
        connectionText2.text = PhotonNetwork.connectionStateDetailed.ToString();
    }

    // Process of joining to a room
    public void JoinLobby()
    {
        //if(createButton.onClick.AddListener())
        onlineWindow.SetActive(true);
        roomListWindow.SetActive(true);
    }
    // Liitytään huoneeseen
    public void JoinRoom()
    {
        RoomOptions ro = new RoomOptions() { isVisible = true, maxPlayers = 4 };
        PhotonNetwork.JoinOrCreateRoom(roomName.text, ro, TypedLobby.Default);
        PhotonNetwork.LoadLevel(1);
        /*
        if (photonView.isMine)
        {
            int index = Random.Range(0, spawnPoints.Length);
            player = PhotonNetwork.Instantiate("Player", spawnPoints[index].position, spawnPoints[index].rotation, 0);
        }
        */
    }

    void OnReceivedRoomListUpdate()
    {
        roomList.text = "";
        // Listaa huoneet
        RoomInfo[] rooms = PhotonNetwork.GetRoomList();
        foreach (RoomInfo room in rooms)
        {
            roomList.text += room.name + "\t";
            nroOfPlayers.text += room.maxPlayers + "\n";
            for (int i = 0; i < rooms.Length; i++)
            {
                GUI.Button(new Rect(203, 125 + (10 * i), 70, 50), "Join");
            }
        }
    }

    /*
    void setStyles()
    {
        textStyle.normal.textColor = Color.white;
        textStyle.fontSize = 30;
    }
    */
}
