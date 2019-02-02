using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RoomList : MonoBehaviour {

    private Transform panel;
    private List<GameObject> serverList;
    private GameObject scroll;
    private GameObject selectedObject;
    private Color unselectedColor;

    public void OnEnable()
    {
        if (serverList == null)
        {
            panel = transform.FindChild("Area/Panel");
            scroll = transform.FindChild("Scrollbar").gameObject;
            serverList = new List<GameObject>();
            unselectedColor = new Color(171 / 255.0f, 174 / 255.0f, 182 / 255.0f, 1);
        }
        InvokeRepeating("PopulateServerList", 0, 2);
    }

    public void OnDisable()
    {
        CancelInvoke();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject server = EventSystem.current.currentSelectedGameObject;
            if (server != null)
            {
                if (server.name == "ServerButton")
                {
                    if (selectedObject != null)
                        selectedObject.transform.FindChild("Image").GetComponent<Image>().color = unselectedColor;

                    selectedObject = server.transform.parent.gameObject;
                    selectedObject.transform.FindChild("Image").GetComponent<Image>().color = Color.white;
                }
            }
        }
    }

    public void PopulateServerList()
    {
        int i = 0;
        RoomInfo[] hostData = PhotonNetwork.GetRoomList();

        int selected = serverList.IndexOf(selectedObject);

        for (int j = 0; j < serverList.Count; j++)
        {
            Destroy(serverList[j]);
        }
        serverList.Clear();

        if (null != hostData)
        {
            for (i = 0; i < hostData.Length; i++)
            {
                if (!hostData[i].open)
                    continue;

                GameObject text = (GameObject)Instantiate(Resources.Load("ServerObject"));
                serverList.Add(text);
                text.transform.SetParent(panel, false);
                text.transform.FindChild("ServerText").GetComponent<Text>().text = hostData[i].name;
                text.transform.FindChild("PlayerText").GetComponent<Text>().text = hostData[i].playerCount + "/" + hostData[i].maxPlayers;
                text.transform.FindChild("MapText").GetComponent<Text>().text = hostData[i].customProperties["map"].ToString();
                text.transform.FindChild("GMText").GetComponent<Text>().text = hostData[i].customProperties["gm"].ToString();
                text.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, (i * -25), 0);
            }
        }
        if ((i * -25) < -290)
        {
            panel.GetComponent<RectTransform>().sizeDelta = new Vector2(400, (i * 25) + 30);
            scroll.SetActive(true);
        }
        else
        {
            panel.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 320);
            scroll.SetActive(false);
        }
        if (selected >= 0 && selected < serverList.Count)
        {
            selectedObject = serverList[selected];
            selectedObject.transform.FindChild("Image").GetComponent<Image>().color = Color.white;
        }
    }
}
