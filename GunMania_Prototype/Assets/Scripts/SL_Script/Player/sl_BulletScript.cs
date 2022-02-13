using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sl_BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
