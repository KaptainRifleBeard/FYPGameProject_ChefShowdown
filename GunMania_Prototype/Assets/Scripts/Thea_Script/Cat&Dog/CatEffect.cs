using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CatEffect : MonoBehaviour
{
    public GameObject partSystem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
        {
            GetComponent<SphereCollider>().enabled = true;
            partSystem.SetActive(true);
        }
    }
}
