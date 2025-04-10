using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    //reference to the waypoint script
    [SerializeField] private Waypoint waypoints;

    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private float distanceThreshold = 0.5f;

    //waypoint target object moving to
    private Transform currentWaypoint;


    // Start is called before the first frame update
    void Start()
    {
        //set initial position
        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint, gameObject);
        transform.position = currentWaypoint.position;

        //Set next target
        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint, gameObject);
        

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, currentWaypoint.position) < distanceThreshold)
        {
            currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint, gameObject);
            
        }

        // Smoothly rotate towards the current waypoint
        Vector3 directionToWaypoint = (currentWaypoint.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(directionToWaypoint);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);
    }
}
