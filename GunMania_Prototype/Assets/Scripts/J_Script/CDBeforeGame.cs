using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CDBeforeGame : MonoBehaviour
{
    public int CDTime;
    public Text CDDisplay;

    private void Start()
    {
        StartCoroutine(CDToStart());
    }

    IEnumerator CDToStart()
    {
        while(CDTime > 0)
        {
            CDDisplay.text = CDTime.ToString();
            yield return new WaitForSeconds(1f);
            CDTime--;
        }

        CDDisplay.text = "START";
        yield return new WaitForSeconds(1f);
        CDDisplay.gameObject.SetActive(false);
    }
}