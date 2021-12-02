using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using UnityEngine.UI;

public class sl_newP2Movement : MonoBehaviour
{
    bool startTheGame = false;

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

    //For UI
    public List<Sprite> p2CharacterList = new List<Sprite>();
    public Sprite wenIcon;
    public Sprite brockIcon;
    public Sprite jihoIcon;
    public Sprite katsukiIcon;

    public Image mainUI;
    public Image tagUI;

    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        view = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();

        StartCoroutine(waitFoeSec());
        StartCoroutine(WhenGameStart());
    }


    void Update()
    {
        if (p2CharacterList != null)
        {
            mainUI.sprite = p2CharacterList[0];
            tagUI.sprite = p2CharacterList[1];
        }

        if (startTheGame == true)
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


            if (sl_P2ShootBehavior.p2Shoot == true || !P2DishEffect.p2canMove)
            {
                myAgent.isStopped = true;
                myAgent.ResetPath();
            }

            if (gameObject.tag == "Player2" && view.IsMine)
            {
                Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
                float rayLength;

                if (groundPlane.Raycast(ray, out rayLength))
                {
                    Vector3 pointToLook = ray.GetPoint(rayLength);
                    view.RPC("PlayerRotate2", RpcTarget.All, pointToLook);

                }


            }

            if (Input.GetKeyDown(KeyCode.W) && gameObject.tag == "Player2" && view.IsMine)
            {
                view.RPC("SyncCharacterUIAndModel2", RpcTarget.All);

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
      
    }

    //For Character model
    //0.brock, 1.wen, 2.jiho, 3.katsuki
    #region
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
    #endregion

    IEnumerator waitFoeSec()
    {
        yield return new WaitForSeconds(0.1f);
        //Show model when in game
        if (sl_SpawnPlayerManager.p2count1 == 1 || sl_SpawnPlayerManager.p2count1 == 0) //0 is default, 1 is choosen
        {
            view.RPC("Brock2", RpcTarget.All);
            mainCharacter = 1;
            p2CharacterList[0] = brockIcon;

        }
        if (sl_SpawnPlayerManager.p2count1 == 2)
        {
            view.RPC("Wen2", RpcTarget.All);
            mainCharacter = 2;
            p2CharacterList[0] = wenIcon;

        }
        if (sl_SpawnPlayerManager.p2count1 == 3)
        {
            view.RPC("Jiho2", RpcTarget.All);
            mainCharacter = 3;
            p2CharacterList[0] = jihoIcon;

        }
        if (sl_SpawnPlayerManager.p2count1 == 4)
        {
            view.RPC("Katsuki2", RpcTarget.All);
            mainCharacter = 4;
            p2CharacterList[0] = katsukiIcon;

        }

        //tag character
        if (sl_SpawnPlayerManager.p2count2 == 0 || sl_SpawnPlayerManager.p2count2 == 1)
        {
            tagCharacter = 1; 
            p2CharacterList[1] = brockIcon;

        }
        if (sl_SpawnPlayerManager.p2count2 == 2)
        {
            tagCharacter = 2; 
            p2CharacterList[1] = wenIcon;

        }
        if (sl_SpawnPlayerManager.p2count2 == 3)
        {
            tagCharacter = 3; 
            p2CharacterList[1] = jihoIcon;

        }
        if (sl_SpawnPlayerManager.p2count2 == 4)
        {
            tagCharacter = 4; 
            p2CharacterList[1] = katsukiIcon;

        }

    }

    IEnumerator WhenGameStart()
    {
        yield return new WaitForSeconds(3f);
        startTheGame = true;
    }

    //For UI SYNC
    #region
    public void Move<T>(List<T> list, int oldIndex, int newIndex)
    {
        T item = list[oldIndex];
        list.RemoveAt(oldIndex);
        list.Insert(newIndex, item);
    }

    [PunRPC]
    public void SyncCharacterUIAndModel2()
    {
        if (i == 0)
        {
            i = 1;
            Move(p2CharacterList, 0, 1);

            if (tagCharacter == 1 || tagCharacter == 0)
            {
                view.RPC("Brock2", RpcTarget.All);
            }
            if (tagCharacter == 2)
            {
                view.RPC("Wen2", RpcTarget.All);
            }
            if (tagCharacter == 3)
            {
                view.RPC("Jiho2", RpcTarget.All);
            }
            if (tagCharacter == 4)
            {
                view.RPC("Katsuki2", RpcTarget.All);
            }

        }
        else
        {
            if (i == 1)
            {
                i = 0;
                Move(p2CharacterList, 1, 0);

                if (mainCharacter == 1 || mainCharacter == 0)
                {
                    view.RPC("Brock2", RpcTarget.All);
                }
                if (mainCharacter == 2)
                {
                    view.RPC("Wen2", RpcTarget.All);
                }
                if (mainCharacter == 3)
                {
                    view.RPC("Jiho2", RpcTarget.All);
                }
                if (mainCharacter == 4)
                {
                    view.RPC("Katsuki2", RpcTarget.All);
                }
            }


        }
    }
    #endregion


    [PunRPC]
    public void PlayerRotate2(Vector3 look)
    {
        //Rotate player
        transform.LookAt(new Vector3(look.x, transform.position.y, look.z));

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
