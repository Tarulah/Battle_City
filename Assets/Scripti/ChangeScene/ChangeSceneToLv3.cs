using UnityEngine;
using System.Collections;

public class ChangeSceneToLv3 : MonoBehaviour {

	public float timer = 2;
	
	void  Update (){
		timer -= Time.deltaTime;
		
		if (timer <= 0){
			Application.LoadLevel("Level 3");
		}
		
	}
}
