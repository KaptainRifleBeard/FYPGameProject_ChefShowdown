using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sl_MolotovRange : MonoBehaviour
{
    public GameObject showVfx;

    private void Start()
    {
        showVfx.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player2")
        {
            showVfx.SetActive(true);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        showVfx.SetActive(false);

    }
}
