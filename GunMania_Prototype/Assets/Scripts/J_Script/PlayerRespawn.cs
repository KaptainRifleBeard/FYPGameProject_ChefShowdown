using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private Transform player1;
    [SerializeField] private Transform respawnPoint1;
    [SerializeField] private Transform player2;
    [SerializeField] private Transform respawnPoint2;

    void Start()
    {
        //StartCoroutine(OnTriggerEnter());
        //Invoke("OnTriggerEnter", 2);

        player1 = GameObject.FindGameObjectWithTag("Player").transform;
        //player2 = GameObject.FindGameObjectWithTag("Player2").transform;

    }

    public void OnTriggerEnter(Collider other)
    {
        //yield return new WaitForSeconds(2f);

        if(other.gameObject.tag == "Player")
        {
            player1.transform.position = respawnPoint1.transform.position;
        }

        if (other.gameObject.tag == "Player2")
        {
            player2.transform.position = respawnPoint2.transform.position;
        }
    }
    
}
