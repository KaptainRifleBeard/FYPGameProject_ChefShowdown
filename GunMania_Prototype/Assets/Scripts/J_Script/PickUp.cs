using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PickUp : MonoBehaviour
{
    public Inventory inventory;
    public FoodIDAssign id;
    public GameObject foodImage;
    public static bool isPicked = false;


    private void Start()
    {
        //inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                //if (inventory.isFull[i] == false)
                if(inventory.IDlist.Count < 2)
                {
                    //inventory.isFull[i] = true;

                    inventory.IDlist.Add(id.FoodIDCheck());
                    Debug.Log(inventory.IDlist[i]);

                    Instantiate(foodImage, inventory.slots[i].transform, false);
                    //Debug.Log("FoodStored");
                    Destroy(gameObject);

                    if ((other.tag == "Player"))
                    {
                        isPicked = true;
                        Debug.Log("Picked");
                    }

                    break;

                }
            }
        }


        
    }
}
