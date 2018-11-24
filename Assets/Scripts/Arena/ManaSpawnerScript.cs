using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaSpawnerScript : MonoBehaviour {

    public GameObject manaPot;
    float randomX;
    float randomY;
    Vector2 spawnerLocation;
    public float spawnRate = 2f;
    float nextSpawn = 5.0f;
    	
	// Update is called once per frame
	void Update () {
		
        if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            randomX = Random.Range(-10.0f, 10.0f);
            randomY = Random.Range(-4.0f, 4.0f);
            spawnerLocation = new Vector2(randomX, randomY);
            Instantiate(manaPot, spawnerLocation, Quaternion.identity);
        }
	}
}
