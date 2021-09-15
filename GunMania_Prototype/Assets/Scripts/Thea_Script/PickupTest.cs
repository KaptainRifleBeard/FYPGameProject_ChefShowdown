using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupTest : MonoBehaviour
{
    public static bool isPicked;

    public void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
        isPicked = true;
    }

}
