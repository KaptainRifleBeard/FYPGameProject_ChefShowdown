using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDestroy : MonoBehaviour
{
    public float waterCountdown = 3f;

    // Update is called once per frame
    void Update()
    {
        if(waterCountdown > 0)
        {
            waterCountdown -= Time.deltaTime;

            if(waterCountdown <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
