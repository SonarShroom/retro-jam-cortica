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
	private Vector2 m_lastNonZeroVector;

	[SerializeField]
	private bool m_canShoot;

	public GameObject m_aimReticule;
	public GamePad.Index m_gamePadIndex;
	public static GamePad.Button PLAYER_SPELL_ONE_BUTTON = GamePad.Button.X;
	public static GamePad.Button PLAYER_SPELL_TWO_BUTTON = GamePad.Button.A;
	public static GamePad.Button PLAYER_SPELL_1_BLOCK_BUTTON = GamePad.Button.Y;
	public static GamePad.Button PLAYER_SPELL_2_BLOCK_BUTTON = GamePad.Button.B;
	public float m_playerSpeed;

	void Start()
	{
		if(!m_aimReticule)
		{
			Debug.LogError("No aim reticule child object found on object: " + name);
		}

		m_rigidbody2D = GetComponent<Rigidbody2D>();
		if(!m_rigidbody2D)
		{
			Debug.LogError("No Rigidbody2D component detected on the gameobject: " + name);
		}

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
		ConsumePlayerMovement();
		ProcessPlayerActions();
	}

	void ProcessPlayerMovement()
	{
		if(m_gamePadIndex != GamePad.Index.Any)
		{
			Vector2 _inputLeftStick = GamePad.GetAxis(GamePad.Axis.LeftStick, m_gamePadIndex);
			
			if(_inputLeftStick != Vector2.zero)
			{
				m_lastNonZeroVector = _inputLeftStick;
			}

			m_inputVector = _inputLeftStick;
		}
		else
		{
			m_inputVector = Vector2.zero;
		}
	}

	void ConsumePlayerMovement()
	{
		
		Vector2 _velocity = m_inputVector * m_playerSpeed;
		m_rigidbody2D.velocity = _velocity;

		m_playerShooter.m_inputVector = m_lastNonZeroVector;

		float _angle = Mathf.Atan2(m_lastNonZeroVector.y, m_lastNonZeroVector.x) * Mathf.Rad2Deg;
		m_aimReticule.transform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
		m_aimReticule.transform.position = transform.position;
	}

	void ProcessPlayerActions()
	{
		if(m_canShoot && m_gamePadIndex != GamePad.Index.Any)
		{
			if(GamePad.GetButtonDown(PLAYER_SPELL_ONE_BUTTON, m_gamePadIndex))
			{
				m_playerShooter.SetupAnimatorShoot(0);
			}
			else if(GamePad.GetButtonDown(PLAYER_SPELL_TWO_BUTTON, m_gamePadIndex))
			{
				m_playerShooter.SetupAnimatorShoot(1);
			}
			else if(GamePad.GetButton(PLAYER_SPELL_1_BLOCK_BUTTON, m_gamePadIndex))
			{
				m_playerShooter.SetupAnimatorBlock(0);
			}
			else if(GamePad.GetButton(PLAYER_SPELL_2_BLOCK_BUTTON, m_gamePadIndex))
			{
				m_playerShooter.SetupAnimatorBlock(1);
			}
		}
	}

}
