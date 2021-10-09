using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishDespawn : MonoBehaviour
{
    public static bool canSpawn;

    private IEnumerator coroutine;
    public int secs = 5;

    // Start is called before the first frame update
    void Start()
    {
        canSpawn = false;
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
        canSpawn = true;
    }
}
