using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public class sl_PlayerListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private sl_PlayerListing playerListing;
    [SerializeField]private Transform content;

    public List<sl_PlayerListing> listings = new List<sl_PlayerListing>();
    private sl_RoomCanvases roomCanvas;

    public override void OnEnable()
    {
        base.OnEnable();
        GetCurrentRoomPlayer();
    }

    public override void OnDisable()
    {
        base.OnDisable();
        for(int i = 0; i < listings.Count; i++)
        {
            Destroy(listings[i].gameObject);
            listings.Clear();
        }
    }

    public void FirstInitialize(sl_RoomCanvases canvases)
    {
        roomCanvas = canvases;
    }


    private void GetCurrentRoomPlayer()
    {
        if(!PhotonNetwork.IsConnected)
        {
            return;
        }

        if(PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null)
        {
            return;
        }
        foreach(KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo.Value);
        }
    }

    private void AddPlayerListing(Player player)
    {
        int i = listings.FindIndex(x => x.Player == player);

        if(i != -1)
        {
            listings[i].SetPlayerInfo(player);
        }
        else
        {
            sl_PlayerListing listing = Instantiate(playerListing, content);
            if (listing != null)
            {
                listing.SetPlayerInfo(player);
                listings.Add(listing);
            }
        }
        
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int i = listings.FindIndex(x => x.Player == otherPlayer); //find which player left

        if (i != -1)
        {
            Destroy(listings[i].gameObject);
            listings.RemoveAt(i);
        }

        sl_SpawnPlayerManager.playerNum_p1 = 0;
        sl_SpawnPlayerManager.playerNum_p2 = 0;
        sl_SpawnPlayerManager.playerTagNum_p1 = 0;
        sl_SpawnPlayerManager.playerTagNum_p2 = 0;

    }

    public void StartGame()
    {
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        if (PhotonNetwork.IsMasterClient/* && playerCount == 2*/)  // room owner
        {
            PhotonNetwork.LoadLevel("sl_TestScene");
            //PhotonNetwork.LoadLevel("sl_ForTesting");

        }
        PhotonNetwork.CurrentRoom.IsOpen = false;

    }

}
