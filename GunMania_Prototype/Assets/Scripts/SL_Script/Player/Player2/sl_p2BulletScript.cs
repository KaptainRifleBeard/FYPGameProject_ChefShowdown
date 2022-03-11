using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sl_p2BulletScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //gameObject.SetActive(false);  // note: cuz when collide with game object distance too close, it destroy immediately then my shoot behavior will have error
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Environment")
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = false;

            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;

            Destroy(gameObject, 1.0f);
        }
    }
}
