using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
	private PlayerResources m_playerResources;
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

	public void TryPlayerShoot()
	{
		//TODO: Check if player resources is holding a spell
		if(m_playerResources.CanShoot())
		{
			m_playerResources.OnPlayerShoot();
		}
	}
}
