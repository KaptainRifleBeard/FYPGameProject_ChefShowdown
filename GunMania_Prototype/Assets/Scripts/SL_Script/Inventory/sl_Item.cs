using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Item", menuName ="Inventory/New Item")]
public class sl_Item : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public int itemHeldNum;
    //[TextArea] //to write more than 1 line text
    //public string itemDescription;

    //public bool equip;  //is it equippable?
}
