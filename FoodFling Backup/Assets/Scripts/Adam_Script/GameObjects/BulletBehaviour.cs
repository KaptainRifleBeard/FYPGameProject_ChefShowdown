using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    //Variables
    public float bulletSpeed;
    public float maxDistance;
    public float damage;

    private GameObject triggeringEnemy;
    private GameObject player;
    private GameObject player2;

    //Methods
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        player2 = GameObject.FindWithTag("Player2");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * bulletSpeed);
        maxDistance += 1 * Time.deltaTime;

        if (maxDistance >= 5)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            triggeringEnemy = other.gameObject;
            triggeringEnemy.GetComponent<EnemyBehaviour>().enemyHealth -= damage;
            Destroy(this.gameObject);
        }

        if (other.tag == "Player")
        {
            player.GetComponent<PlayerBehaviour>().health -= 20;
            Destroy(this.gameObject);
        }

        if (other.tag == "Player2")
        {
            player2.GetComponent<Player2Behaviour>().health -= 20;
            Destroy(this.gameObject);
        }
        if (other.tag == "Cover")
        {
            Destroy(this.gameObject);
        }
    }

}
