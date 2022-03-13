using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class sl_P1PickUp : MonoBehaviour
{
    public sl_Item thisItem;
    public sl_Inventory playerInventory;  //set which inventory should be place in
    PhotonView view;

    public static bool isPicked;
    public static bool isPickedDish;

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
        if(pickup)
        {
            StartCoroutine(WaitToPickAgain()); //prevent pick up too many at 1 location
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (DishEffect.canPick && !pickup)
            {
                if (sl_ShootBehavior.bulletCount < 2)
                {
                    pickup = true;
                    prefabNum = Random.Range(0, 2);

                    audioName = "PickUpSfx";
                    SyncAudio();

                    view.RPC("AddFood", RpcTarget.All, prefabNum, pickup);
                    sl_ShootBehavior.bulletCount += 1;

                    if (gameObject.layer == LayerMask.NameToLayer("Food"))
                    {
                        isPicked = true;
                        AddNewItem();
                    }
                    else if (gameObject.layer == LayerMask.NameToLayer("Dish"))
                    {
                        isPickedDish = true;
                        AddNewItem();

                        view.RPC("DestroyDish", RpcTarget.All);

                    }
                }
                else
                {
                    pickup = false;
                    view.RPC("AddFood", RpcTarget.All, prefabNum, pickup);
                }
            }

        }
    }

    public void AddNewItem()
    {
        //check is it contain in list?
        /*if (!playerInventory.itemList.Contains(thisItem))
        {
            //find is there is empty slot
            for (int i = 0; i < playerInventory.itemList.Count; i++)
            {
                if (playerInventory.itemList[i] == null)
                {
                    playerInventory.itemList[i] = thisItem;
                    break;
                }
            }
        }*/


        for (int i = 0; i < playerInventory.itemList.Count; i++)
        {
            if (playerInventory.itemList[i] == null)
            {
                playerInventory.itemList[i] = thisItem;
                break;
            }
        }
        sl_InventoryManager.RefreshItem();
        
    }

    //[PunRPC]
    public void StartCountdown()
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
    public void AddFood(int i, bool pick)
    {
        prefabNum = i;
        pickup = pick;

        if(pick && i == 0)
        {
            num = 1;

            gameObject.SetActive(false);
            Invoke("StartCountdown", 6);  //wait for 6 sec
        }

        if (pick && i == 1)
        {
            num = 2;

            gameObject.SetActive(false);
            Invoke("StartCountdown", 6);  //wait for 6 sec
        }

        if (pick && i == 2)
        {
            num = 3;

            gameObject.SetActive(false);
            Invoke("StartCountdown", 6);  //wait for 6 sec
        }


    }

    [PunRPC]
    public void DestroyDish()
    {
        Destroy(gameObject);
    }

    [PunRPC]
    public void P1PickSfx(string n)
    {
        audioName = n;
        FindObjectOfType<sl_AudioManager>().Play(n);

    }

    IEnumerator WaitToPickAgain()
    {
        yield return new WaitForSeconds(0.5f);
        pickup = false;
    }

    public void SyncAudio()
    {
        view.RPC("P1PickSfx", RpcTarget.All, audioName);
    }
}
