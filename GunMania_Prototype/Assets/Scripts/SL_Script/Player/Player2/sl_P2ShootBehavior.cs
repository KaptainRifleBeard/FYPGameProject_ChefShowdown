using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class sl_P2ShootBehavior : MonoBehaviour
{
    public Rigidbody bulletPrefab;
    public GameObject cursor;

    public Transform shootPosition;
    public Transform attackPosition;
    public LayerMask layer;

    private Camera cam;
    PhotonView view;

    public static int p2bulletCount;

    void Start()
    {
        view = GetComponent<PhotonView>();
        cam = Camera.main;
    }


    void Update()
    {
        //LaunchProjectile();  //gravity shoot
        ShootStraight();
    }


    void Test()
    {
        Vector3 dir = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 50.0f));
        cursor.transform.position = dir;
    }

    void LaunchProjectile()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 vel = CalculateVelocity(hit.point, shootPosition.position, 1f);

            if (Input.GetMouseButtonDown(0))
            {
                Rigidbody bullet = Instantiate(bulletPrefab, shootPosition.position, Quaternion.identity);
                bullet.velocity = vel;

            }

        }
    }

    Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        //define dist x and y first
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance;

        distanceXZ.y = 0f;

        //create float to represent the distance
        float y = distance.y;
        float xz = distanceXZ.magnitude;

        float velocityXZ = xz / time;
        float velocityY = y / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized;
        result *= velocityXZ;
        result.y = velocityY;

        return result;

    }


    //for direct shoot
    void ShootStraight()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Vector3 targetPosition;
        float shootForce = 50.0f;

        if (Input.GetMouseButtonDown(0) && p2bulletCount > 0)
        {
            if (Physics.Raycast(ray, out hit))
            {
                targetPosition = hit.point;

                Vector3 directionShoot = targetPosition - attackPosition.position;
                Rigidbody bullet = Instantiate(bulletPrefab, attackPosition.position, Quaternion.identity);

                bullet.transform.forward = directionShoot.normalized;
                bullet.GetComponent<Rigidbody>().AddForce(directionShoot.normalized * shootForce, ForceMode.Impulse); //shootforce
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
