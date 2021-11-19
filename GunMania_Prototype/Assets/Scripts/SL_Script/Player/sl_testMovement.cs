using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sl_testMovement : MonoBehaviour
{
    public float speed;
    Vector3 destination;

    private void Start()
    {
        destination = transform.position;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButton(1))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (Vector3.Distance(transform.position, hit.point) > 1.0)
                {
                    destination = hit.point;
                    Movement();
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
            transform.Translate(move.x, 0, move.z);

        }


    }
}
