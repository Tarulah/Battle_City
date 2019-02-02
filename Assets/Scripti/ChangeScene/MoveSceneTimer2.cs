using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveSceneTimer2 : MonoBehaviour {

	public float timer = 60;
	Text text;
	
	void Awake (){
		
		text = GetComponent <Text> ();
		
	}
	void  Update (){
		timer -= Time.deltaTime;
		
		if (timer <= 0){
			Application.LoadLevel("Leve3start");
		}
		
		if (text != null)
			text.text = "Time: " + Mathf.Round(timer);
	}
}
