using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myFoodSpawn : MonoBehaviour
{
    [Header("Food Spawn Points")]
    public List<GameObject> foodSpawnPoint;

    [Header("Japan Dish Spawn Points")]
    public GameObject JPdishSpawnPoint;

    [Header("Korea Dish Spawn Points")]
    public GameObject KRdishSpawnPoint;

    [Header("China Dish Spawn Points")]
    public GameObject CNdishSpawnPoint;

    [Header("Taiwan Dish Spawn Points")]
    public GameObject TWdishSpawnPoint;

    [Header("Food Prefabs")]
    public List<GameObject> prefabs;

    [Header("Japan Dish Prefabs")]
    public List<GameObject> JPdishPrefabs;

    [Header("Korea Dish Prefabs")]
    public List<GameObject> KRdishPrefabs;

    [Header("China Dish Prefabs")]
    public List<GameObject> CNdishPrefabs;

    [Header("Taiwan Dish Prefabs")]
    public List<GameObject> TWdishPrefabs;

    [Header("Respawn Time")]
    public int sec = 6;

    [Header("Dish Spawn Time")]
    public int dishsec = 5;
    public int countdownTime = 5;

    private int prefabInd;

    private IEnumerator countdownCoro;
    private IEnumerator dishCoro;

    int count;
    bool spawn;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < foodSpawnPoint.Count; i++)
        {
            prefabInd = Random.Range(0, prefabs.Count);

            Instantiate(prefabs[prefabInd], foodSpawnPoint[i].transform.position, Quaternion.identity);

        }

        countdownCoro = DishCountdown(countdownTime);
        StartCoroutine(countdownCoro);
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

        if (DishDespawn.canSpawn)
        {
            StartCoroutine(DishRespawn(10));
        }

    }

    private IEnumerator P1Spawn(int secs)
    {
        yield return new WaitForSeconds(secs);
        prefabInd = Random.Range(0, prefabs.Count);
        Instantiate(prefabs[prefabInd], foodSpawnPoint[Respawn.index].transform.position, Quaternion.identity);
        count = 0;


        sl_P1PickUp.isPicked = false;

    }

    private IEnumerator P2Spawn(int secs)
    {
        yield return new WaitForSeconds(secs);
        prefabInd = Random.Range(0, prefabs.Count);
        Instantiate(prefabs[prefabInd], foodSpawnPoint[Respawn.index].transform.position, Quaternion.identity);
        count = 0;


        sl_P2PickUp.isPicked = false;

    }

    private IEnumerator DishCountdown(int countdownTime)
    {
        yield return new WaitForSeconds(countdownTime);
        dishCoro = DishSpawn(dishsec);
        StartCoroutine(dishCoro);
    }

    private IEnumerator DishSpawn(int dishsecs)
    {
        yield return new WaitForSeconds(dishsecs);
        //Japan dish spawn
        Instantiate(JPdishPrefabs[Random.Range(0, JPdishPrefabs.Count)], JPdishSpawnPoint.transform.position, Quaternion.identity);
        //Korea dish
        Instantiate(KRdishPrefabs[Random.Range(0, KRdishPrefabs.Count)], KRdishSpawnPoint.transform.position, Quaternion.identity);
        //China dish
        Instantiate(CNdishPrefabs[Random.Range(0, CNdishPrefabs.Count)], CNdishSpawnPoint.transform.position, Quaternion.identity);
        //Taiwan dish
        Instantiate(TWdishPrefabs[Random.Range(0, TWdishPrefabs.Count)], TWdishSpawnPoint.transform.position, Quaternion.identity);
        count = 0;
    }

    private IEnumerator DishRespawn(int secs)
    {
        yield return new WaitForSeconds(secs);
    }

}
