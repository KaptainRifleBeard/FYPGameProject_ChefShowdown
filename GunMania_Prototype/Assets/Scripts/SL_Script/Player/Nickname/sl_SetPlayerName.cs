using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sl_SetPlayerName : MonoBehaviourPunCallbacks
{
    public InputField playerName;
    public Text setName;
    public GameObject nicknameUI;

    bool changeScene;


    void Start()
    {
        //Changes the character limit in the main input field.
        nicknameUI.SetActive(false);
        playerName.characterLimit = 7;
    }


    public void SetPlayerName()
    {
        PhotonNetwork.NickName = setName.text;
        SceneManager.LoadScene("sl_ServerLobby");

    }

    public void ShowNickname()
    {
        playerName.interactable = true;
        nicknameUI.SetActive(true);
    }

}
