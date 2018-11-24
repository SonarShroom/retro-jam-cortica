using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class PlayerController : MonoBehaviour
{
	private enum PlayerAction
	{
		FireSpell1,
		FireSpell2,
		Block
	}

	private Transform m_transform;
	private PlayerShooter m_playerShooter;
	
	private Queue<PlayerAction> m_playerActionQueue;
	private PlayerAction m_playerAction;
	public GamePad.Index m_gamePadIndex;
	private Vector2 m_inputVector;
	public float m_playerSpeed;

	void Start()
	{
		m_transform = GetComponent<Transform>();

		m_playerShooter = GetComponent<PlayerShooter>();
		if(!m_playerShooter)
		{
			Debug.LogError("No player shooter component detected on the gameobject: " + name);
		}

		if(m_gamePadIndex == GamePad.Index.Any)
		{
			Debug.LogError("Gamepad index for player object " + name + " is not defined.");
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		ProcessPlayerInput();
		ConsumePlayerInput();
	}

	void ProcessPlayerInput()
	{
		if(m_gamePadIndex != GamePad.Index.Any)
		{
			Vector2 _inputDpad = GamePad.GetAxis(GamePad.Axis.Dpad, m_gamePadIndex), 
					_inputLeftStick = GamePad.GetAxis(GamePad.Axis.LeftStick, m_gamePadIndex);
			
			if(_inputLeftStick != Vector2.zero) { m_inputVector = _inputLeftStick; }
			else { m_inputVector = _inputDpad; }

			if(GamePad.GetButtonDown(GamePad.Button.X, m_gamePadIndex))
			{
				m_playerActionQueue.Enqueue(PlayerAction.FireSpell1);
			}

			if(GamePad.GetButtonDown(GamePad.Button.Y, m_gamePadIndex))
			{
				m_playerActionQueue.Enqueue(PlayerAction.FireSpell2);
			}

			if(GamePad.GetButtonDown(GamePad.Button.B, m_gamePadIndex))
			{
				m_playerActionQueue.Enqueue(PlayerAction.Block);
			}
		}
	}

	void ConsumePlayerInput()
	{
		if(m_inputVector != Vector2.zero)
		{
			Vector2 _diffVector = m_inputVector * m_playerSpeed * Time.deltaTime;
			m_transform.position += new Vector3(_diffVector.x, _diffVector.y, 0f);
		}

		
	}
}
