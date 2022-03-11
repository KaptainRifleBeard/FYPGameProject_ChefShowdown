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
    public string parentName;

    GameObject obj;


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
                obj = PhotonNetwork.Instantiate(prefabs[0].name, playerPOS.position, Quaternion.identity);
                obj.transform.SetParent(GameObject.Find(parentName).transform, false);

                //count++;
                //StartCoroutine(reset());

            }
            else if (Input.GetKeyDown("2"))
            {
                obj = PhotonNetwork.Instantiate(prefabs[1].name, playerPOS.position, Quaternion.identity);
                obj.transform.SetParent(GameObject.Find(parentName).transform, false);

                //count++;
                //StartCoroutine(reset());

            }
            else if (Input.GetKeyDown("3"))
            {
                obj = PhotonNetwork.Instantiate(prefabs[2].name, playerPOS.position, Quaternion.identity);
                obj.transform.SetParent(GameObject.Find(parentName).transform, false);

                //count++;
                //StartCoroutine(reset());

            }
            else if (Input.GetKeyDown("4"))
            {
                obj = PhotonNetwork.Instantiate(prefabs[3].name, playerPOS.position, Quaternion.identity);
                obj.transform.SetParent(GameObject.Find(parentName).transform, false);

                //count++;
                //StartCoroutine(reset());

            }
            else if (Input.GetKeyDown("5"))
            {
                obj = PhotonNetwork.Instantiate(prefabs[4].name, playerPOS.position, Quaternion.identity);
                obj.transform.SetParent(GameObject.Find(parentName).transform, false);

                //count++;
                //StartCoroutine(reset());

            }
            else if (Input.GetKeyDown("6"))
            {
                obj = PhotonNetwork.Instantiate(prefabs[5].name, playerPOS.position, Quaternion.identity);
                obj.transform.SetParent(GameObject.Find(parentName).transform, false);

                //count++;
                //StartCoroutine(reset());

            }
            else if (Input.GetKeyDown("7"))
            {
                obj = PhotonNetwork.Instantiate(prefabs[6].name, playerPOS.position, Quaternion.identity);
                obj.transform.SetParent(GameObject.Find(parentName).transform, false);

                //count++;
                //StartCoroutine(reset());

            }
            else if (Input.GetKeyDown("8"))
            {
                obj = PhotonNetwork.Instantiate(prefabs[7].name, playerPOS.position, Quaternion.identity);
                obj.transform.SetParent(GameObject.Find(parentName).transform, false);

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
