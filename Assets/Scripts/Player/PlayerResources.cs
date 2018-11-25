using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{

	private PlayerShooter m_playerShooter;
	[SerializeField]
	private uint m_playerShots, m_maxPlayerShots;

	void Start()
	{
		m_playerShots = m_maxPlayerShots;
		m_playerShooter = GetComponent<PlayerShooter>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
        if (other.CompareTag("ManaPot"))
        {
            Debug.Log("Player Shots: " + m_playerShots);
            if (m_playerShots < m_maxPlayerShots)
            {
                other.GetComponent<ManaPot>().OnCollected();
                m_playerShots++;
            }
        }
		//TODO: if has component of type spell,
		//check who owner and if different or take damage.
		else if(other.tag == "Spell");
		{
			Spell _otherSpell = other.GetComponent<Spell>();
			if(!_otherSpell)
			{
				Debug.LogError("Object " + other.name + " has tag Spell, but has no spell component.");
			}
			else
			{
				//TODO: Spawn particles
				if(!m_playerShooter.IsShielding())
				{
					Destroy(gameObject);
				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag.Contains("Spell"))
		{
			Destroy(other.gameObject);
			Destroy(gameObject);
		}
	}

	public bool HasPlayerShotsAvailable()
	{
		return m_playerShots > 0;
	}
	public void OnPlayerShoot()
	{
		m_playerShots--;
	}

}
