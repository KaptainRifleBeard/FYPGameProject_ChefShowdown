using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sl_ShootBehavior : MonoBehaviour
{
    public Rigidbody bulletPrefab;
    public GameObject cursor;
    public Transform shootPosition;
    public Transform attackPosition;
    public LayerMask layer;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }


    void Update()
    {
        //LaunchProjectile();
        ShootStraight();
    }


    void LaunchProjectile()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 100f, layer))
        {
            cursor.SetActive(true);
            cursor.transform.position = hit.point + Vector3.up * 0.1f;

            Vector3 vel = CalculateVelocity(hit.point, shootPosition.position, 1f);
            transform.rotation = Quaternion.LookRotation(vel);


            if(Input.GetMouseButtonDown(0))
            {
                Rigidbody bullet = Instantiate(bulletPrefab, shootPosition.position, Quaternion.identity);
                bullet.velocity = vel;
            }

        }
        else
        {
            cursor.SetActive(false);
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
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Vector3 targetPosition;
        float shootForce = 100.0f;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                targetPosition = hit.point;

                Vector3 directionShoot = targetPosition - attackPosition.position;
                Rigidbody bullet = Instantiate(bulletPrefab, attackPosition.position, Quaternion.identity);

                bullet.transform.forward = directionShoot.normalized;
                bullet.GetComponent<Rigidbody>().AddForce(directionShoot.normalized * shootForce, ForceMode.Impulse); //shootforce

            }


        }

    }
}