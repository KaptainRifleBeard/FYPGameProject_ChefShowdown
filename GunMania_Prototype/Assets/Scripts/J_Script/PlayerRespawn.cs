using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;

    void Start()
    {
        //StartCoroutine(OnTriggerEnter());
        Invoke("OnTriggerEnter", 2);
    }

    public void OnTriggerEnter(Collider other)
    {
        //yield return new WaitForSeconds(2f);
        player.transform.position = respawnPoint.transform.position;
    }
}
