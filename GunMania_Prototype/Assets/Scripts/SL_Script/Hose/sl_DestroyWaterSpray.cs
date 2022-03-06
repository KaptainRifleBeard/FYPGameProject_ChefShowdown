using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class sl_DestroyWaterSpray : MonoBehaviour
{
    public BoxCollider col;

    void Start()
    {

    }


    void Update()
    {
        StartCoroutine(StopSpreading());

    }

  

    IEnumerator StopSpreading()
    {
        yield return new WaitForSeconds(4.5f);
        col.enabled = false;

        yield return new WaitForSeconds(5.5f);
        Destroy(gameObject);
    }

}
