using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class sl_P2CharacterSelect : MonoBehaviour
{
    PhotonView view;

    [Space(10)]
    [Header("Buttons")]
    public GameObject p2_confirmFirstCharacter;
    public GameObject p2_confirmSecondCharacter;

    public GameObject p2_withdrawFirst;
    public GameObject p2_withdrawSecond;

    public GameObject[] p2_characterButton;
    public Button[] disableConfirmButtn;


    public GameObject[] p2_first_leftRight;
    public GameObject[] p2_second_leftRight;

    public GameObject[] p2_statInfo;

    public GameObject leaveButton;

    [Space(10)]
    [Header("Character")]
    public GameObject[] blankIcon;
    public GameObject[] p2_indicator;

    public GameObject[] p2_characterTypes1;
    public static int p2_firstCharacter; //for change model


    public GameObject[] p2_characterTypes2;
    public static int p2_secondCharacter;


    [Space(10)]
    [Header("Stat Description")]
    public GameObject[] p2_statDesc1;
    public static int p2_firstDesc;

    public GameObject[] p2_statDesc2;
    public static int p2_secondDesc;

    public TextMeshProUGUI nameText;
    public static string roomNickname2;

    [Space(10)]
    [Header("Player Tick")]
    public GameObject tick;

    [Space(10)]
    [Header("Disable P1 Button")]
    public GameObject[] buttonDisable;

    //check withdraw n confirm
    public static int p2_numConfirm1;
    public static int p2_numConfirm2;

    int blank; //for sync blank icon


    void Start()
    {
        view = GetComponent<PhotonView>();

        p2_indicator[0].SetActive(false);
        p2_indicator[1].SetActive(false);

        p2_statInfo[0].SetActive(false);
        p2_statInfo[1].SetActive(false);

        leaveButton.SetActive(true);

        //first buttons
        p2_first_leftRight[0].SetActive(false);
        p2_first_leftRight[1].SetActive(false);

        p2_confirmFirstCharacter.SetActive(false);

        //second button
        p2_second_leftRight[0].SetActive(false);
        p2_second_leftRight[1].SetActive(false);

        p2_confirmSecondCharacter.SetActive(false);
        tick.SetActive(false);

        blank = 0;

    }


    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < buttonDisable.Length; i++)
            {
                buttonDisable[i].SetActive(false);
            }
            leaveButton.SetActive(false);
            //readyButton.SetActive(false);

        }
        else
        {
            p2_characterButton[0].SetActive(true);
            p2_characterButton[1].SetActive(true);

            leaveButton.SetActive(true);
            //readyButton.SetActive(true);

            nameText.text = PhotonNetwork.NickName;
            roomNickname2 = nameText.text;
            ForName2();

            if (p2_numConfirm1 == 1 || p2_numConfirm2 == 1)
            {
                CheckSelectedCharacter();
            }

            if (blank == 0)
            {
                p2_statInfo[0].SetActive(false);
                p2_statInfo[1].SetActive(false);
                ForIcon2();
            }
            if (blank == 1)
            {
                blankIcon[0].SetActive(false);
                ForIcon2();
            }
            if (blank == 2)
            {
                blankIcon[1].SetActive(false);
                ForIcon2();
            }

        }

    }


    public void ForName2()
    {
        view.RPC("SyncName_PlayerRoom2", RpcTarget.All, nameText.text, roomNickname2);

    }


    public void ForIcon2()
    {
        view.RPC("SyncToPlayer1", RpcTarget.All, p2_firstCharacter, p2_secondCharacter, blank, p2_numConfirm1, p2_numConfirm2);

    }

    //Models
    #region
    public void p2_First_NextCharacter()
    {
        p2_characterTypes1[p2_firstCharacter].SetActive(false);
        p2_firstCharacter = (p2_firstCharacter + 1) % p2_characterTypes1.Length;
        p2_characterTypes1[p2_firstCharacter].SetActive(true);

        view.RPC("SyncToPlayer1", RpcTarget.All, p2_firstCharacter, p2_secondCharacter, blank, p2_numConfirm1, p2_numConfirm2);

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

        view.RPC("SyncToPlayer1", RpcTarget.All, p2_firstCharacter, p2_secondCharacter, blank, p2_numConfirm1, p2_numConfirm2);

    }

    public void p2_Second_NextCharacter()
    {
        p2_characterTypes2[p2_secondCharacter].SetActive(false);
        p2_secondCharacter = (p2_secondCharacter + 1) % p2_characterTypes2.Length;
        p2_characterTypes2[p2_secondCharacter].SetActive(true);

        view.RPC("SyncToPlayer1", RpcTarget.All, p2_firstCharacter, p2_secondCharacter, blank, p2_numConfirm1, p2_numConfirm2);
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

        view.RPC("SyncToPlayer1", RpcTarget.All, p2_firstCharacter, p2_secondCharacter, blank, p2_numConfirm1, p2_numConfirm2);
    }
    #endregion


    //For description
    #region
    public void p2_First_NextCharacterStat()
    {
        p2_statDesc1[p2_firstDesc].SetActive(false);
        p2_firstDesc = (p2_firstDesc + 1) % p2_statDesc1.Length;
        p2_statDesc1[p2_firstDesc].SetActive(true);

        view.RPC("SyncToPlayer1", RpcTarget.All, p2_firstCharacter, p2_secondCharacter, blank, p2_numConfirm1, p2_numConfirm2);
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

        view.RPC("SyncToPlayer1", RpcTarget.All, p2_firstCharacter, p2_secondCharacter, blank, p2_numConfirm1, p2_numConfirm2);
    }

    public void p2_Second_NextCharacterStat()
    {
        p2_statDesc2[p2_secondDesc].SetActive(false);
        p2_secondDesc = (p2_secondDesc + 1) % p2_statDesc2.Length;
        p2_statDesc2[p2_secondDesc].SetActive(true);

        view.RPC("SyncToPlayer1", RpcTarget.All, p2_firstCharacter, p2_secondCharacter, blank, p2_numConfirm1, p2_numConfirm2);
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

        view.RPC("SyncToPlayer1", RpcTarget.All, p2_firstCharacter, p2_secondCharacter, blank, p2_numConfirm1, p2_numConfirm2);
    }
    #endregion


    //For Buttons
    #region
    public void p2_Confirm_FirstCharacter()
    {
        //disable first 
        p2_confirmFirstCharacter.SetActive(false);
        p2_first_leftRight[0].SetActive(false);
        p2_first_leftRight[1].SetActive(false);

        p2_withdrawFirst.SetActive(true);

        p2_numConfirm1 = 1;
        view.RPC("SyncToPlayer1", RpcTarget.All, p2_firstCharacter, p2_secondCharacter, blank, p2_numConfirm1, p2_numConfirm2);

    }

    public void p2_Confirm_SecondCharacter()
    {
        //disable second 
        p2_confirmSecondCharacter.SetActive(false);
        p2_second_leftRight[0].SetActive(false);
        p2_second_leftRight[1].SetActive(false);

        p2_withdrawSecond.SetActive(true);

        p2_numConfirm2 = 1;
        view.RPC("SyncToPlayer1", RpcTarget.All, p2_firstCharacter, p2_secondCharacter, blank, p2_numConfirm1, p2_numConfirm2);
    }

    //withdraw part
    public void p2_FirstCharacter_Withdraw()
    {
        //button
        p2_first_leftRight[0].SetActive(true);
        p2_first_leftRight[1].SetActive(true);


        p2_withdrawFirst.SetActive(false);
        p2_confirmFirstCharacter.SetActive(true);

        p2_numConfirm1 = 0;
        view.RPC("SyncToPlayer1", RpcTarget.All, p2_firstCharacter, p2_secondCharacter, blank, p2_numConfirm1, p2_numConfirm2);
    }

    public void p2_SecondCharacter_Withdraw()
    {
        //button
        p2_second_leftRight[0].SetActive(true);
        p2_second_leftRight[1].SetActive(true);


        p2_withdrawSecond.SetActive(false);
        p2_confirmSecondCharacter.SetActive(true);

        p2_numConfirm2 = 0;
        view.RPC("SyncToPlayer1", RpcTarget.All, p2_firstCharacter, p2_secondCharacter, blank, p2_numConfirm1, p2_numConfirm2);
    }


    //click to choose character
    public void p2_FirstIcon_OnClick()
    {
        blank = 1;

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
        //p2_secondCharacter = 0;
        blank = 2;

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


    #endregion

    //check is same character
    public void CheckSelectedCharacter()
    {
        if (p2_firstCharacter == p2_secondCharacter)
        {
            if (p2_numConfirm1 == 1 || p2_numConfirm2 == 1)
            {
                disableConfirmButtn[1].interactable = false;
                disableConfirmButtn[0].interactable = false;

            }

        }
        else
        {
            disableConfirmButtn[0].interactable = true;
            disableConfirmButtn[1].interactable = true;
        }
    }


    public void p2LeftRoom()
    {
        Debug.Log("reset");
        p2_firstCharacter = 0;
        p2_secondCharacter = 0;

        p2_firstDesc = 0;
        p2_secondDesc = 0;

        blank = 0;

        p2_indicator[0].SetActive(false);
        p2_indicator[1].SetActive(false);
    }

    //RPC Area
    [PunRPC]
    public void SyncToPlayer1(int c, int c2, int b, int n, int n2)  //use int because rpc cannot send gameobject[]
    {
        p2_firstCharacter = c;
        p2_secondCharacter = c2;
        blank = b;
        p2_numConfirm1 = n;
        p2_numConfirm2 = n2;

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


        //blank icon
        #region
        if (b == 0)
        {
            blankIcon[0].SetActive(true);
            blankIcon[1].SetActive(true);

            p2_statInfo[0].SetActive(false);
            p2_statInfo[1].SetActive(false);
        }
        if (b == 1)
        {
            blankIcon[0].SetActive(false);

            p2_statInfo[0].SetActive(true);
            p2_statInfo[1].SetActive(false);

        }
        if (b == 2)
        {
            blankIcon[1].SetActive(false);

            p2_statInfo[0].SetActive(false);
            p2_statInfo[1].SetActive(true);
        }
        #endregion

        //show tick
        if (n == 1 && n2 == 1)
        {
            tick.SetActive(true);
        }
        else
        {
            tick.SetActive(false);
        }


    }


    [PunRPC]
    public void SyncName_PlayerRoom2(string n, string n2)
    {
        nameText.text = n;
        roomNickname2 = n2; //sync name to playerListingMenu
    }
}
