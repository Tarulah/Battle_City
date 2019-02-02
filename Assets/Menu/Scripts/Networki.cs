using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Networki : Photon.MonoBehaviour {

    // Serielizefield makes private objects visible in inspector
    //[SerializeField] Text connectionText;
    public Transform[] spawnPoints;
    public Camera sceneCamera;
    public Canvas healthBar;
    public Canvas textCanvas;
    public Camera miniMap;

    GameObject player;

    // Use this for initialization
    void Start()
    {
        healthBar = healthBar.GetComponent<Canvas>();
        textCanvas = textCanvas.GetComponent<Canvas>();
        miniMap = miniMap.GetComponent<Camera>();
        healthBar.enabled = false;
        textCanvas.enabled = false;
        miniMap.enabled = false;
        /*
        PhotonNetwork.logLevel = PhotonLogLevel.ErrorsOnly;
        PhotonNetwork.ConnectUsingSettings("0.1");
        */
    }

    // Update is called once per frame
    void Update() {
        //connectionText.text = PhotonNetwork.connectionStateDetailed.ToString();
        //GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    // Process of joining to a room
    public void JoinLobby()
    {
        //serverWindow.SetActive(true);
    }

    // Liitytään huoneeseen
    public void JoinedLobby()
    {
        //RoomOptions ro = new RoomOptions() { isVisible = true, maxPlayers = 10 };
        //PhotonNetwork.JoinOrCreateRoom("Hello", ro, TypedLobby.Default);
        //PhotonNetwork.LoadLevel(1);
    }

    void OnJoinedRoom()
    {
        //serverWindow.SetActive(false);
        StartSpawnProcess(0f);
    }

    /*
    void OnReceivedRoomListUpdate()
    {
        // Listaa huoneet
        RoomInfo[] rooms = PhotonNetwork.GetRoomList();
        foreach (RoomInfo room in rooms)
        {
            roomList.text += room.name + "\n";
        }
    }
    */

    void StartSpawnProcess(float respawnTime)
    {
        // kun pelaaja kuolee, pelaaja näkee blank screenin
        sceneCamera.enabled = true;
        healthBar.enabled = false;
        textCanvas.enabled = false;
        StartCoroutine("SpawnPlayer", respawnTime);
    }

    IEnumerator SpawnPlayer(float respawnTime)
    {
        yield return new WaitForSeconds(respawnTime);   // odotetaan hetki ennen kuin spawnataan

        // valitsee random spawn kohdan spawnPoints listasta
        int index = Random.Range(0, spawnPoints.Length);
        player = PhotonNetwork.Instantiate("MyPlayer", spawnPoints[index].position, spawnPoints[index].rotation, 0);
        sceneCamera.enabled = false;
        healthBar.enabled = true;
        textCanvas.enabled = true;
        miniMap.enabled = true;
    }
}
