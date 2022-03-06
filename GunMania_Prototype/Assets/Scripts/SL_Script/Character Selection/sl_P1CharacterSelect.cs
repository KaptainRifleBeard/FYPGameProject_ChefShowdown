using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;
public class sl_P1CharacterSelect : MonoBehaviour
{
    PhotonView view;

    [Space(10)] [Header("Buttons")]
    public GameObject startButton;
    public GameObject confirmFirstCharacter;
    public GameObject confirmSecondCharacter;

    public GameObject withdrawFirst;
    public GameObject withdrawSecond;

    public GameObject[] characterButton;
    public Button[] disableConfirmButtn;


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
    public static string roomNickname;

    [Space(10)]
    [Header("Player Tick")]
    public GameObject tick;


    [Space(10)]
    [Header("Disable Buttons")]
    public GameObject[] buttonDisable;


    //check withdraw n confirm
    public static int numConfirm1;
    public static int numConfirm2;


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
        tick.SetActive(false);

        blank = 0;


    }



    /* Important notice -------
            never put rpc in update, cuz rpc cannot sync between scene
            better put it in function
     
     */

    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            characterButton[0].SetActive(true);
            characterButton[1].SetActive(true);
            leaveButton.SetActive(true);

            nameText.text = PhotonNetwork.NickName;
            roomNickname = nameText.text;
            ForName();


            if (numConfirm1 == 1 || numConfirm2 == 1)
            {
                CheckSelectedCharacter();
            }

            if (blank == 0)
            {
                statInfo[0].SetActive(false);
                statInfo[1].SetActive(false);
                ForIcon();
            }
            if (blank == 1)
            {
                blankIcon[0].SetActive(false);
                ForIcon();
            }
            if (blank == 2)
            {
                blankIcon[1].SetActive(false);
                ForIcon();
            }

            if(sl_PlayerListingMenu.p2IsIn == 1)
            {
                ForIcon();
            }

        }
        else
        {
            for (int i = 0; i < buttonDisable.Length; i++)
            {
                buttonDisable[i].SetActive(false);
            }
            leaveButton.SetActive(false);
        }

    }

    public void ForName()
    {
        view.RPC("SyncName_PlayerRoom", RpcTarget.All, nameText.text);
    }

    public void ForIcon()
    {
        view.RPC("SyncToPlayer2", RpcTarget.All, p1_firstCharacter, p1_secondCharacter, blank, numConfirm1, numConfirm2);

    }

    //Models
    #region
    public void First_NextCharacter()
    {
        characterTypes1[p1_firstCharacter].SetActive(false);
        p1_firstCharacter = (p1_firstCharacter + 1) % characterTypes1.Length;
        characterTypes1[p1_firstCharacter].SetActive(true);

        view.RPC("SyncToPlayer2", RpcTarget.All, p1_firstCharacter, p1_secondCharacter, blank, numConfirm1, numConfirm2);

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

        view.RPC("SyncToPlayer2", RpcTarget.All, p1_firstCharacter, p1_secondCharacter, blank, numConfirm1, numConfirm2);

    }

    public void Second_NextCharacter()
    {
        characterTypes2[p1_secondCharacter].SetActive(false);
        p1_secondCharacter = (p1_secondCharacter + 1) % characterTypes2.Length;
        characterTypes2[p1_secondCharacter].SetActive(true);

        view.RPC("SyncToPlayer2", RpcTarget.All, p1_firstCharacter, p1_secondCharacter, blank, numConfirm1, numConfirm2);

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

        view.RPC("SyncToPlayer2", RpcTarget.All, p1_firstCharacter, p1_secondCharacter, blank, numConfirm1, numConfirm2);
    }

    #endregion


    //For description
    #region
    public void First_NextCharacterStat()
    {
        statDesc1[firstDesc].SetActive(false);
        firstDesc = (firstDesc + 1) % statDesc1.Length;
        statDesc1[firstDesc].SetActive(true);

        view.RPC("SyncToPlayer2", RpcTarget.All, p1_firstCharacter, p1_secondCharacter, blank, numConfirm1, numConfirm2);

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

        view.RPC("SyncToPlayer2", RpcTarget.All, p1_firstCharacter, p1_secondCharacter, blank, numConfirm1, numConfirm2);
    }

    public void Second_NextCharacterStat()
    {
        statDesc2[secondDesc].SetActive(false);
        secondDesc = (secondDesc + 1) % statDesc2.Length;
        statDesc2[secondDesc].SetActive(true);

        view.RPC("SyncToPlayer2", RpcTarget.All, p1_firstCharacter, p1_secondCharacter, blank, numConfirm1, numConfirm2);
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

        view.RPC("SyncToPlayer2", RpcTarget.All, p1_firstCharacter, p1_secondCharacter, blank, numConfirm1, numConfirm2);
    }
    #endregion


    //For Buttons
    #region
    public void Confirm_FirstCharacter()
    {
        //disable first 
        confirmFirstCharacter.SetActive(false);
        first_leftRight[0].SetActive(false);
        first_leftRight[1].SetActive(false);

        withdrawFirst.SetActive(true);

        numConfirm1 = 1;
        view.RPC("SyncToPlayer2", RpcTarget.All, p1_firstCharacter, p1_secondCharacter, blank, numConfirm1, numConfirm2);

    }

    public void Confirm_SecondCharacter()
    {
        //disable second 
        confirmSecondCharacter.SetActive(false);
        second_leftRight[0].SetActive(false);
        second_leftRight[1].SetActive(false);

        withdrawSecond.SetActive(true);

        numConfirm2 = 1;
        view.RPC("SyncToPlayer2", RpcTarget.All, p1_firstCharacter, p1_secondCharacter, blank, numConfirm1, numConfirm2);

    }

    //withdraw part
    public void FirstCharacter_Withdraw()
    {
        //button
        first_leftRight[0].SetActive(true);
        first_leftRight[1].SetActive(true);


        withdrawFirst.SetActive(false);
        confirmFirstCharacter.SetActive(true);

        numConfirm1 = 0;
        view.RPC("SyncToPlayer2", RpcTarget.All, p1_firstCharacter, p1_secondCharacter, blank, numConfirm1, numConfirm2);

    }

    public void SecondCharacter_Withdraw()
    {
        //button
        second_leftRight[0].SetActive(true);
        second_leftRight[1].SetActive(true);


        withdrawSecond.SetActive(false);
        confirmSecondCharacter.SetActive(true);

        numConfirm2 = 0;
        view.RPC("SyncToPlayer2", RpcTarget.All, p1_firstCharacter, p1_secondCharacter, blank, numConfirm1, numConfirm2);

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
        //p1_secondCharacter = 0;

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


    #endregion

    //check is same character
    public void CheckSelectedCharacter()
    {
        if (p1_firstCharacter == p1_secondCharacter)
        {
            if(numConfirm1 == 1 || numConfirm2 == 1)
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


    //RPC Area
    [PunRPC]
    public void SyncToPlayer2(int c, int c2, int b, int n, int n2)  //use int because rpc cannot send gameobject[]
    {
        p1_firstCharacter = c;
        p1_secondCharacter = c2;
        blank = b;
        numConfirm1 = n;
        numConfirm2 = n2;

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

        //blank icon
        #region
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
    public void SyncName_PlayerRoom(string n)
    {
        nameText.text = n;
    }

}
