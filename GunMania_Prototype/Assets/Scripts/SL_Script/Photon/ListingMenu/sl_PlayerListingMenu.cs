using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class sl_PlayerListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private sl_PlayerListing playerListing;
    [SerializeField]private Transform content;

    public List<sl_PlayerListing> listings = new List<sl_PlayerListing>();

    private void Awake()
    {
        GetCurrentRoomPlayer();
    }

    private void GetCurrentRoomPlayer()
    {
        foreach(KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo.Value);
        }
    }

    private void AddPlayerListing(Player player)
    {
        sl_PlayerListing listing = Instantiate(playerListing, content);
        if (listing != null)
        {
            listing.SetPlayerInfo(player);
            listings.Add(listing);
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
    }

   
}
