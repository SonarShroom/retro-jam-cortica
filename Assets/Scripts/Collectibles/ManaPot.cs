using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPot : MonoBehaviour {

    public static ManaSpawnerScript spawnerInstance;

	public void OnCollected() {
        spawnerInstance.RemoveManaPot(gameObject);
        Destroy(gameObject);
    }
}
