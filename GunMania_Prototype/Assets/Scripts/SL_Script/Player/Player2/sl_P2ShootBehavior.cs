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
    private bool dragging = false;

    int count;
    bool spawn;

    void Start()
    {
        view = GetComponent<PhotonView>();

        //Instantiate click target prefab
        
    }

    void Update()
    {

        //for indicator
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (view.IsMine)
        {
            if (Input.GetMouseButton(0) && p2bulletCount > 0)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (dragging)
                    {
                        targetObject.transform.position = hit.point;
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && p2bulletCount > 0)
        {
            if (count < 1 && spawn == false)
            {
                spawn = true;
                targetObject = Instantiate(targetIndicatorPrefab, Vector3.zero, Quaternion.identity);
                count++;


            }
            if (count == 1)
            {
                spawn = false;
            }

            view.RPC("SpawnBullet2", RpcTarget.All);
            dragging = true;
        }

        if (Input.GetMouseButtonUp(0) && p2bulletCount > 0)
        {
            view.RPC("ShootBullet2", RpcTarget.All);
            Destroy(targetObject);
            dragging = false;


            count = 0;
            spawn = false;
        }


    }

    IEnumerator stopAnim()
    {
        yield return new WaitForSeconds(0.4f);

    }

    [PunRPC]
    void SpawnBullet2()
    {
        bullet = Instantiate(bulletPrefab, shootPosition.position, Quaternion.identity);
        bullet.transform.SetParent(shootPosition);
    }

    [PunRPC]
    void ShootBullet2()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        float shootForce = 50.0f;

        if (Physics.Raycast(ray, out hit))
        {
            targetPosition2 = hit.point;
            directionShoot2 = targetPosition2 - shootPosition.position;

            bullet.transform.forward = directionShoot2.normalized;
            bullet.GetComponent<Rigidbody>().AddForce(directionShoot2.normalized * shootForce, ForceMode.Impulse); //shootforce

            bullet.transform.SetParent(null);
            p2bulletCount--;
            StartCoroutine(stopAnim());

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
