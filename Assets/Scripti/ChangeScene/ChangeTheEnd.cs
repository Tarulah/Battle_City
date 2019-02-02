using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeTheEnd : MonoBehaviour {

	public float timer = 60;
	Text text;
	
	void Awake (){
		
		text = GetComponent <Text> ();
		
	}
	void  Update (){
		timer -= Time.deltaTime;
		
		if (timer <= 0){
			Application.LoadLevel("Leve1start");
		}
		
		if (text != null)
			text.text = "Time: " + Mathf.Round(timer);
	}
}
