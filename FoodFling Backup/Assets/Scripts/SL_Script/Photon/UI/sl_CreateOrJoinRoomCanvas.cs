using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sl_CreateOrJoinRoomCanvas : MonoBehaviour
{
    [SerializeField] private sl_CreateAndJoinRoom createRoomMenu;
    private sl_RoomCanvases roomCanvas;

    public void FirstInitialize(sl_RoomCanvases canvases)
    {
        roomCanvas = canvases;
        createRoomMenu.FirstInitialize(canvases);
    }
}
