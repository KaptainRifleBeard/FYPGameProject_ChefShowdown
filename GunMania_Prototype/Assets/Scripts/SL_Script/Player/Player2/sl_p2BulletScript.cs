using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class sl_p2BulletScript : MonoBehaviour
{
    public float bulletDmg;
    PhotonView view;
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        //Destroy(gameObject, 5f);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Environment")
        {
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
