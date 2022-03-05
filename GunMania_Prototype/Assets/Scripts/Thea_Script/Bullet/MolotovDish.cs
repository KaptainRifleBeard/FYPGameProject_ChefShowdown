using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MolotovDish : MonoBehaviour
{
    public bool isPlayer;
    public GameObject indicator;

    private void OnTriggerEnter(Collider collision)
    {
        //if (isPlayer)
        //{
        //    if (collision.gameObject.tag == "Environment" || collision.gameObject.tag == "Player2")
        //    {
        //        GetComponent<MeshRenderer>().enabled = false;
        //        GetComponent<SphereCollider>().enabled = true;
        //        indicator.SetActive(true);

        //        //indicator.SetActive(true);
        //        //Destroy(gameObject, 3f);
        //    }
        //}
        //else if (!isPlayer)
        //{
        //    if (collision.gameObject.tag == "Environment" || collision.gameObject.tag == "Player")
        //    {
        //        GetComponent<MeshRenderer>().enabled = false;
        //        GetComponent<SphereCollider>().enabled = true;
        //        indicator.SetActive(true);

        //        //indicator.SetActive(true);
        //        //Destroy(gameObject, 3f);
        //    }
        //}

        if (collision.gameObject.tag == "Environment")
        {
            Debug.Log("collide");
            gameObject.GetComponent<BoxCollider>().isTrigger = false;

            //gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;

            //indicator.SetActive(true);
            //Destroy(gameObject, 3f);
        }
    }

}
