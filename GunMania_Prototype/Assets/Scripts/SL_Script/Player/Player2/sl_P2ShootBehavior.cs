using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class sl_P2ShootBehavior : MonoBehaviour
{
    public GameObject bulletPrefab;
    public List<GameObject> dishBullet;
    public List<GameObject> foodBullet;

    public Transform shootPosition;
    public static int p2bulletCount;

    public sl_Inventory playerInventory;  //set which inventory should be place in

    //for target indicator
    public GameObject targetIndicatorPrefab;
    GameObject targetObject;

    PhotonView view;
    GameObject bullet;
    int bulletNum;

    Vector3 directionShoot2;
    Vector3 targetPosition2;

    //for check shoot once
    int count;
    bool spawn;

    public static bool p2Shoot;

    public Animator anim;
    public GameObject[] theFoodToShow;

    [Header("Range Indicator")]
    public GameObject maxRange;
    public GameObject minRange;

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

                    view.RPC("BulletType2", RpcTarget.All, bulletNum);
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

            //****original shoot range = 40
            if (sl_newP2Movement.changep2Icon == 0) //brock less 2 range
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
            if (sl_newP2Movement.changep2Icon == 2) //jiho increase 2 range
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
    public void BulletType2(int i)
    {
        bulletNum = i;

        //dish
        #region
        if (i == 1)
        {
            theFoodToShow[0].SetActive(true);
            //theFood.SetActive(true);
            bullet = Instantiate(dishBullet[0], shootPosition.position, Quaternion.identity); //explode
            bullet.SetActive(false);
        }

        if (i == 2)
        {
            theFoodToShow[1].SetActive(true);

            //theFood.SetActive(true);
            bullet = Instantiate(dishBullet[1], shootPosition.position, Quaternion.identity); //knockback
            bullet.SetActive(false);
        }
        if (i == 3)
        {
            theFoodToShow[2].SetActive(true);

            //theFood.SetActive(true);

            bullet = Instantiate(dishBullet[2], shootPosition.position, Quaternion.identity); //pull
            bullet.SetActive(false);

        }
        if (i == 4)
        {
            theFoodToShow[3].SetActive(true);

            //theFood.SetActive(true);

            bullet = Instantiate(dishBullet[3], shootPosition.position, Quaternion.identity); //stun
            bullet.SetActive(false);

        }
        #endregion


        //food - start from 10 - 21 
        #region
        if (i == 10) //c_niangao
        {
            theFoodToShow[4].SetActive(true);

            //theFood.SetActive(true);

            bullet = Instantiate(foodBullet[0], shootPosition.position, Quaternion.identity);
            bullet.SetActive(false);


        }
        if (i == 11)//c_spring roll
        {
            theFoodToShow[5].SetActive(true);

            //theFood.SetActive(true);

            bullet = Instantiate(foodBullet[1], shootPosition.position, Quaternion.identity);
            bullet.SetActive(false);

        }
        if (i == 12)//c_wonton
        {
            theFoodToShow[6].SetActive(true);

            //theFood.SetActive(true);

            bullet = Instantiate(foodBullet[2], shootPosition.position, Quaternion.identity);
            bullet.SetActive(false);

        }
        if (i == 13)//j_ichigo
        {
            theFoodToShow[7].SetActive(true);

            //theFood.SetActive(true);

            bullet = Instantiate(foodBullet[3], shootPosition.position, Quaternion.identity);
            bullet.SetActive(false);

        }
        if (i == 14)//j_ikanagi
        {
            theFoodToShow[8].SetActive(true);

            //theFood.SetActive(true);

            bullet = Instantiate(foodBullet[4], shootPosition.position, Quaternion.identity);
            bullet.SetActive(false);

        }
        if (i == 15)//j_sakura
        {
            theFoodToShow[9].SetActive(true);

            //theFood.SetActive(true);

            bullet = Instantiate(foodBullet[5], shootPosition.position, Quaternion.identity);
            bullet.SetActive(false);

        }
        if (i == 16)//k_bap burger
        {
            theFoodToShow[10].SetActive(true);

            //theFood.SetActive(true);

            bullet = Instantiate(foodBullet[6], shootPosition.position, Quaternion.identity);
            bullet.SetActive(false);

        }
        if (i == 17)//k_japchae
        {
            theFoodToShow[11].SetActive(true);

            //theFood.SetActive(true);

            bullet = Instantiate(foodBullet[7], shootPosition.position, Quaternion.identity);
            bullet.SetActive(false);

        }
        if (i == 18)//k_tteobokki
        {
            theFoodToShow[12].SetActive(true);

            //theFood.SetActive(true);

            bullet = Instantiate(foodBullet[8], shootPosition.position, Quaternion.identity);
            bullet.SetActive(false);

        }
        if (i == 19)//t_bubbletea
        {
            theFoodToShow[13].SetActive(true);

            //theFood.SetActive(true);

            bullet = Instantiate(foodBullet[9], shootPosition.position, Quaternion.identity);
            bullet.SetActive(false);

        }
        if (i == 20)//t_pineapple
        {
            theFoodToShow[14].SetActive(true);

            //theFood.SetActive(true);

            bullet = Instantiate(foodBullet[10], shootPosition.position, Quaternion.identity);
            bullet.SetActive(false);

        }
        if (i == 21)//t_taro
        {
            theFoodToShow[15].SetActive(true);

            //theFood.SetActive(true);

            bullet = Instantiate(foodBullet[11], shootPosition.position, Quaternion.identity);
            bullet.SetActive(false);

        }
        #endregion

    }

    [PunRPC]
    public void CancelShoot2()
    {
        //theFood.SetActive(false);
        for (int i = 0; i < theFoodToShow.Length; i++)
        {
            theFoodToShow[i].SetActive(false);
        }

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
        //theFood.SetActive(false);
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
