using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour {

	void Update () {
		//transform.Translate (1f*Time.deltaTime,0f,0f);
		if (Input.GetKey (KeyCode.RightArrow)) {
			float movespeed = 0.0f;
			movespeed++;
			transform.Translate (movespeed, 0, 0);
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			float movespeed = 0.0f;
			movespeed++;
			transform.Translate (-movespeed, 0, 0);
		} else if (Input.GetKey (KeyCode.UpArrow)) {
			float movespeed = 0.0f;
			movespeed++;
			transform.Translate (0,movespeed, 0);
		}
		else if (Input.GetKey (KeyCode.DownArrow)) {
			float movespeed = 0.0f;
			movespeed++;
			transform.Translate (0,-movespeed, 0);
		}
		
	}
}