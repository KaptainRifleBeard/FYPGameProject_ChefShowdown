using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class sl_P2ShootBehavior : MonoBehaviour
{
    public GameObject bulletPrefab;

    public Transform shootPosition;
    public static int p2bulletCount;

    public sl_Inventory playerInventory;  //set which inventory should be place in

    //for target indicator
    public GameObject targetIndicatorPrefab;
    GameObject targetObject;

    PhotonView view;
    GameObject bullet;

    Vector3 directionShoot2;
    Vector3 targetPosition2;

    //for check shoot once
    int count;
    bool spawn;

    public static bool p2Shoot;

    public Animator anim;
    public GameObject theFood;

    [Header("Range Indicator")]
    public GameObject maxRange;
    public GameObject minRange;

    void Start()
    {
        view = GetComponent<PhotonView>();
        targetObject = targetIndicatorPrefab;
        theFood.SetActive(false);

        maxRange.SetActive(false);
        minRange.SetActive(false);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0) && p2Shoot == false && view.IsMine && p2bulletCount > 0)
            {
                p2Shoot = true;  //stop movement when shoot
                anim.SetBool("Aim", true);

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

            if (Vector3.Distance(targetObject.transform.position, shootPosition.position) > 40)
            {
                Debug.Log("out of range");
                maxRange.SetActive(true);
                minRange.SetActive(false);
            }
            else
            {
                maxRange.SetActive(false);
                minRange.SetActive(true);
            }
        }

        if (Input.GetMouseButtonUp(0) && p2bulletCount > 0 && p2Shoot == true)
        {
            //set in range then shoot
            if (Vector3.Distance(targetObject.transform.position, shootPosition.position) > 5 &&
                Vector3.Distance(targetObject.transform.position, shootPosition.position) < 40
                && gameObject.tag == "Player2")  //make sure bullet wont collide with player
            {
                minRange.SetActive(false);
                maxRange.SetActive(false);

                ShootBullet2();
            }
            else
            {
                view.RPC("CancelShoot2", RpcTarget.All);
            }
        }
    }


    [PunRPC]
    public void SpawnBullet2()
    {
        theFood.SetActive(true);
        bullet = Instantiate(bulletPrefab, shootPosition.position, Quaternion.identity);
        bullet.SetActive(false);

        //bullet.transform.SetParent(shootPosition);
    }

    [PunRPC]
    public void CancelShoot2()
    {
        theFood.SetActive(false);
        Destroy(targetObject);
        targetObject = targetIndicatorPrefab;

        p2Shoot = false;
        count = 0;
        spawn = false;
        minRange.SetActive(false);
        maxRange.SetActive(false);
    }

    public void ShootBullet2()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {            
            bullet.transform.SetParent(null);

            //Animation
            anim.SetBool("Aim", false);
            anim.SetBool("Throw", true);

            targetPosition2 = hit.point;
            directionShoot2 = targetPosition2 - shootPosition.position;

            view.RPC("BulletDirection2", RpcTarget.All, directionShoot2);

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
        theFood.SetActive(false);
        bullet.SetActive(true);

        float shootForce = 30.0f;
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
        anim.SetBool("Throw", false);
    }

    //public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(directionShoot2);
    //    }
    //    else
    //    {
    //        directionShoot2 = (Vector3)stream.ReceiveNext();
    //    }
    //}
}
