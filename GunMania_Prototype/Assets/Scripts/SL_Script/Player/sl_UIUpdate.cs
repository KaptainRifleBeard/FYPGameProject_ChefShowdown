using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sl_UIUpdate : MonoBehaviour
{
    void Update()
    {
        sl_InventoryManager.RefreshItem();
        sl_p2InventoryManager.RefreshItem();

    }
}
