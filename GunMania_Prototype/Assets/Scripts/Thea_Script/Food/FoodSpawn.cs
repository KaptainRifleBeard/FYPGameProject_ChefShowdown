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
    private IEnumerator coroutine;

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
        if (sl_P1PickUp.isPicked == true || sl_P2PickUp.isPicked == true)
        {
            coroutine = Spawn(sec);
            StartCoroutine(coroutine);
            PickUp.isPicked = false;
        }
    }

    private IEnumerator Spawn(int secs)
    {
        yield return new WaitForSeconds(secs);
        prefabInd = Random.Range(0, prefabs.Count);
        Instantiate(prefabs[prefabInd], SpawnPoint[Respawn.index].transform.position, Quaternion.identity);
    }


}
