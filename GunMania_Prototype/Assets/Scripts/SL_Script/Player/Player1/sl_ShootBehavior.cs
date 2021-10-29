using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.AI;

public class sl_ShootBehavior : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPosition;

    public static int bulletCount;
    public sl_Inventory playerInventory;  //set which inventory should be place in

    //for target indicator
    public GameObject targetIndicatorPrefab;
    GameObject targetObject;

    PhotonView view;
    GameObject bullet;
    Vector3 directionShoot;
    Vector3 targetPosition;
    public Animator anim;

    int count;
    bool spawn;

    int num;
    bool spawnbullet;

    public static bool p1Shoot = false;

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
            if (Input.GetMouseButtonDown(0) && p1Shoot == false && view.IsMine  /*&& bulletCount > 0 && playerInventory.itemList[0] != null*/)
            {
                Debug.Log("bulletspawn");

                p1Shoot = true;  //stop movement when shoot
                anim.SetBool("Aim", true);

                //make sure only spawn 1
                if (count < 1 && spawn == false)
                {
                    spawn = true;

                    view.RPC("SpawnBullet", RpcTarget.All);
                    targetObject = Instantiate(targetIndicatorPrefab, Vector3.zero, Quaternion.identity);

                    count++;

                }
                if (count == 1)
                {
                    spawn = false;
                }


            }
        }

        if (Input.GetMouseButton(0) && view.IsMine && p1Shoot == true) //indicator follow mouse
        {
            targetObject.transform.position = hit.point;
        }


        if (Input.GetMouseButtonUp(0) && /*bulletCount > 0 &&*/ p1Shoot == true && PhotonNetwork.IsMasterClient)
        {
            if (Vector3.Distance(targetObject.transform.position, shootPosition.position) > 5)  //make sure bullet wont collide with player
            {
                ShootBullet();
            }

        }

    }

    IEnumerator stopAnim()
    {
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("Throw", false);

    }


    [PunRPC]
    public void SpawnBullet()
    {
        bullet = Instantiate(bulletPrefab, shootPosition.position, Quaternion.identity);
        bullet.transform.SetParent(shootPosition);

    }

    public void ShootBullet()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            anim.SetBool("Aim", false);
            anim.SetBool("Throw", true);

            targetPosition = hit.point;
            directionShoot = targetPosition - shootPosition.position;

            view.RPC("BulletDirection", RpcTarget.All, directionShoot);


            bullet.transform.SetParent(null);
            bulletCount--;

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
            p1Shoot = false;

        }


    }

    [PunRPC]
    public void BulletDirection(Vector3 dir)
    {
        float shootForce = 50.0f;

        bullet.transform.forward = dir.normalized;
        bullet.GetComponent<Rigidbody>().AddForce(dir.normalized * shootForce, ForceMode.Impulse); //shootforce

    }

    private IEnumerator MoveToFront()
    {
        yield return new WaitForSeconds(0.1f);
        playerInventory.itemList[0] = playerInventory.itemList[1];
        sl_InventoryManager.RefreshItem();

        yield return new WaitForSeconds(0.1f);
        playerInventory.itemList[1] = null;
        sl_InventoryManager.RefreshItem();

        count = 0;
    }

    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(directionShoot);
        }
        else
        {
            directionShoot = (Vector3)stream.ReceiveNext();
        }
    }

}
