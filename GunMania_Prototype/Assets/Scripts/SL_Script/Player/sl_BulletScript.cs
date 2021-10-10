using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class sl_BulletScript : MonoBehaviour
{
    public int bulletDmg;
   
    void Update()
    {
        waitForSec();
    }

    IEnumerator waitForSec()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }

}
