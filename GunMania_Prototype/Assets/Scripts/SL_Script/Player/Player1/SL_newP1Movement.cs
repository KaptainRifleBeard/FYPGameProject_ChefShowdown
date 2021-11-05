using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;


public class SL_newP1Movement : MonoBehaviour
{
    private NavMeshAgent myAgent;
    PhotonView view;

    public GameObject inventoryVisible;

    public Animator anim;
    bool isrunning;
    bool stopping;

    //Control model
    public GameObject[] BrockChoi;
    public GameObject[] OfficerWen;

    public static int changep1Icon = 0;

    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        view = GetComponent<PhotonView>();
        anim = gameObject.GetComponent<Animator>();
    }


    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (view.IsMine)  //Photon - check is my character
        {
            inventoryVisible.SetActive(true);

            if (Input.GetMouseButton(1) && sl_ShootBehavior.p1Shoot == false)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (Vector3.Distance(transform.position, hit.point) > 1.0)
                    {
                        myAgent.SetDestination(hit.point);
                        isrunning = true;
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

        if(gameObject.tag == "Player")
        {
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

        //Animation
        #region
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
        #endregion

        if (Input.GetKeyDown(KeyCode.W) && gameObject.tag == "Player")
        {
            foreach (GameObject go in BrockChoi)
            {
                if (go.activeSelf)
                {
                    view.RPC("Wen", RpcTarget.All);
                    anim.runtimeAnimatorController = Resources.Load("Assets/Animations/OfficerWen") as RuntimeAnimatorController;
                }
                else
                {
                    view.RPC("Brock", RpcTarget.All);
                    anim.runtimeAnimatorController = Resources.Load("Assets/Animations/BrockChoi") as RuntimeAnimatorController;
                }
            }

        }

    }

    [PunRPC]
    public void Wen()
    {
        changep1Icon = 1;

        for (int i = 0; i < OfficerWen.Length; i++)
        {
            OfficerWen[i].SetActive(true);

        }


        for (int j = 0; j < BrockChoi.Length; j++)
        {
            BrockChoi[j].SetActive(false);

        }
    }

    [PunRPC]
    public void Brock()
    {
        changep1Icon = 0;

        for (int i = 0; i < OfficerWen.Length; i++)
        {
            OfficerWen[i].SetActive(false);

        }


        for (int j = 0; j < BrockChoi.Length; j++)
        {
            BrockChoi[j].SetActive(true);

        }

    }


    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(myAgent.transform.position);
            stream.SendNext(myAgent.transform.rotation);
        }
        else if (stream.IsReading)
        {
            myAgent.transform.position = (Vector3)stream.ReceiveNext();
            myAgent.transform.rotation = (Quaternion)stream.ReceiveNext();

        }
    }
}
