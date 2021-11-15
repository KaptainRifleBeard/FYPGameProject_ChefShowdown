using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class sl_P2PickUp : MonoBehaviour
{
    public sl_Item thisItem;
    public sl_Inventory playerInventory;  //set which inventory should be place in

    public static bool isPicked = false;
    public static bool isPickedDish = false;

    PhotonView view;

    void Start()
    {
        view = GetComponent<PhotonView>();
        isPicked = false;
        isPickedDish = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player2"))
        {
            if (sl_P2ShootBehavior.p2bulletCount < 2)
            {
                view.RPC("AddFood2", RpcTarget.All);
                sl_P2ShootBehavior.p2bulletCount += 1;

                if (gameObject.layer == LayerMask.NameToLayer("Food"))
                {
                    isPicked = true;
                    AddNewItem();


                }
                else if (gameObject.layer == LayerMask.NameToLayer("Dish"))
                {
                    isPickedDish = true;
                    AddNewItem();

                    view.RPC("DestroyDish2", RpcTarget.All);

                }
            }
        }
    }

    public void AddNewItem()
    {
        //check is it contain in list?
        /*if (!playerInventory.itemList.Contains(thisItem))
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
        */


        for (int i = 0; i < playerInventory.itemList.Count; i++)
        {
            if (playerInventory.itemList[i] == null)
            {
                playerInventory.itemList[i] = thisItem;
                break;
            }
        }
        sl_p2InventoryManager.RefreshItem();
    }

    [PunRPC]
    public void StartCountdown()
    {
        gameObject.SetActive(true);
        isPicked = false;
    }


    [PunRPC]
    public void AddFood2()
    {
        gameObject.SetActive(false);
        Invoke("StartCountdown", 6);  //wait for 6 sec

    }


    [PunRPC]
    public void DestroyDish2()
    {
        Destroy(gameObject);
    }

    //private IEnumerator MoveToFront()
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    playerInventory.itemList[0] = playerInventory.itemList[1];
    //    sl_p2InventoryManager.RefreshItem();

    //    yield return new WaitForSeconds(0.1f);
    //    playerInventory.itemList[1] = null;
    //    sl_p2InventoryManager.RefreshItem();

    //    count = 0;
    //}


    //void Update()
    //{
    //    ////completely hard code
    //    //if (sl_P2ShootBehavior.p2Shoot && playerInventory.itemList[0] != null) //if shoot, check list[0] have bullet or not
    //    //{
    //    //    if (count < 1 && spawn == false)  //to spawn only one per time
    //    //    {
    //    //        if (count < 1)
    //    //        {
    //    //            spawn = true;

    //    //            playerInventory.itemList[0] = null;
    //    //            sl_p2InventoryManager.RefreshItem();
    //    //            StartCoroutine(MoveToFront());

    //    //            count++;

    //    //        }
    //    //        if (count == 1)
    //    //        {
    //    //            spawn = false;
    //    //        }
    //    //    }
    //    //}

    //}
}
