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


    void Start()
    {
        view = GetComponent<PhotonView>();        
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {

            if (Input.GetMouseButtonDown(0)/* && p2bulletCount > 0*/ && p2Shoot == false)
            {
                p2Shoot = true;  //stop movement when shoot

                //make sure only spawn 1
                if (count < 1 && spawn == false)
                {
                    spawn = true;
                    bullet = Instantiate(bulletPrefab, shootPosition.position, Quaternion.identity);
                    bullet.transform.SetParent(shootPosition);

                    targetObject = Instantiate(targetIndicatorPrefab, Vector3.zero, Quaternion.identity);
                    count++;


                }
                if (count == 1)
                {
                    spawn = false;
                }


            }

            if (view.IsMine) //indicator follow mouse
            {
                targetObject.transform.position = hit.point;
            }


            if (Input.GetMouseButtonDown(1) /*&& p2bulletCount > 0 */&& p2Shoot == true)
            {
                view.RPC("ShootBullet2", RpcTarget.All);

            }


        }

    }

    IEnumerator stopAnim()
    {
        yield return new WaitForSeconds(0.4f);

    }

    [PunRPC]
    void SpawnBullet2()
    {
        
    }

    [PunRPC]
    void ShootBullet2()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        float shootForce = 50.0f;

        if (Physics.Raycast(ray, out hit))
        {
            if (p2Shoot)
            {

                targetPosition2 = targetObject.transform.position;
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


    //public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(p2bulletCount);

    //    }
    //    else if (stream.IsReading)
    //    {
    //        targetPosition2 = (Vector3)stream.ReceiveNext();
    //    }
    //}
}
