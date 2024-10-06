using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class NavigationScriptKing : MonoBehaviour
{
    public Transform[] waypoints;
    public TextMeshProUGUI progressText;
    private NavMeshAgent agent;
    private int waypointIndex = 0;
    private Vector3 target;

    private bool hasReachedLastWaypoint = false;
    private float totalPathLength;
    private Vector3 startPosition;
    private float traveledDistance = 0f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        agent.avoidancePriority = 0;
        agent.updateRotation = false;
        agent.updatePosition = false;

        startPosition = transform.position;
        totalPathLength = GetTotalPathLength(waypoints);

        UpdateDestination();
    }

    private float GetTotalPathLength(Transform[] waypoints)
    {
        float totalLength = 0f;

        totalLength += Vector3.Distance(startPosition, waypoints[0].position);

        for (int i = 1; i < waypoints.Length; i++)
        {
            totalLength += Vector3.Distance(waypoints[i - 1].position, waypoints[i].position);
        }

        return totalLength;
    }

    private float GetDistanceTraveled(Vector3 agentPosition, Vector3 startPosition, Transform[] waypoints, int currentWaypointIndex)
    {
        float traveledDistance = 0f;

        if (currentWaypointIndex == 0)
        {
            traveledDistance = Vector3.Distance(startPosition, agentPosition);
            return traveledDistance;
        }

        traveledDistance += Vector3.Distance(startPosition, waypoints[0].position);

        for (int i = 1; i < currentWaypointIndex; i++)
        {
            traveledDistance += Vector3.Distance(waypoints[i - 1].position, waypoints[i].position);
        }

        if (currentWaypointIndex > 0 && currentWaypointIndex < waypoints.Length)
        {
            traveledDistance += Vector3.Distance(waypoints[currentWaypointIndex - 1].position, agentPosition);
        }

        return traveledDistance;
    }

    void Update()
    {
        if (hasReachedLastWaypoint)
            return;

        if (Vector3.Distance(transform.position, target) < 5f)
        {
            IterateWaypointIndex();
            UpdateDestination();
        }

        transform.position = agent.nextPosition;

        traveledDistance = GetDistanceTraveled(transform.position, startPosition, waypoints, waypointIndex);

        float percentageCompleted = (traveledDistance / totalPathLength) * 100f;
        int percentageCompletedInt = Mathf.RoundToInt(percentageCompleted);

        progressText.text = percentageCompletedInt + "%";
    }
    public int GetPercentageCompleted()
    {
        float percentageCompleted = (traveledDistance / totalPathLength) * 100f;
        return Mathf.RoundToInt(percentageCompleted);
    }

    void UpdateDestination()
    {
        if (waypointIndex < waypoints.Length)
        {
            target = waypoints[waypointIndex].position;
            agent.SetDestination(target);
        }
    }

    void IterateWaypointIndex()
    {
        waypointIndex++;
        if (waypointIndex >= waypoints.Length)
        {
            hasReachedLastWaypoint = true;
            agent.isStopped = true;
        }
    }
}
