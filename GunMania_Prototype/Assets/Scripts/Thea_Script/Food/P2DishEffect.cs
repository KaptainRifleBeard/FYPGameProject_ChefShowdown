using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2DishEffect : MonoBehaviour
{

    //both speed 100
    //rigidbody drag 2
    public float knockbackSpeed;
    public float pullingSpeed;
    public static bool p2canMove;
    public static bool p2isForced;

    float timer;

    private void Start()
    {
        timer = 0;
        p2canMove = true;
        p2isForced = false;
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
            p2canMove = false;
            StartCoroutine(StunDeactive(6));

            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Hassun")
        {
            sl_P2PlayerHealth.p2currentHealth += 3;
        }
        else if (other.gameObject.tag == "Mukozuke")
        {
            //pull
            Rigidbody playerRidg = gameObject.GetComponent<Rigidbody>();
            sl_P2PlayerHealth.p2currentHealth -= 2;

            Vector3 direction = (other.transform.position - transform.position).normalized;
            direction.y = 0;

            playerRidg.AddForce(direction * pullingSpeed, ForceMode.Impulse);
            p2isForced = true;
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
            //knock
            //Rigidbody playerRidg = gameObject.GetComponent<Rigidbody>();
            //sl_P2PlayerHealth.p2currentHealth -= 2;

            //Vector3 direction = (transform.position - other.transform.position).normalized;
            //direction.y = 0;

            //playerRidg.AddForce(direction * knockbackSpeed, ForceMode.Impulse);
            //p2isForced = true;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "RawStinkyTofu")
        {
            //drop food :')
        }
    }

    public IEnumerator StunDeactive(int time)
    {
        yield return new WaitForSeconds(time);

        p2canMove = true;
    }
}
