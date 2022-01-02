using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;


public class sl_SetNickname : MonoBehaviour
{
    public InputField playerNameInput;
    public Text playerNameText;
    public GameObject nicknameUI;


    void Start()
    {
        playerNameInput.characterLimit = 7;
    }

    public void SetPlayerName()
    {
        PhotonNetwork.NickName = playerNameText.text;
        SceneManager.LoadScene("sl_ServerLobby");
    }

    public void ShowNickname()
    {
        playerNameInput.interactable = true;
        nicknameUI.SetActive(true);
    }
}
