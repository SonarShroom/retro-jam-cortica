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

	private Rigidbody2D m_rigidbody2D;
	private PlayerShooter m_playerShooter;
	private PlayerAction m_playerAction;
	private Vector2 m_inputVector;
	[SerializeField]
	private bool canShoot;

	public GamePad.Index m_gamePadIndex;
	public float m_playerSpeed;

	void Start()
	{
		m_rigidbody2D = GetComponent<Rigidbody2D>();

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
		ProcessPlayerMovement();
		ProcessPlayerActions();
	}

	void ProcessPlayerMovement()
	{
		if(m_gamePadIndex != GamePad.Index.Any)
		{
			Vector2 _inputDpad = GamePad.GetAxis(GamePad.Axis.Dpad, m_gamePadIndex), 
					_inputLeftStick = GamePad.GetAxis(GamePad.Axis.LeftStick, m_gamePadIndex);
			
			if(_inputLeftStick != Vector2.zero) { m_inputVector = _inputLeftStick; }
			else { m_inputVector = _inputDpad; }

		}
		else
		{
			m_inputVector = Vector2.zero;
		}

		Vector2 _velocity = m_inputVector * m_playerSpeed;
		m_rigidbody2D.velocity = _velocity;
	}

	void ProcessPlayerActions()
	{
		if(canShoot && m_gamePadIndex != GamePad.Index.Any)
		{
		}
	}
}
