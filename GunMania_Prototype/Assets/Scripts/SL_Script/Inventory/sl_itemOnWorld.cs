using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sl_itemOnWorld : MonoBehaviour
{
    public sl_Item thisItem;
    public sl_Inventory playerInventory;  //set which inventory should be place in

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            AddNewItem();
            Destroy(gameObject);
        }
    }

    public void AddNewItem()
    {
        //check is it contain in list?
        if(!playerInventory.itemList.Contains(thisItem))
        {
            playerInventory.itemList.Add(thisItem);
        }
        else
        {
            //add num (if already in list) -----> but we nonid this, so leave this code here as reference
            thisItem.itemHeldNum += 1;
        }
    }
}
