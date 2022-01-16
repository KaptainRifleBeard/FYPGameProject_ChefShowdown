using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class sl_P2CharacterSelect : MonoBehaviour
{
    PhotonView view;


    [Space(10)]
    [Header("Buttons")]
    public GameObject readyButton;
    public GameObject p2_confirmFirstCharacter;
    public GameObject p2_confirmSecondCharacter;

    public GameObject p2_withdrawFirst;
    public GameObject p2_withdrawSecond;

    public GameObject[] p2_characterButton;


    public GameObject[] p2_first_leftRight;
    public GameObject[] p2_second_leftRight;

    public GameObject[] p2_statInfo;

    [Space(10)]
    [Header("Character")]
    public GameObject[] p2_indicator;

    public GameObject[] p2_characterTypes1;
    public static int p2_firstCharacter; //for change model


    public GameObject[] p2_characterTypes2;
    public static int p2_secondCharacter;


    [Space(10)]
    [Header("Stat Description")]
    public GameObject[] p2_statDesc1;
    int p2_firstDesc;

    public GameObject[] p2_statDesc2;
    int p2_secondDesc;

    [Space(10)]
    [Header("Disable P1 Button")]
    public GameObject[] buttonDisable;


    //check withdraw n confirm
    bool p2_confirm1;
    bool p2_confirm2;

    bool p2_withdrawing1;
    bool p2_withdrawing2;

    int p2_numConfirm1;
    int p2_numConfirm2;

    int p2_numWithdraw;

    void Start()
    {
        view = GetComponent<PhotonView>();

        p2_indicator[0].SetActive(false);
        p2_indicator[1].SetActive(false);

        p2_statInfo[0].SetActive(true);
        p2_statInfo[1].SetActive(false);

        //first buttons
        p2_first_leftRight[0].SetActive(false);
        p2_first_leftRight[1].SetActive(false);

        p2_confirmFirstCharacter.SetActive(false);

        //second button
        p2_second_leftRight[0].SetActive(false);
        p2_second_leftRight[1].SetActive(false);

        p2_confirmSecondCharacter.SetActive(false);
    }


    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for(int i = 0; i < buttonDisable.Length; i++)
            {
                buttonDisable[i].SetActive(false);
            }
        }
        else
        {
            p2_characterButton[0].SetActive(true);
            p2_characterButton[1].SetActive(true);

        }

        //setup p2
        #region

        //first
        if (p2_confirm1)
        {
            p2_numConfirm1 = 1;
        }
        else
        {
            p2_numConfirm1 = 0;
        }

        //second
        if (p2_confirm2)
        {
            p2_numConfirm2 = 1;
        }
        else
        {
            p2_numConfirm2 = 0;
        }

        #endregion
    }

    //Models
    #region
    public void p2_First_NextCharacter()
    {
        p2_characterTypes1[p2_firstCharacter].SetActive(false);
        p2_firstCharacter = (p2_firstCharacter + 1) % p2_characterTypes1.Length;
        p2_characterTypes1[p2_firstCharacter].SetActive(true);

        view.RPC("SyncToPlayer1", RpcTarget.All, p2_firstCharacter, p2_secondCharacter);

    }

    public void p2_First_PreviousCharacter()
    {
        p2_characterTypes1[p2_firstCharacter].SetActive(false);
        p2_firstCharacter--;

        if (p2_firstCharacter < 0)
        {
            p2_firstCharacter += p2_characterTypes1.Length;
        }
        p2_characterTypes1[p2_firstCharacter].SetActive(true);

        view.RPC("SyncToPlayer1", RpcTarget.All, p2_firstCharacter, p2_secondCharacter);

    }

    public void p2_Second_NextCharacter()
    {
        p2_characterTypes2[p2_secondCharacter].SetActive(false);
        p2_secondCharacter = (p2_secondCharacter + 1) % p2_characterTypes2.Length;
        p2_characterTypes2[p2_secondCharacter].SetActive(true);

        view.RPC("SyncToPlayer1", RpcTarget.All, p2_firstCharacter, p2_secondCharacter);
    }

    public void p2_Second_PreviousCharacter()
    {
        p2_characterTypes2[p2_secondCharacter].SetActive(false);
        p2_secondCharacter--;

        if (p2_secondCharacter < 0)
        {
            p2_secondCharacter += p2_characterTypes2.Length;
        }
        p2_characterTypes2[p2_secondCharacter].SetActive(true);

        view.RPC("SyncToPlayer1", RpcTarget.All, p2_firstCharacter, p2_secondCharacter);
    }
    #endregion


    //For description
    #region
    public void p2_First_NextCharacterStat()
    {
        p2_statDesc1[p2_firstDesc].SetActive(false);
        p2_firstDesc = (p2_firstDesc + 1) % p2_statDesc1.Length;
        p2_statDesc1[p2_firstDesc].SetActive(true);

        view.RPC("SyncToPlayer1", RpcTarget.All, p2_firstCharacter, p2_secondCharacter);
    }

    public void p2_First_PreviousCharacterStat()
    {
        p2_statDesc1[p2_firstDesc].SetActive(false);
        p2_firstDesc--;

        if (p2_firstDesc < 0)
        {
            p2_firstDesc += p2_statDesc1.Length;
        }
        p2_statDesc1[p2_firstDesc].SetActive(true);

        view.RPC("SyncToPlayer1", RpcTarget.All, p2_firstCharacter, p2_secondCharacter);
    }

    public void p2_Second_NextCharacterStat()
    {
        p2_statDesc2[p2_secondDesc].SetActive(false);
        p2_secondDesc = (p2_secondDesc + 1) % p2_statDesc2.Length;
        p2_statDesc2[p2_secondDesc].SetActive(true);

        view.RPC("SyncToPlayer1", RpcTarget.All, p2_firstCharacter, p2_secondCharacter);
    }

    public void p2_Second_PreviousCharacterStat()
    {
        p2_statDesc2[p2_secondDesc].SetActive(false);
        p2_secondDesc--;

        if (p2_secondDesc < 0)
        {
            p2_secondDesc += p2_statDesc2.Length;
        }
        p2_statDesc2[p2_secondDesc].SetActive(true);

        view.RPC("SyncToPlayer1", RpcTarget.All, p2_firstCharacter, p2_secondCharacter);
    }
    #endregion


    //For Buttons
    #region
    public void p2_Confirm_FirstCharacter()
    {
        //PlayerPrefs.SetInt("p1_firstCharacter", p1_firstCharacter);

        //disable first 
        p2_confirmFirstCharacter.SetActive(false);
        p2_first_leftRight[0].SetActive(false);
        p2_first_leftRight[1].SetActive(false);

        p2_withdrawFirst.SetActive(true);

        p2_confirm1 = true;
    }

    public void p2_Confirm_SecondCharacter()
    {
        //PlayerPrefs.SetInt("p1_secondCharacter", p1_secondCharacter);

        //disable second 
        p2_confirmSecondCharacter.SetActive(false);
        p2_second_leftRight[0].SetActive(false);
        p2_second_leftRight[1].SetActive(false);

        p2_withdrawSecond.SetActive(true);

        p2_confirm2 = true;
    }

    //click to choose character
    public void p2_FirstIcon_OnClick()
    {
        p2_indicator[0].SetActive(true);
        p2_indicator[1].SetActive(false);

        p2_statInfo[0].SetActive(true);
        p2_statInfo[1].SetActive(false);

        if (p2_numConfirm1 == 0)
        {
            //first
            p2_first_leftRight[0].SetActive(true);
            p2_first_leftRight[1].SetActive(true);

            p2_confirmFirstCharacter.SetActive(true);
            p2_withdrawFirst.SetActive(false);
        }
        else
        {
            p2_confirmFirstCharacter.SetActive(false);
            p2_withdrawFirst.SetActive(true);
        }

        p2_statDesc1[p2_firstDesc].SetActive(true);

        //second
        p2_second_leftRight[0].SetActive(false);
        p2_second_leftRight[1].SetActive(false);

        p2_confirmSecondCharacter.SetActive(false);
        p2_withdrawSecond.SetActive(false);

        for (int i = 0; i < p2_statDesc2.Length; i++)
        {
            p2_statDesc2[i].SetActive(false);
        }

    }

    public void p2_SecondIcon_OnClick()
    {
        p2_indicator[0].SetActive(false);
        p2_indicator[1].SetActive(true);

        p2_statInfo[0].SetActive(false);
        p2_statInfo[1].SetActive(true);

        //first
        p2_first_leftRight[0].SetActive(false);
        p2_first_leftRight[1].SetActive(false);

        p2_confirmFirstCharacter.SetActive(false);
        p2_withdrawFirst.SetActive(false);

        for (int i = 0; i < p2_statDesc2.Length; i++)
        {
            p2_statDesc1[i].SetActive(false);
        }

        //second
        if (p2_numConfirm2 == 0)
        {
            //first
            p2_second_leftRight[0].SetActive(true);
            p2_second_leftRight[1].SetActive(true);

            p2_confirmSecondCharacter.SetActive(true);
            p2_withdrawSecond.SetActive(false);
        }
        else
        {
            p2_confirmSecondCharacter.SetActive(false);
            p2_withdrawSecond.SetActive(true);
        }
        p2_statDesc2[p2_secondDesc].SetActive(true);

    }


    //withdraw part
    public void p2_FirstCharacter_Withdraw()
    {
        //button
        p2_first_leftRight[0].SetActive(true);
        p2_first_leftRight[1].SetActive(true);


        p2_withdrawFirst.SetActive(false);
        p2_confirmFirstCharacter.SetActive(true);
    }

    public void p2_SecondCharacter_Withdraw()
    {
        //button
        p2_second_leftRight[0].SetActive(true);
        p2_second_leftRight[1].SetActive(true);


        p2_withdrawSecond.SetActive(false);
        p2_confirmSecondCharacter.SetActive(true);

    }


    #endregion


    //RPC Area
    [PunRPC]
    public void SyncToPlayer1(int c, int c2)  //use int because rpc cannot send gameobject[]
    {
        p2_firstCharacter = c;
        p2_secondCharacter = c2;


        //first
        #region
        if (c == 0)
        {
            //model
            p2_characterTypes1[0].SetActive(true);
            p2_characterTypes1[1].SetActive(false);
            p2_characterTypes1[2].SetActive(false);
            p2_characterTypes1[3].SetActive(false);

            //stat
            p2_statDesc1[0].SetActive(true);
            p2_statDesc1[1].SetActive(false);
            p2_statDesc1[2].SetActive(false);
            p2_statDesc1[3].SetActive(false);

        }

        if (c == 1)
        {
            //model
            p2_characterTypes1[0].SetActive(false);
            p2_characterTypes1[1].SetActive(true);
            p2_characterTypes1[2].SetActive(false);
            p2_characterTypes1[3].SetActive(false);

            //stat
            p2_statDesc1[0].SetActive(false);
            p2_statDesc1[1].SetActive(true);
            p2_statDesc1[2].SetActive(false);
            p2_statDesc1[3].SetActive(false);

        }

        if (c == 2)
        {
            //model
            p2_characterTypes1[0].SetActive(false);
            p2_characterTypes1[1].SetActive(false);
            p2_characterTypes1[2].SetActive(true);
            p2_characterTypes1[3].SetActive(false);

            //stat
            p2_statDesc1[0].SetActive(false);
            p2_statDesc1[1].SetActive(false);
            p2_statDesc1[2].SetActive(true);
            p2_statDesc1[3].SetActive(false);

        }

        if (c == 3)
        {
            //model
            p2_characterTypes1[0].SetActive(false);
            p2_characterTypes1[1].SetActive(false);
            p2_characterTypes1[2].SetActive(false);
            p2_characterTypes1[3].SetActive(true);

            //stat
            p2_statDesc1[0].SetActive(false);
            p2_statDesc1[1].SetActive(false);
            p2_statDesc1[2].SetActive(false);
            p2_statDesc1[3].SetActive(true);

        }
        #endregion


        //second
        #region
        if (c2 == 0)
        {
            //model
            p2_characterTypes2[0].SetActive(true);
            p2_characterTypes2[1].SetActive(false);
            p2_characterTypes2[2].SetActive(false);
            p2_characterTypes2[3].SetActive(false);

            //stat
            p2_statDesc2[0].SetActive(true);
            p2_statDesc2[1].SetActive(false);
            p2_statDesc2[2].SetActive(false);
            p2_statDesc2[3].SetActive(false);

        }

        if (c2 == 1)
        {
            //model
            p2_characterTypes2[0].SetActive(false);
            p2_characterTypes2[1].SetActive(true);
            p2_characterTypes2[2].SetActive(false);
            p2_characterTypes2[3].SetActive(false);

            //stat
            p2_statDesc2[0].SetActive(false);
            p2_statDesc2[1].SetActive(true);
            p2_statDesc2[2].SetActive(false);
            p2_statDesc2[3].SetActive(false);

        }

        if (c2 == 2)
        {
            //model
            p2_characterTypes2[0].SetActive(false);
            p2_characterTypes2[1].SetActive(false);
            p2_characterTypes2[2].SetActive(true);
            p2_characterTypes2[3].SetActive(false);

            //stat
            p2_statDesc2[0].SetActive(false);
            p2_statDesc2[1].SetActive(false);
            p2_statDesc2[2].SetActive(true);
            p2_statDesc2[3].SetActive(false);

        }

        if (c2 == 3)
        {
            //model
            p2_characterTypes2[0].SetActive(false);
            p2_characterTypes2[1].SetActive(false);
            p2_characterTypes2[2].SetActive(false);
            p2_characterTypes2[3].SetActive(true);

            //stat
            p2_statDesc2[0].SetActive(false);
            p2_statDesc2[1].SetActive(false);
            p2_statDesc2[2].SetActive(false);
            p2_statDesc2[3].SetActive(true);

        }
        #endregion
    }



}
