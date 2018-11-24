using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour {

	private GameObject m_owner;

	public GameObject Owner
	{
		get { return m_owner; }
		set { m_owner = value; }
	}
	public float m_spellSpeed;

	public void SetSpellVelocity(Vector2 inputVector)
	{
		Rigidbody2D _rigidbody = GetComponent<Rigidbody2D>();
		_rigidbody.velocity = inputVector * m_spellSpeed;
		Debug.Log("Velocity = " + _rigidbody.velocity);
	}
}
