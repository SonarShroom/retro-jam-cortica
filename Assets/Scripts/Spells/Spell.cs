using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour {

	public float m_spellSpeed;

	public void SetSpellVelocity(Vector2 inputVector)
	{
		Rigidbody2D _rigidbody = GetComponent<Rigidbody2D>();
		_rigidbody.velocity = inputVector * m_spellSpeed;
	}

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Contains("Spell"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        if (this.tag.Equals("RedSpell") && other.gameObject.tag == "RedShield")
        {
            //TODO: Deflect RedSpell on RedShield
        }
        if (this.tag.Equals("GreenSpell") && other.gameObject.tag == "GreenShield")
        {
            //TODO: Deflect GreenSpell on GreenShield
        }
    }
}
