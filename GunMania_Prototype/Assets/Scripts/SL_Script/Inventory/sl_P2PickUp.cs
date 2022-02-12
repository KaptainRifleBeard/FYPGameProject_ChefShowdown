using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sl_P2PickUp : MonoBehaviour
{
    public sl_Item thisItem;
    public sl_Inventory playerInventory;  //set which inventory should be place in
    public static bool isPicked = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player2"))
        {
            isPicked = true;

            AddNewItem();
            Destroy(gameObject);
        }
    }

    public void AddNewItem()
    {
        //check is it contain in list?
        if (!playerInventory.itemList.Contains(thisItem))
        {
            //playerInventory.itemList.Add(thisItem);
            //sl_InventoryManager.CreateNewItem(thisItem);

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
        sl_p2InventoryManager.RefreshItem();
    }
}
