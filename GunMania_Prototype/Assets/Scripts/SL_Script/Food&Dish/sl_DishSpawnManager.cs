using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class sl_DishSpawnManager : MonoBehaviour
{
    PhotonView view;

    public int dishRespawnTime;
    public int spawnCooldown;

    public Transform[] dishSpawnPosition;

    public GameObject[] taiwanDish;
    public GameObject[] koreaDish;
    public GameObject[] chinaDish;
    public GameObject[] japanDish;


    //for animation set position
    public string dishParentName;
    GameObject obj;

    //timer animation
    public GameObject[] displayTimer;
    int dishIndex;
    int spawnNum;

    int count;
    bool spawn;

    public Text rand;

    void Start()
    {
        view = GetComponent<PhotonView>();
        view.RPC("DishTimerSpawn", RpcTarget.All);

        spawn = false;
        count = 0;

        for (int i = 0; i < displayTimer.Length; i++)
        {
            displayTimer[i].SetActive(false);
        }
    }

    void Update()
    {
        if(PhotonNetwork.IsMasterClient) //make sure it wont run in both build, only for masterclient
        {
            if (count < 1 && spawn == false)
            {
                if (count < 1)
                {
                    int randomNum = Random.Range(0, dishSpawnPosition.Length);
                    view.RPC("SyncRandomNumber", RpcTarget.All, randomNum); //to sync rand num then spawn the correct dish

                    StartCoroutine(DishSpawn(dishRespawnTime));
                    count++;

                }
                if (count == 1)
                {
                    spawn = false;
                }
                else
                {
                    spawn = true;
                }
            }
        }
       
        //Debug.Log("spawn " + spawn);
        //Debug.Log("count " + count);

    }

    [PunRPC]
    public void SyncRandomNumber(int i)
    {
        dishIndex = i;
        rand.text = i.ToString(); //check dish num

    }

    //Dish spawn
    #region
    public IEnumerator DishSpawn(int dishsecs)
    {
        yield return new WaitForSeconds(dishsecs);

        if (PhotonNetwork.IsMasterClient)
        {
            if (dishIndex == 0)
            {
                //japan
                spawn = true;
                spawnNum = 1;

                view.RPC("DishTimerSpawn", RpcTarget.All);
            }
            else if (dishIndex == 1)
            {
                //Korea dish
                spawn = true;
                spawnNum = 2;

                view.RPC("DishTimerSpawn", RpcTarget.All);
            }
            else if (dishIndex == 2)
            {
                //China dish
                spawn = true;
                spawnNum = 3;

                view.RPC("DishTimerSpawn", RpcTarget.All);
            }
            else if (dishIndex == 3)
            {
                //Taiwan dish
                spawn = true;
                spawnNum = 4;

                view.RPC("DishTimerSpawn", RpcTarget.All);

            }
            Debug.Log("dish timer spawn");

        }

    }

    [PunRPC]
    public void SL_SyncDishPosition(int i)
    {
        if (i == 1) //j
        {
            obj = PhotonNetwork.Instantiate(japanDish[Random.Range(0, japanDish.Length)].name, dishSpawnPosition[0].transform.position, Quaternion.identity);
            dishParentName = "JapanDishSpawn";

            obj.transform.SetParent(GameObject.Find(dishParentName).transform, false);

            spawn = false;
            count = 0;
        }


        if (i == 2)//k
        {
            obj = PhotonNetwork.Instantiate(koreaDish[Random.Range(0, koreaDish.Length)].name, dishSpawnPosition[1].transform.position, Quaternion.identity);
            dishParentName = "KoreaDishSpawn";

            obj.transform.SetParent(GameObject.Find(dishParentName).transform, false);

            spawn = false;
            count = 0;
        }

        if (i == 3)//c
        {
            obj = PhotonNetwork.Instantiate(chinaDish[Random.Range(0, chinaDish.Length)].name, dishSpawnPosition[2].transform.position, Quaternion.identity);
            dishParentName = "ChinaDishSpawn";

            obj.transform.SetParent(GameObject.Find(dishParentName).transform, false);

            spawn = false;
            count = 0;
        }

        if (i == 4)//t
        {
            obj = PhotonNetwork.Instantiate(taiwanDish[Random.Range(0, taiwanDish.Length)].name, dishSpawnPosition[3].transform.position, Quaternion.identity);
            dishParentName = "TaiwanDishSpawn";

            obj.transform.SetParent(GameObject.Find(dishParentName).transform, false);

            spawn = false;
            count = 0;
        }
    }


    #endregion

    //Timer animation
    #region

    [PunRPC]
    public void DishTimerSpawn()
    {
        if (spawnNum == 1)
        {
            Debug.Log("JapanDishSpawn 1");

            displayTimer[0].SetActive(true);
            StartCoroutine(StopTimer());
        }
        if (spawnNum == 2)
        {
            Debug.Log("KoreaDishSpawn 2");

            displayTimer[1].SetActive(true);
            StartCoroutine(StopTimer());
        }
        if (spawnNum == 3)
        {
            Debug.Log("ChinaDishSpawn 3");

            displayTimer[2].SetActive(true);
            StartCoroutine(StopTimer());

        }
        if (spawnNum == 4)
        {
            Debug.Log("TaiwanDishSpawn 4");

            displayTimer[3].SetActive(true);
            StartCoroutine(StopTimer());
        }
    }

    public IEnumerator StopTimer()
    {
        yield return new WaitForSeconds(4f);

        if(spawn)
        {
            view.RPC("SL_SyncDishPosition", RpcTarget.All, spawnNum);
            spawn = false;
            count = 0;
        }

        for (int i = 0; i < displayTimer.Length; i++)
        {
            displayTimer[i].SetActive(false);
        }
    }
    #endregion
}
