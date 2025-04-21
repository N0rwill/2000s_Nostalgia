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

    private GameObject clone;
    public int current = 0;
    public int max;


    //waypoint target object moving to
    private Transform currentWaypoint;

    
    void Start()
    {

        StartCoroutine(SpawnObjectsAfterDelay());
                
    }

    private IEnumerator SpawnObjectsAfterDelay()
    {
        
        // Wait for the specified delay
        yield return new WaitForSeconds(spawnDelay);

        // Spawn a new GameObject
        clone = Instantiate(gameObject, transform.position, Quaternion.identity);
        current++;
            

        // Initialize the WaypointMover on the new object
        WaypointMover mover = clone.GetComponent<WaypointMover>();
        if (mover != null)
        {
            mover.Initialize(waypoints, moveSpeed, distanceThreshold);
        }
        
    }

    public void Initialize(Waypoint waypoints, float moveSpeed, float distanceThreshold)
    {
        this.waypoints = waypoints;
        this.moveSpeed = moveSpeed;
        this.distanceThreshold = distanceThreshold;

        // Set initial position
        currentWaypoint = waypoints.GetNextWaypoint(null, gameObject);
        transform.position = currentWaypoint.position;

        // Set next target
        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint, gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (current >= max)
        {
            StopAllCoroutines();
        }

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
            if (waypoints.endWaypoint)
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

    /*public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine); // Stop the coroutine
            //spawnCoroutine = null; // Clear the reference
            Debug.Log("Spawn coroutine stopped.");
        }
    }*/



}
