using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Photon.Pun;

public class sl_PlayerControl : MonoBehaviour /*, IPunObservable*/
{
    //NavMesh AI movement for click to move
    private NavMeshAgent myAgent;
    PhotonView view;

    //NEW MOVEMENT VARIABLE
    public GameObject targetDestionation;
    public GameObject inventoryVisible;

    public Animator anim;
    bool isrunning;
    bool stopping;
    Vector3 wantedPosition;

    private void Awake()
    {
        myAgent = GetComponent<NavMeshAgent>();
    }


    void Start()
    {
        sl_InventoryManager.ClearAllInList();
        view = GetComponent<PhotonView>();

    }

    public void Update()
    {
        if (view.IsMine)  //Photon - check is my character
        {
            inventoryVisible.SetActive(true);

            if (Input.GetMouseButton(1) && sl_ShootBehavior.p1Shoot == false)
            {
                //NEW MOVEMENT - current using
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (Vector3.Distance(transform.position, hit.point) > 10.0)
                    {
                        transform.LookAt(wantedPosition);
                        myAgent.SetDestination(hit.point);
                        isrunning = true;
                    }

                }

                //Rotate player
                Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
                float rayLength;

                if (groundPlane.Raycast(ray, out rayLength))
                {
                    Vector3 pointToLook = ray.GetPoint(rayLength);
                    transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
                }

            }

            if (sl_ShootBehavior.p1Shoot == true)
            {
                myAgent.isStopped = true;
                myAgent.ResetPath();
            }


        }
        else
        {
            inventoryVisible.SetActive(false);
        }






        //ANIMATION
        if (!myAgent.pathPending)
        {
            if (myAgent.remainingDistance <= myAgent.stoppingDistance)
            {
                isrunning = false;
                stopping = true;
            }
            else
            {
                stopping = false;

            }
        }


        if (isrunning && sl_ShootBehavior.p1Shoot == false && PhotonNetwork.IsMasterClient)
        {

            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }


        if (sl_ShootBehavior.bulletCount == 1 && !stopping && PhotonNetwork.IsMasterClient)
        {
            anim.SetBool("isRunning", false);

            //anim.SetBool("Throw", false);
            anim.SetBool("hold1food", true);
            anim.SetBool("hold2food", false);
        }

        if (sl_ShootBehavior.bulletCount == 2 && !stopping && PhotonNetwork.IsMasterClient)
        {
            anim.SetBool("isRunning", false);

            //anim.SetBool("Throw", false);
            anim.SetBool("hold1food", false);
            anim.SetBool("hold2food", true);
        }
        else if (sl_ShootBehavior.bulletCount == 0 && !stopping && PhotonNetwork.IsMasterClient)
        {
            anim.SetBool("isRunning", true);

            //anim.SetBool("Throw", false);
            anim.SetBool("hold1food", false);
            anim.SetBool("hold2food", false);
        }

        if (stopping)
        {
            anim.SetBool("stop", true);

            anim.SetBool("isRunning", false);
            //anim.SetBool("Throw", false);
            anim.SetBool("hold1food", false);
            anim.SetBool("hold2food", false);
        }
        else
        {
            anim.SetBool("stop", false);

        }


    }


    //public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(myAgent.transform.position);s
    //        stream.SendNext(myAgent.transform.rotation);
    //        stream.SendNext(myAgent.velocity);
    //    }
    //    else if (stream.IsReading)
    //    {
    //        myAgent.transform.position = (Vector3)stream.ReceiveNext();
    //        myAgent.transform.rotation = (Quaternion)stream.ReceiveNext();
    //        myAgent.velocity = (Vector3)stream.ReceiveNext();


    //        float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTimestamp));
    //        myAgent.transform.position += myAgent.velocity * lag;

    //    }

    //}

}
