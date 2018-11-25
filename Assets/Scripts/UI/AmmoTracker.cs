using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoTracker : MonoBehaviour {

	[SerializeField]
	private GameObject m_playerToTrack;
	[SerializeField]
	private List<Sprite> m_ammoIndicators;
	
	void Update () {
		if(!m_playerToTrack)
			return;
		int _currentPlayerAmmo = (int)m_playerToTrack.GetComponent<PlayerResources>().m_playerShots;
		GetComponent<Image>().sprite = m_ammoIndicators[_currentPlayerAmmo - 1];
	}

}
