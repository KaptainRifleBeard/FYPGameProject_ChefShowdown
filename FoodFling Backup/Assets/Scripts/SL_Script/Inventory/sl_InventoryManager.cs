using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class sl_InventoryManager : MonoBehaviour
{
    static sl_InventoryManager instance;

    public sl_Inventory myInventory;
    public GameObject slotGrid;
    //public Text itemInfo;


    //public sl_Slot slotPrefab;
    public GameObject emptySlot;
    public List<GameObject> slots = new List<GameObject>();
    PhotonView view;


    private void Awake()
    {
        view = GetComponent<PhotonView>();
        if (instance != null)
        {
            Destroy(this);
        }

        instance = this;
    }
   
    private void OnEnable()
    {
        RefreshItem();
    }



    //public static void CreateNewItem(sl_Item item)
    //{
    //    sl_Slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);
    //    newItem.gameObject.transform.SetParent(instance.slotGrid.transform);

    //    newItem.slotItem = item;
    //    newItem.slotImage.sprite = item.itemImage;
        
    //}


    public static void RefreshItem()  //if the item already in the inventory, refresh the list
    {
        //to refresh and change numbers/blablable in the ui,
        //just delete the all gameobjects in the slotgrid, then instantiate again

        for(int i = 0; i < instance.slotGrid.transform.childCount; i++)  //delete
        {
            if(instance.slotGrid.transform.childCount == 0)
            {
                break;  //dont do anything
            }
            else
            {
                Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
                instance.slots.Clear();
            }
        }

        for (int i = 0; i < instance.myInventory.itemList.Count; i++)  //instantiate back, check how many items in the inventoryUI
        {
            //CreateNewItem(instance.myInventory.itemList[i]);
            instance.slots.Add(Instantiate(instance.emptySlot));
            instance.slots[i].transform.SetParent(instance.slotGrid.transform);

            instance.slots[i].GetComponent<sl_Slot>().SetupSlot(instance.myInventory.itemList[i]);
        }

    }


    public static void ClearAllInList()
    {
        for (int i = 0; i < instance.myInventory.itemList.Count; i++)  
        {
            instance.myInventory.itemList[i] = null;
            RefreshItem();
        }
    }

    //public static void MoveToFront()
    //{
    //    for (int i = 0; i < instance.myInventory.itemList.Count; i++)
    //    {
    //        if(instance.myInventory.itemList[i] != null)
    //        {
    //            //instance.myInventory.itemList[i] = instance.myInventory.itemList[i - 1];

    //            //remove first element then add 1 more slot
    //            //instance.myInventory.itemList.RemoveAt(i);
    //            instance.myInventory.itemList[i - 1] = null;


    //        }


    //    }

    //}

}
