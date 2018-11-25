using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
	private PlayerResources m_playerResources;
	[SerializeField]
	private List<GameObject> m_playerSpellPrefabs;
    private List<GameObject> m_playerShieldPrefabs;

	public float m_absoluteOffset;

	void Start()
	{
		m_playerResources = GetComponent<PlayerResources>();
		if(!m_playerResources)
		{
			Debug.LogError("No player resources component found on object: " + name);
		}

		if(m_absoluteOffset == 0f)
		{
			Debug.LogWarning("The player offset on game object " + name + "is 0." + 
							 "The spells may spawn inside the player.");
		}
	}

	public void PlayerShoot(Vector2 inputVector, int spellIndex)
	{
		//TODO: Check if player resources is holding a spell
		if(m_playerResources.CanShoot())
		{
			if(!(spellIndex >= m_playerSpellPrefabs.Count))
			{
				//TODO: Animator params for animating and place this code on another function
				//to call at the end of the animation;
				Vector3 _inputVector3D = new Vector3(inputVector.x, inputVector.y, 0f).normalized;
				Vector3 _initialSpellPosition = transform.position + _inputVector3D * m_absoluteOffset;
				GameObject _newSpell = Instantiate(m_playerSpellPrefabs[spellIndex], 
												  _initialSpellPosition, 
												  Quaternion.identity);
				_newSpell.GetComponent<Spell>().SetSpellVelocity(_inputVector3D);
				m_playerResources.OnPlayerShoot();
			}
		}
	}

	public void SpellBlock(Vector2 inputVector, int shieldIndex)
	{
        //TODO: Animator params for block animation
        Vector3 _inputVector3D = new Vector3(inputVector.x, inputVector.y, 0f).normalized;
        var playerObject = GameObject.Find("Player");
        Vector3 _shieldPosition = playerObject.transform.position;
        GameObject _newShield = Instantiate(m_playerShieldPrefabs[shieldIndex], _shieldPosition, Quaternion.identity);
	}
}
