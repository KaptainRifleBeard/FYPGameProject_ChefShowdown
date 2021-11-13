using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class sl_DishSpawn : MonoBehaviour
{
    public List<GameObject> dishSpawnPoint;

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

    int count;
    bool spawn;
    
    void Start()
    {
        view = GetComponent<PhotonView>();
    }


    void Update()
    {
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

    //when player pick up
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
                    StartCoroutine(DishRespawn(dishrespawnSec));
                    sl_P1PickUp.isPickedDish = false;
                }
                if (sl_P2PickUp.isPickedDish == true)
                {
                    StartCoroutine(DishRespawn(dishrespawnSec));
                    sl_P2PickUp.isPickedDish = false;
                }
            }
        }
    }

    //if no pick then disappear
    IEnumerator DishRespawn(int time)
    {
        yield return new WaitForSeconds(time);
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
}
