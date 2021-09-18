using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPickup : MonoBehaviour
{
    private Inventory inventory;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Player2"))
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if(inventory.isFull[i] == false)
                {
                    inventory.isFull[i] = true;
                    Debug.Log("FoodStored");
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
