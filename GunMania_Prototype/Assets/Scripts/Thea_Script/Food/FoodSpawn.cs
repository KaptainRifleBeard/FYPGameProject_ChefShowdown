using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FoodSpawn : MonoBehaviour
{
    [Header("Japan Dish Spawn Points (1 for JP, 2 for KR, 3 for CN, 4 for TW)")]
    public List<GameObject> dishSpawnPoint;

    [Header("Japan Dish Prefabs")]
    public List<GameObject> JPdishPrefabs;

    [Header("Korea Dish Prefabs")]
    public List<GameObject> KRdishPrefabs;

    [Header("China Dish Prefabs")]
    public List<GameObject> CNdishPrefabs;

    [Header("Taiwan Dish Prefabs")]
    public List<GameObject> TWdishPrefabs;

    [Header("Dish Spawn Time //Time = time before countdown + start countdown and spawn")]
    public int dishsec;
    public int countdownTime;

    [Header("Dish Respawn Time")]
    public int dishrespawnSec;

    PhotonView view;

    int count;
    bool spawn;

    //for animation set position
    public string dishParentName;
    GameObject obj;

    int respawnNum;
    int spawnNum;

    //animation
    public GameObject[] displayTimer;
    int dishIndex;

    void Start()
    {
        view = GetComponent<PhotonView>();
        //StartCoroutine(UI_DishSpawn());
        DishTimerSpawn();

        for (int i = 0; i < displayTimer.Length; i++)
        {
            displayTimer[i].SetActive(false);
        }
    }

    void Update()
    {
        if (count < 1 && spawn == false && PhotonNetwork.IsMasterClient) //make sure it wont run in both build, only for masterclient
        {
            if (count < 1)
            {
                DishTimerSpawn();
                dishSpawnUpdate();

                if (DishDespawn.canSpawn == true)
                {
                    StartCoroutine(DishRespawn(dishrespawnSec));
                    DishDespawn.canSpawn = false;
                    count++;
                }
                
            }
            if (count == 1)
            {
                spawn = false;
            }
        }

    }

    //Dish
    #region
    public void dishSpawnUpdate() //for first time spawn
    {
        int layerMask = 1 << 6;

        for (int i = 0; i < dishSpawnPoint.Count; i++)
        {
            Collider[] hitColliders = Physics.OverlapSphere(dishSpawnPoint[i].transform.position, 10, layerMask);
            if (hitColliders.Length == 0)
            {
                if (sl_P1PickUp.isPickedDish == true)
                {
                    StartCoroutine(DishSpawn(dishrespawnSec));
                    sl_P1PickUp.isPickedDish = false;
                }
                if (sl_P2PickUp.isPickedDish == true)
                {
                    StartCoroutine(DishSpawn(dishrespawnSec));
                    sl_P2PickUp.isPickedDish = false;
                }
            }
        }

        

    }

    public IEnumerator DishSpawn(int dishsecs)
    {
        yield return new WaitForSeconds(dishsecs);
        dishIndex = Random.Range(0, dishSpawnPoint.Count);

        if (dishIndex == 0)
        {
            //japan
            spawnNum = 1;
            view.RPC("SyncDishPosition", RpcTarget.All, spawnNum);
            DishDespawn.canSpawn = false;
        }
        else if (dishIndex == 1)
        {
            //Korea dish
            spawnNum = 2;
            view.RPC("SyncDishPosition", RpcTarget.All, spawnNum);
            DishDespawn.canSpawn = false;
        }
        else if (dishIndex == 2)
        {
            //China dish
            spawnNum = 3;
            view.RPC("SyncDishPosition", RpcTarget.All, spawnNum);
            DishDespawn.canSpawn = false;
        }
        else if (dishIndex == 3)
        {
            //Taiwan dish
            spawnNum = 4;
            view.RPC("SyncDishPosition", RpcTarget.All, spawnNum);
            DishDespawn.canSpawn = false;
        }

        Debug.Log("dish spawn");
        
    }


    public IEnumerator DishRespawn(int secs)
    {
        yield return new WaitForSeconds(secs);
        dishIndex = Random.Range(0, dishSpawnPoint.Count);

        if (dishIndex == 0)
        {
            //Japan dish spawn
            respawnNum = 1;
            view.RPC("SyncRespawnPosition", RpcTarget.All, respawnNum);

            DishDespawn.canSpawn = false;
            spawn = false;
            count = 0;
        }
        else if (dishIndex == 1)
        {
            //Korea dish
            respawnNum = 2;
            view.RPC("SyncRespawnPosition", RpcTarget.All, respawnNum);

            DishDespawn.canSpawn = false;
            spawn = false;
            count = 0;

        }
        else if (dishIndex == 2)
        {
            //China dish
            respawnNum = 3;
            view.RPC("SyncRespawnPosition", RpcTarget.All, respawnNum);

            DishDespawn.canSpawn = false;
            spawn = false;
            count = 0;

        }
        else if (dishIndex == 4)
        {
            //Taiwan dish
            respawnNum = 1;
            view.RPC("SyncRespawnPosition", RpcTarget.All, respawnNum);

            DishDespawn.canSpawn = false;
            spawn = false;
            count = 0;

        }

    }

    IEnumerator StopTimer()
    {
        yield return new WaitForSeconds(4f);
        for(int i = 0; i < displayTimer.Length; i++)
        {
            displayTimer[i].SetActive(false);
        }
    }

    public void DishTimerSpawn()
    {
        if (spawnNum == 1 || respawnNum == 1)
        {
            Debug.Log("JapanDishSpawn 1");

            displayTimer[0].SetActive(true);
            StartCoroutine(StopTimer());
        }
        if (spawnNum == 1 || respawnNum == 2)
        {
            Debug.Log("KoreaDishSpawn 2");

            displayTimer[1].SetActive(true);
            StartCoroutine(StopTimer());
        }
        if (spawnNum == 3 || respawnNum == 3)
        {
            Debug.Log("ChinaDishSpawn 3");

            displayTimer[2].SetActive(true);
            StartCoroutine(StopTimer());

        }
        if (spawnNum == 4 || respawnNum == 4)
        {
            Debug.Log("TaiwanDishSpawn 4");

            displayTimer[3].SetActive(true);
            StartCoroutine(StopTimer());
        }
    }

    #endregion

    IEnumerator SpawnAfterCountdown(int i)
    {
        yield return new WaitForSeconds(4f);
        if (i == 1)
        {
            obj = PhotonNetwork.Instantiate(JPdishPrefabs[Random.Range(0, JPdishPrefabs.Count)].name, dishSpawnPoint[0].transform.position, Quaternion.identity);
            dishParentName = "JapanDishSpawn";

            obj.transform.SetParent(GameObject.Find(dishParentName).transform, false);
        }
        if (i == 2)
        {
            obj = PhotonNetwork.Instantiate(KRdishPrefabs[Random.Range(0, KRdishPrefabs.Count)].name, dishSpawnPoint[0].transform.position, Quaternion.identity);
            dishParentName = "KoreaDishSpawn";

            obj.transform.SetParent(GameObject.Find(dishParentName).transform, false);
        }
        if (i == 3)
        {
            obj = PhotonNetwork.Instantiate(CNdishPrefabs[Random.Range(0, CNdishPrefabs.Count)].name, dishSpawnPoint[0].transform.position, Quaternion.identity);
            dishParentName = "ChinaDishSpawn";

            obj.transform.SetParent(GameObject.Find(dishParentName).transform, false);
        }
        if (i == 4)
        {
            obj = PhotonNetwork.Instantiate(TWdishPrefabs[Random.Range(0, TWdishPrefabs.Count)].name, dishSpawnPoint[0].transform.position, Quaternion.identity);
            dishParentName = "TaiwanDishSpawn";

            obj.transform.SetParent(GameObject.Find(dishParentName).transform, false);
        }
    }

    [PunRPC]
    public void SyncDishPosition(int i)
    {
        spawnNum = i;
        StartCoroutine(SpawnAfterCountdown(i));
    }

    [PunRPC]
    public void SyncRespawnPosition(int i)
    {
        respawnNum = i;
        StartCoroutine(SpawnAfterCountdown(i));

    }

}
