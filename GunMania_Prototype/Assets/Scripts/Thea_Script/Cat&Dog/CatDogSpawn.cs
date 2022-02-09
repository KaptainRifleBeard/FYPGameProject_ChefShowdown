using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CatDogSpawn : MonoBehaviour
{
    public List<Transform> catSpawnPoints;
    public List<Transform> dogSpawnPoints;
    public GameObject catPrefab;
    public GameObject dogPrefab;
    int catORdog;
    int rSpawn;
    public static bool canSpawn;
    public static bool catCanSpawn;
    public static bool dogCanSpawn;

    public bool spawn2;
    public int spawnTime = 20;

    // Start is called before the first frame update
    void Start()
    {
        canSpawn = false;

       StartCoroutine(Spawn(spawnTime));
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
        {
            StartCoroutine(Spawn(spawnTime));
            canSpawn = false;
        }

    }

    public IEnumerator Spawn(int sec)
    {
        yield return new WaitForSeconds(sec);

        catORdog = Random.Range(0, 2);

        if (catORdog == 0)
        {
            //PhotonNetwork.Instantiate(catPrefab.name, catSpawnPoints[Random.Range(0, catSpawnPoints.Count)].transform.position, Quaternion.identity);
            Instantiate(catPrefab, catSpawnPoints[Random.Range(0, catSpawnPoints.Count)].transform.position, Quaternion.identity);
        }
        else if (catORdog == 1)
        {
            //PhotonNetwork.Instantiate(dogPrefab.name, dogSpawnPoints[Random.Range(0, dogSpawnPoints.Count)].transform.position, Quaternion.identity);
            Instantiate(dogPrefab, dogSpawnPoints[Random.Range(0, dogSpawnPoints.Count)].transform.position, Quaternion.identity);
        }

    }

    //spawn terbalik
    //public IEnumerator Spawn(int sec)
    //{
    //    yield return new WaitForSeconds(sec);

    //    catORdog = Random.Range(0, 2);

    //    if (catORdog == 0)
    //    {
    //        //PhotonNetwork.Instantiate(catPrefab.name, dogSpawnPoints[Random.Range(0, dogSpawnPoints.Count)].transform.position, Quaternion.identity);
    //        Instantiate(catPrefab, dogSpawnPoints[Random.Range(0, dogSpawnPoints.Count)].transform.position, Quaternion.identity);
    //    }
    //    else if (catORdog == 1)
    //    {
    //        //PhotonNetwork.Instantiate(dogPrefab.name, catSpawnPoints[Random.Range(0, catSpawnPoints.Count)].transform.position, Quaternion.identity);
    //        Instantiate(dogPrefab, catSpawnPoints[Random.Range(0, catSpawnPoints.Count)].transform.position, Quaternion.identity);
    //    }

    //}

    //spawn species randomly
    //public IEnumerator Spawn(int sec)
    //{
    //    yield return new WaitForSeconds(sec);

    //    catORdog = Random.Range(0, 2);
    //    rSpawn = Random.Range(0, 2);

    //    if (catORdog == 0)
    //    {
    //        if (rSpawn == 0)
    //        {
    //            //PhotonNetwork.Instantiate(catPrefab.name, catSpawnPoints[Random.Range(0, catSpawnPoints.Count)].transform.position, Quaternion.identity);
    //            Instantiate(catPrefab, catSpawnPoints[Random.Range(0, catSpawnPoints.Count)].transform.position, Quaternion.identity);
    //        }
    //        else if (rSpawn == 1)
    //        {
    //            //PhotonNetwork.Instantiate(catPrefab.name, dogSpawnPoints[Random.Range(0, dogSpawnPoints.Count)].transform.position, Quaternion.identity);
    //            Instantiate(catPrefab, dogSpawnPoints[Random.Range(0, dogSpawnPoints.Count)].transform.position, Quaternion.identity);
    //        }
    //    }
    //    else if (catORdog == 1)
    //    {
    //        if (rSpawn == 0)
    //        {
    //            //PhotonNetwork.Instantiate(dogPrefab.name, dogSpawnPoints[Random.Range(0, dogSpawnPoints.Count)].transform.position, Quaternion.identity);
    //            Instantiate(dogPrefab, dogSpawnPoints[Random.Range(0, dogSpawnPoints.Count)].transform.position, Quaternion.identity);
    //        }
    //        else if (rSpawn == 1)
    //        {
    //            //PhotonNetwork.Instantiate(dogPrefab.name, dogSpawnPoints[Random.Range(0, dogSpawnPoints.Count)].transform.position, Quaternion.identity);
    //            Instantiate(dogPrefab, catSpawnPoints[Random.Range(0, catSpawnPoints.Count)].transform.position, Quaternion.identity);
    //        }

    //    }

    //}
}
