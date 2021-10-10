using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Photon.Pun;

public class sl_PlayerControl : MonoBehaviour
{
    public Text healthText;

    //NavMesh AI movement for click to move
    private NavMeshAgent myAgent;
    PhotonView view;

    //NEW MOVEMENT VARIABLE
    public GameObject targetDestionation;

    //HEALTH
    
    [Space(10)] [Header("Health")]
    private int maxHealth = 16;
    public static int currentHealth = 0;

    public GameObject bulletScript;

    private void Awake()
    {
        myAgent = GetComponent<NavMeshAgent>();
    }


    void Start()
    {
        view = GetComponent<PhotonView>();
        currentHealth = maxHealth;
    }

    public void Update()
    {
        healthText.text = currentHealth.ToString();

        if (view.IsMine)  //Photon - check is my character
        {
            //NEW MOVEMENT - current using
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Input.GetMouseButtonDown(1))
            {
                if (Physics.Raycast(ray, out hit))
                {
                    targetDestionation.transform.position = hit.point;
                    myAgent.SetDestination(hit.point);

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


            if(currentHealth == 0)
            {
                Destroy(gameObject);
            }
        }
    }


    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(currentHealth);
        }
        else if (stream.IsReading)
        {
            currentHealth = (int)stream.ReceiveNext();
        }

    }

    [PunRPC]
    public void BulletDamage()
    {
        currentHealth -= bulletScript.GetComponent<sl_BulletScript>().bulletDmg;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Bullet")
        {
            if(view.IsMine)
            {
                view.RPC("BulletDamage", RpcTarget.All);

            }

        }
    }

    //************* OLD CODE *************//
    /*
     
    
    CharacterController characterController;
    Vector3 direction;
    Vector3 targetPosition;
    Vector3 mousePos;

    public float speed;
    public float jumpForce;
    public float gravity;
    public float rotateSpeed;

    public GameObject cursor;
    public bool isGrounded;


    public sl_Inventory playerInventory;  //set which inventory should be place in

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        myAgent = GetComponent<NavMeshAgent>();
    }


    void Start()
    {
        view = GetComponent<PhotonView>();
    }
   
    private bool PlayerJumped => characterController.isGrounded && Input.GetKey(KeyCode.Space);

    public void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(horizontal, 0, vertical);
        Vector3 moveDirection = transform.TransformDirection(inputDirection);

        Vector3 movement = speed * Time.deltaTime * moveDirection;
        direction = new Vector3(movement.x, movement.y, movement.z);

        if (PlayerJumped)
        {
            direction.y = jumpForce;
        }
        else if (characterController.isGrounded)
        {
            direction.y = 0f;
        }
        else
        {
            direction.y -= gravity * Time.deltaTime;
        }

        characterController.Move(direction);


        //rotate player
        if (Vector3.Distance(transform.position, cursor.transform.position) >= 5f) //vector3.distance
        {
            //Quaternion toRotate = Quaternion.LookRotation(direction, Vector3.up);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotateSpeed * Time.deltaTime);

            transform.LookAt(cursor.transform.position);

        }
    }

    public void MoveToClickLocation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            myAgent.SetDestination(hit.point);

            //targetPosition = hit.point;
            //this.transform.LookAt(targetPosition);

        }
    }

    public void Update()
    {
        if (view.IsMine)  //Photon - check is my character
        {

            //Movement();
            if (Input.GetMouseButton(1))
            {
                MoveToClickLocation();
            }


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;

            if (groundPlane.Raycast(ray, out rayLength))
            {
                Vector3 pointToLook = ray.GetPoint(rayLength);
                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }
        }
    }
    */
}
