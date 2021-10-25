using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class sl_ShootBehavior : MonoBehaviour
{
    public Rigidbody bulletPrefab;
    public Transform shootPosition;


    public static int bulletCount;

    //for target indicator
    public GameObject targetIndicatorPrefab;
    GameObject targetObject;

    PhotonView view;

    private bool dragging = false;
    public Animator anim;

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
        //LaunchProjectile();  //gravity shoot
        ShootStraight();

    }

    //void LaunchProjectile()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;

    //    if(Physics.Raycast(ray, out hit))
    //    {
    //        Vector3 vel = CalculateVelocity(hit.point, shootPosition.position, 1f);

    //        if (Input.GetMouseButtonDown(0)/* && bulletCount > 0 */)
    //        {
    //            Rigidbody bullet = Instantiate(bulletPrefab, shootPosition.position, Quaternion.identity);
    //            bullet.velocity = vel;

    //        }

    //    }
    //}

    //Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    //{
    //    //define dist x and y first
    //    Vector3 distance = target - origin;
    //    Vector3 distanceXZ = distance;

    //    distanceXZ.y = 0f;

    //    //create float to represent the distance
    //    float y = distance.y;
    //    float xz = distanceXZ.magnitude;

    //    float velocityXZ = xz / time;
    //    float velocityY = y / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

    //    Vector3 result = distanceXZ.normalized;
    //    result *= velocityXZ;
    //    result.y = velocityY;

    //    return result;

    //}


    //for direct shoot


    Rigidbody bullet;
    void ShootStraight()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Vector3 targetPosition;
        float shootForce = 50.0f;


        if (Input.GetMouseButtonDown(0) /* && bulletCount > 0 */)
        {
            if (Physics.Raycast(ray, out hit))
            {
                bullet = Instantiate(bulletPrefab, shootPosition.position, Quaternion.identity);
                bullet.transform.SetParent(shootPosition);

                if (targetObject && view.IsMine)
                {
                    //targetObject.transform.position = hit.point;
                    targetObject.SetActive(true);
                    dragging = true;

                }
            }

        }

        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (dragging && view.IsMine)
                {
                    targetObject.transform.position = hit.point;

                }
            }
                
        }


        if (Input.GetMouseButtonUp(0) /* && bulletCount > 0 */)
        {
            anim.SetBool("Throw", true);
            anim.speed = 1.2f;

            targetPosition = targetObject.transform.position;
            Vector3 directionShoot = targetPosition - shootPosition.position;

            bullet.transform.forward = directionShoot.normalized;
            bullet.GetComponent<Rigidbody>().AddForce(directionShoot.normalized * shootForce, ForceMode.Impulse); //shootforce

            bullet.transform.SetParent(null);
            bulletCount--;
            dragging = false;
            targetObject.SetActive(false);

        }



    }



    [PunRPC]
    public void BulletCount(int count)
    {
        bulletCount -= count;

    }

    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(bulletCount);
        }
        else if (stream.IsReading)
        {
            bulletCount = (int)stream.ReceiveNext();
        }
    }
}
