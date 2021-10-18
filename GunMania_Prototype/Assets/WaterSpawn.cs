using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpawn : MonoBehaviour
{
    public GameObject water;
    //public bool stopSpawning = false;
    public float spawnTime;
    private float currentTimeToSpawn;

    void Update()
    {
        if (currentTimeToSpawn > 0)
        {
            currentTimeToSpawn -= Time.deltaTime;
        }
        else
        {
            SpawnObject();
            currentTimeToSpawn = spawnTime;
        }
    }

    public void SpawnObject()
    {
        Instantiate(water, transform.position, transform.rotation);
    }
}
