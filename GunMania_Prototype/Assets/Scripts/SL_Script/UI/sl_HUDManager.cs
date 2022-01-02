using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class sl_HUDManager : MonoBehaviour
{
    public Sprite wenIcon;
    public Sprite brockIcon;
    public Sprite jihoIcon;
    public Sprite katsukiIcon;

    //player1
    [Header("Player 1")]
    public GameObject mainCharacter_p1;
    public GameObject tagCharacter_p1;

    //player2
    [Header("Player 2")]
    public GameObject mainCharacter_p2;
    public GameObject tagCharacter_p2;

    int p1MainNumber;
    int p1TagNumber;

    int p2MainNumber;
    int p2TagNumber;

    public List<int> p1CharacterList = new List<int>();
    public List<int> p2CharacterList = new List<int>();

    public void P1_CheckCharacter()
    {
        //Show model when in game
        if (sl_SpawnPlayerManager.count1 == 1 || sl_SpawnPlayerManager.count1 == 0)//0 is default, 1 is choosen
        {
            p1MainNumber = 1;

        }
        if (sl_SpawnPlayerManager.count1 == 2)
        {
            p1MainNumber = 2;

        }
        if (sl_SpawnPlayerManager.count1 == 3)
        {
            p1MainNumber = 3;

        }
        if (sl_SpawnPlayerManager.count1 == 4)
        {
            p1MainNumber = 4;

        }

        //tag character
        //define int for tag character, ****i put -1 because somehow the integer auto +1 when i switch scene, but default 0 no problem
        if (sl_SpawnPlayerManager.count2 == 0 || sl_SpawnPlayerManager.count2 == 1)
        {
            p1TagNumber = 1;

        }
        if (sl_SpawnPlayerManager.count2 == 2)
        {
            p1TagNumber = 2;

        }
        if (sl_SpawnPlayerManager.count2 == 3)
        {
            p1TagNumber = 3;

        }
        if (sl_SpawnPlayerManager.count2 == 4)
        {
            p1TagNumber = 4;
        }
        p1_AddToList(p1TagNumber);
        p1_AddToList(p1MainNumber);

    }

    public void P2_CheckCharacter()
    {
        //Show model when in game
        if (sl_SpawnPlayerManager.p2count1 == 1 || sl_SpawnPlayerManager.p2count1 == 0)//0 is default, 1 is choosen
        {
            p2MainNumber = 1;

        }
        if (sl_SpawnPlayerManager.p2count1 == 2)
        {
            p2MainNumber = 2;

        }
        if (sl_SpawnPlayerManager.p2count1 == 3)
        {
            p2MainNumber = 3;

        }
        if (sl_SpawnPlayerManager.p2count1 == 4)
        {
            p2MainNumber = 4;

        }

        //tag character
        //define int for tag character, ****i put -1 because somehow the integer auto +1 when i switch scene, but default 0 no problem
        if (sl_SpawnPlayerManager.p2count2 == 0 || sl_SpawnPlayerManager.p2count2 == 1)
        {
            p2TagNumber = 1;

        }
        if (sl_SpawnPlayerManager.p2count2 == 2)
        {
            p2TagNumber = 2;

        }
        if (sl_SpawnPlayerManager.p2count2 == 3)
        {
            p2TagNumber = 3;

        }
        if (sl_SpawnPlayerManager.p2count2 == 4)
        {
            p2TagNumber = 4;
        }
        p2_AddToList(p2TagNumber);
        p2_AddToList(p2MainNumber);

    }

    public void p1_AddToList(int value)
    {
        p1CharacterList.Add(value);
    }

    public void p2_AddToList(int value)
    {
        p1CharacterList.Add(value);
    }
}
