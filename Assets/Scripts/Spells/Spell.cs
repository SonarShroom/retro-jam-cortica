using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour {

    [SerializeField]
    private string m_spellColor = "Green";

    public List<Sprite> m_spellLevelSprite;
	public float m_initialSpellSpeed;
    public float m_speedIncrementPerLevel = 2;
    public int m_maxSpellLevel = 2;
    public int m_currentSpellLevel = 0;
    public uint m_maxBounces = 3;

	public void SetSpellVelocity(Vector2 inputVector)
	{
		Rigidbody2D _rigidbody = GetComponent<Rigidbody2D>();
		_rigidbody.velocity = inputVector * (m_initialSpellSpeed + m_currentSpellLevel * m_speedIncrementPerLevel);
	}

    public void LevelUp()
    {
        if(!(m_currentSpellLevel >= m_maxSpellLevel))
        {
            m_currentSpellLevel++;
        }
        GetComponent<SpriteRenderer>().sprite = m_spellLevelSprite[m_currentSpellLevel];
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Glass")
        {
            m_maxBounces--;
            if(m_maxBounces == 0)
            {
                Destroy(gameObject);
            }
        }

        if (other.gameObject.tag.Contains("Spell"))
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == m_spellColor + "Shield")
        {
            //TODO: Hold spell and change its level
            gameObject.SetActive(false);
            other.transform.parent.gameObject.GetComponent<PlayerShooter>().SpellSteal(gameObject);
        }
    }
}
