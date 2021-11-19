using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using System.Linq;


public class SL_newP1Movement : MonoBehaviour
{
    //private NavMeshAgent myAgent;
    PhotonView view;

    public GameObject inventoryVisible;

    public Animator anim;
    bool isrunning;
    bool stopping;

    //Control model
    public GameObject[] BrockChoi;
    public GameObject[] OfficerWen;

    public static int changep1Icon = 0;

    public bool p2IsWen;


    //new movement
    bool detectAndStop;
    public float speed;
    Vector3 destination;

    private LineRenderer lineRenderer;
    private List<Vector3> point;

    void Start()
    {
        destination = transform.position;

        //myAgent = GetComponent<NavMeshAgent>();
        view = GetComponent<PhotonView>();
        anim = gameObject.GetComponent<Animator>();
        StartCoroutine(waitFoeSec());

        lineRenderer = GetComponent<LineRenderer>();
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
                    //if (Vector3.Distance(transform.position, hit.point) > 1.0)
                    //{
                    //    myAgent.SetDestination(hit.point);
                    //    isrunning = true;
                    //}
                    if (Vector3.Distance(transform.position, hit.point) > 1.0)
                    {
                        destination = hit.point;
                        Movement();
                    }
                }

            }

            if (Input.GetMouseButtonUp(1))
            {
                //detectAndStop = false;
                //myAgent.isStopped = true;
                //myAgent.ResetPath();
            }
        }
        else
        {
            inventoryVisible.SetActive(false);
        }

        //DrawLine();

        //ROTATE player
        if (gameObject.tag == "Player")
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


        if (sl_ShootBehavior.p1Shoot == true || detectAndStop)
        {
            //myAgent.isStopped = true;
            //myAgent.ResetPath();
        }



        //ANIMATION
        //if (!myAgent.pathPending)
        //{
        //    if (myAgent.remainingDistance <= myAgent.stoppingDistance)
        //    {
        //        isrunning = false;
        //        stopping = true;
        //    }
        //    else
        //    {
        //        stopping = false;

        //    }
        //}

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
                    //anim.runtimeAnimatorController = Resources.Load("Assets/Animations/OfficerWen") as RuntimeAnimatorController;
                }
                else
                {
                    view.RPC("Brock", RpcTarget.All);
                    //anim.runtimeAnimatorController = Resources.Load("Assets/Animations/BrockChoi") as RuntimeAnimatorController;
                }
            }

        }


        //edit spped when pass through off mesh link on roof
        //if (myAgent.isOnOffMeshLink)
        //{
        //    OffMeshLinkData data = myAgent.currentOffMeshLinkData;

        //    //calculate the final point of the link
        //    Vector3 endPos = data.endPos + Vector3.up * myAgent.baseOffset;

        //    //Move the agent to the end point
        //    myAgent.transform.position = Vector3.MoveTowards(myAgent.transform.position, endPos, myAgent.speed * Time.deltaTime);

        //    //when the agent reach the end point you should tell it, and the agent will "exit" the link and work normally after that
        //    if (myAgent.transform.position == endPos)
        //    {
        //        myAgent.CompleteOffMeshLink();
        //    }
        //}
    }

    public void Movement()
    {
        //get the distance between the player and the destination pos
        float dis = Vector3.Distance(transform.position, destination);
        if (dis > 0)
        {
            // decide the moveDis for this frame. 
            //(Mathf.Clamp limits the first value, to make sure if the distance between the player and the destination pos is short than you set,
            // it only need to move to the destination. So at that moment, the moveDis should set to the "dis".)
            float moveDis = Mathf.Clamp(speed * Time.fixedDeltaTime, 0, dis);

            //get the unit vector which means the move direction, and multiply by the move distance.
            Vector3 move = (destination - transform.position).normalized * moveDis;
            transform.Translate(move.x, 0, move.z);

        }


    }

    //detect obstacles the stop moving
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Environment")
        {
            //detectAndStop = true;
            Debug.Log("detect");

        }
    }

    //IEnumerator disableStop()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    detectAndStop = false;
    //}

    //public void DrawLine()
    //{
    //    if (myAgent.path.corners.Length < 2) return;

    //    int i = 1;
    //    while(i < myAgent.path.corners.Length)
    //    {
    //        lineRenderer.positionCount = myAgent.path.corners.Length;
    //        point = myAgent.path.corners.ToList();

    //        for(int j = 0; j < point.Count; j++)
    //        {
    //            lineRenderer.SetPosition(j, point[j]);
    //        }
    //        i++;
    //    }
    //}


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

    IEnumerator waitFoeSec()
    {
        yield return new WaitForSeconds(0.1f);
        //Show model when in game
        if (sl_SpawnPlayers.p1_StartModel == 1)
        {
            view.RPC("Brock", RpcTarget.All);

        }
        if (sl_SpawnPlayers.p1_StartModel == 2)
        {
            view.RPC("Wen", RpcTarget.All);

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
