using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sl_DishSpawnTimer : MonoBehaviour
{
    public GameObject[] colliderPos;

    public float spawnTime;
    public float spawnDelay;

    void Start()
    {
        InvokeRepeating("SpawnCollider", spawnTime, spawnDelay);
    }

    public void SpawnTimer()
    {

    }
}
