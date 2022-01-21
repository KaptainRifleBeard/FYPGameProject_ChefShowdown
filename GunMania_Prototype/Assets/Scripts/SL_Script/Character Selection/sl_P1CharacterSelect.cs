using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class sl_P1CharacterSelect : MonoBehaviour
{
    PhotonView view;

    [Header("Player1")]
    [Space(10)] [Header("Buttons")]
    public GameObject startButton;
    public GameObject confirmFirstCharacter;
    public GameObject confirmSecondCharacter;

    public GameObject withdrawFirst;
    public GameObject withdrawSecond;

    public GameObject[] characterButton;


    public GameObject[] first_leftRight;
    public GameObject[] second_leftRight;

    public GameObject[] statInfo;

    [Space(10)]
    [Header("Character")]
    public GameObject[] blankIcon;
    public GameObject[] indicator;

    public GameObject[] characterTypes1;
    public static int p1_firstCharacter; //for change model


    public GameObject[] characterTypes2;
    public static int p1_secondCharacter;

    public GameObject leaveButton;

    [Space(10)] [Header("Stat Description")]
    public GameObject[] statDesc1;
    protected int firstDesc;

    public GameObject[] statDesc2;
    protected int secondDesc;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI nameText2;

    public static string roomNickname;


    [Space(10)]
    [Header("Disable Buttons")]
    public GameObject[] buttonDisable;


    //check withdraw n confirm
    protected bool confirm1;
    protected bool confirm2;

    protected bool withdrawing1;
    protected bool withdrawing2;

    protected int numConfirm1;
    protected int numConfirm2;

    protected int numWithdraw;

    int blank; //for sync blank icon


    void Start()
    {
        view = GetComponent<PhotonView>();

        indicator[0].SetActive(false);
        indicator[1].SetActive(false);

        blankIcon[0].SetActive(true);
        blankIcon[1].SetActive(true);

        statInfo[0].SetActive(false);
        statInfo[1].SetActive(false);

        leaveButton.SetActive(true);

        //first buttons
        first_leftRight[0].SetActive(false);
        first_leftRight[1].SetActive(false);

        confirmFirstCharacter.SetActive(false);

        //second button
        second_leftRight[0].SetActive(false);
        second_leftRight[1].SetActive(false);

        confirmSecondCharacter.SetActive(false);

        blank = 0;

    }


    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            characterButton[0].SetActive(true);
            characterButton[1].SetActive(true);
            leaveButton.SetActive(true);

            nameText.text = PhotonNetwork.NickName;
            roomNickname = nameText.text;

            view.RPC("SyncName_PlayerRoom", RpcTarget.All, nameText.text);
           
        }
        else
        {
            for (int i = 0; i < buttonDisable.Length; i++)
            {
                buttonDisable[i].SetActive(false);
            }
            leaveButton.SetActive(false);
        }

        if(PhotonNetwork.IsMasterClient)
        {
            if(blank == 0)
            {
                statInfo[0].SetActive(false);
                statInfo[1].SetActive(false);

            }
            if (blank == 1)
            {
                blankIcon[0].SetActive(false);
                view.RPC("SyncToPlayer2", RpcTarget.All, p1_firstCharacter, p1_secondCharacter, blank);

            }
            if (blank == 2)
            {
                blankIcon[1].SetActive(false);
                view.RPC("SyncToPlayer2", RpcTarget.All, p1_firstCharacter, p1_secondCharacter, blank);
            }

        }

        //setup
        #region
        //first
        if (confirm1)
        {
            numConfirm1 = 1;
        }
        else
        {
            numConfirm1 = 0;
        }

        //second
        if (confirm2)
        {
            numConfirm2 = 1;
        }
        else
        {
            numConfirm2 = 0;
        }

        #endregion

        
    }

    //Models
    #region
    public void First_NextCharacter()
    {
        characterTypes1[p1_firstCharacter].SetActive(false);
        p1_firstCharacter = (p1_firstCharacter + 1) % characterTypes1.Length;
        characterTypes1[p1_firstCharacter].SetActive(true);

        view.RPC("SyncToPlayer2", RpcTarget.All, p1_firstCharacter, p1_secondCharacter, blank);

    }

    public void First_PreviousCharacter()
    {
        characterTypes1[p1_firstCharacter].SetActive(false);
        p1_firstCharacter--;

        if(p1_firstCharacter < 0)
        {
            p1_firstCharacter += characterTypes1.Length;
        }
        characterTypes1[p1_firstCharacter].SetActive(true);

        view.RPC("SyncToPlayer2", RpcTarget.All, p1_firstCharacter, p1_secondCharacter, blank);

    }

    public void Second_NextCharacter()
    {
        characterTypes2[p1_secondCharacter].SetActive(false);
        p1_secondCharacter = (p1_secondCharacter + 1) % characterTypes2.Length;
        characterTypes2[p1_secondCharacter].SetActive(true);

        view.RPC("SyncToPlayer2", RpcTarget.All, p1_firstCharacter, p1_secondCharacter, blank);

    }

    public void Second_PreviousCharacter()
    {
        characterTypes2[p1_secondCharacter].SetActive(false);
        p1_secondCharacter--;

        if (p1_secondCharacter < 0)
        {
            p1_secondCharacter += characterTypes2.Length;
        }
        characterTypes2[p1_secondCharacter].SetActive(true);

        view.RPC("SyncToPlayer2", RpcTarget.All, p1_firstCharacter, p1_secondCharacter, blank);
    }
    #endregion


    //For description
    #region
    public void First_NextCharacterStat()
    {
        statDesc1[firstDesc].SetActive(false);
        firstDesc = (firstDesc + 1) % statDesc1.Length;
        statDesc1[firstDesc].SetActive(true);

        view.RPC("SyncToPlayer2", RpcTarget.All, p1_firstCharacter, p1_secondCharacter, blank);

    }

    public void First_PreviousCharacterStat()
    {
        statDesc1[firstDesc].SetActive(false);
        firstDesc--;

        if (firstDesc < 0)
        {
            firstDesc += statDesc1.Length;
        }
        statDesc1[firstDesc].SetActive(true);

        view.RPC("SyncToPlayer2", RpcTarget.All, p1_firstCharacter, p1_secondCharacter, blank);
    }

    public void Second_NextCharacterStat()
    {
        statDesc2[secondDesc].SetActive(false);
        secondDesc = (secondDesc + 1) % statDesc2.Length;
        statDesc2[secondDesc].SetActive(true);

        view.RPC("SyncToPlayer2", RpcTarget.All, p1_firstCharacter, p1_secondCharacter, blank);
    }

    public void Second_PreviousCharacterStat()
    {
        statDesc2[secondDesc].SetActive(false);
        secondDesc--;

        if (secondDesc < 0)
        {
            secondDesc += statDesc2.Length;
        }
        statDesc2[secondDesc].SetActive(true);

        view.RPC("SyncToPlayer2", RpcTarget.All, p1_firstCharacter, p1_secondCharacter, blank);
    }
    #endregion


    //For Buttons
    #region
    public void Confirm_FirstCharacter()
    {
        //PlayerPrefs.SetInt("p1_firstCharacter", p1_firstCharacter);
       
        //disable first 
        confirmFirstCharacter.SetActive(false);
        first_leftRight[0].SetActive(false);
        first_leftRight[1].SetActive(false);

        withdrawFirst.SetActive(true);

        confirm1 = true;
    }

    public void Confirm_SecondCharacter()
    {
        //PlayerPrefs.SetInt("p1_secondCharacter", p1_secondCharacter);

        //disable second 
        confirmSecondCharacter.SetActive(false);
        second_leftRight[0].SetActive(false);
        second_leftRight[1].SetActive(false);

        withdrawSecond.SetActive(true);

        confirm2 = true;
    }

    //click to choose character
    public void FirstIcon_OnClick()
    {
        blank = 1;

        indicator[0].SetActive(true);
        indicator[1].SetActive(false);

        statInfo[0].SetActive(true);
        statInfo[1].SetActive(false);
        

        if (numConfirm1 == 0)
        {
            //first
            first_leftRight[0].SetActive(true);
            first_leftRight[1].SetActive(true);

            confirmFirstCharacter.SetActive(true);
            withdrawFirst.SetActive(false);
        }
        else
        {
            confirmFirstCharacter.SetActive(false);
            withdrawFirst.SetActive(true);
        }

        statDesc1[firstDesc].SetActive(true);

        //second
        second_leftRight[0].SetActive(false);
        second_leftRight[1].SetActive(false);

        confirmSecondCharacter.SetActive(false);
        withdrawSecond.SetActive(false);

        for(int i = 0; i < statDesc2.Length; i++)
        {
            statDesc2[i].SetActive(false);
        }

    }

    public void SecondIcon_OnClick()
    {
        blank = 2;

        indicator[0].SetActive(false);
        indicator[1].SetActive(true);

        statInfo[0].SetActive(false);
        statInfo[1].SetActive(true);

        //first
        first_leftRight[0].SetActive(false);
        first_leftRight[1].SetActive(false);

        confirmFirstCharacter.SetActive(false);
        withdrawFirst.SetActive(false);

        for (int i = 0; i < statDesc2.Length; i++)
        {
            statDesc1[i].SetActive(false);
        }

        //second
        if (numConfirm2 == 0)
        {
            //first
            second_leftRight[0].SetActive(true);
            second_leftRight[1].SetActive(true);

            confirmSecondCharacter.SetActive(true);
            withdrawSecond.SetActive(false);
        }
        else
        {
            confirmSecondCharacter.SetActive(false);
            withdrawSecond.SetActive(true);
        }
        statDesc2[secondDesc].SetActive(true);

    }


    //withdraw part
    public void FirstCharacter_Withdraw()
    {
        //button
        first_leftRight[0].SetActive(true);
        first_leftRight[1].SetActive(true);


        withdrawFirst.SetActive(false);
        confirmFirstCharacter.SetActive(true);
    }

    public void SecondCharacter_Withdraw()
    {
        //button
        second_leftRight[0].SetActive(true);
        second_leftRight[1].SetActive(true);


        withdrawSecond.SetActive(false);
        confirmSecondCharacter.SetActive(true);


    }

    #endregion

    //RPC Area
    [PunRPC]
    public void SyncToPlayer2(int c, int c2, int b)  //use int because rpc cannot send gameobject[]
    {
        p1_firstCharacter = c;
        p1_secondCharacter = c2;
        blank = b;

        //first
        #region
        if (c == 0)
        {
            //model
            characterTypes1[0].SetActive(true);
            characterTypes1[1].SetActive(false);
            characterTypes1[2].SetActive(false);
            characterTypes1[3].SetActive(false);

            //stat
            statDesc1[0].SetActive(true);
            statDesc1[1].SetActive(false);
            statDesc1[2].SetActive(false);
            statDesc1[3].SetActive(false);

        }

        if (c == 1)
        {
            //model
            characterTypes1[0].SetActive(false);
            characterTypes1[1].SetActive(true);
            characterTypes1[2].SetActive(false);
            characterTypes1[3].SetActive(false);

            //stat
            statDesc1[0].SetActive(false);
            statDesc1[1].SetActive(true);
            statDesc1[2].SetActive(false);
            statDesc1[3].SetActive(false);

        }

        if (c == 2)
        {
            //model
            characterTypes1[0].SetActive(false);
            characterTypes1[1].SetActive(false);
            characterTypes1[2].SetActive(true);
            characterTypes1[3].SetActive(false);

            //stat
            statDesc1[0].SetActive(false);
            statDesc1[1].SetActive(false);
            statDesc1[2].SetActive(true);
            statDesc1[3].SetActive(false);

        }

        if (c == 3)
        {
            //model
            characterTypes1[0].SetActive(false);
            characterTypes1[1].SetActive(false);
            characterTypes1[2].SetActive(false);
            characterTypes1[3].SetActive(true);

            //stat
            statDesc1[0].SetActive(false);
            statDesc1[1].SetActive(false);
            statDesc1[2].SetActive(false);
            statDesc1[3].SetActive(true);

        }
        #endregion


        //second
        #region
        if (c2 == 0)
        {
            //model
            characterTypes2[0].SetActive(true);
            characterTypes2[1].SetActive(false);
            characterTypes2[2].SetActive(false);
            characterTypes2[3].SetActive(false);

            //stat
            statDesc2[0].SetActive(true);
            statDesc2[1].SetActive(false);
            statDesc2[2].SetActive(false);
            statDesc2[3].SetActive(false);

        }

        if (c2 == 1)
        {
            //model
            characterTypes2[0].SetActive(false);
            characterTypes2[1].SetActive(true);
            characterTypes2[2].SetActive(false);
            characterTypes2[3].SetActive(false);

            //stat
            statDesc2[0].SetActive(false);
            statDesc2[1].SetActive(true);
            statDesc2[2].SetActive(false);
            statDesc2[3].SetActive(false);

        }

        if (c2 == 2)
        {
            //model
            characterTypes2[0].SetActive(false);
            characterTypes2[1].SetActive(false);
            characterTypes2[2].SetActive(true);
            characterTypes2[3].SetActive(false);

            //stat
            statDesc2[0].SetActive(false);
            statDesc2[1].SetActive(false);
            statDesc2[2].SetActive(true);
            statDesc2[3].SetActive(false);

        }

        if (c2 == 3)
        {
            //model
            characterTypes2[0].SetActive(false);
            characterTypes2[1].SetActive(false);
            characterTypes2[2].SetActive(false);
            characterTypes2[3].SetActive(true);

            //stat
            statDesc2[0].SetActive(false);
            statDesc2[1].SetActive(false);
            statDesc2[2].SetActive(false);
            statDesc2[3].SetActive(true);

        }
        #endregion

        if (b == 0)
        {
            
            blankIcon[0].SetActive(true);
            blankIcon[1].SetActive(true);

            statInfo[0].SetActive(false);
            statInfo[1].SetActive(false);
        }
        if (b == 1)
        {
            blankIcon[0].SetActive(false);

            //enable the stat bg info
            statInfo[0].SetActive(true);
            statInfo[1].SetActive(false);
        }
        if (b == 2)
        {
            blankIcon[1].SetActive(false);

            statInfo[0].SetActive(false);
            statInfo[1].SetActive(true);
        }
    }


    [PunRPC]
    public void SyncName_PlayerRoom(string n)
    {
        nameText.text = n;
    }

}
