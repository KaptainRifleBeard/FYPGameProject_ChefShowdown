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
    private NavMeshAgent myAgent;

    public static int bulletCount;

    //for target indicator
    public GameObject targetIndicatorPrefab;
    GameObject targetObject;

    PhotonView view;
    GameObject bullet;
    Vector3 directionShoot;
    Vector3 targetPosition;
    public Animator anim;

    int count;
    bool spawn;

    public static bool p1Shoot = false;

    void Start()
    {
        view = GetComponent<PhotonView>();
        myAgent = GetComponent<NavMeshAgent>();
        targetObject = targetIndicatorPrefab;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {

            if (Input.GetMouseButtonDown(0)/* && bulletCount > 0*/ && p1Shoot == false)
            {
                p1Shoot = true;  //stop movement when shoot

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

            if(view.IsMine) //indicator follow mouse
            {
                targetObject.transform.position = hit.point;
            }


            if (Input.GetMouseButtonDown(1) /*&& bulletCount > 0 */&& p1Shoot == true)
            {
                view.RPC("ShootBullet", RpcTarget.All);

            }


        }

    }

    IEnumerator stopAnim()
    {
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("Throw", false);

    }


    [PunRPC]
    void SpawnBullet()
    {
        

    }

    [PunRPC]
    void ShootBullet()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        float shootForce = 50.0f;

        if (Physics.Raycast(ray, out hit))
        {
            if(p1Shoot)
            {
                anim.SetBool("Aim", false);
                anim.SetBool("Throw", true);

                targetPosition = targetObject.transform.position;
                directionShoot = targetPosition - shootPosition.position;

                bullet.transform.forward = directionShoot.normalized;
                bullet.GetComponent<Rigidbody>().AddForce(directionShoot.normalized * shootForce, ForceMode.Impulse); //shootforce

                bullet.transform.SetParent(null);
                bulletCount--;

                StartCoroutine(stopAnim());
                Destroy(targetObject);

                //reset
                targetObject = targetIndicatorPrefab;
                count = 0;
                spawn = false;

                p1Shoot = false;


            }

        }


    }


    //public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(bulletCount);

    //    }
    //    else if (stream.IsReading)
    //    {
    //        bulletCount = (int)stream.ReceiveNext();
    //    }
    //}
}
