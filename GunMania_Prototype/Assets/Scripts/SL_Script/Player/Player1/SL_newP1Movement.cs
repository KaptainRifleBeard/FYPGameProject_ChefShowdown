using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using System.Linq;
using UnityEngine.UI;

public class SL_newP1Movement : MonoBehaviour, IPunObservable
{
    bool startTheGame = false;

    private NavMeshAgent myAgent;
    PhotonView view;

    //UI variables


    //Animation Variables
    [Header("Animation")]
    public Animator myAnimator;
    public Animator brock_Animator;
    public Animator wen_Animator;
    public Animator jiho_Animator;
    public Animator katsuki_Animator;
    string animName;

    bool throwing = false;

    bool isrunning;
    bool stopping;

    //Character model variables
    [Header("Model")]
    public GameObject[] BrockChoi;
    public GameObject[] OfficerWen;
    public GameObject[] AuntJiho;
    public GameObject[] MrKatsuki;

    public GameObject wenTrail;

    public static int changeModelAnim = 0;

    public bool p2IsWen;

    public bool toRoof;

    //new movement
    bool detectAndStop;
    public float speed;
    Vector3 destination;

    public bool knockback;
    Vector3 direction;

    private LineRenderer lineRenderer;
    private List<Vector3> point;

    //to define which is main and tag character
    int mainCharacter;
    int tagCharacter;
    int i = 0;

    //For UI
    [Header("UI")]
    public List<Sprite> p1CharacterList = new List<Sprite>();
    public Sprite wenIcon;
    public Sprite brockIcon;
    public Sprite jihoIcon;
    public Sprite katsukiIcon;

    public Image mainUI;
    public Image tagUI;

    public GameObject inventoryVisible;
    public GameObject indicatorVisible;

    public Text p1Name;
    public static string p1CurrentName;

    bool stopRotate;

    public ParticleSystem particle;


    void Start()
    {
        toRoof = false;

        destination = transform.position;
        knockback = false;
        myAgent = GetComponent<NavMeshAgent>();
        view = GetComponent<PhotonView>();
        myAnimator = gameObject.GetComponent<Animator>();
        lineRenderer = GetComponent<LineRenderer>();

        StartCoroutine(waitFoeSec());
        StartCoroutine(WhenGameStart());

        inventoryVisible.SetActive(false);
        indicatorVisible.SetActive(false);

        //reset
        sl_PlayerHealth.playerDead = false;
        myAnimator.SetFloat("Blend", 0f);
        GetAnimation();

        if (view.IsMine)
        {
            p1Name.text = PhotonNetwork.NickName;
            view.RPC("p1NickName", RpcTarget.All, p1Name.text);

        }


    }

    void Update()
    {
        if (p1CharacterList != null)
        {
            mainUI.sprite = p1CharacterList[0];
            tagUI.sprite = p1CharacterList[1];
        }


        if (startTheGame == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (view.IsMine)  //Photon - check is my character
            {
                inventoryVisible.SetActive(true);
                indicatorVisible.SetActive(true);

                //Hold to move
                #region
                //if (Input.GetMouseButton(1) && sl_ShootBehavior.p1Shoot == false)
                //{
                //    if (Physics.Raycast(ray, out hit))
                //    {
                //        if (Vector3.Distance(transform.position, hit.point) > 1.0)
                //        {
                //            myAgent.SetDestination(hit.point);

                //            view.RPC("PlayerMove", RpcTarget.All, hit.point);
                //            isrunning = true;
                //        }
                //    }

                //}

                //if (Input.GetMouseButtonUp(1))
                //{
                //    detectAndStop = false;
                //    myAgent.isStopped = true;
                //    myAgent.ResetPath();
                //}

                #endregion


                if (Input.GetMouseButtonDown(1) && sl_ShootBehavior.p1Shoot == false)
                {
                    if (Physics.Raycast(ray, out hit))
                    {
                        myAgent.SetDestination(hit.point);
                        isrunning = true;
                    }

                }

            }
            else
            {
                inventoryVisible.SetActive(false);
                indicatorVisible.SetActive(false);

            }

            //DrawLine();

            //stop when shoot
            if (sl_ShootBehavior.p1Shoot == true || !DishEffect.canMove)
            {
                wenTrail.SetActive(false);

                stopping = true;
                myAgent.isStopped = true;
                myAgent.ResetPath();

            }

            //particle system
            #region
            if (myAgent.velocity.magnitude < 0.1f)
            {
                wenTrail.SetActive(false);
            }
            else
            {
                if (sl_PlayerHealth.currentHealth > 4 && changeModelAnim == 1)
                {
                    wenTrail.SetActive(true);
                    particle.Play();
                    myAgent.speed = 48; //stat: wen increase 20% speed when more than half heart, original = 40
                }
                else if (sl_PlayerHealth.currentHealth < 4 && changeModelAnim == 1)
                {
                    wenTrail.SetActive(false);
                    myAgent.speed = 40;
                }
                else if (changeModelAnim == 3)
                {
                    myAgent.speed = 28;
                }
                else
                {
                    myAgent.speed = 40;
                }

            }
            #endregion

            if (gameObject.tag == "Player" && view.IsMine && !stopRotate)
            {
                Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
                float rayLength;

                if (groundPlane.Raycast(ray, out rayLength))
                {
                    Vector3 pointToLook = ray.GetPoint(rayLength);
                    view.RPC("PlayerRotate", RpcTarget.All, pointToLook);

                }

            }


            if (Input.GetKeyDown(KeyCode.W) && gameObject.tag == "Player" && view.IsMine)
            {
                view.RPC("SyncCharacterUIAndModel", RpcTarget.All);
            }

            //  Unused region -- offmesh link
            #region
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
            #endregion
        }


        //ANIMATION PART
        #region

        if (PhotonNetwork.IsMasterClient && myAnimator != null)
        {
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
               

            //modal change start from here
            if (changeModelAnim == 0)
            {
                myAnimator = brock_Animator;
                GetAnimation();

            }
            if (changeModelAnim == 1)
            {
                myAnimator = wen_Animator;
                GetAnimation();

            }

            if (changeModelAnim == 2)
            {
                myAnimator = jiho_Animator;
                GetAnimation();

            }

            if (changeModelAnim == 3)
            {
                myAnimator = katsuki_Animator;
                GetAnimation();
               
            }

        }
        #endregion

        //Areamask for hold to move
            #region
            /*
            int areaMask = myAgent.areaMask;

            if (toRoof)
            {
                areaMask -= 2 << NavMesh.GetAreaFromName("Roof");
                myAgent.areaMask = areaMask;
            }


            if (!toRoof)
            {
                areaMask += 2 << NavMesh.GetAreaFromName("Roof"); //turn off roof
                myAgent.areaMask = areaMask;
            }
            */
            #endregion
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ToRoofArea") //stair
        {
            toRoof = true;
        }
        if (other.gameObject.tag == "OffRoof")
        {
            toRoof = false;
        }

    }
    */

    //-----RPC Area-----
    #region

    //For Character model
    //0.brock, 1.wen, 2.jiho, 3.katsuki
    #region
    [PunRPC]
    public void Brock()
    {
        changeModelAnim = 0;
        //myAgent.speed = 40;

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
        changeModelAnim = 1;
        //myAgent.speed = 48;

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
        changeModelAnim = 2;
        //myAgent.speed = 40;

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
        changeModelAnim = 3;
        //myAgent.speed = 28;

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


    //For UI SYNC
    #region
    public void Move<T>(List<T> list, int oldIndex, int newIndex)
    {
        T item = list[oldIndex];
        list.RemoveAt(oldIndex);
        list.Insert(newIndex, item);
    }


    [PunRPC]
    public void SyncCharacterUIAndModel()
    {
        if (i == 0)
        {
            //change ui
            Move(p1CharacterList, 0, 1);

            i = 1;
            //change mode
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
                Move(p1CharacterList, 1, 0);
                i = 0;
                //change mode
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
    #endregion


    //Player movement Control
    #region
    [PunRPC]
    public void PlayerRotate(Vector3 look)
    {
        //Rotate player
        transform.LookAt(new Vector3(look.x, transform.position.y, look.z));

    }

    [PunRPC]
    public void PlayerMove(Vector3 dest)
    {
        myAgent.SetDestination(dest);
    }


    #endregion

    #endregion

    public void GetAnimation()
    {
        if (isrunning && sl_ShootBehavior.p1Shoot == false && !throwing) //run
        {
            myAnimator.SetFloat("Blend", 0.5f);
        }
        else
        {
            if (!throwing)
            {
                myAnimator.SetFloat("Blend", 0f);
            }
        }

        if (Input.GetMouseButton(0) && sl_ShootBehavior.p1Shoot == true && stopping) //aim
        {
            myAnimator.SetFloat("Blend", 1f);
            throwing = true;
        }
        if (Input.GetMouseButtonUp(0) && throwing && stopping) //throw
        {
            stopping = true;
            isrunning = false;
            myAnimator.SetFloat("Blend", 1.5f);
            StartCoroutine(ThrowTime());
        }


        if(sl_PlayerHealth.getDamage == true && sl_PlayerHealth.currentHealth > 0)
        {
            stopRotate = true;
            myAgent.isStopped = true;
            throwing = false;

            animName = "GetDmg";
            myAnimator.SetBool(animName, true);

            StartCoroutine(DamageTime());
        }

        if(sl_PlayerHealth.playerDead == true)
        {
            animName = "isPlayerDead";
            myAnimator.SetBool(animName, true);

            myAgent.isStopped = true;
            throwing = false;
            stopRotate = true;

            StartCoroutine(ResetDead());
        }
    }

    IEnumerator ResetDead()
    {
        yield return new WaitForSeconds(4.0f);
        sl_PlayerHealth.playerDead = false;
        GetAnimation();
    }

    [PunRPC]
    public void p1NickName(string name)
    {
        p1Name.text = name;
    }

    IEnumerator ThrowTime()
    {
        yield return new WaitForSeconds(0.3f);
        //myAnimator.SetFloat("Blend", 0f);
        throwing = false;
    }

    IEnumerator DamageTime()
    {
        yield return new WaitForSeconds(1.0f);
        myAnimator.SetBool("GetDmg", false);

        myAgent.isStopped = false;
        stopRotate = false;
    }

    IEnumerator waitFoeSec()
    {
        yield return new WaitForSeconds(0.2f);

        //Show model when in game
        if (sl_P1CharacterSelect.p1_firstCharacter == 0)
        {
            view.RPC("Brock", RpcTarget.All);
            mainCharacter = 1;
            p1CharacterList[0] = brockIcon;

        }
        if (sl_P1CharacterSelect.p1_firstCharacter == 1)
        {
            view.RPC("Wen", RpcTarget.All);
            mainCharacter = 2;
            p1CharacterList[0] = wenIcon;


        }
        if (sl_P1CharacterSelect.p1_firstCharacter == 2)
        {
            view.RPC("Jiho", RpcTarget.All);
            mainCharacter = 3;
            p1CharacterList[0] = jihoIcon;


        }
        if (sl_P1CharacterSelect.p1_firstCharacter == 3)
        {
            view.RPC("Katsuki", RpcTarget.All);
            mainCharacter = 4;
            p1CharacterList[0] = katsukiIcon;


        }

        //tag character
        if (sl_P1CharacterSelect.p1_secondCharacter == 0)
        {
            tagCharacter = 1;
            p1CharacterList[1] = brockIcon;

        }
        if (sl_P1CharacterSelect.p1_secondCharacter == 1)
        {
            tagCharacter = 2;
            p1CharacterList[1] = wenIcon;

        }
        if (sl_P1CharacterSelect.p1_secondCharacter == 2)
        {
            tagCharacter = 3;
            p1CharacterList[1] = jihoIcon;

        }
        if (sl_P1CharacterSelect.p1_secondCharacter == 3)
        {
            tagCharacter = 4;
            p1CharacterList[1] = katsukiIcon;

        }

    }

    IEnumerator WhenGameStart()
    {
        yield return new WaitForSeconds(4f); //add more 1sec for ui loading
        startTheGame = true;

        p1CurrentName = p1Name.text;

    }

    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //if (stream.IsWriting)
        //{
        //    stream.SendNext(transform.position);
        //    stream.SendNext(transform.rotation);

        //}
        //else if (stream.IsReading)
        //{
        //    transform.position = (Vector3)stream.ReceiveNext();
        //    transform.rotation = (Quaternion)stream.ReceiveNext();

        //    //float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
        //    //myAgent.transform.position += myAgent.velocity * lag;
        //}
    }


    public void DrawLine()
    {
        if (myAgent.path.corners.Length < 2) return;

        int i = 1;
        while (i < myAgent.path.corners.Length)
        {
            lineRenderer.positionCount = myAgent.path.corners.Length;
            point = myAgent.path.corners.ToList();

            for (int j = 0; j < point.Count; j++)
            {
                lineRenderer.SetPosition(j, point[j]);
            }
            i++;
        }
    }

}
