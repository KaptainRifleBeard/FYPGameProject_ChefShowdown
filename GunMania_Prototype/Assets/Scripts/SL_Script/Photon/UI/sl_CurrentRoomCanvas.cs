using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sl_CurrentRoomCanvas : MonoBehaviour
{

    private sl_RoomCanvases roomCanvas;

    public void FirstInitialize(sl_RoomCanvases canvases)
    {
        roomCanvas = canvases;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
