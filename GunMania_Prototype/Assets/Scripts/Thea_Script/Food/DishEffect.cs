using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishEffect : MonoBehaviour
{
    
    //both speed 100
    //rigidbody drag 2
    public float knockbackSpeed;
    public float pullingSpeed;
    public static bool canMove;

    float timer;

    private void Start()
    {
        timer = 0;
        canMove = true;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "P2Sinseollo")
        {
            sl_PlayerHealth.currentHealth -= 3;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "P2Tojangjochi")
        {
            //stun
            canMove = false;
            StartCoroutine(StunDeactive(6));

            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "P2Hassun")
        {
            sl_PlayerHealth.currentHealth += 3;
        }
        else if (other.gameObject.tag == "P2Mukozuke")
        {
            Rigidbody playerRidg = gameObject.GetComponent<Rigidbody>();
            sl_PlayerHealth.currentHealth -= 2;

            Vector3 direction = (other.transform.position - transform.position).normalized;
            direction.y = 0;

            playerRidg.AddForce(direction * pullingSpeed, ForceMode.Impulse);
            Destroy(other.gameObject); 
        }
        else if (other.gameObject.tag == "P2BirdNestSoup")
        {
            //aoe
        }
        else if (other.gameObject.tag == "P2BuddhaJumpsOvertheWall")
        {
            //silence
        }
        else if (other.gameObject.tag == "P2FoxtailMillet")
        {
            Rigidbody playerRidg = gameObject.GetComponent<Rigidbody>();
            sl_PlayerHealth.currentHealth -= 2;
            
            Vector3 direction = (transform.position - other.transform.position).normalized;
            direction.y = 0;

            playerRidg.AddForce(direction * knockbackSpeed, ForceMode.Impulse);
               
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "P2RawStinkyTofu")
        {
            //drop food :')
        }
    }

    public IEnumerator StunDeactive(int time)
    {
        yield return new WaitForSeconds(time);

        canMove = true;
    }
}
