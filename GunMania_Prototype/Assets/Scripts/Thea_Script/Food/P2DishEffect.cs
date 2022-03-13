using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class P2DishEffect : MonoBehaviour
{
    PhotonView view;

    Rigidbody playerRidg;
    //both speed 100
    //rigidbody drag 2
    public float knockbackSpeed;
    public float pullingSpeed;
    public static bool p2canMove;
    public static bool p2canPick;

    public sl_Inventory playerInventory;

    public List<GameObject> dishPrefab;
    public List<GameObject> foodPrefab;

    Vector3 offset;
    GameObject obj;
    int syncnum;

    private void Start()
    {
        p2canMove = true;
        p2canPick = true;
        playerRidg = gameObject.GetComponent<Rigidbody>();
        view = GetComponent<PhotonView>();

        offset = new Vector3(0, 0, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tojangjochi")
        {
            //stun
            view.RPC("Stun2", RpcTarget.All);

            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Mukozuke")
        {
            //pull
            Vector3 direction = (other.transform.position - transform.position).normalized;
            direction.y = 0;

            view.RPC("Pull2", RpcTarget.All, direction);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "BuddhaJumpsOvertheWall")
        {
            //silence
            view.RPC("Silence2", RpcTarget.All);

            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "FoxtailMillet")
        {
            //knock
            Vector3 direction = (transform.position - other.transform.position).normalized;
            direction.y = 0;

            view.RPC("Push2", RpcTarget.All, direction);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "RawStinkyTofu")
        {
            //drop food :')
            DropFood();

            if (playerInventory.itemList[0] != null)
            {
                sl_P2ShootBehavior.p2bulletCount--;
                playerInventory.itemList[0] = null;
                sl_InventoryManager.RefreshItem();

            }

            Destroy(other.gameObject);
        }
    }

    public IEnumerator StunDeactive()
    {
        yield return new WaitForSeconds(6.0f);

        p2canMove = true;
    }

    public IEnumerator SilenceDeactive()
    {
        yield return new WaitForSeconds(4.0f);
        p2canPick = true;
    }

    [PunRPC]
    public void Pull2(Vector3 dir)
    {
        playerRidg.AddForce(dir * pullingSpeed, ForceMode.Impulse);
    }

    [PunRPC]
    public void Push2(Vector3 dir)
    {
        playerRidg.AddForce(dir * knockbackSpeed, ForceMode.Impulse);
    }

    [PunRPC]
    public void Stun2()
    {
        p2canMove = false;
        StartCoroutine(StunDeactive());
    }
    [PunRPC]
    public void Silence2()
    {
        p2canPick = false;
        StartCoroutine(SilenceDeactive());
    }
    public void DropFood()
    {
        if (playerInventory.itemList[0] != null)
        {
            if (playerInventory.itemList[0].itemHeldNum == 1)
            {
                syncnum = 1;
                view.RPC("SyncFood", RpcTarget.All, syncnum);
            }
            else if (playerInventory.itemList[0].itemHeldNum == 2)
            {
                syncnum = 2; view.RPC("SyncFood2", RpcTarget.All, syncnum);

            }
            else if (playerInventory.itemList[0].itemHeldNum == 3)
            {
                syncnum = 3; view.RPC("SyncFood2", RpcTarget.All, syncnum);


            }
            else if (playerInventory.itemList[0].itemHeldNum == 4)
            {
                syncnum = 4; view.RPC("SyncFood2", RpcTarget.All, syncnum);


            }
            else if (playerInventory.itemList[0].itemHeldNum == 5)
            {
                syncnum = 5; view.RPC("SyncFood2", RpcTarget.All, syncnum);


            }
            else if (playerInventory.itemList[0].itemHeldNum == 6)
            {
                syncnum = 6; view.RPC("SyncFood", RpcTarget.All, syncnum);


            }
            else if (playerInventory.itemList[0].itemHeldNum == 7)
            {
                syncnum = 7; view.RPC("SyncFood2", RpcTarget.All, syncnum);


            }
            else if (playerInventory.itemList[0].itemHeldNum == 8)
            {
                syncnum = 8; view.RPC("SyncFood2", RpcTarget.All, syncnum);

            }



            //from here is food (12 food)
            else if (playerInventory.itemList[0].itemHeldNum == 10)
            {
                syncnum = 10; view.RPC("SyncFood2", RpcTarget.All, syncnum);

            }
            else if (playerInventory.itemList[0].itemHeldNum == 11)
            {
                syncnum = 11; view.RPC("SyncFood2", RpcTarget.All, syncnum);

            }
            else if (playerInventory.itemList[0].itemHeldNum == 12)
            {
                syncnum = 12; view.RPC("SyncFood2", RpcTarget.All, syncnum);

            }
            else if (playerInventory.itemList[0].itemHeldNum == 13)
            {
                syncnum = 13; view.RPC("SyncFood2", RpcTarget.All, syncnum);

            }
            else if (playerInventory.itemList[0].itemHeldNum == 14)
            {
                syncnum = 14; view.RPC("SyncFood2", RpcTarget.All, syncnum);

            }
            else if (playerInventory.itemList[0].itemHeldNum == 15)
            {
                syncnum = 15; view.RPC("SyncFood2", RpcTarget.All, syncnum);

            }
            else if (playerInventory.itemList[0].itemHeldNum == 16)
            {
                syncnum = 16; view.RPC("SyncFood2", RpcTarget.All, syncnum);

            }
            else if (playerInventory.itemList[0].itemHeldNum == 17)
            {
                syncnum = 17; view.RPC("SyncFood2", RpcTarget.All, syncnum);

            }
            else if (playerInventory.itemList[0].itemHeldNum == 18)
            {
                syncnum = 18; view.RPC("SyncFood2", RpcTarget.All, syncnum);

            }
            else if (playerInventory.itemList[0].itemHeldNum == 19)
            {
                syncnum = 19; view.RPC("SyncFood2", RpcTarget.All, syncnum);

            }
            else if (playerInventory.itemList[0].itemHeldNum == 20)
            {
                syncnum = 20; view.RPC("SyncFood2", RpcTarget.All, syncnum);

            }
            else if (playerInventory.itemList[0].itemHeldNum == 21)
            {
                syncnum = 21; view.RPC("SyncFood2", RpcTarget.All, syncnum);

            }
        }
    }


    [PunRPC]
    public void SyncFood2(int n)
    {
        syncnum = n;
        if (n == 1)
        {
            obj = Instantiate(dishPrefab[0], transform.position + offset, Quaternion.identity);
            Destroy(obj, 3.0f);
            StartCoroutine(MoveToFront());
        }
        else if (n == 2)
        {
            obj = Instantiate(dishPrefab[1], transform.position + offset, Quaternion.identity);
            Destroy(obj, 3.0f);
            StartCoroutine(MoveToFront());

        }
        else if (n == 3)
        {
            obj = Instantiate(dishPrefab[2], transform.position + offset, Quaternion.identity);
            Destroy(obj, 3.0f);
            StartCoroutine(MoveToFront());


        }
        else if (n == 4)
        {
            obj = Instantiate(dishPrefab[3], transform.position + offset, Quaternion.identity);
            Destroy(obj, 3.0f);
            StartCoroutine(MoveToFront());


        }
        else if (n == 5)
        {
            obj = Instantiate(dishPrefab[4], transform.position + offset, Quaternion.identity);
            Destroy(obj, 3.0f);
            StartCoroutine(MoveToFront());


        }
        else if (n == 6)
        {
            obj = Instantiate(dishPrefab[5], transform.position + offset, Quaternion.identity);
            Destroy(obj, 3.0f);
            StartCoroutine(MoveToFront());


        }
        else if (n == 7)
        {
            obj = Instantiate(dishPrefab[6], transform.position + offset, Quaternion.identity);
            Destroy(obj, 3.0f);
            StartCoroutine(MoveToFront());


        }
        else if (n == 8)
        {
            obj = Instantiate(dishPrefab[7], transform.position + offset, Quaternion.identity);
            Destroy(obj, 3.0f);
            StartCoroutine(MoveToFront());


        }



        //from here is food (12 food)
        else if (n == 10)
        {
            obj = Instantiate(foodPrefab[0], transform.position + offset, Quaternion.identity);
            Destroy(obj, 3.0f);
            StartCoroutine(MoveToFront());

        }
        else if (n == 11)
        {
            obj = Instantiate(foodPrefab[1], transform.position + offset, Quaternion.identity);
            Destroy(obj, 3.0f);
            StartCoroutine(MoveToFront());

        }
        else if (n == 12)
        {
            obj = Instantiate(foodPrefab[2], transform.position + offset, Quaternion.identity);
            Destroy(obj, 3.0f);
            StartCoroutine(MoveToFront());

        }
        else if (n == 13)
        {
            obj = Instantiate(foodPrefab[3], transform.position + offset, Quaternion.identity);
            Destroy(obj, 3.0f);
            StartCoroutine(MoveToFront());

        }
        else if (n == 14)
        {
            obj = Instantiate(foodPrefab[4], transform.position + offset, Quaternion.identity);
            Destroy(obj, 3.0f);
            StartCoroutine(MoveToFront());

        }
        else if (n == 15)
        {
            obj = Instantiate(foodPrefab[5], transform.position + offset, Quaternion.identity);
            Destroy(obj, 3.0f);
            StartCoroutine(MoveToFront());

        }
        else if (n == 16)
        {
            obj = Instantiate(foodPrefab[6], transform.position + offset, Quaternion.identity);
            Destroy(obj, 3.0f);
            StartCoroutine(MoveToFront());

        }
        else if (n == 17)
        {
            obj = Instantiate(foodPrefab[7], transform.position + offset, Quaternion.identity);
            Destroy(obj, 3.0f);
            StartCoroutine(MoveToFront());

        }
        else if (n == 18)
        {
            obj = Instantiate(foodPrefab[8], transform.position + offset, Quaternion.identity);
            Destroy(obj, 3.0f);
            StartCoroutine(MoveToFront());

        }
        else if (n == 19)
        {
            obj = Instantiate(foodPrefab[9], transform.position + offset, Quaternion.identity);
            Destroy(obj, 3.0f);
            StartCoroutine(MoveToFront());

        }
        else if (n == 20)
        {
            obj = Instantiate(foodPrefab[10], transform.position + offset, Quaternion.identity);
            Destroy(obj, 3.0f);
            StartCoroutine(MoveToFront());

        }
        else if (n == 21)
        {
            obj = Instantiate(foodPrefab[11], transform.position + offset, Quaternion.identity);
            Destroy(obj, 3.0f);
            StartCoroutine(MoveToFront());

        }
    }

    private IEnumerator MoveToFront()
    {
        yield return new WaitForSeconds(0.1f);
        playerInventory.itemList[0] = playerInventory.itemList[1];
        sl_p2InventoryManager.RefreshItem();

        yield return new WaitForSeconds(0.1f);
        playerInventory.itemList[1] = null;
        sl_p2InventoryManager.RefreshItem();
    }

}
