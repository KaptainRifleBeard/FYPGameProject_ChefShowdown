using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sl_WaterSprayCollider : MonoBehaviour
{

    public Transform colliderPos;
    public GameObject col;

    public float spawnTime;
    public float spawnDelay;


    void Start()
    {
        InvokeRepeating("SpawnCollider", spawnTime, spawnDelay);

    }

    public void SpawnCollider()
    {
        Instantiate(col, colliderPos.position, Quaternion.identity);

    }
}
