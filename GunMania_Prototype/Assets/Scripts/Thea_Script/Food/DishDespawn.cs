using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DishDespawn : MonoBehaviour
{
    public static bool canSpawn;


    private IEnumerator coroutine;
    private int secs = 10;
    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
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
        view.RPC("DishDestroy", RpcTarget.All);
    }

    [PunRPC]
    public void DishDestroy()
    {
        Destroy(gameObject);
        canSpawn = true;
    }

}
