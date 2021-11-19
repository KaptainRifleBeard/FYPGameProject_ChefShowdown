using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishEffect : MonoBehaviour
{
    public sl_PlayerHealth player1Health;


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Sinseollo")
        {
            SinseolloEffect();
        }
        else if (other.gameObject.tag == "Tojangjochi")
        {
            Tojangjochi();
        }
        else if (other.gameObject.tag == "Hassun")
        {
            Hassun();
        }
        else if (other.gameObject.tag == "Mukozuke")
        {
            Mukozuke();
        }
        else if (other.gameObject.tag == "BirdNestSoup")
        {
            BirdNestSoup();
        }
        else if (other.gameObject.tag == "BuddhaJumpsOvertheWall")
        {
            BuddhaJumpsOvertheWall();
        }
        else if (other.gameObject.tag == "FoxtailMillet")
        {
            FoxtailMillet();
        }
        else if (other.gameObject.tag == "RawStinkyTofu")
        {
            RawStinkyTofu();
        }
    }

        public void SinseolloEffect()
    {
        sl_PlayerHealth.currentHealth -= 3;
    }

    public void Tojangjochi()
    {
        //stun player code
    }

    public void Hassun()
    {
        sl_PlayerHealth.currentHealth += 3;
    }

    public void Mukozuke()
    {
        sl_PlayerHealth.currentHealth -= 2;
        //movement
        //stun
    }

    public void BirdNestSoup()
    {
        //aoe damage???
    }

    public void BuddhaJumpsOvertheWall()
    {
        sl_PlayerHealth.currentHealth -= 2;
        //silence
    }

    public void FoxtailMillet()
    {
        sl_PlayerHealth.currentHealth -= 2;
        //knockback
    }

    public void RawStinkyTofu()
    {
        //drop food :')
    }
}
