using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class sl_CharacterInfoWinLose : MonoBehaviour
{
    public TextMeshProUGUI chamOrRunner_text;
    public Text playerNickname;
    public Sprite[] icons;
    public Image playerCurrentIcon;

    PhotonView view;

    void Start()
    {
        view.GetComponent<PhotonView>();
    }


    void Update()
    {
        if(view.IsMine)
        {
            playerNickname.text = PhotonNetwork.NickName;
        }
        else
        {
            playerNickname.text = view.Owner.NickName;
        }

        //for text
        if(sl_PlayerHealth.currentHealth <= 0 || sl_P2PlayerHealth.p2currentHealth <= 0)
        {
            chamOrRunner_text.text = "Runner-up";
        }
        else
        {
            chamOrRunner_text.text = "Fling Champion";
        }

        //for icon
        if (SL_newP1Movement.changeModelAnim == 0 || sl_newP2Movement.changep2Icon == 0) //brock
        {
            playerCurrentIcon.sprite = icons[0];
        }
        if (SL_newP1Movement.changeModelAnim == 1 || sl_newP2Movement.changep2Icon == 1) //wen
        {
            playerCurrentIcon.sprite = icons[1];
        }
        if (SL_newP1Movement.changeModelAnim == 2 || sl_newP2Movement.changep2Icon == 2) //jiho
        {
            playerCurrentIcon.sprite = icons[2];
        }
        if (SL_newP1Movement.changeModelAnim == 3 || sl_newP2Movement.changep2Icon == 3) //katsuki
        {
            playerCurrentIcon.sprite = icons[3];
        }

    }
}
