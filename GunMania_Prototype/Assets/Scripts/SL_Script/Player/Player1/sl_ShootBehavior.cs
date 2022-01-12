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

    public sl_Inventory playerInventory;  //set which inventory should be place in

    public static int bulletCount;
    public static bool p1Shoot = false;


    //for target indicator
    public GameObject targetIndicatorPrefab;
    GameObject targetObject;

    PhotonView view;
    GameObject bullet;

    Vector3 directionShoot;
    Vector3 targetPosition;

    int bulletNum;
    //for check shoot once
    int count;
    bool spawn;


    [Header("Range Indicator")]
    public GameObject maxRange;
    public GameObject minRange;


    [Header("Bullet List")]
    public List<GameObject> dishBullet;
    public List<GameObject> foodBullet;

    public GameObject[] theFoodToShow;



    void Start()
    {
        view = GetComponent<PhotonView>();
        targetObject = targetIndicatorPrefab;

        maxRange.SetActive(false);
        minRange.SetActive(false);

    }

    void Update()
    {
        //define bullet types
        #region
        if (playerInventory.itemList[0] != null)
        {
            if (playerInventory.itemList[0].itemHeldNum == 1)
            {
                bulletNum = 1;
            }
            else if (playerInventory.itemList[0].itemHeldNum == 2)
            {
                bulletNum = 2;
            }
            else if (playerInventory.itemList[0].itemHeldNum == 3)
            {
                bulletNum = 3;
            }
            else if (playerInventory.itemList[0].itemHeldNum == 4)
            {
                bulletNum = 4;
            }
            //from here is food (12 food)
            else if (playerInventory.itemList[0].itemHeldNum == 10)  
            {
                bulletNum = 10;
            }
            else if (playerInventory.itemList[0].itemHeldNum == 11)  
            {
                bulletNum = 11;
            }
            else if (playerInventory.itemList[0].itemHeldNum == 12) 
            {
                bulletNum = 12;
            }
            else if (playerInventory.itemList[0].itemHeldNum == 13) 
            {
                bulletNum = 13;
            }
            else if (playerInventory.itemList[0].itemHeldNum == 14)  
            {
                bulletNum = 14;
            }
            else if (playerInventory.itemList[0].itemHeldNum == 15)
            {
                bulletNum = 15;
            }
            else if (playerInventory.itemList[0].itemHeldNum == 16)
            {
                bulletNum = 16;
            }
            else if (playerInventory.itemList[0].itemHeldNum == 17)
            {
                bulletNum = 17;
            }
            else if (playerInventory.itemList[0].itemHeldNum == 18)
            {
                bulletNum = 18;
            }
            else if (playerInventory.itemList[0].itemHeldNum == 19)
            {
                bulletNum = 19;
            }
            else if (playerInventory.itemList[0].itemHeldNum == 20)
            {
                bulletNum = 20;
            }
            else if (playerInventory.itemList[0].itemHeldNum == 21)
            {
                bulletNum = 21;
            }

        }
        #endregion

        //MAIN
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0) && p1Shoot == false && view.IsMine && bulletCount > 0)
            {
                p1Shoot = true;  //stop movement when shoot

                //make sure only spawn 1
                if (count < 1 && spawn == false)
                {
                    spawn = true;

                    view.RPC("BulletType", RpcTarget.All, bulletNum);
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

            //****original shoot range = 40
            if(SL_newP1Movement.changeModelAnim == 0) //brock less 2 range
            {
                if (Vector3.Distance(targetObject.transform.position, shootPosition.position) > 35)
                {
                    maxRange.SetActive(true);
                    minRange.SetActive(false);
                }
                else
                {
                    maxRange.SetActive(false);
                    minRange.SetActive(true);
                }
            }
            if (SL_newP1Movement.changeModelAnim == 2) //jiho increase 2 range
            {
                if (Vector3.Distance(targetObject.transform.position, shootPosition.position) > 45)
                {
                    maxRange.SetActive(true);
                    minRange.SetActive(false);
                }
                else
                {
                    maxRange.SetActive(false);
                    minRange.SetActive(true);
                }
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

    [PunRPC]
    public void BulletType(int i)
    {
        bulletNum = i;

        //dish
        #region
        if (i == 1)
        {
            theFoodToShow[0].SetActive(true);
            bullet = Instantiate(dishBullet[0], shootPosition.position, Quaternion.identity); //explode
            bullet.SetActive(false);
        }

        if (i == 2)
        {
            theFoodToShow[1].SetActive(true);
            bullet = Instantiate(dishBullet[1], shootPosition.position, Quaternion.identity); //knockback
            bullet.SetActive(false);
        }
        if (i == 3)
        {
            theFoodToShow[2].SetActive(true);

            bullet = Instantiate(dishBullet[2], shootPosition.position, Quaternion.identity); //pull
            bullet.SetActive(false);

        }
        if (i == 4)
        {
            theFoodToShow[3].SetActive(true);

            bullet = Instantiate(dishBullet[3], shootPosition.position, Quaternion.identity); //stun
            bullet.SetActive(false);

        }
        #endregion


        //food - start from 10 - 21 
        #region
        if (i == 10) //c_niangao
        {
            theFoodToShow[4].SetActive(true);

            bullet = Instantiate(foodBullet[0], shootPosition.position, Quaternion.identity);
            bullet.SetActive(false);


        }
        if (i == 11)//c_spring roll
        {
            theFoodToShow[5].SetActive(true);

            bullet = Instantiate(foodBullet[1], shootPosition.position, Quaternion.identity);
            bullet.SetActive(false);

        }
        if (i == 12)//c_wonton
        {
            theFoodToShow[6].SetActive(true);

            bullet = Instantiate(foodBullet[2], shootPosition.position, Quaternion.identity);
            bullet.SetActive(false);

        }
        if (i == 13)//j_ichigo
        {
            theFoodToShow[7].SetActive(true);

            bullet = Instantiate(foodBullet[3], shootPosition.position, Quaternion.identity);
            bullet.SetActive(false);

        }
        if (i == 14)//j_ikanagi
        {
            theFoodToShow[8].SetActive(true);

            bullet = Instantiate(foodBullet[4], shootPosition.position, Quaternion.identity);
            bullet.SetActive(false);

        }
        if (i == 15)//j_sakura
        {
            theFoodToShow[9].SetActive(true);

            bullet = Instantiate(foodBullet[5], shootPosition.position, Quaternion.identity);
            bullet.SetActive(false);

        }
        if (i == 16)//k_bap burger
        {
            theFoodToShow[10].SetActive(true);

            bullet = Instantiate(foodBullet[6], shootPosition.position, Quaternion.identity);
            bullet.SetActive(false);

        }
        if (i == 17)//k_japchae
        {
            theFoodToShow[11].SetActive(true);

            bullet = Instantiate(foodBullet[7], shootPosition.position, Quaternion.identity);
            bullet.SetActive(false);

        }
        if (i == 18)//k_tteobokki
        {
            theFoodToShow[12].SetActive(true);

            bullet = Instantiate(foodBullet[8], shootPosition.position, Quaternion.identity);
            bullet.SetActive(false);

        }
        if (i == 19)//t_bubbletea
        {
            theFoodToShow[13].SetActive(true);

            bullet = Instantiate(foodBullet[9], shootPosition.position, Quaternion.identity);
            bullet.SetActive(false);

        }
        if (i == 20)//t_pineapple
        {
            theFoodToShow[14].SetActive(true);

            bullet = Instantiate(foodBullet[10], shootPosition.position, Quaternion.identity);
            bullet.SetActive(false);

        }
        if (i == 21)//t_taro
        {
            theFoodToShow[15].SetActive(true);

            bullet = Instantiate(foodBullet[11], shootPosition.position, Quaternion.identity);
            bullet.SetActive(false);

        }
        #endregion
    }


    [PunRPC]
    public void CancelShoot()
    {
        for(int i = 0; i < theFoodToShow.Length; i++)
        {
            theFoodToShow[i].SetActive(false);
        }


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

            targetPosition = hit.point;
            directionShoot = targetPosition - shootPosition.position;

            view.RPC("BulletDirection", RpcTarget.All, directionShoot);
            bulletCount--;

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
        for (int i = 0; i < theFoodToShow.Length; i++)
        {
            theFoodToShow[i].SetActive(false);
        }
        bullet.SetActive(true);

        float shootForce = 80.0f;
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
