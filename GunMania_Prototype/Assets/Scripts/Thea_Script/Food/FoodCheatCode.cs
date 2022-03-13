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
    PhotonView view;

    int prefabNum;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        startCheatCde = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (count < 1 && PhotonNetwork.IsMasterClient)
        {
            if (Input.GetKeyDown("1"))
            {
                prefabNum = 1;
                view.RPC("CheckSyncDishPosition", RpcTarget.All, prefabNum);

            }
            else if (Input.GetKeyDown("2"))
            {
                prefabNum = 2;
                view.RPC("CheckSyncDishPosition", RpcTarget.All, prefabNum);

            }
            else if (Input.GetKeyDown("3"))
            {
                prefabNum = 3;
                view.RPC("CheckSyncDishPosition", RpcTarget.All, prefabNum);

            }
            else if (Input.GetKeyDown("4"))
            {
                prefabNum = 4;
                view.RPC("CheckSyncDishPosition", RpcTarget.All, prefabNum);

            }
            else if (Input.GetKeyDown("5"))
            {
                prefabNum = 5;
                view.RPC("CheckSyncDishPosition", RpcTarget.All, prefabNum);

            }
            else if (Input.GetKeyDown("6"))
            {
                prefabNum = 6;
                view.RPC("CheckSyncDishPosition", RpcTarget.All, prefabNum);

            }
            else if (Input.GetKeyDown("7"))
            {
                prefabNum = 7;
                view.RPC("CheckSyncDishPosition", RpcTarget.All, prefabNum);

            }
            else if (Input.GetKeyDown("8"))
            {
                prefabNum = 8;
                view.RPC("CheckSyncDishPosition", RpcTarget.All, prefabNum);

            }
        }
        
    }

    [PunRPC]
    public void CheckSyncDishPosition(int i)
    {
        prefabNum = i;

        if(i == 1)
        {
            obj = PhotonNetwork.Instantiate(prefabs[0].name, playerPOS.position, Quaternion.identity);
            obj.transform.SetParent(GameObject.Find(parentName).transform, false);
        }
        if (i == 2)
        {
            obj = PhotonNetwork.Instantiate(prefabs[1].name, playerPOS.position, Quaternion.identity);
            obj.transform.SetParent(GameObject.Find(parentName).transform, false);
        }
        if (i == 3)
        {
            obj = PhotonNetwork.Instantiate(prefabs[2].name, playerPOS.position, Quaternion.identity);
            obj.transform.SetParent(GameObject.Find(parentName).transform, false);
        }
        if (i == 4)
        {
            obj = PhotonNetwork.Instantiate(prefabs[3].name, playerPOS.position, Quaternion.identity);
            obj.transform.SetParent(GameObject.Find(parentName).transform, false);
        }
        if (i == 5)
        {
            obj = PhotonNetwork.Instantiate(prefabs[4].name, playerPOS.position, Quaternion.identity);
            obj.transform.SetParent(GameObject.Find(parentName).transform, false);
        }
        if (i == 6)
        {
            obj = PhotonNetwork.Instantiate(prefabs[5].name, playerPOS.position, Quaternion.identity);
            obj.transform.SetParent(GameObject.Find(parentName).transform, false);
        }
        if (i == 7)
        {
            obj = PhotonNetwork.Instantiate(prefabs[6].name, playerPOS.position, Quaternion.identity);
            obj.transform.SetParent(GameObject.Find(parentName).transform, false);
        }
        if (i == 8)
        {
            obj = PhotonNetwork.Instantiate(prefabs[7].name, playerPOS.position, Quaternion.identity);
            obj.transform.SetParent(GameObject.Find(parentName).transform, false);
        }

    }

}
