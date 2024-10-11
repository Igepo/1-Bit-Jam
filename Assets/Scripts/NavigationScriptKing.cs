using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using System;

public class NavigationScriptKing : MonoBehaviour
{
    public Transform[] waypoints;
    public TextMeshProUGUI progressText;

    public AudioClip[] moveSounds;
    public AudioSource moveAudioSource;

    public static event Action OnVictory;

    private NavMeshAgent agent;
    private int waypointIndex = 0;
    private Vector3 target;

    private bool hasReachedLastWaypoint = false;
    private float totalPathLength;
    private Vector3 startPosition;
    private float traveledDistance = 0f;

    public float pauseDuration = 1f;
    public float moveDuration = 1f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        agent.avoidancePriority = 0;
        agent.updateRotation = false;

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
            StartCoroutine(MoveWithPauses());
        }
    }

    IEnumerator MoveWithPauses()
    {
        while (Vector3.Distance(transform.position, target) > 0.5f)
        {
            agent.isStopped = false;

            yield return new WaitForSeconds(moveDuration);

            PlayRandomMoveSound();

            agent.isStopped = true;
            yield return new WaitForSeconds(pauseDuration);
        }

        IterateWaypointIndex();
    }

    void IterateWaypointIndex()
    {
        waypointIndex++;
        Debug.Log("waypointindex : " + waypointIndex);
        if (waypointIndex >= waypoints.Length)
        {
            hasReachedLastWaypoint = true;
            agent.isStopped = true;

            OnVictory?.Invoke();
        }
        else
        {
            UpdateDestination();
        }
    }

    void PlayRandomMoveSound()
    {
        if (moveSounds.Length > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, moveSounds.Length);
            moveAudioSource.clip = moveSounds[randomIndex];
            moveAudioSource.Play();
        }
    }
}
