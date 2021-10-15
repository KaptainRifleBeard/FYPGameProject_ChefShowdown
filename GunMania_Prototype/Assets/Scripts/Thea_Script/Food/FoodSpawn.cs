using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawn : MonoBehaviour
{
    [Header("Food Spawn Points (1 for JP, 2 for KR, 3 for CN, 4 for TW)")]
    public List<GameObject> foodSpawnPoint;

    [Header("Japan Dish Spawn Points (1 for JP, 2 for KR, 3 for CN, 4 for TW)")]
    public List<GameObject> dishSpawnPoint;

    [Header("Japan Food Prefabs")]
    public List<GameObject> JPfoodPrefabs;

    [Header("Korea Food Prefabs")]
    public List<GameObject> KRfoodPrefabs;

    [Header("China Food Prefabs")]
    public List<GameObject> CNfoodPrefabs;

    [Header("Taiwan Food Prefabs")]
    public List<GameObject> TWfoodPrefabs;

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


    // Start is called before the first frame update
    void Start()
    {
        Instantiate(JPfoodPrefabs[Random.Range(0, JPfoodPrefabs.Count)], foodSpawnPoint[0].transform.position, Quaternion.identity);

        Instantiate(KRfoodPrefabs[Random.Range(0, JPfoodPrefabs.Count)], foodSpawnPoint[1].transform.position, Quaternion.identity);

        Instantiate(CNfoodPrefabs[Random.Range(0, JPfoodPrefabs.Count)], foodSpawnPoint[2].transform.position, Quaternion.identity);

        Instantiate(TWfoodPrefabs[Random.Range(0, JPfoodPrefabs.Count)], foodSpawnPoint[3].transform.position, Quaternion.identity);

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

    #region

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

        Debug.Log("spawn at " + index);
        sl_P1PickUp.isPicked = false;

    }

    private IEnumerator P2Spawn(int secs, int index)
    {
        yield return new WaitForSeconds(secs);
        prefabInd = Random.Range(0, prefabs.Count);
        Instantiate(prefabs[prefabInd], foodSpawnPoint[index].transform.position, Quaternion.identity);

        Debug.Log("spawn at " + index);
        sl_P2PickUp.isPicked = false;

    }

    #endregion


    #region

    public void dishSpawnUpdate()
    {
        int layerMask = 1 << 6;

        Collider[] jpColliders = Physics.OverlapSphere(JPdishSpawnPoint.transform.position, 2, layerMask);
        if (jpColliders.Length == 0)
        {
        }
        
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

    #endregion

}
