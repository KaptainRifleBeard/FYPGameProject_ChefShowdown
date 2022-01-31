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
    public static bool canSpawn;
    public static bool catCanSpawn;
    public static bool dogCanSpawn;

    public bool spawn2;
    public int spawnTime = 20;

    // Start is called before the first frame update
    void Start()
    {
        canSpawn = false;
        catCanSpawn = false;
        dogCanSpawn = false;

        if (!spawn2)
        {
            StartCoroutine(Spawn(spawnTime));
        }
        else
        {
            StartCoroutine(SpawnTest(spawnTime));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawn2)
        {
            if (canSpawn)
            {
                StartCoroutine(Spawn(spawnTime));
                canSpawn = false;
            }
        }
        else
        {
            if (catCanSpawn && dogCanSpawn)
            {
                StartCoroutine(SpawnTest(spawnTime));
                catCanSpawn = false;
                dogCanSpawn = false;
            }
        }

    }

    public IEnumerator Spawn(int sec)
    {
        yield return new WaitForSeconds(sec);

        catORdog = Random.Range(0, 2);

        if(catORdog == 0)
        {
            //PhotonNetwork.Instantiate(catPrefab.name, catSpawnPoints[Random.Range(0, catSpawnPoints.Count)].transform.position, Quaternion.identity);
            Instantiate(catPrefab, catSpawnPoints[Random.Range(0, catSpawnPoints.Count)].transform.position, Quaternion.identity);
        }
        else if(catORdog == 1)
        {
            //PhotonNetwork.Instantiate(dogPrefab.name, dogSpawnPoints[Random.Range(0, dogSpawnPoints.Count)].transform.position, Quaternion.identity);
            Instantiate(dogPrefab, dogSpawnPoints[Random.Range(0, dogSpawnPoints.Count)].transform.position, Quaternion.identity);
        }
        
    }

    public IEnumerator SpawnTest(int sec)
    {
        yield return new WaitForSeconds(sec);

        Instantiate(catPrefab, catSpawnPoints[Random.Range(0, catSpawnPoints.Count)].transform.position, Quaternion.identity);

        Instantiate(dogPrefab, dogSpawnPoints[Random.Range(0, dogSpawnPoints.Count)].transform.position, Quaternion.identity);
    }
}
