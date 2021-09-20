using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupTest : MonoBehaviour
{
    public static bool isPicked = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Player2")
        {
            Destroy(this.gameObject);
            isPicked = true;
        }
    }

}
