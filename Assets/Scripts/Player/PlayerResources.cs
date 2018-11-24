using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
	[SerializeAttribute]
	private uint m_playerHealth, m_maxPlayerHealth, m_playerShots, m_maxPlayerShots;

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
	public void OnPlayerShoot()
	{
		m_playerShots--;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		//TODO: if has component of type collectible
		//and player shots still less than max shots
		//add one to player shots and remove object from play

		//TODO: if has component of type spell,
		//check who owner and if different, take damage.
	}

}
