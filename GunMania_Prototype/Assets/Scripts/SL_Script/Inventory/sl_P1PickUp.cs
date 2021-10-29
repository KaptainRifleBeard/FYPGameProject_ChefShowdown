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
    int count;
    bool spawn;
    private bool hasCoroutineStarted = false;


    void Start()
    {
        view = GetComponent<PhotonView>();
        isPicked = false;
        isPickedDish = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && sl_ShootBehavior.bulletCount < 2)
        {

            if (gameObject.layer == LayerMask.NameToLayer("Food"))
            {
                isPicked = true;
                AddNewItem();
                //gameObject.SetActive(false);
                Destroy(gameObject);


            }
            else if (gameObject.layer == LayerMask.NameToLayer("Dish"))
            {
                isPickedDish = true;

                AddNewItem();
                //gameObject.SetActive(false);
                Destroy(gameObject);


            }
            sl_ShootBehavior.bulletCount += 1;

            //Destroy(gameObject);
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
        sl_InventoryManager.RefreshItem();
        
    }

    IEnumerator waitToSpawn()
    {
        yield return new WaitForSeconds(3.0f);
        gameObject.SetActive(true);
        hasCoroutineStarted = false;
    }

    //private IEnumerator MoveToFront()
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    playerInventory.itemList[0] = playerInventory.itemList[1];
    //    sl_InventoryManager.RefreshItem();

    //    yield return new WaitForSeconds(0.1f);
    //    playerInventory.itemList[1] = null;
    //    sl_InventoryManager.RefreshItem();

    //    count = 0;
    //}


    //void Update()
    //{
    //    ////completely hard code
    //    //if (Input.GetMouseButtonDown(1) && playerInventory.itemList[0] != null) //if shoot, check list[0] have bullet or not
    //    //{
    //    //    if (count < 1 && spawn == false)  //to spawn only one per time
    //    //    {
    //    //        if (count < 1)
    //    //        {
    //    //            spawn = true;

    //    //            playerInventory.itemList[0] = null;

    //    //            sl_InventoryManager.RefreshItem();
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

    public void Update()
    {
        //if (isPicked == true || isPickedDish == true)
        //{
        //    StartCoroutine(waitToSpawn());
        //}
    }

}
