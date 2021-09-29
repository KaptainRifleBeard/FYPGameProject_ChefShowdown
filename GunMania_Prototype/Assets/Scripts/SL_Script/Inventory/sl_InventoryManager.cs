using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sl_InventoryManager : MonoBehaviour
{
    static sl_InventoryManager instance;

    public sl_Inventory myInventory;
    public GameObject slotGrid;
    public sl_Slot slotPrefab;
    //public Text itemInfo;


    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }

        instance = this;
    }

    public static void CreateNewItem(sl_Item item)
    {
        sl_Slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.slotGrid.transform);

        newItem.slotItem = item;
        newItem.slotImage.sprite = item.itemImage;
        
    }
}
