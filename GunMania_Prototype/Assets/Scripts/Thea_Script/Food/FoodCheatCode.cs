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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            PhotonNetwork.Instantiate(prefabs[0].name, playerPOS.position + offset, Quaternion.identity);
            
        }
        else if (Input.GetKeyDown("2"))
        {
            PhotonNetwork.Instantiate(prefabs[1].name, playerPOS.position + offset, Quaternion.identity);
            
        }
        else if (Input.GetKeyDown("3"))
        {
            PhotonNetwork.Instantiate(prefabs[2].name, playerPOS.position + offset, Quaternion.identity);

        }
        else if (Input.GetKeyDown("4"))
        {
            PhotonNetwork.Instantiate(prefabs[3].name, playerPOS.position + offset, Quaternion.identity);

        }
        else if (Input.GetKeyDown("5"))
        {
            PhotonNetwork.Instantiate(prefabs[4].name, playerPOS.position + offset, Quaternion.identity);

        }
        else if (Input.GetKeyDown("6"))
        {
            PhotonNetwork.Instantiate(prefabs[5].name, playerPOS.position + offset, Quaternion.identity);

        }
        else if (Input.GetKeyDown("7"))
        {
            PhotonNetwork.Instantiate(prefabs[6].name, playerPOS.position + offset, Quaternion.identity);

        }
        else if (Input.GetKeyDown("8"))
        {
            PhotonNetwork.Instantiate(prefabs[7].name, playerPOS.position + offset, Quaternion.identity);

        }
    }
}
