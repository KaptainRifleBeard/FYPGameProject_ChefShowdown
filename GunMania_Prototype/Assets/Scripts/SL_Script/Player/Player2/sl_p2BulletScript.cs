using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class sl_p2BulletScript : MonoBehaviour
{
    PhotonView view;

    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Environment")
        {
            gameObject.SetActive(false);  // note: cuz when collide with game object distance too close, it destroy immediately then my shoot behavior will have error
            Destroy(gameObject);

            //if (gameObject.CompareTag("P2Bullet"))
            //{
            //    view.RPC("DestroyObject2", RpcTarget.All);
            //}
        }
    }

    [PunRPC]
    public void DestroyObject2()
    {
        gameObject.SetActive(false);  // note: cuz when collide with game object distance too close, it destroy immediately then my shoot behavior will have error
        Destroy(gameObject);
    }
}
