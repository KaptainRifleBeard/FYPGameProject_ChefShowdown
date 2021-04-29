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

    //Methods
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
    }
}
