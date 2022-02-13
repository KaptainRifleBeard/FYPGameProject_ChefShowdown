using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sl_CurrentRoomCanvas : MonoBehaviour
{
    [SerializeField] private sl_PlayerListingMenu playerListingMenu;
    [SerializeField] private sl_LeaveRoomMenu leaveRoomMenu;

    private sl_RoomCanvases roomCanvas;

    public void FirstInitialize(sl_RoomCanvases canvases)
    {
        roomCanvas = canvases;
        playerListingMenu.FirstInitialize(canvases);
        leaveRoomMenu.FirstInitialize(canvases);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
