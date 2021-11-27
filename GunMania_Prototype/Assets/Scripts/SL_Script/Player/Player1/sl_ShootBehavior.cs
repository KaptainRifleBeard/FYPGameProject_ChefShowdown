using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.AI;

public class sl_ShootBehavior : MonoBehaviour
{
    public GameObject bulletPrefab;
    public List<GameObject> dishBullet;
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

    public static bool p1Shoot = false;
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
            if (Input.GetMouseButtonDown(0) && p1Shoot == false && view.IsMine && bulletCount > 0)
            {
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
            minRange.SetActive(true);
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


        if (Input.GetMouseButtonUp(0) && bulletCount > 0 && p1Shoot == true)
        {
            //set in range then shoot
            if (Vector3.Distance(targetObject.transform.position, shootPosition.position) > 5 && 
                Vector3.Distance(targetObject.transform.position, shootPosition.position) < 40  
                && gameObject.tag == "Player")  //make sure bullet wont collide with player
            {
                minRange.SetActive(false);
                maxRange.SetActive(false);

                ShootBullet();
            }
            else
            {
                view.RPC("CancelShoot", RpcTarget.All);
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
        theFood.SetActive(true);
        if (playerInventory.itemList[0].itemHeldNum == 1)
        {
            bullet = Instantiate(dishBullet[0], shootPosition.position, Quaternion.identity); //explode
        }
        else if (playerInventory.itemList[0].itemHeldNum == 2)
        {
            bullet = Instantiate(dishBullet[1], shootPosition.position, Quaternion.identity); //knockback
        }
        else if (playerInventory.itemList[0].itemHeldNum == 3)
        {
            bullet = Instantiate(dishBullet[2], shootPosition.position, Quaternion.identity); //pull
        }
        else if (playerInventory.itemList[0].itemHeldNum == 4)
        {
            bullet = Instantiate(dishBullet[3], shootPosition.position, Quaternion.identity); //stun
        }
        else
        {
            bullet = Instantiate(bulletPrefab, shootPosition.position, Quaternion.identity);
        }

    bullet.SetActive(false);
        //bullet.transform.SetParent(shootPosition);
    }

    [PunRPC]
    public void CancelShoot()
    {
        theFood.SetActive(false);
        Destroy(targetObject);
        targetObject = targetIndicatorPrefab;

        p1Shoot = false;
        count = 0;
        spawn = false;
        minRange.SetActive(false);
        maxRange.SetActive(false);
    }

    public void ShootBullet()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            bullet.transform.SetParent(null);

            anim.SetBool("Aim", false);
            anim.SetBool("Throw", true);


            targetPosition = hit.point;
            directionShoot = targetPosition - shootPosition.position;

            view.RPC("BulletDirection", RpcTarget.All, directionShoot);
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
        sl_InventoryManager.RefreshItem();

        yield return new WaitForSeconds(0.1f);
        playerInventory.itemList[1] = null;
        sl_InventoryManager.RefreshItem();

        count = 0;
    }

    //public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if(stream.IsWriting)
    //    {
    //        stream.SendNext(directionShoot);
    //    }
    //    else
    //    {
    //        directionShoot = (Vector3)stream.ReceiveNext();
    //    }
    //}

}
