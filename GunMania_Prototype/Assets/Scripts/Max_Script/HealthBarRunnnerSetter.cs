using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarRunnnerSetter : MonoBehaviour
{
    UltimateBar UltBarmanager;
    HealthBar HealthManager;

    public bool IsThisPlayer2 = false;

    private void OnCollisionEnter(Collision collision)
    {
        // need to insert damage to healthbar, retrieve food damage value from object value.
        //refer to gamedesign document for further references.


        //DEPRECATED UNTIL WE FIND OUT WHO IS RESPONSIBLE FOR PLAYER DAMAGE
        if (collision.gameObject.tag == "food")
        {
            if (collision.gameObject.tag == "enemy")
            {
                UltBarmanager.SetBar(12.5f, true);

                HealthManager.UpdateHealth(2, true);




            }
            else if (collision.gameObject.tag == "player")
            {
                UltBarmanager.SetBar(12.5f, false);
                HealthManager.UpdateHealth(2, false);
            }
        }


        if (IsThisPlayer2 == false)
        {
            UltBarmanager.SetBar(12.5f, false);

            if (collision.gameObject.tag == "Sinseollo")
            {

            }
            else if (collision.gameObject.tag == "Tojangjochi")
            {

            }else if (collision.gameObject.tag == "Hassun")
            {

            }else if (collision.gameObject.tag == "Mukozuke")
            {

            }else if (collision.gameObject.tag == "BirdsNest")
            {

            }else if (collision.gameObject.tag == "BuddahJumpsOverTheWall")
            {

            }else if (collision.gameObject.tag == "Foxtailmillet")
            {

            }else if (collision.gameObject.tag == "StinkyTofu")
            {

            }

        }
        else
        {
            UltBarmanager.SetBar(12.5f, true);




        }

        //else if (collision.gameObject.tag == "superFood")
        //{
        //    if (collision.gameObject.tag == "enemy")
        //    {
        //        Healthmanager.UpdateHealth(2, true);
        //    }
        //    else if (collision.gameObject.tag == "player")
        //    {
        //        Healthmanager.UpdateHealth(2, false);
        //    }
        //}

    }
}
