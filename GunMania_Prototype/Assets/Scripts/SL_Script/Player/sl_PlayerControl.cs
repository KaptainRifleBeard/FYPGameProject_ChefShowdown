using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class sl_PlayerControl : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float gravity;
    public float rotateSpeed;

    CharacterController characterController;
    Vector3 direction;
    Vector3 targetPosition;
    Vector3 mousePos;

    PhotonView view;

    public GameObject cursor;

    public bool isGrounded;


    //NavMesh AI movement for click to move
    public LayerMask whatCanBeClickOn;
    private NavMeshAgent myAgent;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        myAgent = GetComponent<NavMeshAgent>();
    }


    void Start()
    {
        view = GetComponent<PhotonView>();
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

            //to clear the food list in ui
            if (PhotonNetwork.IsMasterClient)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    sl_InventoryManager.ClearAllInList();
                }

            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    
                    sl_p2InventoryManager.ClearAllInList();
                }

            }


            //Look at mouse            
            Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f));

            //calculate the line between mouse and cam with xz
            float t = Camera.main.transform.position.y / (Camera.main.transform.position.y - point.y);
            Vector3 finalPoint = new Vector3(t * (point.x - Camera.main.transform.position.x) +  Camera.main.transform.position.x, 1.0f, 
                                             t * (point.z - Camera.main.transform.position.z) + Camera.main.transform.position.z);
            
            //Rotating the object to that point
            transform.LookAt(finalPoint, Vector3.up);
        }
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

    
}
