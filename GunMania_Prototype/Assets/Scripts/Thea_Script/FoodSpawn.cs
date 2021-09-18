using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawn : MonoBehaviour
{
    [Header("Spawn Points")]
    public List<GameObject> SpawnPoint;

    [Header("Food Prefabs")]
    public List<GameObject> prefabs;

    private int spawnInd;
    private int prefabInd;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < SpawnPoint.Count; i++)
        {
            prefabInd = Random.Range(0, prefabs.Count);

            Instantiate(prefabs[prefabInd], SpawnPoint[i].transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
