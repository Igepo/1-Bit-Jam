using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailPath : MonoBehaviour
{
    // Script to show the path of the king

    public NavigationScriptKing navigationScriptKing;
    public float speed = 50;

    private Transform kingPosition;
    private Vector3 target;
    private int index = 0;
    private Transform[] waypoints;

    private Transform waypointNextTarget;

    private void Start()
    {
        kingPosition = navigationScriptKing.gameObject.transform;
        transform.position = kingPosition.position;
        waypoints = navigationScriptKing.GetWaypoints();

        target = waypoints[index].position;
        StartCoroutine(MoveToTarget());
    }
    void Update()
    {
        //foreach (var waypoint in waypoints) {
        //    transform.position = Vector3.Lerp(transform.position, waypoint.position, speed * Time.deltaTime);
        //}
        for (int i = 0; i < waypoints.Length - 1; i++) {
            Debug.DrawLine(waypoints[i].position, waypoints[i + 1].position, Color.red);
        }
    }

    IEnumerator MoveToTarget()
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, target) < 0.1f)
            {
                index++;

                if (index >= waypoints.Length)
                {
                    ParticleSystem trailParticleSystem = GetComponent<ParticleSystem>();

                    if (trailParticleSystem != null)
                        trailParticleSystem.Clear();

                    index = navigationScriptKing.GetNextWaypointIndex();
                    transform.position = kingPosition.position;
                }

                if (index < waypoints.Length)
                {
                    target = waypoints[index].position;
                }
            }

            yield return null;
        }
    }
}
