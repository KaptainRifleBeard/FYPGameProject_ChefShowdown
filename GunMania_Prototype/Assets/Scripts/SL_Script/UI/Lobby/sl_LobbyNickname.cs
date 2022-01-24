using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


public class sl_LobbyNickname : MonoBehaviour
{
    public InputField nicknameInput_Lobby;
    
    void Start()
    {
        nicknameInput_Lobby.text = PhotonNetwork.NickName;
        nicknameInput_Lobby.interactable = false;

    }

}
