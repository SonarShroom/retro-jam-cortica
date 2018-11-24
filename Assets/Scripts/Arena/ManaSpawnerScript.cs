using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaSpawnerScript : MonoBehaviour {

    public GameObject manaPot;
    public List<GameObject> pots = new List<GameObject>();
    float randomX;
    float randomY;
    Vector2 spawnerLocation;
    public float spawnRate = 2f;
    public int spawnLimit = 5;
    public float spawnTime = 0.0f;

    private void Start()
    {
        ManaPot.spawnerInstance = this;
    }
    
    void Update () {
        spawnTime += Time.deltaTime;
        if (pots.Count < spawnLimit) {
            if (spawnTime > spawnRate) {
                spawnTime += Time.deltaTime;
                randomX = Random.Range(-10.0f, 10.0f);
                randomY = Random.Range(-4.0f, 4.0f);
                spawnerLocation = new Vector2(randomX, randomY);
                pots.Add(Instantiate(manaPot, spawnerLocation, Quaternion.identity));
                spawnTime = 0.0f;
            }
        }
        else
        {
            spawnTime = 0.0f;
        }
    }

    public void RemoveManaPot(GameObject manaPotToRemove)
    {
        pots.Remove(manaPotToRemove);
    }
}
