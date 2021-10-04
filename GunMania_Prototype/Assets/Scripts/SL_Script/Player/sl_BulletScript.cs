using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sl_BulletScript : MonoBehaviour
{

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
