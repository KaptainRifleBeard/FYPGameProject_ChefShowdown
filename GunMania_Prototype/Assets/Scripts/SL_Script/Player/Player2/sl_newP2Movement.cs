using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class sl_newP2Movement : MonoBehaviour
{
    private NavMeshAgent myAgent;
    PhotonView view;

    public GameObject inventoryVisible;

    public Animator anim;
    bool isrunning;
    bool stopping;

    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        view = GetComponent<PhotonView>();

    }


    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (view.IsMine)  //Photon - check is my character
        {
            inventoryVisible.SetActive(true);

            if (Input.GetMouseButton(1))
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (Vector3.Distance(transform.position, hit.point) > 1.0)
                    {
                        myAgent.SetDestination(hit.point);
                    }

                }

            }

            if (Input.GetMouseButtonUp(1))
            {
                myAgent.isStopped = true;
                myAgent.ResetPath();
            }
        }
        else
        {
            inventoryVisible.SetActive(false);
        }

        //Rotate player
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(ray, out rayLength))
        {
            Vector3 pointToLook = ray.GetPoint(rayLength);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }


        if (sl_P2ShootBehavior.p2Shoot == true)
        {
            myAgent.isStopped = true;
            myAgent.ResetPath();
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

            //anim.SetBool("Throw", false);
            anim.SetBool("hold1food2", true);
            anim.SetBool("hold2food2", false);
        }

        if (sl_P2ShootBehavior.p2bulletCount == 2 && !stopping && !PhotonNetwork.IsMasterClient)
        {
            anim.SetBool("isRunning2", false);

            //anim.SetBool("Throw", false);
            anim.SetBool("hold1food2", false);
            anim.SetBool("hold2food2", true);
        }
        else if (sl_P2ShootBehavior.p2bulletCount == 0 && !stopping && !PhotonNetwork.IsMasterClient)
        {
            anim.SetBool("isRunning2", true);

            //anim.SetBool("Throw", false);
            anim.SetBool("hold1food2", false);
            anim.SetBool("hold2food2", false);
        }

        if (stopping)
        {
            anim.SetBool("stop2", true);

            anim.SetBool("isRunning2", false);
            //anim.SetBool("Throw", false);
            anim.SetBool("hold1food2", false);
            anim.SetBool("hold2food2", false);
        }
        else
        {
            anim.SetBool("stop2", false);

        }




    }
}
