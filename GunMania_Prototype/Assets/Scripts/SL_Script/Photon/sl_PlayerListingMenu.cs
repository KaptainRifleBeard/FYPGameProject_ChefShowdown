using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class sl_PlayerListingMenu : MonoBehaviourPunCallbacks
{
    public sl_RoomListing roomListingPrefab;
    public Transform content;

    public List<sl_RoomListing> listings = new List<sl_RoomListing>();

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {

            if (info.RemovedFromList)
            {
                int i = listings.FindIndex(x => x.RoomInfo.Name == info.Name); //check the list have the same name

                if (i != -1)
                {
                    Destroy(listings[i].gameObject);
                    listings.RemoveAt(i);
                }
            }
            else  //added to room list
            {
                sl_RoomListing listing = Instantiate(roomListingPrefab, content);
                if (listing != null)
                {
                    listing.SetRoomInfo(info);
                    listings.Add(listing);
                }
            }



        }
    }
}
