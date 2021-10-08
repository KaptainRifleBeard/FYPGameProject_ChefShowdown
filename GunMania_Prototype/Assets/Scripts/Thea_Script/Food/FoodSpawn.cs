using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawn : MonoBehaviour
{
    [Header("Spawn Points")]
    public List<GameObject> SpawnPoint;

    [Header("Food Prefabs")]
    public List<GameObject> prefabs;

    [Header("Respawn Time")]
    public int sec = 6;

    private int prefabInd;

    int count;
    bool spawn;

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
        if (sl_P1PickUp.isPicked == true)
        {
            if (count < 1 && spawn == false)  //to spawn only one per time
            {
                if (count < 1)
                {
                    spawn = true;

                    StartCoroutine(P1Spawn(sec));
                    sl_P1PickUp.isPicked = false;

                    count++;

                }
                if (count == 1)
                {
                    spawn = false;
                }
            }

        }

        if (sl_P2PickUp.isPicked == true)
        {
            if (count < 1 && spawn == false)  //to spawn only one per time
            {
                if (count < 1)
                {
                    spawn = true;

                    StartCoroutine(P2Spawn(sec));
                    sl_P2PickUp.isPicked = false;

                    count++;

                }
                if (count == 1)
                {
                    spawn = false;
                }
            }

        }

    }

    private IEnumerator P1Spawn(int secs)
    {
        yield return new WaitForSeconds(secs);
        prefabInd = Random.Range(0, prefabs.Count);
        Instantiate(prefabs[prefabInd], SpawnPoint[Respawn.index].transform.position, Quaternion.identity);
        count = 0;


        sl_P1PickUp.isPicked = false;

    }

    private IEnumerator P2Spawn(int secs)
    {
        yield return new WaitForSeconds(secs);
        prefabInd = Random.Range(0, prefabs.Count);
        Instantiate(prefabs[prefabInd], SpawnPoint[Respawn.index].transform.position, Quaternion.identity);
        count = 0;


        sl_P2PickUp.isPicked = false;

    }

}
