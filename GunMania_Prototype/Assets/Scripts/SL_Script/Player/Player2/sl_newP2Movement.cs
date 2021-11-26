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
    public GameObject indicatorVisible;

    public Animator anim;
    bool isrunning;
    bool stopping;

    //Control model
    public GameObject[] BrockChoi;
    public GameObject[] OfficerWen;

    public static int changep2Icon = 0;

    public SL_newP1Movement checkPlayerModel;  //when start game

    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        view = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();

        StartCoroutine(waitFoeSec());

    }


    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (view.IsMine)  //Photon - check is my character
        {
            inventoryVisible.SetActive(true);
            indicatorVisible.SetActive(true);

            if (Input.GetMouseButton(1))
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
            indicatorVisible.SetActive(false);

        }

        if (gameObject.tag == "Player2")
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


        //Animation
        #region
        if (isrunning && sl_P2ShootBehavior.p2Shoot == false && !PhotonNetwork.IsMasterClient)
        {

            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (sl_P2ShootBehavior.p2bulletCount == 1 && !stopping && !PhotonNetwork.IsMasterClient)
        {
            anim.SetBool("isRunning", false);

            //anim.SetBool("Throw", false);
            anim.SetBool("hold1food", true);
            anim.SetBool("hold2food", false);
        }

        if (sl_P2ShootBehavior.p2bulletCount == 2 && !stopping && !PhotonNetwork.IsMasterClient)
        {
            anim.SetBool("isRunning", false);

            //anim.SetBool("Throw", false);
            anim.SetBool("hold1food", false);
            anim.SetBool("hold2food", true);
        }
        else if (sl_P2ShootBehavior.p2bulletCount == 0 && !stopping && !PhotonNetwork.IsMasterClient)
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


        if (Input.GetKeyDown(KeyCode.W) && gameObject.tag == "Player2")
        {
            foreach (GameObject go in BrockChoi)
            {
                if (go.activeSelf)
                {
                    view.RPC("Wen2", RpcTarget.All);
                    //anim.runtimeAnimatorController = Resources.Load("Animations/OfficerWen") as RuntimeAnimatorController;
                }
                else
                {
                    view.RPC("Brock2", RpcTarget.All);
                    //anim.runtimeAnimatorController = Resources.Load("Animations/BrockChoi") as RuntimeAnimatorController;

                }
            }

        }

        //edit spped when pass through off mesh link on roof
        if (myAgent.isOnOffMeshLink)
        {
            OffMeshLinkData data = myAgent.currentOffMeshLinkData;

            //calculate the final point of the link
            Vector3 endPos = data.endPos + Vector3.up * myAgent.baseOffset;

            //Move the agent to the end point
            myAgent.transform.position = Vector3.MoveTowards(myAgent.transform.position, endPos, myAgent.speed * Time.deltaTime);

            //when the agent reach the end point you should tell it, and the agent will "exit" the link and work normally after that
            if (myAgent.transform.position == endPos)
            {
                myAgent.CompleteOffMeshLink();
            }
        }
    }

    [PunRPC]
    public void Wen2()
    {
        changep2Icon = 0;
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
    public void Brock2()
    {
        changep2Icon = 1;

        for (int i = 0; i < OfficerWen.Length; i++)
        {
            OfficerWen[i].SetActive(false);

        }


        for (int j = 0; j < BrockChoi.Length; j++)
        {
            BrockChoi[j].SetActive(true);

        }

    }

    IEnumerator waitFoeSec()
    {
        yield return new WaitForSeconds(0.1f);
        //Show model when in game
        if (sl_SpawnPlayers.p2_StartModel == 1) //brock
        {
            view.RPC("Brock2", RpcTarget.All);

        }
        if (sl_SpawnPlayers.p2_StartModel == 2)
        {
            view.RPC("Wen2", RpcTarget.All);

        }
    }


    //public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(myAgent.transform.position);
    //        stream.SendNext(myAgent.transform.rotation);
    //    }
    //    else if (stream.IsReading)
    //    {
    //        myAgent.transform.position = (Vector3)stream.ReceiveNext();
    //        myAgent.transform.rotation = (Quaternion)stream.ReceiveNext();

    //    }
    //}
}
