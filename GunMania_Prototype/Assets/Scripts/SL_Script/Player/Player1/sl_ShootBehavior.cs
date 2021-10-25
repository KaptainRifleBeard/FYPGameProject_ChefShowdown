using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class sl_ShootBehavior : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPosition;

    public static int bulletCount;

    //for target indicator
    public GameObject targetIndicatorPrefab;
    GameObject targetObject;

    PhotonView view;
    GameObject bullet;
    Vector3 directionShoot;
    Vector3 targetPosition;

    private bool dragging = false;
    public Animator anim;

    int count;
    bool spawn;

    [Space(10)]
    [Header("Line renderer veriables")]
    
    private LineRenderer lineRenderer;
    public int lineSegment = 50;

    private List<Vector3> linePoint = new List<Vector3>();

    void Start()
    {
        view = GetComponent<PhotonView>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        //for indicator
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        


        if (Input.GetMouseButtonDown(0) && bulletCount > 0)
        {
            if (Physics.Raycast(ray, out hit))
            {
                if(count < 1 && spawn == false)
                {
                    spawn = true;
                    targetObject = Instantiate(targetIndicatorPrefab, Vector3.zero, Quaternion.identity);
                    count++;


                }
                if (count == 1)
                {
                    spawn = false;
                }

                view.RPC("SpawnBullet", RpcTarget.All);
                dragging = true;
            }
                
        }


        if (view.IsMine)
        {
            if (Input.GetMouseButton(0) && bulletCount > 0)  //DRAG
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

        if (Input.GetMouseButtonUp(0) && bulletCount > 0)
        {
            view.RPC("ShootBullet", RpcTarget.All);

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
        bullet = Instantiate(bulletPrefab, shootPosition.position, Quaternion.identity);
        bullet.transform.SetParent(shootPosition);

    }

    [PunRPC]
    void ShootBullet()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        float shootForce = 50.0f;

        if (Physics.Raycast(ray, out hit))
        {
            anim.SetBool("Aim", false);
            anim.SetBool("Throw", true);

            targetPosition = hit.point;
            directionShoot = targetPosition - shootPosition.position;

            bullet.transform.forward = directionShoot.normalized;
            bullet.GetComponent<Rigidbody>().AddForce(directionShoot.normalized * shootForce, ForceMode.Impulse); //shootforce

            bullet.transform.SetParent(null);
            bulletCount--;

            StartCoroutine(stopAnim());


            dragging = false;
            Destroy(targetObject);
            targetObject = null;

            count = 0;
            spawn = false;
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
