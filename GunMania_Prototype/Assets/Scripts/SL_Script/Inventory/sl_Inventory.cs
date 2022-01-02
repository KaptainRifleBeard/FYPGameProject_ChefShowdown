using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/New Inventory")]
public class sl_Inventory : ScriptableObject
{
    public List<sl_Item> itemList = new List<sl_Item>();
}
