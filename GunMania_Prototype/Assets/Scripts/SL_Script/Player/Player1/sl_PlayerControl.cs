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
