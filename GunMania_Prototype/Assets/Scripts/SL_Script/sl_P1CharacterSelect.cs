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

    //public GameObject withdrawFirst;
    //public GameObject withdrawSecond;


    public GameObject[] first_leftRight;
    public GameObject[] second_leftRight;


    [Space(10)] [Header("Character")]
    public GameObject[] characterTypes1;
    int p1_firstCharacter;


    public GameObject[] characterTypes2;
    int p1_secondCharacter;


    [Space(10)] [Header("Stat Description")]
    public GameObject[] statDesc1;
    int firstDesc;

    public GameObject[] statDesc2;
    int secondDesc;

    //For SYNC RPC

    void Start()
    {
        view = GetComponent<PhotonView>();
    }


    void Update()
    {
        if(view.IsMine)
        {
            view.RPC("SyncToPlayer2", RpcTarget.All, p1_firstCharacter, p1_secondCharacter);
        }
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
        PlayerPrefs.SetInt("p1_firstCharacter", p1_firstCharacter);

        //disable first 
        confirmFirstCharacter.SetActive(false);
        first_leftRight[0].SetActive(false);
        first_leftRight[1].SetActive(false);
        for(int i = 0; i < statDesc1.Length; i++)
        {
            statDesc1[i].SetActive(false);
        }

        //enable second
        statDesc2[0].SetActive(true);

        confirmSecondCharacter.SetActive(true);
        second_leftRight[0].SetActive(true);
        second_leftRight[1].SetActive(true);
    }

    public void Confirm_SecondCharacter()
    {
        PlayerPrefs.SetInt("p1_secondCharacter", p1_secondCharacter);

        //disable second 
        confirmSecondCharacter.SetActive(false);
        second_leftRight[0].SetActive(false);
        second_leftRight[1].SetActive(false);

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
