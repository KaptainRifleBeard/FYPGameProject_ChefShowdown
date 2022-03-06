using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sl_ShootRangeControl : MonoBehaviour
{

    void Start()
    {
        //****original shoot range = 10f

        if (SL_newP1Movement.changeModelAnim == 0) //brock's shootrange minus 2
        {
            gameObject.transform.localScale = new Vector3(8f, 8f, 8f);
        }

        if (SL_newP1Movement.changeModelAnim == 2) //jiho extra 2 range
        {
            gameObject.transform.localScale = new Vector3(12f, 12f, 12f);
        }
    }


    void Update()
    {
        //****original shoot range = 10f

        if (SL_newP1Movement.changeModelAnim == 0) //brock's shootrange minus 2
        {
            gameObject.transform.localScale = new Vector3(8f, 8f, 8f);
        }

        if (SL_newP1Movement.changeModelAnim == 2) //jiho extra 2 range
        {
            gameObject.transform.localScale = new Vector3(12f, 12f, 12f);
        }
    }
}
