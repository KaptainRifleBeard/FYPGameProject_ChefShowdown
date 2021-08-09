using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPickup : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Player2")
        {
            this.gameObject.SetActive(false);
        }
    }
}
