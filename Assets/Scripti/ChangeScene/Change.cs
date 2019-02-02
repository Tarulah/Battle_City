using UnityEngine;
using System.Collections;

public class Change : MonoBehaviour {

	public float timer = 2;
	
	void  Update (){
		timer -= Time.deltaTime;
		
		if (timer <= 0){
			Application.LoadLevel("Level 2");
		}
		
	}
}
