using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sl_P2PickUp : MonoBehaviour
{
    public sl_Item thisItem;
    public sl_Inventory playerInventory;  //set which inventory should be place in

    public static bool isPicked = false;
    int count;
    bool spawn;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player2"))
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


    private IEnumerator MoveToFront()
    {
        yield return new WaitForSeconds(0.1f);
        playerInventory.itemList[0] = playerInventory.itemList[1];
        sl_InventoryManager.RefreshItem();

        yield return new WaitForSeconds(0.1f);
        playerInventory.itemList[1] = null;
        sl_InventoryManager.RefreshItem();

        count = 0;
    }


    void Update()
    {
        Debug.Log("bullet count: " + sl_ShootBehavior.bulletCount);
        //completely hard code
        if (Input.GetMouseButtonDown(0)) //if shoot, check list[0] have bullet or not
        {
            if (count < 1 && spawn == false)  //to spawn only one per time
            {
                if (count < 1)
                {
                    spawn = true;

                    playerInventory.itemList[0] = null;
                    sl_InventoryManager.RefreshItem();
                    StartCoroutine(MoveToFront());

                    count++;

                }
                if (count == 1)
                {
                    spawn = false;
                }
            }
        }

    }
}
