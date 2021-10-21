using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverBehaviour : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    private bool p1Wins;
    private bool p2Wins;

    public void Start()
    {
        player1 = GameObject.FindWithTag("Player");
        player2 = GameObject.FindWithTag("Player2");
    }

    // Update is called once per frame
    void Update()
    {
        if (player2 == null)
        {
            SceneManager.LoadScene("Player1WinScreen");
        }

        else if (player1 == null)
        {
            SceneManager.LoadScene("Player2WinScreen");
        }
    }
}
