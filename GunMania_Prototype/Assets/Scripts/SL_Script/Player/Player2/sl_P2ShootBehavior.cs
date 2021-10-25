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

    private bool dragging = false;

    void Start()
    {
        view = GetComponent<PhotonView>();

        //Instantiate click target prefab
        if (targetIndicatorPrefab)
        {
            targetObject = Instantiate(targetIndicatorPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            targetObject.SetActive(false);
        }
    }


    void Update()
    {
        //for indicator
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (view.IsMine)
        {
            ShootStraight2();

            if (Input.GetMouseButtonDown(0))
            {
                if (targetObject)
                {
                    targetObject.SetActive(true);
                    dragging = true;

                }
            }

            if (Input.GetMouseButton(0))
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (dragging)
                    {
                        targetObject.transform.position = hit.point;

                    }
                }

            }

            if (Input.GetMouseButtonUp(0))
            {
                dragging = false;
                targetObject.SetActive(false);
            }
        }

    }

    void ShootStraight2()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Vector3 targetPosition;
        float shootForce = 50.0f;

        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0) && p2bulletCount > 0)
            {
                bullet = Instantiate(bulletPrefab, shootPosition.position, Quaternion.identity);
                bullet.transform.SetParent(shootPosition);

            }

            if (Input.GetMouseButton(0) && p2bulletCount > 0)
            {


            }

            if (Input.GetMouseButtonUp(0) && p2bulletCount > 0)
            {
                targetPosition = hit.point;
                Vector3 directionShoot = targetPosition - shootPosition.position;

                bullet.transform.forward = directionShoot.normalized;
                bullet.GetComponent<Rigidbody>().AddForce(directionShoot.normalized * shootForce, ForceMode.Impulse); //shootforce

                bullet.transform.SetParent(null);
                p2bulletCount--;

            }
        }

           

    }



    [PunRPC]
    public void BulletCount(int count)
    {
        p2bulletCount -= count;

    }

    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(p2bulletCount);

        }
        else if (stream.IsReading)
        {
            p2bulletCount = (int)stream.ReceiveNext();
        }
    }
}
