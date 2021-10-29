using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sl_ActiveController : MonoBehaviour
{
    private static sl_ActiveController _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    public static void DelayedStart(GameObject target, float time)
    {
        _instance.StartCoroutine(DelayedStartCoroutine(target, time));
    }


    private static IEnumerator DelayedStartCoroutine(GameObject target, float time)
    {
        if (target.activeInHierarchy) target.SetActive(false);

        yield return new WaitForSeconds(time);

        target.SetActive(true);

        //Do Function here...
    }
}
