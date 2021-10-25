using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Photon.Pun;

public class sl_PlayerControl : MonoBehaviour
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
        sl_InventoryManager.ClearAllInList();
        view = GetComponent<PhotonView>();

    }

    public void Update()
    {
        if (view.IsMine)  //Photon - check is my character
        {
            inventoryVisible.SetActive(true);

            //NEW MOVEMENT - current using
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Input.GetMouseButtonDown(1))
            {
                if (Physics.Raycast(ray, out hit))
                {
                    targetDestionation.transform.position = hit.point;
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

        if (isrunning && sl_ShootBehavior.bulletCount < 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }


        if (sl_ShootBehavior.bulletCount == 1 && !stopping)
        {
            anim.SetBool("isRunning", false);

            //anim.SetBool("Throw", false);
            anim.SetBool("hold1food", true);
            anim.SetBool("hold2food", false);
        }

        if (sl_ShootBehavior.bulletCount == 2 && !stopping)
        {
            anim.SetBool("isRunning", false);

            //anim.SetBool("Throw", false);
            anim.SetBool("hold1food", false);
            anim.SetBool("hold2food", true);
        }
        else if (sl_ShootBehavior.bulletCount == 0 && !stopping)
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

}
