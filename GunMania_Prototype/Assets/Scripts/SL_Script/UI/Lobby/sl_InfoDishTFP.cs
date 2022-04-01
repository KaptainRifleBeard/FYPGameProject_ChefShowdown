using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sl_InfoDishTFP : MonoBehaviour
{
    public Sprite[] pictures;

    public Image pic;
    int rand;

    void Start()
    {
        rand = Random.Range(0, pictures.Length);

    }

    private void Update()
    {
        //food
        if (rand == 0 || rand == 1)
        {
            pic.sprite = pictures[0];
        }
        if (rand == 2)
        {
            pic.sprite = pictures[1];

        }
        if (rand == 3)
        {
            pic.sprite = pictures[2];
        }
        if (rand == 4)
        {
            pic.sprite = pictures[3];
        }
        if (rand == 5)
        {
            pic.sprite = pictures[4];
 }
        if (rand == 6)
        {
            pic.sprite = pictures[5];
        }
        if (rand == 7)
        {
            pic.sprite = pictures[6];
        }
        if (rand == 8)
        {
            pic.sprite = pictures[7];
        }
        if (rand == 9)
        {
            pic.sprite = pictures[8];
        }
        if (rand == 10)
        {
            pic.sprite = pictures[9];
        }
        if (rand == 11)
        {
            pic.sprite = pictures[10];

       }
        if (rand == 12)
        {
            pic.sprite = pictures[11];
        }
    }

    public void ToServerLobby()
    {
        SceneManager.LoadScene("sl_ServerLobby");
    }
}
