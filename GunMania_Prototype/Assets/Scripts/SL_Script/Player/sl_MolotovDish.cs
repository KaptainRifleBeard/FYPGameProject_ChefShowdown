using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sl_MolotovDish : MonoBehaviour
{
    public GameObject areaDamage;
    Rigidbody rb;
    
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
        if (other.gameObject.tag == "Environment")
        {
            areaDamage.SetActive(true);
            gameObject.GetComponent<BoxCollider>().isTrigger = false;

            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;

            Destroy(gameObject, 10.0f);
        }
    }
}
