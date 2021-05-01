using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    //Variables
    public float enemyHealth;
    public GameObject player;
    public float pointsToGive;

    public float waitTimeBeforeShoot;
    public float currentWait;
    private bool shot;
    public GameObject bullet;
    public Transform bulletSpawnPoint;
    private Transform bulletSpawn;

    private Transform pistolHolder;

    //Methods

    public void Start()
    {
        player = GameObject.FindWithTag("Player");
        pistolHolder = this.transform.GetChild(0);
        bulletSpawnPoint = pistolHolder.GetChild(2);
    }

    public void Update()
    {
        if (!bulletSpawnPoint)
        {
            pistolHolder = this.transform.GetChild(0);
            bulletSpawnPoint = pistolHolder.GetChild(2);
        }

        if (enemyHealth <= 0)
        {
            Die();
        }

        this.transform.LookAt(player.transform);

        if (currentWait == 0)
        {
            Shoot();
        }

        if (shot && currentWait < waitTimeBeforeShoot)
        {
            currentWait += 1 * Time.deltaTime;
        }

        if (currentWait >= waitTimeBeforeShoot)
        {
            currentWait = 0;
        }
    }

    public void Die()
    {
        Destroy(this.gameObject);
        player.GetComponent<PlayerBehaviour>().points += pointsToGive;
    }

    public void Shoot()
    {
        shot = true;
        bulletSpawn = Instantiate(bullet.transform, bulletSpawnPoint.transform.position, Quaternion.identity);
        bulletSpawn.rotation = this.transform.rotation;
    }

}
