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
    public GameObject[] AuntJiho;
    public GameObject[] MrKatsuki;

    public static int changep2Icon = 0;

    //to define which is main and tag character
    int mainCharacter;
    int tagCharacter;
    int i = 0;

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


        #region
        //if (isrunning && sl_P2ShootBehavior.p2Shoot == false && !PhotonNetwork.IsMasterClient)
        //{

        //    anim.SetBool("isRunning", true);
        //}
        //else
        //{
        //    anim.SetBool("isRunning", false);
        //}

        //if (sl_P2ShootBehavior.p2bulletCount == 1 && !stopping && !PhotonNetwork.IsMasterClient)
        //{
        //    anim.SetBool("isRunning", false);

        //    //anim.SetBool("Throw", false);
        //    anim.SetBool("hold1food", true);
        //    anim.SetBool("hold2food", false);
        //}

        //if (sl_P2ShootBehavior.p2bulletCount == 2 && !stopping && !PhotonNetwork.IsMasterClient)
        //{
        //    anim.SetBool("isRunning", false);

        //    //anim.SetBool("Throw", false);
        //    anim.SetBool("hold1food", false);
        //    anim.SetBool("hold2food", true);
        //}
        //else if (sl_P2ShootBehavior.p2bulletCount == 0 && !stopping && !PhotonNetwork.IsMasterClient)
        //{
        //    anim.SetBool("isRunning", true);

        //    //anim.SetBool("Throw", false);
        //    anim.SetBool("hold1food", false);
        //    anim.SetBool("hold2food", false);
        //}

        //if (stopping)
        //{
        //    anim.SetBool("stop", true);

        //    anim.SetBool("isRunning", false);
        //    //anim.SetBool("Throw", false);
        //    anim.SetBool("hold1food", false);
        //    anim.SetBool("hold2food", false);
        //}
        //else
        //{
        //    anim.SetBool("stop", false);

        //}

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
        #endregion



        //define int for tag character
        if (sl_SpawnPlayerManager.playerTagNum_p2 == 1)
        {
            tagCharacter = 1;
        }
        if (sl_SpawnPlayerManager.playerTagNum_p2 == 2)
        {
            tagCharacter = 2;
        }
        if (sl_SpawnPlayerManager.playerTagNum_p2 == 3)
        {
            tagCharacter = 3;
        }
        if (sl_SpawnPlayerManager.playerTagNum_p2 == 4)
        {
            tagCharacter = 4;
        }


        if (Input.GetKeyDown(KeyCode.W) && gameObject.tag == "Player2")
        {
            Debug.Log("current main " + mainCharacter);

            if (i < 1)
            {
                if (tagCharacter == 1)
                {
                    view.RPC("Brock2", RpcTarget.All);
                    i = 1;
                }
                if (tagCharacter == 2)
                {
                    view.RPC("Wen2", RpcTarget.All);
                    i = 1;
                }
                if (tagCharacter == 3)
                {
                    view.RPC("Jiho2", RpcTarget.All);
                    i = 1;
                }
                if (tagCharacter == 4)
                {
                    view.RPC("Katsuki2", RpcTarget.All);
                    i = 1;
                }
            }
            else
            {
                if (mainCharacter == 1)
                {
                    view.RPC("Brock2", RpcTarget.All);
                    i--;
                }
                if (mainCharacter == 2)
                {
                    view.RPC("Wen2", RpcTarget.All);
                    i--;
                }
                if (mainCharacter == 3)
                {
                    view.RPC("Jiho2", RpcTarget.All);
                    i--;
                }
                if (mainCharacter == 4)
                {
                    view.RPC("Katsuki2", RpcTarget.All);
                    i--;
                }
            }
            Debug.Log("i " + i);

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

    //For Character model
    //0.brock, 1.wen, 2.jiho, 3.katsuki
    [PunRPC]
    public void Brock2()
    {
        changep2Icon = 0;

        for (int j = 0; j < BrockChoi.Length; j++)
        {
            BrockChoi[j].SetActive(true);

        }
        for (int i = 0; i < OfficerWen.Length; i++)
        {
            OfficerWen[i].SetActive(false);

        }

        for (int j = 0; j < AuntJiho.Length; j++)
        {
            AuntJiho[j].SetActive(false);

        }

        for (int j = 0; j < MrKatsuki.Length; j++)
        {
            MrKatsuki[j].SetActive(false);

        }
    }

    [PunRPC]
    public void Wen2()
    {
        changep2Icon = 1;


        for (int j = 0; j < BrockChoi.Length; j++)
        {
            BrockChoi[j].SetActive(false);

        }

        for (int i = 0; i < OfficerWen.Length; i++)
        {
            OfficerWen[i].SetActive(true);

        }

        for (int j = 0; j < AuntJiho.Length; j++)
        {
            AuntJiho[j].SetActive(false);

        }

        for (int j = 0; j < MrKatsuki.Length; j++)
        {
            MrKatsuki[j].SetActive(false);

        }
    }

    [PunRPC]
    public void Jiho2()
    {
        changep2Icon = 2;

        for (int i = 0; i < BrockChoi.Length; i++)
        {
            BrockChoi[i].SetActive(false);

        }
        for (int i = 0; i < OfficerWen.Length; i++)
        {
            OfficerWen[i].SetActive(false);

        }

        for (int j = 0; j < AuntJiho.Length; j++)
        {
            AuntJiho[j].SetActive(true);

        }

        for (int j = 0; j < MrKatsuki.Length; j++)
        {
            MrKatsuki[j].SetActive(false);

        }
    }

    [PunRPC]
    public void Katsuki2()
    {
        changep2Icon = 3;

        for (int i = 0; i < BrockChoi.Length; i++)
        {
            BrockChoi[i].SetActive(false);

        }
        for (int i = 0; i < OfficerWen.Length; i++)
        {
            OfficerWen[i].SetActive(false);

        }

        for (int j = 0; j < AuntJiho.Length; j++)
        {
            AuntJiho[j].SetActive(false);

        }

        for (int j = 0; j < MrKatsuki.Length; j++)
        {
            MrKatsuki[j].SetActive(true);

        }
    }

    IEnumerator waitFoeSec()
    {
        Debug.LogWarning("p2 spawn + "+ sl_SpawnPlayerManager.playerNum_p2);

        yield return new WaitForSeconds(0.1f);
        //Show model when in game
        if (sl_SpawnPlayerManager.playerNum_p2 == 1)
        {
            view.RPC("Brock2", RpcTarget.All);
            mainCharacter = 1;
        }
        if (sl_SpawnPlayerManager.playerNum_p2 == 2)
        {
            view.RPC("Wen2", RpcTarget.All);
            mainCharacter = 2;

        }
        if (sl_SpawnPlayerManager.playerNum_p2 == 3)
        {
            view.RPC("Jiho2", RpcTarget.All);
            mainCharacter = 3;

        }
        if (sl_SpawnPlayerManager.playerNum_p2 == 4)
        {
            view.RPC("Katsuki2", RpcTarget.All);
            mainCharacter = 4;

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
