using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationScriptPawn : MonoBehaviour
{
    public Transform[] waypoints;


    private Transform target;
    private NavMeshAgent agent;

    private void Awake()
    {
        target = GameObject.FindWithTag("Ally").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        agent.updateRotation = false;
        agent.updatePosition = false;
    }

    void Update()
    {
        agent.SetDestination(target.position);

        if (agent.isOnNavMesh)
        {
            transform.position = agent.nextPosition;
        }
    }
}
