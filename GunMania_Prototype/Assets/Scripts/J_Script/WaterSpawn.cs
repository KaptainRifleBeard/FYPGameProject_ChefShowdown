using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpawn : MonoBehaviour
{
    public Transform spawnPos;
    public GameObject spawnee;
    public float spawnTime;
    public float spawnDelay;

    void Start()
    {

        InvokeRepeating("SpawnWater", spawnTime, spawnDelay);

        //after 5secs stop, wait 10secs spawn again. so its 15secs
    }

    public void SpawnWater()
    {
        Instantiate(spawnee, spawnPos.position, spawnPos.rotation);
    }


}
