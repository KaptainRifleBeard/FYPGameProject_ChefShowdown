using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class sl_P2PickUp : MonoBehaviour
{
    public sl_Item thisItem;
    public sl_Inventory playerInventory;  //set which inventory should be place in

    public static bool isPicked = false;
    public static bool isPickedDish = false;

    PhotonView view;

    public int prefabNum;
    public GameObject[] foodPrefab;

    int num;
    bool pickup; //stop pick when is full
    string audioName;

    void Start()
    {
        view = GetComponent<PhotonView>();
        isPicked = false;
        isPickedDish = false;
    }


    private void Update()
    {
        if (pickup)
        {
            StartCoroutine(WaitToPickAgain()); //prevent pick up too many at 1 location
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player2"))
        {
            if (P2DishEffect.p2canPick)
            {
                if (sl_P2ShootBehavior.p2bulletCount < 2 && !pickup)
                {
                    audioName = "PickUpSfx";
                    SyncAudio();

                    pickup = true;
                    prefabNum = Random.Range(0, 2);

                    view.RPC("AddFood2", RpcTarget.All, prefabNum, pickup);
                    sl_P2ShootBehavior.p2bulletCount += 1;


                    if (gameObject.layer == LayerMask.NameToLayer("Food"))
                    {

                        isPicked = true;
                        AddNewItem();


                    }
                    else if (gameObject.layer == LayerMask.NameToLayer("Dish"))
                    {
                        isPickedDish = true;
                        AddNewItem();

                        view.RPC("DestroyDish2", RpcTarget.All);

                    }
                }
                else
                {
                    pickup = false;
                    view.RPC("AddFood2", RpcTarget.All, prefabNum, pickup);
                }
            }

        }
    }

    public void AddNewItem()
    {
        //check is it contain in list?
        /*if (!playerInventory.itemList.Contains(thisItem))
        {
            //playerInventory.itemList.Add(thisItem);
            //sl_InventoryManager.CreateNewItem(thisItem);

            //find is there is empty slot
            for (int i = 0; i < playerInventory.itemList.Count; i++)
            {
                if (playerInventory.itemList[i] == null)
                {
                    playerInventory.itemList[i] = thisItem;
                    break;
                }
            }
        }
        else
        {
            //add num (if already in list) -----> but we nonid this, so leave this code here as reference
            //thisItem.itemHeldNum += 1;
        }
        */


        for (int i = 0; i < playerInventory.itemList.Count; i++)
        {
            if (playerInventory.itemList[i] == null)
            {
                playerInventory.itemList[i] = thisItem;
                break;
            }
        }
        sl_p2InventoryManager.RefreshItem();
    }

    //[PunRPC]
    public void StartCountdown2()
    {
        if (num == 1)
        {
            foodPrefab[0].SetActive(true);
            isPicked = false;
        }

        if (num == 2)
        {
            foodPrefab[1].SetActive(true);
            isPicked = false;
        }

        if (num == 3)
        {
            foodPrefab[2].SetActive(true);
            isPicked = false;
        }

    }


    [PunRPC]
    public void AddFood2(int i, bool pick)
    {
        prefabNum = i;
        pickup = pick;

        if (pick && i == 0)
        {
            num = 1;

            gameObject.SetActive(false);
            Invoke("StartCountdown2", 6);  //wait for 6 sec
        }

        if (pick && i == 1)
        {
            num = 2;

            gameObject.SetActive(false);
            Invoke("StartCountdown2", 6);  //wait for 6 sec
        }

        if (pick && i == 2)
        {
            num = 3;

            gameObject.SetActive(false);
            Invoke("StartCountdown2", 6);  //wait for 6 sec
        }

    }

    [PunRPC]
    public void P2PickSfx(string n)
    {
        audioName = n;
        FindObjectOfType<sl_AudioManager>().Play(n);

    }

    [PunRPC]
    public void DestroyDish2()
    {
        Destroy(gameObject);
    }

    IEnumerator WaitToPickAgain()
    {
        yield return new WaitForSeconds(0.2f);
        pickup = false;
    }

    public void SyncAudio()
    {
        view.RPC("P2PickSfx", RpcTarget.All, audioName);
    }
}
