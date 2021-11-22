using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishEffect : MonoBehaviour
{
    public sl_PlayerHealth player1Health;
    public GameObject bullet;
    
    
    public float knockbackSpeed;
    public float pullingSpeed;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Sinseollo")
        {
            sl_PlayerHealth.currentHealth -= 3;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Tojangjochi")
        {
            //stun
        }
        else if (other.gameObject.tag == "Hassun")
        {
            //sl_PlayerHealth.currentHealth += 3;
        }
        else if (other.gameObject.tag == "Mukozuke")
        {
            Rigidbody playerRidg = gameObject.GetComponent<Rigidbody>();
            sl_PlayerHealth.currentHealth -= 2;

            Vector3 direction = (other.transform.position - transform.position).normalized;
            direction.y = 0;

            playerRidg.AddForce(direction * pullingSpeed, ForceMode.Impulse);
            Destroy(other.gameObject); 
        }
        else if (other.gameObject.tag == "BirdNestSoup")
        {
            //aoe
        }
        else if (other.gameObject.tag == "BuddhaJumpsOvertheWall")
        {
            //silence
        }
        else if (other.gameObject.tag == "FoxtailMillet")
        {
            Rigidbody playerRidg = gameObject.GetComponent<Rigidbody>();
            sl_PlayerHealth.currentHealth -= 2;
            
            Vector3 direction = (transform.position - other.transform.position).normalized;
            direction.y = 0;

            playerRidg.AddForce(direction * knockbackSpeed, ForceMode.Impulse);
               
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "RawStinkyTofu")
        {
            //drop food :')
        }
    }

   
}
