using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpawn : MonoBehaviour
{
    public GameObject spawnee;
    public float spawnTime;
    public float spawnDelay;

    void Start()
    {
        InvokeRepeating("SpawnWater", spawnTime, spawnDelay);
    }

    public void SpawnWater()
    {
        Instantiate(spawnee, transform.position, transform.rotation);
    }
}
