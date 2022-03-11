using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sl_MolotovDish : MonoBehaviour
{
    public GameObject areaDamage;
    public ParticleSystem particle;

    private void Start()
    {
        areaDamage.SetActive(false);
        particle.Play();
    }

    private void Update()
    {
        transform.rotation = Quaternion.identity; //reset range rotation always face up
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Environment")
        {
            if(particle.isPlaying)
            {
                particle.Stop();
            }
            areaDamage.SetActive(true);
            gameObject.GetComponent<BoxCollider>().isTrigger = false;

            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;

            Destroy(gameObject, 6.0f);
        }
    }
}
