using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class sl_PlayerListing : MonoBehaviour
{
    [SerializeField] private Text text;

    public Player Player { get; private set; }

    public void SetPlayerInfo(Player p)
    {
        Player = p;
        text.text = p.NickName;
    }
}
