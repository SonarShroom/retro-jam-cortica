using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class GameMode : MonoBehaviour {

	public List<GameObject> objectsActiveOnStart;
	
	// Update is called once per frame
	void Update () {
		if(GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.Any))
		{
			foreach(GameObject go in objectsActiveOnStart)
			{
				go.SetActive(true);
			}
			gameObject.SetActive(false);
		}
	}
}
