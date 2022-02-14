using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MolotovDish : MonoBehaviour
{
    public bool isPlayer;
    private bool isHit;

    public GameObject indicator;

    private void OnCollisionEnter(Collision collision)
    {
        if(isPlayer)
        {
            if (collision.gameObject.tag == "Environment" || collision.gameObject.tag == "Player2")
            {
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<SphereCollider>().enabled = true;
                indicator.SetActive(true);
                isHit = true;
            }
        }
        else if(!isPlayer)
        {
            if (collision.gameObject.tag == "Environment" || collision.gameObject.tag == "Player")
            {
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<SphereCollider>().enabled = true;
                indicator.SetActive(true);
                isHit = true;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isHit)
        {
            StartCoroutine(DestroySelf());
        }
    }

    public IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(3);

        Destroy(gameObject);
    }
}
