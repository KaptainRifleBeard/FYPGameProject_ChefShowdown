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

    public int prefabNum;
    public GameObject[] foodPrefab;
    int num;

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
            if (P2DishEffect.p2canPick)
            {
                if (sl_P2ShootBehavior.p2bulletCount < 2)
                {
                    prefabNum = Random.Range(0, 2);

                    view.RPC("AddFood2", RpcTarget.All, prefabNum);
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

    //[PunRPC]
    public void StartCountdown2()
    {
        //gameObject.SetActive(true);
        if (num == 1)
        {
            foodPrefab[0].SetActive(true);
        }

        if (num == 2)
        {
            foodPrefab[1].SetActive(true);
        }

        if (num == 3)
        {
            foodPrefab[2].SetActive(true);
        }

        isPicked = false;

    }


    [PunRPC]
    public void AddFood2(int i)
    {
        gameObject.SetActive(false);
        Invoke("StartCountdown2", 3);  //wait for 6 sec

        prefabNum = i;
        if (i == 0)
        {
            num = 1;
        }

        if (i == 1)
        {
            num = 2;
        }

        if (i == 2)
        {
            num = 3;
        }

    }


    [PunRPC]
    public void DestroyDish2()
    {
        Destroy(gameObject);
    }

}
