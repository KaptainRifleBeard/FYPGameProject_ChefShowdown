using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sl_DestroyWaterSpray : MonoBehaviour
{


    void Start()
    {

    }


    void Update()
    {
        StartCoroutine(StopSpreading());

    }

    IEnumerator StopSpreading()
    {
        yield return new WaitForSeconds(10f); //destroy after 3 sec
        Destroy(gameObject);
    }

}
