using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameObject spawner;
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
        Instantiate(spawner, transform.position, transform.rotation);
    }
}
