using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationScriptKing : MonoBehaviour
{
    public Transform[] waypoints;


    //private Transform player;
    private NavMeshAgent agent;
    private int waypointIndex = 0;
    Vector3 target;

    private void Awake()
    {
        //player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        agent.avoidancePriority = 0; // Doesn't avoid other agents
        agent.updateRotation = false;
        agent.updatePosition = false;

        UpdateDestination();
    }

    void Update()
    {        
        //agent.destination = target.position;
        if (Vector3.Distance(transform.position, target) < 5f)
        {
            IterateWaypointIndex();
            UpdateDestination();
        }
        transform.position = agent.nextPosition;
    }

    void UpdateDestination()
    {
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
    }

    void IterateWaypointIndex()
    {
        waypointIndex++;
        if(waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }
}
