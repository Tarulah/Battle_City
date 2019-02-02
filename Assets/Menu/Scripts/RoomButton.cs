using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RoomButton : MonoBehaviour
{
    public string name;
    public int players;
    public int ping;
    public GameObject uiCanvas;

    void Start()
    {
        int currentPlayers = PhotonNetwork.countOfPlayersInRooms;

        transform.GetChild(0).GetComponent<Text>().text = name + "                        " + currentPlayers.ToString() + "/" + players.ToString();
        uiCanvas = GameObject.FindGameObjectWithTag("LobbyCanvas").gameObject;
    }

    public void JoinGame()
    {
        PhotonNetwork.JoinRoom(name);
        Destroy(uiCanvas.gameObject);

    }
}