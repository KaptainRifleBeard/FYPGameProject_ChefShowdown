using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class sl_BulletScript : MonoBehaviour
{
    PhotonView view;

    void Start()
    {
        view = GetComponent<PhotonView>();
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player2" || other.gameObject.tag == "Environment")
        {
            gameObject.SetActive(false);  // note: cuz when collide with game object distance too close, it destroy immediately then my shoot behavior will have error
            Destroy(gameObject);
            //if (gameObject.CompareTag("Bullet"))
            //{
            //    view.RPC("DestroyObject", RpcTarget.All);
            //}
        }
    }

    [PunRPC]
    public void DestroyObject()
    {
        gameObject.SetActive(false);  // note: cuz when collide with game object distance too close, it destroy immediately then my shoot behavior will have error
        Destroy(gameObject);
    }
}
