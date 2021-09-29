using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sl_p2InventoryManager : MonoBehaviour
{
    static sl_p2InventoryManager instance;

    public sl_Inventory myInventory;
    public GameObject slotGrid;
    //public Text itemInfo;


    //public sl_Slot slotPrefab;
    public GameObject emptySlot;
    public List<GameObject> slots = new List<GameObject>();


    private void Awake()
    {
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

        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)  //delete
        {
            if (instance.slotGrid.transform.childCount == 0)
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
}
