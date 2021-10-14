using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public static int index;

    public void OnTriggerEnter(Collider other)
    {
        index = FindObjectOfType<FoodSpawn>().foodSpawnPoint.IndexOf(this.gameObject);
    }
}
