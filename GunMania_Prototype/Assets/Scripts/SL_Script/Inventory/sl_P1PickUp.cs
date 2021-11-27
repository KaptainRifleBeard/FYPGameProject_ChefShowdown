using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class sl_P1PickUp : MonoBehaviour
{
    public sl_Item thisItem;
    public sl_Inventory playerInventory;  //set which inventory should be place in
    PhotonView view;

    public static bool isPicked;
    public static bool isPickedDish;
   

    void Start()
    {
        view = GetComponent<PhotonView>();
        isPicked = false;
        isPickedDish = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (sl_ShootBehavior.bulletCount < 2)
            {
                view.RPC("AddFood", RpcTarget.All);
                sl_ShootBehavior.bulletCount += 1;

                if (gameObject.layer == LayerMask.NameToLayer("Food"))
                {
                    isPicked = true;
                    AddNewItem();
                    

                }
                else if (gameObject.layer == LayerMask.NameToLayer("Dish"))
                {
                    isPickedDish = true;
                    AddNewItem();

                    view.RPC("DestroyDish", RpcTarget.All);

                }
            }

        }
    }

    public void AddNewItem()
    {
        
        //check is it contain in list?
        /*if (!playerInventory.itemList.Contains(thisItem))
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
        }*/


        for (int i = 0; i < playerInventory.itemList.Count; i++)
        {
            if (playerInventory.itemList[i] == null)
            {
                playerInventory.itemList[i] = thisItem;
                break;
            }
        }
        sl_InventoryManager.RefreshItem();
        
    }

    [PunRPC]
    public void StartCountdown()
    {
        gameObject.SetActive(true);
        isPicked = false;
    }


    [PunRPC]
    public void AddFood()
    {
        gameObject.SetActive(false);
        Invoke("StartCountdown", 6);  //wait for 6 sec

    }

    [PunRPC]
    public void DestroyDish()
    {
        Destroy(gameObject);
    }
}
