using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using TMPro;

public class sl_PlayerListingMenu : MonoBehaviourPunCallbacks
{
    PhotonView view;

    [SerializeField] private sl_PlayerListing playerListing;
    [SerializeField]private Transform content;

    public List<sl_PlayerListing> listings = new List<sl_PlayerListing>();
    private sl_RoomCanvases roomCanvas;


    [Space(10)]
    [Header("When P2 is waiting")]
    public TextMeshProUGUI waitingText;

    public GameObject[] p1_blankIcon;
    public GameObject[] blankIcon;

    public GameObject[] thingsToDisable;
    public GameObject[] disableP2Indicator;

    public static int p2IsIn;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

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

        if (i != -1)
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
    }

    public void StartGame()
    {
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        if (PhotonNetwork.IsMasterClient /*&& playerCount == 2*/)  // room owner
        {
            PhotonNetwork.LoadLevel("sl_TestScene");

        }
        PhotonNetwork.CurrentRoom.IsOpen = false;

    }

    public void DisableP2Indicator()
    {

        for (int i = 0; i < disableP2Indicator.Length; i++)
        {
            disableP2Indicator[i].SetActive(false);
        }

    }

    private void Update()
    {
        if (listings.Count == 1)
        {
            waitingText.text = "Waiting";

            blankIcon[0].SetActive(true);
            blankIcon[1].SetActive(true);

            for (int i = 0; i < thingsToDisable.Length; i++)
            {
                thingsToDisable[i].SetActive(false);
            }
            DisableP2Indicator();

            p2IsIn = 0;
        }
        else
        {

            waitingText.text = sl_P2CharacterSelect.roomNickname2;
            p2IsIn = 1;

            //blankIcon[0].SetActive(false);
            //blankIcon[1].SetActive(false);

            for (int i = 0; i < thingsToDisable.Length; i++)
            {
                thingsToDisable[i].SetActive(true);
            }



        }


    }

}
