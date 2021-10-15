using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class sl_ShootBehavior : MonoBehaviour
{
    public Rigidbody bulletPrefab;
    public GameObject cursor;

    public Transform shootPosition;
    public Transform attackPosition;
    public LayerMask layer;

    private Camera cam;
    PhotonView view;

    public static int bulletCount;
    public static bool p1Shoot;

    //HELP AIM
    public LineRenderer lineVisual;
    public int lineSegment = 10;


    void Start()
    {
        view = GetComponent<PhotonView>();
        cam = Camera.main;
        lineVisual.positionCount = lineSegment;
    }


    void Update()
    {
        if (view.IsMine)  //Photon - check is my character
        {
            LaunchProjectile();  //gravity shoot
            //ShootStraight();
        }
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

        if(Physics.Raycast(ray, out hit))
        {
            Vector3 vel = CalculateVelocity(hit.point, shootPosition.position, 1f);
            Visualize(vel);

            if (Input.GetMouseButtonDown(0) /* && bulletCount > 0 */)
            {
                Rigidbody bullet = Instantiate(bulletPrefab, shootPosition.position, Quaternion.identity);
                bullet.velocity = vel;
                bulletCount--;

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


    //for aiming
    void Visualize(Vector3 vel)
    {
        for(int i = 0; i < lineSegment; i++)
        {
            Vector3 pos = CalculatePosInTime(vel, i / (float)lineSegment);
            lineVisual.SetPosition(i, pos);

        }
    }

    Vector3 CalculatePosInTime(Vector3 velocity, float time)
    {
        Vector3 Vxz = velocity;
        Vxz.y = 0f;

        Vector3 result = attackPosition.position + velocity * time;
        float startY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (velocity.y * time) + attackPosition.position.y;

        result.y = startY;
        return result;
    }


    //for direct shoot
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
                targetPosition = hit.point;

                Vector3 directionShoot = targetPosition - attackPosition.position;
                Rigidbody bullet = Instantiate(bulletPrefab, attackPosition.position, Quaternion.identity);

                bullet.transform.forward = directionShoot.normalized;
                bullet.GetComponent<Rigidbody>().AddForce(directionShoot.normalized * shootForce, ForceMode.Impulse); //shootforce
                bulletCount--;

            }

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
