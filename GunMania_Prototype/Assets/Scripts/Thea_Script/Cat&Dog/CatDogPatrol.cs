using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class CatDogPatrol : MonoBehaviour
{
    public bool isCat;
    public List<Transform> catSpawnPoints;
    public List<Transform> dogSpawnPoints;
    int index;

    public List<Transform> Waypoint1;
    public List<Transform> Waypoint2;
    public List<Transform> Waypoint3;
    public List<Transform> Waypoint4;

    private int destPoint = 0;
    private NavMeshAgent agent;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Spawn1")
        {
            index = 1;
        }
        else if(other.gameObject.tag == "Spawn2")
        {
            index = 2;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;

        if (index == 1 && isCat)
        {
            GoToNextPoint1();
        }
        else if (index == 2 && isCat)
        {
            GoToNextPoint2();
        }
        else if (index == 1 && !isCat)
        {
            GoToNextPoint3();
        }
        else if (index == 2 && !isCat)
        {
            GoToNextPoint4();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            if(index == 1 && isCat)
            {
                GoToNextPoint1();
            }
            else if(index == 2 && isCat)
            {
                GoToNextPoint2();
            }
            else if(index == 1 && !isCat)
            {
                GoToNextPoint3();
            }
            else if(index == 2 && !isCat)
            {
                GoToNextPoint4();
            }
            
        }
            
    }

    void GoToNextPoint1()
    {
        if (Waypoint1.Count == 0)
        {
            return;
        }

        agent.destination = Waypoint1[destPoint].position;

        destPoint = (destPoint + 1) % Waypoint1.Count;
    }

    void GoToNextPoint2()
    {
        if (Waypoint2.Count == 0)
        {
            return;
        }

        agent.destination = Waypoint2[destPoint].position;

        destPoint = (destPoint + 1) % Waypoint2.Count;
    }

    void GoToNextPoint3()
    {
        if (Waypoint3.Count == 0)
        {
            return;
        }

        agent.destination = Waypoint3[destPoint].position;

        destPoint = (destPoint + 1) % Waypoint3.Count;
    }

    void GoToNextPoint4()
    {
        if (Waypoint4.Count == 0)
        {
            return;
        }

        agent.destination = Waypoint4[destPoint].position;

        destPoint = (destPoint + 1) % Waypoint4.Count;
    }
}
