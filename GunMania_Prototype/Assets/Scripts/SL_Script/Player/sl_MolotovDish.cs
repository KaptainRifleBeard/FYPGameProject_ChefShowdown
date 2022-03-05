using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sl_MolotovDish : MonoBehaviour
{
    public GameObject areaDamage;
    Rigidbody rb;
    public bool isPlayer;

    private void Start()
    {
        areaDamage.SetActive(false);
    }

    private void Update()
    {
        transform.rotation = Quaternion.identity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPlayer)
        {
            if (other.gameObject.tag == "Environment" || other.gameObject.tag == "Player2")
            {
                areaDamage.SetActive(true);
                gameObject.GetComponent<BoxCollider>().isTrigger = false;
                GetComponent<SphereCollider>().enabled = true;
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                gameObject.GetComponent<Rigidbody>().isKinematic = true;

                Destroy(gameObject, 3.0f);
            }
        }
        else
        {
            if (other.gameObject.tag == "Environment" || other.gameObject.tag == "Player")
            {
                areaDamage.SetActive(true);
                gameObject.GetComponent<BoxCollider>().isTrigger = false;
                GetComponent<SphereCollider>().enabled = true;
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                gameObject.GetComponent<Rigidbody>().isKinematic = true;

                Destroy(gameObject, 3.0f);
            }
        }
    }
}
