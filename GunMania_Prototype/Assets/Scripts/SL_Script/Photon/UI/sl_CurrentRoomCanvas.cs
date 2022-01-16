using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class sl_CurrentRoomCanvas : MonoBehaviour
{
    [SerializeField] private sl_PlayerListingMenu playerListingMenu;
    [SerializeField] private sl_LeaveRoomMenu leaveRoomMenu;

    private sl_RoomCanvases roomCanvas;
    public GameObject startButton;

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

    private void Update()
    {
        //startButton.SetActive(true);

        if (PhotonNetwork.IsMasterClient && sl_P2CharacterSelect.p2Num == 1)
        {
            startButton.SetActive(true);

        }
        else
        {
            startButton.SetActive(false);
        }
    }
}
