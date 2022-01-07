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
    public GameObject errorMsg;

    void Start()
    {
        playerNameInput.characterLimit = 7;
    }

    

    public void SetPlayerName()
    {
        if(playerNameInput.text == "")
        {
            StartCoroutine(BlinkText());

        }
        else
        {
            PhotonNetwork.NickName = playerNameText.text;
            SceneManager.LoadScene("sl_ServerLobby");
        }
        
    }

    public void ShowNickname()
    {
        playerNameInput.interactable = true;
        nicknameUI.SetActive(true);
    }

    IEnumerator BlinkText()
    {
        errorMsg.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        errorMsg.SetActive(false);

    }
}
