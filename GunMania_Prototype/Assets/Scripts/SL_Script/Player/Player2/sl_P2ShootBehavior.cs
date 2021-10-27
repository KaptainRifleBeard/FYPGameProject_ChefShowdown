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

    //for target indicator
    public GameObject targetIndicatorPrefab;
    GameObject targetObject;

    PhotonView view;
    Rigidbody bullet;

    Vector3 directionShoot2;
    Vector3 targetPosition2;

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
            if (Input.GetMouseButtonDown(0) && p2Shoot == false/* && p2bulletCount > 0*/  && !PhotonNetwork.IsMasterClient)
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


        if (view.IsMine && p2Shoot == true) //indicator follow mouse
        {
            targetObject.transform.position = hit.point;
        }

        if (Input.GetMouseButtonDown(1) /*&& p2bulletCount > 0 */&& p2Shoot == true && !PhotonNetwork.IsMasterClient)
        {
            view.RPC("ShootBullet2", RpcTarget.All);

        }

    }

    IEnumerator stopAnim()
    {
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("Throw2", false);
    }

    [PunRPC]
    public void SpawnBullet2()
    {
        bullet = Instantiate(bulletPrefab, shootPosition.position, Quaternion.identity);
        bullet.transform.SetParent(shootPosition);
    }

    [PunRPC]
    public void ShootBullet2()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        float shootForce = 50.0f;

        if (Physics.Raycast(ray, out hit))
        {
            anim.SetBool("Aim2", false);
            anim.SetBool("Throw2", true);

            targetPosition2 = hit.point;
            directionShoot2 = targetPosition2 - shootPosition.position;

            bullet.transform.forward = directionShoot2.normalized;
            bullet.GetComponent<Rigidbody>().AddForce(directionShoot2.normalized * shootForce, ForceMode.Impulse); //shootforce

            bullet.transform.SetParent(null);
            p2bulletCount--;

            StartCoroutine(stopAnim());
            Destroy(targetObject);

            //reset
            targetObject = targetIndicatorPrefab;
            count = 0;
            spawn = false;

            p2Shoot = false;

        }
    }

}
