using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FoodCheatCode : MonoBehaviour
{
    [Header("Food Prefabs")]
    public List<GameObject> prefabs;

    [Header("Player")]
    public Transform playerPOS;
    Vector3 offset = new Vector3(0, 2, 10);

    public static bool startCheatCde;
    int count;
    private void Start()
    {
        startCheatCde = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (count < 1 && PhotonNetwork.IsMasterClient)
        {
            if (Input.GetKeyDown("1"))
            {
                PhotonNetwork.Instantiate(prefabs[0].name, playerPOS.position, Quaternion.identity);
                //count++;
                //StartCoroutine(reset());

            }
            else if (Input.GetKeyDown("2"))
            {
                PhotonNetwork.Instantiate(prefabs[1].name, playerPOS.position, Quaternion.identity);
                //count++;
                //StartCoroutine(reset());

            }
            else if (Input.GetKeyDown("3"))
            {
                PhotonNetwork.Instantiate(prefabs[2].name, playerPOS.position, Quaternion.identity);
                //count++;
                //StartCoroutine(reset());

            }
            else if (Input.GetKeyDown("4"))
            {
                PhotonNetwork.Instantiate(prefabs[3].name, playerPOS.position, Quaternion.identity);
                //count++;
                //StartCoroutine(reset());

            }
            else if (Input.GetKeyDown("5"))
            {
                PhotonNetwork.Instantiate(prefabs[4].name, playerPOS.position, Quaternion.identity);
                //count++;
                //StartCoroutine(reset());

            }
            else if (Input.GetKeyDown("6"))
            {
                PhotonNetwork.Instantiate(prefabs[5].name, playerPOS.position, Quaternion.identity);
                //count++;
                //StartCoroutine(reset());

            }
            else if (Input.GetKeyDown("7"))
            {
                PhotonNetwork.Instantiate(prefabs[6].name, playerPOS.position, Quaternion.identity);
                //count++;
                //StartCoroutine(reset());

            }
            else if (Input.GetKeyDown("8"))
            {
                PhotonNetwork.Instantiate(prefabs[7].name, playerPOS.position, Quaternion.identity);
                //count++;
                //StartCoroutine(reset());
            }
        }
        
    }

    IEnumerator reset()
    {
        yield return new WaitForSeconds(0.5f);
        count = 0;
    }
}
