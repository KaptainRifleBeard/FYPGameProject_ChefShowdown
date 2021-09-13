using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sl_PlayerControl : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float gravity;

    CharacterController characterController;
    Vector3 direction;

    public bool isGrounded;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }


    void Start()
    {
        
    }


    void FixedUpdate()
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
        else if(characterController.isGrounded)
        {
            direction.y = 0f;
        }
        else
        {
            direction.y -= gravity * Time.deltaTime;
        }

        characterController.Move(direction);

    }



    private bool PlayerJumped => characterController.isGrounded && Input.GetKey(KeyCode.Space);
}
