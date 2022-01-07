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
    public int stunTime;
    public int silenceTime;
    public static bool p2canMove;
    public static bool p2canPick;


    private void Start()
    {
        p2canMove = true;
        p2canPick = true;
        playerRidg = gameObject.GetComponent<Rigidbody>();
        view = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Sinseollo")
        {
            view.RPC("Explode", RpcTarget.All);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Tojangjochi")
        {
            //stun
            view.RPC("Stun", RpcTarget.All, stunTime);

            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "P2Hassun")
        {
            //sl_PlayerHealth.currentHealth += 3;
            //if (sl_PlayerHealth.currentHealth >= 8)
            //{
            //    sl_PlayerHealth.currentHealth = 8;
            //}
        }
        else if (other.gameObject.tag == "Mukozuke")
        {
            //pull
            Vector3 direction = (other.transform.position - transform.position).normalized;
            direction.y = 0;

            view.RPC("Pull", RpcTarget.All, direction);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "BirdNestSoup")
        {
            //aoe
        }
        else if (other.gameObject.tag == "BuddhaJumpsOvertheWall")
        {
            //silence
            view.RPC("Silence", RpcTarget.All, silenceTime);

            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "FoxtailMillet")
        {
            //knock
            Vector3 direction = (transform.position - other.transform.position).normalized;
            direction.y = 0;

            view.RPC("Push", RpcTarget.All, direction);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "RawStinkyTofu")
        {
            //drop food :')
        }
    }

    public IEnumerator StunDeactive(int time)
    {
        yield return new WaitForSeconds(time);

        p2canMove = true;
    }

    public IEnumerator SilenceDeactive(int time)
    {
        yield return new WaitForSeconds(time);
        p2canPick = true;
    }

    [PunRPC]
    public void Pull(Vector3 dir)
    {
        //sl_P2PlayerHealth.p2currentHealth -= 1;
        
        playerRidg.AddForce(dir * pullingSpeed, ForceMode.Impulse);
    }

    [PunRPC]
    public void Push(Vector3 dir)
    {
        //sl_P2PlayerHealth.p2currentHealth -= 1;

        playerRidg.AddForce(dir * knockbackSpeed, ForceMode.Impulse);
    }

    [PunRPC]
    public void Stun()
    {
        p2canMove = false;
        StartCoroutine(StunDeactive(6));
    }

    [PunRPC]
    public void Explode()
    {
        //sl_P2PlayerHealth.p2currentHealth -= 1.5f;
    }

    [PunRPC]
    public void Silence()
    {
        //sl_P2PlayerHealth.p2currentHealth -= 1;
        p2canPick = false;
        StartCoroutine(SilenceDeactive(4));
    }
}
