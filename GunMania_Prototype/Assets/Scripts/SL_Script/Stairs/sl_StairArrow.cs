using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sl_StairArrow : MonoBehaviour
{
    public GameObject[] upStairs;

    void Start()
    {
        for (int i = 0; i < upStairs.Length; i++)
        {
            upStairs[i].SetActive(false);
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
        StopAllCoroutines();
        for (int i = 0; i < upStairs.Length; i++)
        {
            upStairs[i].SetActive(false);
        }

    }

    IEnumerator ArrowTime()
    {
        while (true)
        {
            for (int i = 0; i < upStairs.Length;)
            {
                upStairs[i].SetActive(true);
                yield return new WaitForSeconds(0.1f);

                i++;
            }

            for (int i = 0; i < upStairs.Length;)
            {
                upStairs[i].SetActive(false);
                yield return new WaitForSeconds(0.1f);

                i++;
            }

        }
    }
}
