using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class sl_BulletScript : MonoBehaviour
{
    public int bulletDmg;
    
    private void OnTriggerEnter(Collider other)
    {
        if(gameObject.tag == "Bullet")
        {
            if (other.gameObject.tag == "Player2" || other.gameObject.tag == "Environment")
            {
                Destroy(gameObject);
            }
        }

        if (gameObject.tag == "P2Bullet")
        {
            if (other.gameObject.tag == "Player" || other.gameObject.tag == "Environment")
            {
                Destroy(gameObject);
            }
        }

    }

}
