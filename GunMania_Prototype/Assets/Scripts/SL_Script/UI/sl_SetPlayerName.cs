using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sl_SetPlayerName : MonoBehaviour
{
    //static sl_SetPlayerName instance;

    public InputField playerName;
    public Text setName;
    public GameObject nicknameUI;

    //string pName;

    //private void Awake()
    //{
    //    if (instance != null)
    //    {

    //        Destroy(this);
    //    }

    //    instance = this;
    //}

    void Start()
    {
        //Changes the character limit in the main input field.
        nicknameUI.SetActive(false);
        playerName.characterLimit = 7;

        //setName.text = pName;
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
