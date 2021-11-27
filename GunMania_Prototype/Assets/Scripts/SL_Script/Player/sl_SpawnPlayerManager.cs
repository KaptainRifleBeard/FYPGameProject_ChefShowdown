using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class sl_SpawnPlayerManager : MonoBehaviour
{
    public static int playerNum_p1;
    public static int playerTagNum_p1;

    public static int playerNum_p2;
    public static int playerTagNum_p2;

    public GameObject p1_mainRightClick;
    public GameObject p2_mainRightClick;

    public GameObject p1_tagRightClick;
    public GameObject p2_tagRightClick;

    PhotonView view;

    [Header("Button Click UI")]
    public Text p1TextField;
    public Text p2TextField;

    [Header("Stat describe")]
    public Text p1StatField;
    public Text p2StatField;

    [Header("Button Confirm")]
    public GameObject p1MainConfirm;
    public GameObject p1TagConfirm;

    public GameObject p2MainConfirm;
    public GameObject p2TagConfirm;

    [Header("Character model")]
    public GameObject[] p1MainModel;
    public GameObject[] p1TagModel;

    public GameObject[] p2MainModel;
    public GameObject[] p2TagModel;

    public static int count1;
    public static int count2;

    public static int p2count1;
    public static int p2count2;

    public GameObject readyButton;
    public GameObject startButton;

    public static int p2Ready;
    bool p2done;

    /*
     Note: 
    All count i set as 0, to let the button enable and disable in view. 
    If its not 0, then means i picked a character

    **this is the only way i can think to change value in update()

    for character:
    1. brock
    2. wen
    3. jiho
    4. katsuki
     
     
     */

    void Start()
    {
        Debug.Log("main count default: " + count1);

        p2Ready = 0;

        view = GetComponent<PhotonView>();
        count1 = 5;
        p2count1 = 5;

        //setup p1
        p1MainModel[0].SetActive(true);
        p1TagModel[0].SetActive(true);

        p1TextField.text = "Brock";
        p1StatField.text = "Does an extra 50% damage for all foods & dishes thrown \n\nFood & Dish thrown has minus 2 to their travel distance";

        //setup p2
        p2MainModel[0].SetActive(true);
        p2TagModel[0].SetActive(true);

        p2TextField.text = "Brock";
        p2StatField.text = "Does an extra 50% damage for all foods & dishes thrown \n\nFood & Dish thrown has minus 2 to their travel distance";

    }

    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //p1
            if (count1 == 5)
            {
                count1 = 1;
                p1_mainRightClick.SetActive(true);
                p1MainConfirm.SetActive(true);

            }

            //p2
            p2_mainRightClick.SetActive(false);
            p2_tagRightClick.SetActive(false);

            p2MainConfirm.SetActive(false);
            p2TagConfirm.SetActive(false);

        }
        else
        {
            //p1
            p1_mainRightClick.SetActive(false);
            p1_tagRightClick.SetActive(false);

            p1MainConfirm.SetActive(false);
            p1TagConfirm.SetActive(false);

            //p2
            if (p2count1 == 5)
            {
                p2count1 = 1;
                p2_mainRightClick.SetActive(true);
                p2MainConfirm.SetActive(true);
            }

        }


    }

    //to check p2 is ready
    #region
    public void ReadyGame()
    {
        Debug.Log("p2 ready" + p2Ready);
        readyButton.SetActive(false);
        p2done = true;

        if(p2done)
        {
            view.RPC("SendReadyGame", RpcTarget.All);
        }
    }

    [PunRPC]
    public void SendReadyGame()
    {
        p2Ready = 1;
    }
    #endregion

    public void Player1Click()
    {
        Debug.Log("main count click: " + count1);

        if (count1 < 5)
        {
            count1++;
            view.RPC("ViewP1Pick", RpcTarget.AllBufferedViaServer, count1);

        }
        if (count1 == 5)
        {
            count1 = 0;
            view.RPC("ViewP1Pick", RpcTarget.AllBufferedViaServer, count1);

        }

    }

    //tag
    public void Player1TagClick()
    {
        Debug.Log("tag count click: " + count2);

        if (count2 < 5)
        {
            count2++;
            view.RPC("ViewP1Tag", RpcTarget.AllBufferedViaServer, count2);

        }
        if (count2 == 5)
        {
            count2 = 0;
            view.RPC("ViewP1Tag", RpcTarget.AllBufferedViaServer, count2);

        }

    }


    public void Player2Click()
    {
        Debug.Log("p2 main click " + p2count1);

        if (p2count1 < 5)
        {
            p2count1++;
            view.RPC("ViewP2Pick", RpcTarget.AllBufferedViaServer, p2count1);

        }
        if (p2count1 == 5)
        {
            p2count1 = 0;
            view.RPC("ViewP2Pick", RpcTarget.AllBufferedViaServer, p2count1);

        }

    }

    public void Player2TagClick()
    {
        Debug.Log("p2 tag click " + p2count2);

        if (p2count2 < 5)
        {
            p2count2++;
            view.RPC("ViewP2Tag", RpcTarget.AllBufferedViaServer, p2count2);

        }
        if (p2count2 == 5)
        {
            p2count2 = 0;
            view.RPC("ViewP2Tag", RpcTarget.AllBufferedViaServer, p2count2);

        }

    }

    //for choose player and tag team
    #region
    public void Player1_MainConfirm()
    {
        Debug.Log("p1 main confirm " + count1);

        p1_mainRightClick.SetActive(false);
        p1_tagRightClick.SetActive(true);

        p1MainConfirm.SetActive(false);
        p1TagConfirm.SetActive(true);
    }

    public void Player1_TagConfirm()
    {
        Debug.Log("p1 tag confirm " + count2);
        p1_mainRightClick.SetActive(false);
        p1_tagRightClick.SetActive(false);

        p1MainConfirm.SetActive(false);
        p1TagConfirm.SetActive(false);

    }
    
    public void Player2_MainConfirm()
    {        
        Debug.Log("p2 main confirm " + p2count1);

        //if (p2count1 == 0)
        //{
        //    playerNum_p2 = 1;
        //}
        //if (p2count1 == 1)
        //{
        //    playerNum_p2 = 2;
        //}
        //if (p2count1 == 2)
        //{
        //    playerNum_p2 = 3;
        //}
        //if (p2count1 == 3)
        //{
        //    playerNum_p2 = 4;
        //}

        p2_mainRightClick.SetActive(false);
        p2_tagRightClick.SetActive(true);

        p2MainConfirm.SetActive(false);
        p2TagConfirm.SetActive(true);
    }

    public void Player2_TagConfirm()
    {
        Debug.Log("p2 tag confirm " + p2count2);

        //if (p2count2 == 0)
        //{
        //    playerTagNum_p2 = 1;
        //}
        //if (p2count2 == 1)
        //{
        //    playerTagNum_p2 = 2;

        //}
        //if (p2count2 == 2)
        //{
        //    playerTagNum_p2 = 3;

        //}
        //if (p2count2 == 3)
        //{
        //    playerTagNum_p2 = 4;

        //}

        p2_mainRightClick.SetActive(false);
        p2_tagRightClick.SetActive(false);

        p2MainConfirm.SetActive(false);
        p2TagConfirm.SetActive(false);

        //check is ready
        readyButton.SetActive(true);
    }


    #endregion

    [PunRPC]
    public void ViewP1Pick(int c)
    {
        count1 = c;
        if (c == 1)
        {
            p1MainModel[0].SetActive(true);
            p1MainModel[1].SetActive(false);
            p1MainModel[2].SetActive(false);
            p1MainModel[3].SetActive(false);

            p1TextField.text = "Brock";
            p1StatField.text = "Does an extra 50% damage for all foods & dishes thrown \n\nFood & Dish thrown has minus 2 to their travel distance";

        }
        if (c == 2)
        {
            p1MainModel[0].SetActive(false);
            p1MainModel[1].SetActive(true);
            p1MainModel[2].SetActive(false);
            p1MainModel[3].SetActive(false);

            p1TextField.text = "Wen";
            p1StatField.text = "Increased movement speed by 20% when hearts is more than half \n\nTakes extra half heart damage from Food & Dishes";

        }
        if (c == 3)
        {
            p1MainModel[0].SetActive(false);
            p1MainModel[1].SetActive(false);
            p1MainModel[2].SetActive(true);
            p1MainModel[3].SetActive(false);

            p1TextField.text = "JiHo";
            p1StatField.text = "Food/ Dishes thrown travels for an extra 2 spaces \n\nFood / Dishes does 50 % less damage";

        }
        if (c == 4)
        {
            p1MainModel[0].SetActive(false);
            p1MainModel[1].SetActive(false);
            p1MainModel[2].SetActive(false);
            p1MainModel[3].SetActive(true);

            p1TextField.text = "Katsuki";
            p1StatField.text = "Takes 50% less damage from attacks \n\nMovement speed is reduced by 30%";
        }
    }

    [PunRPC]
    public void ViewP2Pick(int c)
    {
        //For button click change word - p2
        p2count1 = c;

        if (c == 1)
        {
            p2MainModel[0].SetActive(true);
            p2MainModel[1].SetActive(false);
            p2MainModel[2].SetActive(false);
            p2MainModel[3].SetActive(false);

            p2TextField.text = "Brock";
            p2StatField.text = "Does an extra 50% damage for all foods & dishes thrown \n\nFood & Dish thrown has minus 2 to their travel distance";
        }
        if (c == 2)
        {
            p2MainModel[0].SetActive(false);
            p2MainModel[1].SetActive(true);
            p2MainModel[2].SetActive(false);
            p2MainModel[3].SetActive(false);

            p2TextField.text = "Wen";
            p2StatField.text = "Increased movement speed by 20% when hearts is more than half \n\nTakes extra half heart damage from Food & Dishes";

        }
        if (c == 3)
        {
            p2MainModel[0].SetActive(false);
            p2MainModel[1].SetActive(false);
            p2MainModel[2].SetActive(true);
            p2MainModel[3].SetActive(false);

            p2TextField.text = "JiHo";
            p2StatField.text = "Food/ Dishes thrown travels for an extra 2 spaces \n\nFood / Dishes does 50 % less damage";

        }
        if (c == 4)
        {
            p2MainModel[0].SetActive(false);
            p2MainModel[1].SetActive(false);
            p2MainModel[2].SetActive(false);
            p2MainModel[3].SetActive(true);

            p2TextField.text = "Katsuki";
            p2StatField.text = "Takes 50% less damage from attacks \n\nMovement speed is reduced by 30%";

        }
    }

    //tag team
    [PunRPC]
    public void ViewP1Tag(int c)
    {
        p1TagModel[0].SetActive(true);

        count2 = c;
        if (c == 1)
        {
            p1TagModel[0].SetActive(true);
            p1TagModel[1].SetActive(false);
            p1TagModel[2].SetActive(false);
            p1TagModel[3].SetActive(false);

            p1TextField.text = "Brock";
            p1StatField.text = "Does an extra 50% damage for all foods & dishes thrown \n\nFood & Dish thrown has minus 2 to their travel distance";
        }
        if (c == 2)
        {
            p1TagModel[0].SetActive(false);
            p1TagModel[1].SetActive(true);
            p1TagModel[2].SetActive(false);
            p1TagModel[3].SetActive(false);

            p1TextField.text = "Wen";
            p1StatField.text = "Increased movement speed by 20% when hearts is more than half \n\nTakes extra half heart damage from Food & Dishes";

        }
        if (c == 3)
        {
            p1TagModel[0].SetActive(false);
            p1TagModel[1].SetActive(false);
            p1TagModel[2].SetActive(true);
            p1TagModel[3].SetActive(false);

            p1TextField.text = "JiHo";
            p1StatField.text = "Food/ Dishes thrown travels for an extra 2 spaces \n\nFood / Dishes does 50 % less damage";

        }
        if (c == 4)
        {
            p1TagModel[0].SetActive(false);
            p1TagModel[1].SetActive(false);
            p1TagModel[2].SetActive(false);
            p1TagModel[3].SetActive(true);

            p1TextField.text = "Katsuki";
            p1StatField.text = "Takes 50% less damage from attacks \n\nMovement speed is reduced by 30%";

        }
    }

    [PunRPC]
    public void ViewP2Tag(int c)
    {
        p2TagModel[0].SetActive(true);

        p2count2 = c;

        if (c == 1)
        {
            p2TagModel[0].SetActive(true);
            p2TagModel[1].SetActive(false);
            p2TagModel[2].SetActive(false);
            p2TagModel[3].SetActive(false);

            p2TextField.text = "Brock";
            p2StatField.text = "Does an extra 50% damage for all foods & dishes thrown \n\nFood & Dish thrown has minus 2 to their travel distance";
        }
        if (c == 2)
        {
            p2TagModel[0].SetActive(false);
            p2TagModel[1].SetActive(true);
            p2TagModel[2].SetActive(false);
            p2TagModel[3].SetActive(false);

            p2TextField.text = "Wen";
            p2StatField.text = "Increased movement speed by 20% when hearts is more than half \n\nTakes extra half heart damage from Food & Dishes";

        }
        if (c == 3)
        {
            p2TagModel[0].SetActive(false);
            p2TagModel[1].SetActive(false);
            p2TagModel[2].SetActive(true);
            p2TagModel[3].SetActive(false);

            p2TextField.text = "JiHo";
            p2StatField.text = "Food/ Dishes thrown travels for an extra 2 spaces \n\nFood / Dishes does 50 % less damage";

        }
        if (c == 4)
        {
            p2TagModel[0].SetActive(false);
            p2TagModel[1].SetActive(false);
            p2TagModel[2].SetActive(false);
            p2TagModel[3].SetActive(true);

            p2TextField.text = "Katsuki";
            p2StatField.text = "Takes 50% less damage from attacks \n\nMovement speed is reduced by 30%";

        }
    }

}
