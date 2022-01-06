using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DishEffect : MonoBehaviour
{
    PhotonView view;

    Rigidbody playerRidg;
    //both speed 100
    //rigidbody drag 2
    public float knockbackSpeed;
    public float pullingSpeed;
    public int stunTime;
    public int silenceTime;
    public static bool canMove;
    public static bool canPick;

    public sl_Inventory playerInventory;

    public List<GameObject> foodPrefab;

    Vector3 offset;


    private void Start()
    {
        canMove = true;
        canPick = true;
        playerRidg = gameObject.GetComponent<Rigidbody>();
        view = GetComponent<PhotonView>();

        offset = new Vector3(0, 0, 5);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "P2Sinseollo")
        {
            view.RPC("Explode", RpcTarget.All);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "P2Tojangjochi")
        {
            //stun
            view.RPC("Stun", RpcTarget.All);

            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Hassun")
        {
            //sl_PlayerHealth.currentHealth += 3;
            //if(sl_PlayerHealth.currentHealth >= 8)
            //{
            //    sl_PlayerHealth.currentHealth = 8;
            //}
            
        }
        else if (other.gameObject.tag == "P2Mukozuke")
        {
            //pull
            Vector3 direction = (other.transform.position - transform.position).normalized;
            direction.y = 0;

            view.RPC("Pull", RpcTarget.All, direction);

            Destroy(other.gameObject); 
        }
        else if (other.gameObject.tag == "P2BirdNestSoup")
        {
            //aoe
            view.RPC("aoe", RpcTarget.All);

            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "P2BuddhaJumpsOvertheWall")
        {
            //silence
            view.RPC("Silence", RpcTarget.All);

            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "P2FoxtailMillet")
        {
            //knock
            Vector3 direction = (transform.position - other.transform.position).normalized;
            direction.y = 0;

            view.RPC("Push", RpcTarget.All, direction);

            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "P2RawStinkyTofu")
        {
            //drop food :')
            view.RPC("DropFood", RpcTarget.All);

            sl_ShootBehavior.bulletCount--;
            playerInventory.itemList[0] = null;
            sl_InventoryManager.RefreshItem();

            Destroy(other.gameObject);
        }
    }

    public IEnumerator StunDeactive(int time)
    {
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    public IEnumerator SilenceDeactive(int time)
    {
        yield return new WaitForSeconds(time);
        canPick = true;
    }

    public IEnumerator DMGoverTime(int time)
    {
        yield return new WaitForSeconds(time);
        //sl_PlayerHealth.currentHealth -= 1.5f;
        yield return new WaitForSeconds(time);
        //sl_PlayerHealth.currentHealth -= 1.5f;
        yield return new WaitForSeconds(time);
        //sl_PlayerHealth.currentHealth -= 1.5f;
    }

    [PunRPC]
    public void Pull(Vector3 dir)
    {
        //sl_PlayerHealth.currentHealth -= 1;
        
        playerRidg.AddForce(dir * pullingSpeed, ForceMode.Impulse);
    }

    [PunRPC]
    public void Push(Vector3 dir)
    {
        //sl_PlayerHealth.currentHealth -= 1;
        
        playerRidg.AddForce(dir * knockbackSpeed, ForceMode.Impulse);
    }

    [PunRPC]
    public void Stun()
    {
        canMove = false;
        StartCoroutine(StunDeactive(6));
    }

    [PunRPC]
    public void Explode()
    {
        //sl_PlayerHealth.currentHealth -= 1.5f;
    }

    [PunRPC]
    public void Silence()
    {
        //sl_PlayerHealth.currentHealth -= 1;
        canPick = false;
        StartCoroutine(SilenceDeactive(6));
    }

    [PunRPC]
    public void aoe()
    {
        //sl_PlayerHealth.currentHealth -= 1;
        StartCoroutine(DMGoverTime(1));
    }

    [PunRPC]
    public void DropFood()
    {
        if (playerInventory.itemList[0] != null)
        {
            if (playerInventory.itemList[0].itemHeldNum == 1)
            {
                Instantiate(foodPrefab[0], transform.position + offset, Quaternion.identity);
            }
            else if (playerInventory.itemList[0].itemHeldNum == 2)
            {
                Instantiate(foodPrefab[1], transform.position + offset, Quaternion.identity);
            }
            else if (playerInventory.itemList[0].itemHeldNum == 3)
            {
                Instantiate(foodPrefab[2], transform.position + offset, Quaternion.identity);
            }
            else if (playerInventory.itemList[0].itemHeldNum == 4)
            {
                Instantiate(foodPrefab[3], transform.position + offset, Quaternion.identity);
            }
            //from here is food (12 food)
            else if (playerInventory.itemList[0].itemHeldNum == 10)
            {
                Instantiate(foodPrefab[4], transform.position + offset, Quaternion.identity);
            }
            else if (playerInventory.itemList[0].itemHeldNum == 11)
            {
                Instantiate(foodPrefab[5], transform.position + offset, Quaternion.identity);
            }
            else if (playerInventory.itemList[0].itemHeldNum == 12)
            {
                Instantiate(foodPrefab[6], transform.position + offset, Quaternion.identity);
            }
            else if (playerInventory.itemList[0].itemHeldNum == 13)
            {
                Instantiate(foodPrefab[7], transform.position + offset, Quaternion.identity);
            }
            else if (playerInventory.itemList[0].itemHeldNum == 14)
            {
                Instantiate(foodPrefab[8], transform.position + offset, Quaternion.identity);
            }
            else if (playerInventory.itemList[0].itemHeldNum == 15)
            {
                Instantiate(foodPrefab[9], transform.position + offset, Quaternion.identity);
            }
            else if (playerInventory.itemList[0].itemHeldNum == 16)
            {
                Instantiate(foodPrefab[10], transform.position + offset, Quaternion.identity);
            }
            else if (playerInventory.itemList[0].itemHeldNum == 17)
            {
                Instantiate(foodPrefab[0], transform.position + offset, Quaternion.identity);
            }
            else if (playerInventory.itemList[0].itemHeldNum == 18)
            {
                Instantiate(foodPrefab[11], transform.position + offset, Quaternion.identity);
            }
            else if (playerInventory.itemList[0].itemHeldNum == 19)
            {
                Instantiate(foodPrefab[12], transform.position + offset, Quaternion.identity);
            }
            else if (playerInventory.itemList[0].itemHeldNum == 20)
            {
                Instantiate(foodPrefab[13], transform.position + offset, Quaternion.identity);
            }
            else if (playerInventory.itemList[0].itemHeldNum == 21)
            {
                Instantiate(foodPrefab[14], transform.position + offset, Quaternion.identity);
            }

        }
    }
}
