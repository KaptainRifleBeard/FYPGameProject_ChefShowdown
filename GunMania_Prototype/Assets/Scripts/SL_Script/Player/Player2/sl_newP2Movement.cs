using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using UnityEngine.UI;

public class sl_newP2Movement : MonoBehaviour, IPunObservable
{
    bool startTheGame = false;
    int speed;

    private NavMeshAgent myAgent;
    PhotonView view;

    public GameObject inventoryVisible;
    public GameObject indicatorVisible;

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

    //Control model
    [Header("Model")]
    public GameObject[] BrockChoi;
    public GameObject[] OfficerWen;
    public GameObject[] AuntJiho;
    public GameObject[] MrKatsuki;

    public GameObject wenTrail;

    public static int changep2Icon = 0;

    //to define which is main and tag character
    int mainCharacter;
    int tagCharacter;
    int i = 0;

    //For UI
    [Header("UI")]
    public List<Sprite> p2CharacterList = new List<Sprite>();
    public Sprite wenIcon;
    public Sprite brockIcon;
    public Sprite jihoIcon;
    public Sprite katsukiIcon;

    public Image mainUI;
    public Image tagUI;

    public Text p2Name;
    public static string p2CurrentName;

    bool stopRotate;

    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        view = GetComponent<PhotonView>();
        myAnimator = GetComponent<Animator>();

        StartCoroutine(waitFoeSec());
        StartCoroutine(WhenGameStart());

        inventoryVisible.SetActive(false);
        indicatorVisible.SetActive(false);

        if (view.IsMine)
        {
            p2Name.text = PhotonNetwork.NickName;
            view.RPC("p2NickName", RpcTarget.All, p2Name.text);

        }
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

                            //view.RPC("PlayerMove2", RpcTarget.All, hit.point);
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


            if (sl_PlayerHealth.currentHealth > 4 && changep2Icon == 1)
            {
                wenTrail.SetActive(true);
                myAgent.speed = 48; //stat: wen increase 20% speed when more than half heart, original = 40
            }
            else if (sl_P2PlayerHealth.p2currentHealth < 4 && changep2Icon == 1)
            {
                wenTrail.SetActive(false);
                myAgent.speed = 40;
            }
            else if (changep2Icon == 3)
            {
                myAgent.speed = 28;
            }
            else
            {
                myAgent.speed = 40;
            }



        }

        //ANIMATION PART
        #region
        if (!PhotonNetwork.IsMasterClient && myAnimator != null)
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

            if (changep2Icon == 0)
            {
                myAnimator = brock_Animator;
                GetAnimation();

            }
            if (changep2Icon == 1)
            {
                myAnimator = wen_Animator;
                GetAnimation();

            }

            if (changep2Icon == 2)
            {
                myAnimator = jiho_Animator;
                GetAnimation();

            }

            if (changep2Icon == 3)
            {
                myAnimator = katsuki_Animator;
                GetAnimation();
            }
        }
        
        #endregion
    }


    public void GetAnimation()
    {
        if (isrunning && sl_P2ShootBehavior.p2Shoot == false && !throwing) //run
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

        if (Input.GetMouseButton(0) && sl_P2ShootBehavior.p2Shoot == true && stopping) //aim
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


        if (sl_P2PlayerHealth.getDamage2 == true && sl_P2PlayerHealth.p2currentHealth > 0)
        {
            stopRotate = true;
            myAgent.isStopped = true;
            throwing = false;

            animName = "GetDmg";
            myAnimator.SetBool(animName, true);

            StartCoroutine(DamageTime());
        }

        if (sl_P2PlayerHealth.player2Dead == true)
        {
            animName = "isPlayerDead";
            myAnimator.SetBool(animName, true);

            myAgent.isStopped = true;
            throwing = false;
            stopRotate = true;

        }
    }


    [PunRPC]
    public void p2NickName(string name)
    {
        p2Name.text = name;
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


    //For Character model
    //0.brock, 1.wen, 2.jiho, 3.katsuki
    #region
    [PunRPC]
    public void Brock2()
    {
        changep2Icon = 0;
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
    public void Wen2()
    {
        changep2Icon = 1;
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
    public void Jiho2()
    {
        changep2Icon = 2;
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
    public void Katsuki2()
    {
        changep2Icon = 3;
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

    IEnumerator waitFoeSec()
    {
        yield return new WaitForSeconds(0.1f);
        //Show model when in game
        if (sl_P2CharacterSelect.p2_firstCharacter == 0) 
        {
            view.RPC("Brock2", RpcTarget.All);
            mainCharacter = 1;
            p2CharacterList[0] = brockIcon;

        }
        if (sl_P2CharacterSelect.p2_firstCharacter == 1)
        {
            view.RPC("Wen2", RpcTarget.All);
            mainCharacter = 2;
            p2CharacterList[0] = wenIcon;

        }
        if (sl_P2CharacterSelect.p2_firstCharacter == 2)
        {
            view.RPC("Jiho2", RpcTarget.All);
            mainCharacter = 3;
            p2CharacterList[0] = jihoIcon;

        }
        if (sl_P2CharacterSelect.p2_firstCharacter == 3)
        {
            view.RPC("Katsuki2", RpcTarget.All);
            mainCharacter = 4;
            p2CharacterList[0] = katsukiIcon;

        }

        //tag character
        if (sl_P2CharacterSelect.p2_secondCharacter == 0)
        {
            tagCharacter = 1; 
            p2CharacterList[1] = brockIcon;

        }
        if (sl_P2CharacterSelect.p2_secondCharacter == 1)
        {
            tagCharacter = 2; 
            p2CharacterList[1] = wenIcon;

        }
        if (sl_P2CharacterSelect.p2_secondCharacter == 2)
        {
            tagCharacter = 3; 
            p2CharacterList[1] = jihoIcon;

        }
        if (sl_P2CharacterSelect.p2_secondCharacter == 3)
        {
            tagCharacter = 4; 
            p2CharacterList[1] = katsukiIcon;

        }

    }

    IEnumerator WhenGameStart()
    {
        yield return new WaitForSeconds(3f);
        startTheGame = true;

        p2CurrentName = p2Name.text;

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
}
