using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishDespawn : MonoBehaviour
{
    public static bool canSpawn;

    [Header("Check which dish spawn point is this")]
    public bool JPpoint;
    public bool KRpoint;
    public bool CNpoint;
    public bool TWpoint;

    public static bool isJP;
    public static bool isKR;
    public static bool isCN;
    public static bool isTW;

    private IEnumerator coroutine;
    private int secs = 10;

    // Start is called before the first frame update
    void Start()
    {
        canSpawn = false;
        isJP = false;
        isKR = false;
        isCN = false;
        isTW = false;
    }

    // Update is called once per frame
    void Update()
    {
        coroutine = Despawn(secs);
        StartCoroutine(coroutine);
    }

    private IEnumerator Despawn(int secs)
    {
        yield return new WaitForSeconds(secs);
        Destroy(gameObject);

        if (JPpoint)
        {
            isJP = true;
        }
        else if (KRpoint)
        {
            isKR = true;
        }
        else if (CNpoint)
        {
            isCN = true;
        }
        else if (TWpoint)
        {
            isTW = true;
        }
    }

}
