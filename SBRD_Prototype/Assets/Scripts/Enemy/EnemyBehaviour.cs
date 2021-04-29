using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    //Variables
    public float enemyHealth;
    public GameObject player;
    public float pointsToGive;

    //Methods

    public void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void Update()
    {
        if (enemyHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(this.gameObject);
        player.GetComponent<PlayerBehaviour>().points += pointsToGive;
    }

}
