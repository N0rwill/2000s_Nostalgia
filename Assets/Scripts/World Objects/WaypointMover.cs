using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    //reference to the waypoint script
    [SerializeField] private Waypoint waypoints;

    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private float distanceThreshold = 0.5f;

    [SerializeField] public float spawnDelay = 0f;

    //waypoint target object moving to
    private Transform currentWaypoint;

    public GameObject chase;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAfterDelay());
    }

    private IEnumerator SpawnAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(spawnDelay);

        // Initialize the chase object
        chase = GameObject.FindWithTag("Chase");

        // Set initial position
        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint, gameObject);
        transform.position = currentWaypoint.position;

        // Set next target
        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint, gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        if (currentWaypoint == null)
        {
            return;
        }

        // Move towards the current waypoint
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);


        if (Vector3.Distance(transform.position, currentWaypoint.position) < distanceThreshold)
        {
            currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint, gameObject);

            // If no next waypoint is found, stop further movement
            if (currentWaypoint == null)
            {
                return;
            }
        }

        // Smoothly rotate towards the current waypoint
        Vector3 directionToWaypoint = currentWaypoint.position - transform.position;
        if (directionToWaypoint.sqrMagnitude > 0.001f) // Ensure the direction vector is not zero
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToWaypoint.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);
        }
    }

    
}
