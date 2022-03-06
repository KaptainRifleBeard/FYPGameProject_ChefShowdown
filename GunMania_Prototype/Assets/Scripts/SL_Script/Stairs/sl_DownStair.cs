using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sl_DownStair : MonoBehaviour
{
    public GameObject[] downStair;

    void Start()
    {
        for (int i = 0; i < downStair.Length; i++)
        {
            downStair[i].SetActive(false);
        }

    }


    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
        {
            StartCoroutine(ArrowTime());

        }
    }

    public void OnTriggerExit(Collider collision)
    {
        Debug.Log("Exit");
        StopAllCoroutines();
        for (int i = 0; i < downStair.Length; i++)
        {
            downStair[i].SetActive(false);
        }

    }

    IEnumerator ArrowTime()
    {
        while (true)
        {
            for (int i = 0; i < downStair.Length;)
            {
                downStair[i].SetActive(true);
                yield return new WaitForSeconds(0.1f);

                i++;
            }

            for (int i = 0; i < downStair.Length;)
            {
                downStair[i].SetActive(false);
                yield return new WaitForSeconds(0.1f);

                i++;
            }

        }
    }
}
