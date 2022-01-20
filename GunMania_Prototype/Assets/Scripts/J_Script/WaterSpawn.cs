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

        /*
        10, 20, 30sec delay, 3sec shoot 

         1st = 10 + 3 = 13sec
        2nd = 13 + 13 = 26sec
        3rd = 26 + 13 = 39sec
         
         */
    }

    public void SpawnWater()
    {
        Instantiate(spawnee, spawnPos.position, spawnPos.rotation);

    }


}
