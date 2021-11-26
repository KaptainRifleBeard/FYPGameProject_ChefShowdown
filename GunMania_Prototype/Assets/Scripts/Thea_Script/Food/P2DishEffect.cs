using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2DishEffect : MonoBehaviour
{
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
            sl_P2PlayerHealth.p2currentHealth -= 3;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Tojangjochi")
        {
            //stun
        }
        else if (other.gameObject.tag == "Hassun")
        {
            sl_P2PlayerHealth.p2currentHealth += 3;
        }
        else if (other.gameObject.tag == "Mukozuke")
        {
            Rigidbody playerRidg = gameObject.GetComponent<Rigidbody>();
            sl_P2PlayerHealth.p2currentHealth -= 2;

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
            sl_P2PlayerHealth.p2currentHealth -= 2;

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
