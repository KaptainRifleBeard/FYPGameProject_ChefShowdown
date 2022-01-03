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
    }

    public void OnTriggerEnter(Collider other)
    {
        //yield return new WaitForSeconds(2f);
        player1.transform.position = respawnPoint1.transform.position;
        player2.transform.position = respawnPoint2.transform.position;
    }
}
