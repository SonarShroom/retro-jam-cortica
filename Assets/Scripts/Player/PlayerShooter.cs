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
	private Animator m_animator;
	private uint m_nextSpellIndex;

	public Vector2 m_inputVector;
	public float m_absoluteOffset;

	void Start()
	{
		m_playerResources = GetComponent<PlayerResources>();
		if(!m_playerResources)
		{
			Debug.LogError("No player resources component found on object: " + name);
		}

		m_animator = GetComponent<Animator>();
		if(!m_animator)
		{
			Debug.LogError("No animator present on gameobject: " + name);
		}

		if(m_absoluteOffset == 0f)
		{
			Debug.LogWarning("The player offset on game object " + name + " is 0." + 
							 "The spells may spawn inside the player.");
		}
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

	private void PlayerShoot()
	{
		Vector3 _inputVector3D = new Vector3(inputVector.x, inputVector.y, 0f).normalized;
		Vector3 _initialSpellPosition = transform.position + _inputVector3D * m_absoluteOffset;
		GameObject _newSpell = Instantiate(m_playerSpellPrefabs[spellIndex], 
											_initialSpellPosition, 
											Quaternion.identity);
		_newSpell.GetComponent<Spell>().SetSpellVelocity(_inputVector3D);
		m_playerResources.OnPlayerShoot();
	}

	private void SpellBlock()
	{
        //TODO: Spawn blocking shield for a small ammount of time
		GameObject _newSpellBlock = Instantiate(m_playerShieldPrefabs[m_nextSpellIndex],
												transform);
	}
}
