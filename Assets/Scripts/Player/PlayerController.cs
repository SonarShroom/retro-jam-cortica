using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class PlayerController : MonoBehaviour
{
	private PlayerShooter m_playerShooter;
	private Vector2 inputVector;

	public GamePad.Index gamePadIndex;


	void Start()
	{
		m_playerShooter = GetComponent<PlayerShooter>();
		if(!m_playerShooter)
		{
			Debug.LogError("No player shooter component detected on the gameobject: " + name);
		}

		if(gamePadIndex == GamePad.Index.Any)
		{
			Debug.LogError("Gamepad index for player object " + name + " is not defined.");
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(gamePadIndex != GamePad.Index.Any)
		{
			
		}
	}
}
