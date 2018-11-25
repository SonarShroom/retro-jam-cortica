using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
	private enum PlayerShooterAction
	{
		Spell,
		SpellBlock
	}

	private PlayerShooterAction m_nextShooterAction;
	private PlayerResources m_playerResources;
	[SerializeField]
	private List<GameObject> m_playerSpellPrefabs;
	[SerializeField]
    private List<GameObject> m_playerShieldPrefabs;
	[SerializeField]
	private List<GameObject> m_playerSpellHoldPrefabs;
	private GameObject m_stolenSpell;
	private GameObject m_currentShield;
	[SerializeField]
	private AudioClip m_spellShootingSound;
	private Animator m_animator;
	private uint m_nextSpellIndex;
	private float m_maxSpellShieldTime = 0.75f;

	public Vector2 m_inputVector;
	public float m_absoluteOffset;
	public float m_currentSpellShieldTime;

	void Start()
	{
		m_playerResources = GetComponent<PlayerResources>();
		if(!m_playerResources)
		{
			Debug.LogError("No player resources component found on object: " + name);
		}

		m_animator = GetComponent<Animator>();

		if(m_absoluteOffset == 0f)
		{
			Debug.LogWarning("The player offset on game object " + name + " is 0." + 
							 "The spells may spawn inside the player.");
		}
	}

	void Update()
	{
		if(m_currentSpellShieldTime < 0f)
		{
			Destroy(m_currentShield);
		}
		else
		{
			m_currentSpellShieldTime -= Time.deltaTime;
		}
	}

	public bool IsShielding()
	{
		return m_currentShield != null;
	}
	
	public bool IsHoldingSpell()
	{
		return m_stolenSpell != null;
	}

	public void SetupAnimatorShoot(uint spellIndex)
	{
		//TODO: Check if player resources is holding a spell
		if(m_playerResources.HasPlayerShotsAvailable())
		{
			if(!(spellIndex >= m_playerSpellPrefabs.Count))
			{
				//TODO: Animator params for animating and place this code on another function
				//to call at the end of the animation;
				m_nextSpellIndex = spellIndex;
				m_nextShooterAction = PlayerShooterAction.Spell;
				m_animator.SetTrigger("ShootSpell");
			}
		}
	}

	public void SetupAnimatorBlock(uint spellIndex)
	{
		if(!(spellIndex >= m_playerSpellPrefabs.Count))
		{
			m_nextSpellIndex = spellIndex;
			m_nextShooterAction = PlayerShooterAction.SpellBlock;
			m_animator.SetTrigger("ShootSpell");
		}
	}

	public void CastNextAction()
	{
		Debug.Log("m_nextShooterAction = " + m_nextShooterAction);
		switch(m_nextShooterAction)
		{
			case PlayerShooterAction.Spell:
				PlayerShoot();
				break;
			case PlayerShooterAction.SpellBlock:
				SpellBlock();
				break;
		}
	}

	public void SpellSteal(GameObject spell)
	{
		m_stolenSpell = spell;
	}

	private void PlayerShoot()
	{
		Vector3 _inputVector3D = new Vector3(m_inputVector.x, m_inputVector.y, 0f).normalized;
		Vector3 _initialSpellPosition = transform.position + _inputVector3D * m_absoluteOffset;
		GameObject _newSpell;
		if(m_stolenSpell)
		{
			_newSpell = m_stolenSpell;
			_newSpell.SetActive(true);
			_newSpell.transform.position = _initialSpellPosition;
			_newSpell.GetComponent<Spell>().LevelUp();
			_newSpell.GetComponent<Spell>().m_maxBounces = 3;
			m_stolenSpell = null;
		}
		else
		{
			_newSpell = Instantiate(m_playerSpellPrefabs[(int)m_nextSpellIndex], 
											_initialSpellPosition, 
											Quaternion.identity);
			m_playerResources.OnPlayerShoot();
		}
		_newSpell.GetComponent<Spell>().SetSpellVelocity(_inputVector3D);
		AudioSource.PlayClipAtPoint(m_spellShootingSound, _initialSpellPosition);
	}

	private void SpellBlock()
	{
        //TODO: Spawn blocking shield for a small ammount of time
		if(!m_currentShield)
		{
			m_currentShield = Instantiate(m_playerShieldPrefabs[(int)m_nextSpellIndex],
												transform);
			m_currentSpellShieldTime = m_maxSpellShieldTime;
		}
	}
}
