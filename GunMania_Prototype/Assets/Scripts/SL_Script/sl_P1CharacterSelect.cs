using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

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


    public GameObject[] first_leftRight;
    public GameObject[] second_leftRight;

    public GameObject[] statInfo;

    [Space(10)] [Header("Character")]
    public GameObject[] indicator;

    public GameObject[] characterTypes1;
    public static int p1_firstCharacter; //for change model


    public GameObject[] characterTypes2;
    public static int p1_secondCharacter;


    [Space(10)] [Header("Stat Description")]
    public GameObject[] statDesc1;
    int firstDesc;

    public GameObject[] statDesc2;
    int secondDesc;


    //check withdraw n confirm
    bool confirm1;
    bool confirm2;

    bool withdrawing1;
    bool withdrawing2;

    int numConfirm1;
    int numConfirm2;

    int numWithdraw;

    void Start()
    {
        view = GetComponent<PhotonView>();

        indicator[0].SetActive(false);
        indicator[1].SetActive(false);

        statInfo[0].SetActive(true);
        statInfo[1].SetActive(false);

        //first buttons
        first_leftRight[0].SetActive(false);
        first_leftRight[1].SetActive(false);

        confirmFirstCharacter.SetActive(false);

        //second button
        second_leftRight[0].SetActive(false);
        second_leftRight[1].SetActive(false);

        confirmSecondCharacter.SetActive(false);
    }


    void Update()
    {
        if(view.IsMine)
        {
            view.RPC("SyncToPlayer2", RpcTarget.All, p1_firstCharacter, p1_secondCharacter);
        }

        if(PhotonNetwork.IsMasterClient)
        {
            characterButton[0].SetActive(true);
            characterButton[1].SetActive(true);
        }
        else
        {
            characterButton[0].SetActive(false);
            characterButton[1].SetActive(false);
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
    }

    public void Second_NextCharacter()
    {
        characterTypes2[p1_secondCharacter].SetActive(false);
        p1_secondCharacter = (p1_secondCharacter + 1) % characterTypes2.Length;
        characterTypes2[p1_secondCharacter].SetActive(true);
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
    }
    #endregion


    //For description
    #region
    public void First_NextCharacterStat()
    {
        statDesc1[firstDesc].SetActive(false);
        firstDesc = (firstDesc + 1) % statDesc1.Length;
        statDesc1[firstDesc].SetActive(true);
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
    }

    public void Second_NextCharacterStat()
    {
        statDesc2[secondDesc].SetActive(false);
        secondDesc = (secondDesc + 1) % statDesc2.Length;
        statDesc2[secondDesc].SetActive(true);
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
        indicator[0].SetActive(true);
        indicator[1].SetActive(false);

        statInfo[0].SetActive(true);
        statInfo[1].SetActive(false);

        if(numConfirm1 == 0)
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
    public void SyncToPlayer2(int c, int c2)  //use int because rpc cannot send gameobject[]
    {
        p1_firstCharacter = c;
        p1_secondCharacter = c2;


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
    }


    public void ToTestScene()
    {
        //check p2 is ready

    }
}
