using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
	[SerializeField]
	private uint m_playerHealth, m_maxPlayerHealth, m_playerShots, m_maxPlayerShots;

	void Start()
	{
		m_playerHealth = m_maxPlayerHealth;
		m_playerShots = m_maxPlayerShots;
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

	public uint PlayerHealth
	{
		get { return m_playerHealth; }
		set { m_playerHealth = value; }
	}

	public uint PlayerShots
	{
		get { return m_playerShots; }
		set { m_playerShots = value; }
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
