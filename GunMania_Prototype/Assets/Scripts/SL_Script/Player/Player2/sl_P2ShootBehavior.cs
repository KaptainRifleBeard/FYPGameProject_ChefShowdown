using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class sl_P2ShootBehavior : MonoBehaviour
{
    public Rigidbody bulletPrefab;

    public Transform shootPosition;
    public static int p2bulletCount;

    public sl_Inventory playerInventory;  //set which inventory should be place in

    //for target indicator
    public GameObject targetIndicatorPrefab;
    GameObject targetObject;

    PhotonView view;
    Rigidbody bullet;

    Vector3 directionShoot2;
    Vector3 targetPosition2;

    //for check shoot once
    int count;
    bool spawn;

    public static bool p2Shoot;

    public Animator anim;

    void Start()
    {
        view = GetComponent<PhotonView>();
        targetObject = targetIndicatorPrefab;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0) && p2Shoot == false && p2bulletCount > 0  && !PhotonNetwork.IsMasterClient && playerInventory.itemList[0] != null)
            {
                p2Shoot = true;  //stop movement when shoot
                anim.SetBool("Aim2", true);

                //make sure only spawn 1
                if (count < 1 && spawn == false)
                {
                    spawn = true;
                    view.RPC("SpawnBullet2", RpcTarget.All);

                    targetObject = Instantiate(targetIndicatorPrefab, Vector3.zero, Quaternion.identity);
                    count++;

                }
                if (count == 1)
                {
                    spawn = false;
                }


            }
        }

        if (Input.GetMouseButton(0) && view.IsMine && p2Shoot == true) //indicator follow mouse
        {
            targetObject.transform.position = hit.point;
        }

        if (Input.GetMouseButtonUp(0) && p2bulletCount > 0 && p2Shoot == true && !PhotonNetwork.IsMasterClient)
        {
            if (Vector3.Distance(targetObject.transform.position, shootPosition.position) > 5)  //make sure bullet wont collide with player
            {
                ShootBullet2();
            }
        }

    }


    [PunRPC]
    public void SpawnBullet2()
    {
        bullet = Instantiate(bulletPrefab, shootPosition.position, Quaternion.identity);
        bullet.transform.SetParent(shootPosition);
    }

    public void ShootBullet2()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            //Animation
            anim.SetBool("Aim2", false);
            anim.SetBool("Throw2", true);

            targetPosition2 = hit.point;
            directionShoot2 = targetPosition2 - shootPosition.position;

            view.RPC("BulletDirection2", RpcTarget.All, directionShoot2);

            bullet.transform.SetParent(null);
            p2bulletCount--;

            StartCoroutine(stopAnim());
            Destroy(targetObject);

            //for item list ui
            playerInventory.itemList[0] = null;
            sl_InventoryManager.RefreshItem();
            StartCoroutine(MoveToFront());

            //reset
            targetObject = targetIndicatorPrefab;
            count = 0;
            spawn = false;
            p2Shoot = false;

        }
    }

    [PunRPC]
    public void BulletDirection2(Vector3 dir)
    {
        float shootForce = 50.0f;

        bullet.transform.forward = dir.normalized;
        bullet.GetComponent<Rigidbody>().AddForce(dir.normalized * shootForce, ForceMode.Impulse); //shootforce

    }


    private IEnumerator MoveToFront()
    {
        yield return new WaitForSeconds(0.1f);
        playerInventory.itemList[0] = playerInventory.itemList[1];
        sl_p2InventoryManager.RefreshItem();

        yield return new WaitForSeconds(0.1f);
        playerInventory.itemList[1] = null;
        sl_p2InventoryManager.RefreshItem();

        count = 0;
    }

    IEnumerator stopAnim()
    {
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("Throw2", false);
    }

    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(directionShoot2);
        }
        else
        {
            directionShoot2 = (Vector3)stream.ReceiveNext();
        }
    }
}
