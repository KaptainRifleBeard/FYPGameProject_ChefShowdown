using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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

    [Header("Food Respawn Time")]
    public int sec;

    [Header("Dish Spawn Time //Time = time before countdown + start countdown and spawn")]
    public int dishsec;
    public int countdownTime;

    [Header("Dish Respawn Time")]
    public int dishrespawnSec;


    private IEnumerator countdownCoro;
    private IEnumerator dishCoro;

    PhotonView view;


    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();

        Instantiate(JPfoodPrefabs[Random.Range(0, JPfoodPrefabs.Count)], foodSpawnPoint[0].transform.position, Quaternion.identity);

        Instantiate(KRfoodPrefabs[Random.Range(0, KRfoodPrefabs.Count)], foodSpawnPoint[1].transform.position, Quaternion.identity);

        Instantiate(CNfoodPrefabs[Random.Range(0, CNfoodPrefabs.Count)], foodSpawnPoint[2].transform.position, Quaternion.identity);

        Instantiate(TWfoodPrefabs[Random.Range(0, TWfoodPrefabs.Count)], foodSpawnPoint[3].transform.position, Quaternion.identity);

        //countdownCoro = DishCountdown(countdownTime);
        //StartCoroutine(countdownCoro);

        view.RPC("DishCountdown", RpcTarget.All, countdownTime);

    }
    // Update is called once per frame
    void Update()
    {
        //spawnUpdate();
        //dishSpawnUpdate();
        view.RPC("dishSpawnUpdate", RpcTarget.All);
        view.RPC("spawnUpdate", RpcTarget.All);

        if (DishDespawn.isJP || DishDespawn.isKR || DishDespawn.isCN || DishDespawn.isTW)
        {
            view.RPC("DishRespawn", RpcTarget.All, dishrespawnSec);

            //StartCoroutine(DishRespawn(dishrespawnSec));
        }

    }

    #region

    [PunRPC]
    public void spawnUpdate()
    {
        int layerMask = 1 << 3;

        for (int i = 0; i < foodSpawnPoint.Count; i++)
        {
            Collider[] hitColliders = Physics.OverlapSphere(foodSpawnPoint[i].transform.position, 10, layerMask);
            if (hitColliders.Length == 0)
            {
                Debug.Log("gone");
                if (sl_P1PickUp.isPicked == true)
                {
                    //StartCoroutine(Spawn(sec, i));
                    view.RPC("Spawn", RpcTarget.All, sec, i);

                    sl_P1PickUp.isPicked = false;
                }
                if (sl_P2PickUp.isPicked == true)
                {
                    //StartCoroutine(Spawn(sec, i));
                    view.RPC("Spawn", RpcTarget.All, sec, i);

                    sl_P2PickUp.isPicked = false;
                }
            }
        }
    }

    [PunRPC]
    public void Spawn(int secs, int index)
    {
        float timer = 0;

        timer += Time.deltaTime;
        
        if(timer > secs)
        {
            switch (index)
            {
                case 0:
                    Instantiate(JPfoodPrefabs[Random.Range(0, JPfoodPrefabs.Count)], foodSpawnPoint[0].transform.position, Quaternion.identity);
                    break;
                case 1:
                    Instantiate(KRfoodPrefabs[Random.Range(0, KRfoodPrefabs.Count)], foodSpawnPoint[1].transform.position, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(CNfoodPrefabs[Random.Range(0, CNfoodPrefabs.Count)], foodSpawnPoint[2].transform.position, Quaternion.identity);
                    break;
                case 3:
                    Instantiate(TWfoodPrefabs[Random.Range(0, TWfoodPrefabs.Count)], foodSpawnPoint[3].transform.position, Quaternion.identity);
                    break;
                default:
                    Debug.Log("unknown spawn point");
                    break;
            }

            Debug.Log("spawn at " + index);
            timer = 0;
        }
        

    }

    #endregion


    #region
    [PunRPC]
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
                    //StartCoroutine(dishSpawn(dishrespawnSec, i));

                    view.RPC("dishSpawn", RpcTarget.All, dishrespawnSec, i);
                    sl_P1PickUp.isPickedDish = false;
                }
                if (sl_P2PickUp.isPickedDish == true)
                {
                    //StartCoroutine(dishSpawn(dishrespawnSec, i));
                    view.RPC("dishSpawn", RpcTarget.All, dishrespawnSec, i);

                    sl_P2PickUp.isPickedDish = false;
                }
            }
        }

    }

    [PunRPC]
    public void dishSpawn(int secs, int index)
    {
        float timer = 0;

        timer += Time.deltaTime;

        if (timer > secs)
        {
            switch (index)
            {
                case 0:
                    Instantiate(JPdishPrefabs[Random.Range(0, JPdishPrefabs.Count)], dishSpawnPoint[0].transform.position, Quaternion.identity);
                    break;
                case 1:
                    Instantiate(KRdishPrefabs[Random.Range(0, KRdishPrefabs.Count)], dishSpawnPoint[1].transform.position, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(CNdishPrefabs[Random.Range(0, CNdishPrefabs.Count)], dishSpawnPoint[2].transform.position, Quaternion.identity);
                    break;
                case 3:
                    Instantiate(TWdishPrefabs[Random.Range(0, TWdishPrefabs.Count)], dishSpawnPoint[3].transform.position, Quaternion.identity);
                    break;
                default:
                    Debug.Log("unknown spawn point");
                    break;
            }

            Debug.Log("dish spawn at " + index);
            timer = 0;
        }
    }

    [PunRPC]
    public void DishCountdown(int countdownTime)
    {
        float timer = 0;

        timer += Time.deltaTime;

        if (timer > countdownTime)
        {
            //dishCoro = DishSpawn(dishsec);
            //StartCoroutine(dishCoro);

            view.RPC("DishSpawn", RpcTarget.All, dishsec);
            timer = 0;
        }
    }

    [PunRPC]
    public void DishSpawn(int dishsecs)
    {
        float timer = 0;

        timer += Time.deltaTime;

        if (timer > dishsecs)
        {
            //Japan dish spawn
            Instantiate(JPdishPrefabs[Random.Range(0, JPdishPrefabs.Count)], dishSpawnPoint[0].transform.position, Quaternion.identity);
            //Korea dish
            Instantiate(KRdishPrefabs[Random.Range(0, KRdishPrefabs.Count)], dishSpawnPoint[1].transform.position, Quaternion.identity);
            //China dish
            Instantiate(CNdishPrefabs[Random.Range(0, CNdishPrefabs.Count)], dishSpawnPoint[2].transform.position, Quaternion.identity);
            //Taiwan dish
            Instantiate(TWdishPrefabs[Random.Range(0, TWdishPrefabs.Count)], dishSpawnPoint[3].transform.position, Quaternion.identity);
            timer = 0;
        }
    }

    [PunRPC]
    public void DishRespawn(int secs)
    {
        float timer = 0;

        timer += Time.deltaTime;

        if (timer > secs)
        {

            if (DishDespawn.isJP)
            {
                Instantiate(JPdishPrefabs[Random.Range(0, JPdishPrefabs.Count)], dishSpawnPoint[0].transform.position, Quaternion.identity);
                DishDespawn.isJP = false;
            }
            if (DishDespawn.isKR)
            {
                Instantiate(KRdishPrefabs[Random.Range(0, KRdishPrefabs.Count)], dishSpawnPoint[1].transform.position, Quaternion.identity);
                DishDespawn.isKR = false;
            }
            if (DishDespawn.isCN)
            {
                Instantiate(CNdishPrefabs[Random.Range(0, CNdishPrefabs.Count)], dishSpawnPoint[2].transform.position, Quaternion.identity);
                DishDespawn.isCN = false;
            }
            if (DishDespawn.isTW)
            {
                Instantiate(TWdishPrefabs[Random.Range(0, TWdishPrefabs.Count)], dishSpawnPoint[3].transform.position, Quaternion.identity);
                DishDespawn.isTW = false;
            }

            DishDespawn.canSpawn = false;
            timer = 0;
        }
    }

    #endregion

}
