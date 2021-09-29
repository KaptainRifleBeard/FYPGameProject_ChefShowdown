using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sl_Slot : MonoBehaviour
{
    public sl_Item slotItem;
    public Image slotImage;

    public GameObject itemInSlot;

    //public Text slotNum; //show in ui - how many item in a slot

    public void SetupSlot(sl_Item item)
    {
        if(item == null) // if is empty
        {
            itemInSlot.SetActive(false);
            return;
        }
        slotImage.sprite = item.itemImage;
    }
}
