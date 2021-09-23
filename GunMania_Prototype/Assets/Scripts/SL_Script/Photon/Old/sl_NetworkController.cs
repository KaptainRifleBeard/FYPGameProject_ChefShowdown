using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class sl_NetworkController : MonoBehaviourPunCallbacks
{

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();  //connect to photon master server
    }


    void Update()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        //base.OnConnectedToMaster();

        Debug.Log("We are now connect to the " + PhotonNetwork.CloudRegion + " server.");
    }

}
