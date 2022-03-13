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

    private void Update()
    {
        transform.rotation = Quaternion.identity;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player2")
        {
            //gameObject.SetActive(false);  // note: cuz when collide with game object distance too close, it destroy immediately then my shoot behavior will have error
            //onhit.SetActive(true);
            if (gameObject.tag != "BuddhaJumpsOvertheWall")
            {
                Destroy(gameObject);

            }
        }

        if (other.gameObject.tag == "Environment")
        {
            //gameObject.GetComponent<BoxCollider>().isTrigger = false;

            //gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //gameObject.GetComponent<Rigidbody>().isKinematic = true;
            //onhit.SetActive(true);
            Destroy(gameObject, 1.0f);

        }
    }

}
