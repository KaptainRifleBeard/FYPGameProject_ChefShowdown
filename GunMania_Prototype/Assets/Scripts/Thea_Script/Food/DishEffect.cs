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


    private void Start()
    {
        canMove = true;
        canPick = true;
        playerRidg = gameObject.GetComponent<Rigidbody>();
        view = GetComponent<PhotonView>();
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
}
