using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sl_TestActive : MonoBehaviour
{
    IEnumerator waitToSpawn()
    {
        yield return new WaitForSeconds(6f);
        gameObject.SetActive(true);
    }


    public void Update()
    {
        //if (isPicked == true || isPickedDish == true)
        //{
        //    StartCoroutine(waitToSpawn());
        //}
    }

}
