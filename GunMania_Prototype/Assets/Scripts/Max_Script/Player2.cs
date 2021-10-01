using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player2 : MonoBehaviour
{

    public GameObject[] tag1Characters;
    public GameObject[] tag2Characters;


    //private void Awake()
    //{

    //    for (int i = 0; i <= tag1Characters.Length; i++)
    //    {
    //        tag1Characters[i].SetActive(false);
    //        tag2Characters[i].SetActive(false);
    //    }

    //}


    // Update is called once per frame
    void Update()
    {
        // immediately after both character 1 and character 2 is set from 0-3
        // Set Characters 3 and characters 4 back to -1 on main menu - this is important. and at the end of every round.
        if (PlayerPrefs.GetInt("Character3") != -1 && PlayerPrefs.GetInt("Character4") != -1 )
        {
            tag1Characters[PlayerPrefs.GetInt("Character3")].SetActive(true);
            tag2Characters[PlayerPrefs.GetInt("Character4")].SetActive(true);
        }
    }
}
