using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class sl_DisplayName : MonoBehaviour
{
    public Text namePlayer;
    public Text namePlayer2;

    PhotonView view;

    void Start()
    {
        view = GetComponent<PhotonView>();

    }


    void Update()
    {
        //set nickname
        if (view.IsMine)
        {
            namePlayer.text = view.Owner.NickName;

        }
        else
        {
            namePlayer2.text = view.Owner.NickName; //if not my view, then is other's name

        }
    }
}
