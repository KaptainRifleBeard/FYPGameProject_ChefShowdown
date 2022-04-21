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

    int randDish; //for dish to check which one to spawn

    public Text rand;

    void Start()
    {
        view = GetComponent<PhotonView>();
        spawn = false;
        count = 0;

        for (int i = 0; i < displayTimer.Length; i++)
        {
            displayTimer[i].SetActive(false);
        }
    }

    void Update()
    {
        if (PhotonNetwork.IsMasterClient) //make sure it wont run in both build, only for masterclient
        {
            if (count < 1 && spawn == false)
            {
                randDish = Random.Range(0, 1);
                dishIndex = Random.Range(0, dishSpawnPosition.Length);
                view.RPC("SyncRandomNumber", RpcTarget.All, dishIndex); //to sync rand num then spawn the correct dish

                StartCoroutine(DishSpawn(dishRespawnTime));
                count++;
                spawn = true;
               
            }
        }

        Debug.Log("spawn " + spawn);
        Debug.Log("count " + count);

    }

    [PunRPC]
    public void SyncRandomNumber(int i)
    {
        dishIndex = i;
        rand.text = i.ToString(); //check dish position
    }

    //Dish spawn
    #region
    public IEnumerator DishSpawn(int dishsecs) //spawn timer
    {
        yield return new WaitForSeconds(dishsecs);
        if (dishIndex == 0)
        {
            //japan
            spawnNum = 1;
            view.RPC("DishTimerSpawn", RpcTarget.All, spawnNum);
        }
        else if (dishIndex == 1)
        {
            //Korea dish
            spawnNum = 2;
            view.RPC("DishTimerSpawn", RpcTarget.All, spawnNum);
        }
        else if (dishIndex == 2)
        {
            //China dish
            spawnNum = 3;
            view.RPC("DishTimerSpawn", RpcTarget.All, spawnNum);
        }
        else if (dishIndex == 3)
        {
            //Taiwan dish
            spawnNum = 4;
            view.RPC("DishTimerSpawn", RpcTarget.All, spawnNum);

        }
        Debug.Log("dish timer spawn");
    }

    [PunRPC]
    public void SL_SyncDishPosition(int i, int num)
    {
        dishIndex = i;
        randDish = num;

        if (i == 1) //j
        {
            if(num == 0)
            {
                obj = PhotonNetwork.Instantiate(japanDish[0].name, dishSpawnPosition[0].transform.position, Quaternion.identity);

            }
            else
            {
                obj = PhotonNetwork.Instantiate(japanDish[1].name, dishSpawnPosition[0].transform.position, Quaternion.identity);

            }
            dishParentName = "JapanDishSpawn";

            obj.transform.SetParent(GameObject.Find(dishParentName).transform, false);

            spawn = false;
            count = 0;
        }


        if (i == 2)//k
        {
            if (num == 0)
            {
                obj = PhotonNetwork.Instantiate(koreaDish[0].name, dishSpawnPosition[1].transform.position, Quaternion.identity);

            }
            else
            {
                obj = PhotonNetwork.Instantiate(koreaDish[1].name, dishSpawnPosition[1].transform.position, Quaternion.identity);

            }
            dishParentName = "KoreaDishSpawn";

            obj.transform.SetParent(GameObject.Find(dishParentName).transform, false);

            spawn = false;
            count = 0;
        }

        if (i == 3)//c
        {
            if (num == 0)
            {
                obj = PhotonNetwork.Instantiate(chinaDish[0].name, dishSpawnPosition[2].transform.position, Quaternion.identity);

            }
            else
            {
                obj = PhotonNetwork.Instantiate(chinaDish[1].name, dishSpawnPosition[2].transform.position, Quaternion.identity);

            }
            dishParentName = "ChinaDishSpawn";

            obj.transform.SetParent(GameObject.Find(dishParentName).transform, false);

            spawn = false;
            count = 0;
        }

        if (i == 4)//t
        {
            if (num == 0)
            {
                obj = PhotonNetwork.Instantiate(taiwanDish[0].name, dishSpawnPosition[3].transform.position, Quaternion.identity);

            }
            else
            {
                obj = PhotonNetwork.Instantiate(taiwanDish[1].name, dishSpawnPosition[3].transform.position, Quaternion.identity);

            }
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
    public void DishTimerSpawn(int i)
    {
        spawnNum = i;

        if (i == 1)
        {
            //Debug.Log("JapanDishSpawn 1");

            displayTimer[0].SetActive(true);
            StartCoroutine(StopTimer());
        }
        if (i == 2)
        {
            //Debug.Log("KoreaDishSpawn 2");

            displayTimer[1].SetActive(true);
            StartCoroutine(StopTimer());
        }
        if (i == 3)
        {
            //Debug.Log("ChinaDishSpawn 3");

            displayTimer[2].SetActive(true);
            StartCoroutine(StopTimer());

        }
        if (i == 4)
        {
            //Debug.Log("TaiwanDishSpawn 4");

            displayTimer[3].SetActive(true);
            StartCoroutine(StopTimer());
        }
    }

    public IEnumerator StopTimer()
    {
        yield return new WaitForSeconds(4f);

        if(spawn)
        {

            view.RPC("SL_SyncDishPosition", RpcTarget.All, spawnNum, randDish);
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
