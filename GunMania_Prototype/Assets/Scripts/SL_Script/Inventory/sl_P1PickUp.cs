using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class sl_P1PickUp : MonoBehaviour
{
    public sl_Item thisItem;
    public sl_Inventory playerInventory;  //set which inventory should be place in
    PhotonView view;

    public static bool isPicked = false;


    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (sl_ShootBehavior.bulletCount < 2)
            {
                sl_ShootBehavior.bulletCount += 1;

                isPicked = true;
                AddNewItem();
                Destroy(gameObject);
            }
            else
            {
                isPicked = false;

            }

        }
    }

    public void AddNewItem()
    {
        //check is it contain in list?
        if (!playerInventory.itemList.Contains(thisItem))
        {
            //find is there is empty slot
            for (int i = 0; i < playerInventory.itemList.Count; i++)
            {
                if (playerInventory.itemList[i] == null)
                {
                    playerInventory.itemList[i] = thisItem;
                    break;
                }
            }
        }
        else
        {
            //add num (if already in list) -----> but we nonid this, so leave this code here as reference
            //thisItem.itemHeldNum += 1;
        }
        sl_InventoryManager.RefreshItem();
        
    }


    public void RemoveItem()
    {
        if (playerInventory.itemList.Contains(thisItem))
        {
            //find is there is empty slot
            for (int i = 0; i < playerInventory.itemList.Count; i++)
            {
                playerInventory.itemList[i] = null;
                break;
            }
        }
        sl_InventoryManager.RefreshItem();

        if(view)
        {
            view.RPC("AddNewItem", )

        }
    }


    private void Update()
    {
        //if (Input.GetMouseButtonDown(0) && sl_ShootBehavior.bulletCount > 0)
        //{
        //    RemoveItem();
        //}
    }
}
