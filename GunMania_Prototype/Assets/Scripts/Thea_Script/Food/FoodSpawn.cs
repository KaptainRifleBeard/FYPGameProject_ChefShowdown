using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawn : MonoBehaviour
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
    public int dishsec = 10;
    public int countdownTime = 10;

    private int prefabInd;

    private IEnumerator countdownCoro;
    private IEnumerator dishCoro;

    int count;
    bool spawn;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < foodSpawnPoint.Count; i++)
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
        spawnUpdate();


        if (DishDespawn.canSpawn)
        {
            StartCoroutine(DishRespawn(20));
        }

    }

    public void spawnUpdate()
    {
        int layerMask = 1 << 3;

        for (int i = 0; i < foodSpawnPoint.Count; i++)
        {
            Collider[] hitColliders = Physics.OverlapSphere(foodSpawnPoint[i].transform.position, 2, layerMask);
            if (hitColliders.Length == 0)
            {
                if (sl_P1PickUp.isPicked == true)
                {
                    StartCoroutine(P1Spawn(sec, i));
                    sl_P1PickUp.isPicked = false;
                }
                if (sl_P2PickUp.isPicked == true)
                {
                    StartCoroutine(P2Spawn(sec, i));
                    sl_P2PickUp.isPicked = false;
                }
            }
        }
    }

    public IEnumerator P1Spawn(int secs, int index)
    {
        yield return new WaitForSeconds(secs);
        prefabInd = Random.Range(0, prefabs.Count);
        Instantiate(prefabs[prefabInd], foodSpawnPoint[index].transform.position, Quaternion.identity);
        count = 0;

        Debug.Log("spawn at " + index);
        sl_P1PickUp.isPicked = false;

    }

    private IEnumerator P2Spawn(int secs, int index)
    {
        yield return new WaitForSeconds(secs);
        prefabInd = Random.Range(0, prefabs.Count);
        Instantiate(prefabs[prefabInd], foodSpawnPoint[index].transform.position, Quaternion.identity);
        count = 0;

        Debug.Log("spawn at " + index);
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

        if (DishDespawn.isJP)
        {
            Instantiate(JPdishPrefabs[Random.Range(0, JPdishPrefabs.Count)], JPdishSpawnPoint.transform.position, Quaternion.identity);
            DishDespawn.isJP = false;
        }
        if (DishDespawn.isKR)
        {
            Instantiate(KRdishPrefabs[Random.Range(0, KRdishPrefabs.Count)], KRdishSpawnPoint.transform.position, Quaternion.identity);
            DishDespawn.isKR = false;
        }
        if (DishDespawn.isCN)
        {
            Instantiate(CNdishPrefabs[Random.Range(0, CNdishPrefabs.Count)], CNdishSpawnPoint.transform.position, Quaternion.identity);
            DishDespawn.isCN = false;
        }
        if (DishDespawn.isTW)
        {
            Instantiate(TWdishPrefabs[Random.Range(0, TWdishPrefabs.Count)], TWdishSpawnPoint.transform.position, Quaternion.identity);
            DishDespawn.isTW = false;
        }

        DishDespawn.canSpawn = false;
    }

}
