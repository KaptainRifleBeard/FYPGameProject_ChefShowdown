using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class sl_BulletScript : MonoBehaviour
{
    public ParticleSystem particle;

    private void Start()
    {
        particle.Play();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player2")
        {
            //gameObject.SetActive(false);  // note: cuz when collide with game object distance too close, it destroy immediately then my shoot behavior will have error
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Environment")
        {
            //gameObject.GetComponent<BoxCollider>().isTrigger = false;

            //gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //gameObject.GetComponent<Rigidbody>().isKinematic = true;

            Destroy(gameObject, 1.0f);

        }
    }

}
