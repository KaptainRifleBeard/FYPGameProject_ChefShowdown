using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using System.Linq;


public class SL_newP1Movement : MonoBehaviour
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

    public static int changep1Icon = 0;

    public bool p2IsWen;


    //new movement
    bool detectAndStop;
    public float speed;
    Vector3 destination;

    private LineRenderer lineRenderer;
    private List<Vector3> point;


    //to define which is main and tag character
    int mainCharacter;
    int tagCharacter;
    int i = 0;

    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        view = GetComponent<PhotonView>();
        anim = gameObject.GetComponent<Animator>();
        lineRenderer = GetComponent<LineRenderer>();

        StartCoroutine(waitFoeSec());
        destination = transform.position;


    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (view.IsMine)  //Photon - check is my character
        {
            inventoryVisible.SetActive(true);
            indicatorVisible.SetActive(true);

            if (Input.GetMouseButton(1) && sl_ShootBehavior.p1Shoot == false)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (Vector3.Distance(transform.position, hit.point) > 1.0)
                    {
                        myAgent.SetDestination(hit.point);
                        isrunning = true;
                    }
                    //if (Vector3.Distance(transform.position, hit.point) > 1.0)
                    //{
                    //    destination = hit.point;
                    //    Movement();
                    //}
                }

            }

            if (Input.GetMouseButtonUp(1))
            {
                //detectAndStop = false;
                myAgent.isStopped = true;
                myAgent.ResetPath();
            }
        }
        else
        {
            inventoryVisible.SetActive(false);
            indicatorVisible.SetActive(false);

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


        if (sl_ShootBehavior.p1Shoot == true)
        {
            myAgent.isStopped = true;
            myAgent.ResetPath();
        }


        #region
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

        //if (isrunning && sl_ShootBehavior.p1Shoot == false && PhotonNetwork.IsMasterClient)
        //{

        //    anim.SetBool("isRunning", true);
        //}
        //else
        //{
        //    anim.SetBool("isRunning", false);
        //}


        //if (sl_ShootBehavior.bulletCount == 1 && !stopping && PhotonNetwork.IsMasterClient)
        //{
        //    anim.SetBool("isRunning", false);

        //    //anim.SetBool("Throw", false);
        //    anim.SetBool("hold1food", true);
        //    anim.SetBool("hold2food", false);
        //}

        //if (sl_ShootBehavior.bulletCount == 2 && !stopping && PhotonNetwork.IsMasterClient)
        //{
        //    anim.SetBool("isRunning", false);

        //    //anim.SetBool("Throw", false);
        //    anim.SetBool("hold1food", false);
        //    anim.SetBool("hold2food", true);
        //}
        //else if (sl_ShootBehavior.bulletCount == 0 && !stopping && PhotonNetwork.IsMasterClient)
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
        #endregion
        

        if (Input.GetKeyDown(KeyCode.W) && gameObject.tag == "Player")
        {
            if (i == 0)
            {
                i = 1;
                Debug.Log("tagCharacter: " + tagCharacter);

                if (tagCharacter == 1 || tagCharacter == 0)
                {
                    view.RPC("Brock", RpcTarget.All);
                }
                if (tagCharacter == 2)
                {
                    view.RPC("Wen", RpcTarget.All);
                }
                if (tagCharacter == 3)
                {
                    view.RPC("Jiho", RpcTarget.All);
                }
                if (tagCharacter == 4)
                {
                    view.RPC("Katsuki", RpcTarget.All);
                }

            }
            else 
            {
                if (i == 1)
                {
                    i = 0;
                    Debug.Log("main character: " + mainCharacter);
                    if (mainCharacter == 1 || mainCharacter == 0)
                    {
                        view.RPC("Brock", RpcTarget.All);
                    }
                    if (mainCharacter == 2)
                    {
                        view.RPC("Wen", RpcTarget.All);
                    }
                    if (mainCharacter == 3)
                    {
                        view.RPC("Jiho", RpcTarget.All);
                    }
                    if (mainCharacter == 4)
                    {
                        view.RPC("Katsuki", RpcTarget.All);
                    }
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


    //For Character model
    //0.brock, 1.wen, 2.jiho, 3.katsuki
    [PunRPC]
    public void Brock()
    {
        changep1Icon = 0;

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
    public void Wen()
    {
        changep1Icon = 1;


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
    public void Jiho()
    {
        changep1Icon = 2;

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
    public void Katsuki()
    {
        changep1Icon = 3;

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
        Debug.LogWarning("p1 spawn:" + sl_SpawnPlayerManager.count1);
        Debug.LogWarning("p1 tag: " + sl_SpawnPlayerManager.count2);

        yield return new WaitForSeconds(0.1f);
        //Show model when in game
        if (sl_SpawnPlayerManager.count1 == 1 || sl_SpawnPlayerManager.count1 == 0)//0 is default, 1 is choosen
        {
            view.RPC("Brock", RpcTarget.All);
            mainCharacter = 1;
        }
        if (sl_SpawnPlayerManager.count1 == 2)
        {
            view.RPC("Wen", RpcTarget.All);
            mainCharacter = 2;

        }
        if (sl_SpawnPlayerManager.count1 == 3)
        {
            view.RPC("Jiho", RpcTarget.All);
            mainCharacter = 3;

        }
        if (sl_SpawnPlayerManager.count1 == 4)
        {
            view.RPC("Katsuki", RpcTarget.All);
            mainCharacter = 4;

        }

        //tag character
        //define int for tag character, ****i put -1 because somehow the integer auto +1 when i switch scene, but default 0 no problem
        if (sl_SpawnPlayerManager.count2 == 0 || sl_SpawnPlayerManager.count2 == 1)
        {
            tagCharacter = 1;
        }
        if (sl_SpawnPlayerManager.count2 == 2)
        {
            tagCharacter = 2;
        }
        if (sl_SpawnPlayerManager.count2 == 3)
        {
            tagCharacter = 3;
        }
        if (sl_SpawnPlayerManager.count2 == 4)
        {
            tagCharacter = 4;
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
