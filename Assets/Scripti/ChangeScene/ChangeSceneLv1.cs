using UnityEngine;
using System.Collections;

public class ChangeSceneLv1 : MonoBehaviour {

	public float timer = 2;

	void  Update (){
		timer -= Time.deltaTime;
		
		if (timer <= 0){
			Application.LoadLevel("1LevelYksinPeli");
		}

	}
}