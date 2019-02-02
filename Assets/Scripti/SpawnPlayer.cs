using UnityEngine;
using System.Collections;

public class SpawnPlayer : MonoBehaviour {

    public Vector3 pos;
    public float xOffset;
    public float yOffset;

    // Use this for initialization
    void Start () {
        pos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x + xOffset, transform.position.y + yOffset, transform.position.z));
        pos.y = Screen.height - pos.y;
    }
	
	// Update is called once per frame
	void Update () {
        pos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x + xOffset, transform.position.y + yOffset, transform.position.z));
        pos.y = Screen.height - pos.y;
    }
}
