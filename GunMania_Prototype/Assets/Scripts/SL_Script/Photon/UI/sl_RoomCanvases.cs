using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sl_RoomCanvases : MonoBehaviour
{
    [SerializeField] private sl_CreateOrJoinRoomCanvas createOrJoinCanvas;
    public sl_CreateOrJoinRoomCanvas CreateOrJoinCanvas { get { return createOrJoinCanvas; } }



    //[SerializeField] private sl_CurrentRoomCanvas currentRoomCanvas;
    //public sl_CurrentRoomCanvas CurrentRoomCanvas { get { return currentRoomCanvas; } }


    private void Awake()
    {
        FirstInitialize();
    }

    public void FirstInitialize()
    {
        CreateOrJoinCanvas.FirstInitialize(this);
        //CurrentRoomCanvas.FirstInitialize(this);
    }


}
