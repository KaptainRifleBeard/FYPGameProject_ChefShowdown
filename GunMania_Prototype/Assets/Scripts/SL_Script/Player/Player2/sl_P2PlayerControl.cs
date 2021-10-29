using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Photon.Pun;

public class sl_P2PlayerControl : MonoBehaviour/*, IPunObservable*/
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

    private void Awake()
    {
        myAgent = GetComponent<NavMeshAgent>();
    }


    void Start()
    {
        sl_p2InventoryManager.ClearAllInList();
        view = GetComponent<PhotonView>();
    }

    public void Update()
    {
        if (view.IsMine)  //Photon - check is my character's view
        {
            inventoryVisible.SetActive(true);

            //NEW MOVEMENT - current using
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Input.GetMouseButtonDown(1) && sl_P2ShootBehavior.p2Shoot == false)
            {

                if (Physics.Raycast(ray, out hit))
                {
                    targetDestionation.transform.position = hit.point;
                    myAgent.SetDestination(hit.point);
                    isrunning = true;

                }

            }

            if (sl_P2ShootBehavior.p2Shoot == true)
            {
                myAgent.isStopped = true;
                myAgent.ResetPath();
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

        if (isrunning && sl_P2ShootBehavior.p2Shoot == false && !PhotonNetwork.IsMasterClient)
        {
            anim.SetBool("isRunning2", true);
        }
        else
        {
            anim.SetBool("isRunning2", false);
        }


        if (sl_P2ShootBehavior.p2bulletCount == 1 && !stopping && !PhotonNetwork.IsMasterClient)
        {
            anim.SetBool("isRunning2", false);

            anim.SetBool("hold1food2", true);
            anim.SetBool("hold2food2", false);
        }

        if (sl_P2ShootBehavior.p2bulletCount == 2 && !stopping && !PhotonNetwork.IsMasterClient)
        {
            anim.SetBool("isRunning2", false);

            anim.SetBool("hold1food2", false);
            anim.SetBool("hold2food2", true);
        }
        else if (sl_P2ShootBehavior.p2bulletCount == 0 && !stopping && !PhotonNetwork.IsMasterClient)
        {
            anim.SetBool("isRunning2", true);

            anim.SetBool("hold1food2", false);
            anim.SetBool("hold2food2", false);
        }

        if (stopping && !PhotonNetwork.IsMasterClient)
        {
            anim.SetBool("stop2", true);

            anim.SetBool("isRunning2", false);
            anim.SetBool("hold1food2", false);
            anim.SetBool("hold2food2", false);
        }
        else
        {
            anim.SetBool("stop2", false);

        }

    }

    //public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(myAgent.transform.position);
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
