using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FoodSpawn : MonoBehaviour
{
    [Header("Japan Dish Spawn Points (1 for JP, 2 for KR, 3 for CN, 4 for TW)")]
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

    [Header("Food Respawn Time")]
    public int sec;

    [Header("Dish Spawn Time //Time = time before countdown + start countdown and spawn")]
    public int dishsec;
    public int countdownTime;

    [Header("Dish Respawn Time")]
    public int dishrespawnSec;

    PhotonView view;

    private IEnumerator countdownCoro;
    private IEnumerator dishCoro;
    private int[] index = new int[4];

    int count;
    bool spawn;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();

        index[0] = Random.Range(0, JPfoodPrefabs.Count);

        JPfoodPrefabs[index[0]].SetActive(true);

        index[1] = Random.Range(0, KRfoodPrefabs.Count);

        KRfoodPrefabs[index[1]].SetActive(true);

        index[2] = Random.Range(0, CNfoodPrefabs.Count);

        CNfoodPrefabs[index[2]].SetActive(true);

        index[3] = Random.Range(0, TWfoodPrefabs.Count);

        TWfoodPrefabs[index[3]].SetActive(true);

    }
    // Update is called once per frame
    void Update()
    {
        //view.RPC("dishSpawnUpdate", RpcTarget.All);
        //view.RPC("spawnUpdate", RpcTarget.All);

        if (PhotonNetwork.IsMasterClient)
        {
            spawnUpdate();

            if (count < 1 && spawn == false)
            {
                if (count < 1)
                {
                    dishSpawnUpdate();

                    StartCoroutine(DishRespawn(dishrespawnSec));
                    //view.RPC("DishRespawn", RpcTarget.All, dishrespawnSec);
                    DishDespawn.canSpawn = false;
                    count++;
                }
                if (count == 1)
                {
                    spawn = false;
                }
            }


        }

    }


    #region
    public void spawnUpdate()
    {
        if (sl_P1PickUp.isPicked == true)
        {
            if(Respawn.index == 0)
            {
                JPfoodPrefabs[index[0]].SetActive(false);
            }
            else if(Respawn.index == 1)
            {
                KRfoodPrefabs[index[1]].SetActive(false);
            }
            else if(Respawn.index == 2)
            {
                CNfoodPrefabs[index[2]].SetActive(false);
            }
            else if(Respawn.index == 3)
            {
                TWfoodPrefabs[index[3]].SetActive(false);
            }
            StartCoroutine(Spawn(sec, Respawn.index));
            //view.RPC("Spawn", RpcTarget.All, sec, i);
            sl_P1PickUp.isPicked = false;
        }
        if (sl_P2PickUp.isPicked == true)
        {
            StartCoroutine(Spawn(sec, Respawn.index));
            //view.RPC("Spawn", RpcTarget.All, sec, i);
            sl_P2PickUp.isPicked = false;
        }
            

    }

    public IEnumerator Spawn(int secs, int i)
    {
        yield return new WaitForSeconds(secs);

            switch (i)
            {
                case 0:
                    index[i] = Random.Range(0, JPfoodPrefabs.Count);
                    JPfoodPrefabs[index[0]].SetActive(true);
                    break;
                case 1:
                    index[i] = Random.Range(0, KRfoodPrefabs.Count);
                    KRfoodPrefabs[index[1]].SetActive(true);
                    break;
                case 2:
                    index[i] = Random.Range(0, CNfoodPrefabs.Count);
                    CNfoodPrefabs[index[2]].SetActive(true);
                    break;
                case 3:
                    index[i] = Random.Range(0, TWfoodPrefabs.Count);
                    TWfoodPrefabs[index[3]].SetActive(true);
                    break;
                default:
                    Debug.Log("unknown spawn point");
                    break;
            }

            Debug.Log("spawn at " + index);
        Debug.Log("dish spawn");
    }

    #endregion



    //Dish
    #region
    public void dishSpawnUpdate()
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

    public IEnumerator DishCountdown(int countdownTime)
    {
        yield return new WaitForSeconds(countdownTime);

            dishCoro = DishSpawn(dishsec);
            StartCoroutine(dishCoro);
    }


    public IEnumerator DishSpawn(int dishsecs)
    {
        yield return new WaitForSeconds(dishsecs);
        int dishIndex;
        dishIndex = Random.Range(0, dishSpawnPoint.Count);

        if (dishIndex == 0)
        {
            //Japan dish spawn
            PhotonNetwork.Instantiate(JPdishPrefabs[Random.Range(0, JPdishPrefabs.Count)].name, dishSpawnPoint[0].transform.position, Quaternion.identity);
        }
        else if (dishIndex == 1)
        {
            //Korea dish
            PhotonNetwork.Instantiate(KRdishPrefabs[Random.Range(0, KRdishPrefabs.Count)].name, dishSpawnPoint[1].transform.position, Quaternion.identity);
        }
        else if (dishIndex == 2)
        {
            //China dish
            PhotonNetwork.Instantiate(CNdishPrefabs[Random.Range(0, CNdishPrefabs.Count)].name, dishSpawnPoint[2].transform.position, Quaternion.identity);
        }
        else if (dishIndex == 3)
        {
            //Taiwan dish
            PhotonNetwork.Instantiate(TWdishPrefabs[Random.Range(0, TWdishPrefabs.Count)].name, dishSpawnPoint[3].transform.position, Quaternion.identity);
        }

        Debug.Log("dish spawn");
        
    }


    public IEnumerator DishRespawn(int secs)
    {
        yield return new WaitForSeconds(secs);
        int dishIndex;
        dishIndex = Random.Range(0, dishSpawnPoint.Count);

        if (dishIndex == 0)
        {
            //Japan dish spawn
            PhotonNetwork.Instantiate(JPdishPrefabs[Random.Range(0, JPdishPrefabs.Count)].name, dishSpawnPoint[0].transform.position, Quaternion.identity);
            DishDespawn.canSpawn = false;
            spawn = false;
            count = 0;
        }
        else if (dishIndex == 1)
        {
            //Korea dish
            PhotonNetwork.Instantiate(KRdishPrefabs[Random.Range(0, KRdishPrefabs.Count)].name, dishSpawnPoint[1].transform.position, Quaternion.identity);
            DishDespawn.canSpawn = false;
            spawn = false;
            count = 0;

        }
        else if (dishIndex == 2)
        {
            //China dish
            PhotonNetwork.Instantiate(CNdishPrefabs[Random.Range(0, CNdishPrefabs.Count)].name, dishSpawnPoint[2].transform.position, Quaternion.identity);
            DishDespawn.canSpawn = false;
            spawn = false;
            count = 0;

        }
        else if (dishIndex == 3)
        {
            //Taiwan dish
            PhotonNetwork.Instantiate(TWdishPrefabs[Random.Range(0, TWdishPrefabs.Count)].name, dishSpawnPoint[3].transform.position, Quaternion.identity);
            DishDespawn.canSpawn = false;
            spawn = false;
            count = 0;

        }

        Debug.Log("dish respawn");

    }

    #endregion

}
