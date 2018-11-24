using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaSpawnerScript : MonoBehaviour {

    public GameObject manaPot;
    int potCounter = 0;
    float randomX;
    float randomY;
    Vector2 spawnerLocation;
    public float spawnRate = 2f;
    public int spawnLimit = 5;
    public float spawnTime = 0.0f;
    	
	// Update is called once per frame
	void Update () {
		
        if(potCounter < spawnLimit) { 
            if(Time.time > spawnTime)
            {
                spawnTime = Time.time + spawnRate;
                randomX = Random.Range(-10.0f, 10.0f);
                randomY = Random.Range(-4.0f, 4.0f);
                spawnerLocation = new Vector2(randomX, randomY);
                Instantiate(manaPot, spawnerLocation, Quaternion.identity);
                potCounter++;
            }
        }
    }
}
