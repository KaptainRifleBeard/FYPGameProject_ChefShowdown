using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class sl_SetPlayerName : MonoBehaviour
{
    public Text playerName;
    PhotonView view;

    void Start()
    {
        playerName.text = view.Owner.NickName;
    }

    void Update()
    {
        
    }
}
