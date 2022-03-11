using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sl_testMovement : MonoBehaviour
{
    Rigidbody rb;

    public float speed;
    Vector3 destination;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        destination = transform.position;
    }

    void Update()
    {
        Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButton(1))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (Vector3.Distance(transform.position, hit.point) > 1.0)
                {

                    rb.MovePosition(Vector3.MoveTowards(transform.position, hit.point, speed * Time.deltaTime));

                    //destination = hit.point;
                    //Movement();
                }
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
            rb.MovePosition(move * Time.deltaTime * speed);

            //transform.Translate(move.x, 0, move.z);

        }


    }
}
