using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public static int index;
    public FoodSpawn s;

    public void OnTriggerEnter(Collider other)
    {
        index = FindObjectOfType<FoodSpawn>().SpawnPoint.IndexOf(this.gameObject);
        //s.checkRespawn(index);
        Debug.Log(index);
    }
}
