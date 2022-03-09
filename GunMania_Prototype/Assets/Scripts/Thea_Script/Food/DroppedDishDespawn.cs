using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DroppedDishDespawn : MonoBehaviour
{


    private IEnumerator coroutine;
    private int secs = 3;
    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        view.RPC("DishDestroy", RpcTarget.All);
    }

    private IEnumerator Despawn(int secs)
    {
        yield return new WaitForSeconds(secs);
        
    }

    [PunRPC]
    public void DishDestroy()
    {
        Destroy(gameObject);
    }
}
