using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [Range(0f, 10f)]
    [SerializeField] private float waypointSize = 1f;

    public bool endWaypoint = false;

    private void OnDrawGizmos()
    {
        foreach(Transform t in transform)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(t.position, waypointSize);
        }

        Gizmos.color = Color.red;
        for (int i=0; i < transform.childCount - 1; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }

        Gizmos.DrawLine(transform.GetChild(transform.childCount - 1).position, transform.GetChild(0).position);
    }

    public Transform GetNextWaypoint(Transform currentWaypoint, GameObject movingObject)
    {
        //go to first waypoint
        if (currentWaypoint == null)
        {
            return transform.GetChild(0);
        }
        if (movingObject.CompareTag("Chase"))
        {
            if (currentWaypoint.GetSiblingIndex() >= transform.childCount - 1)
            {
                return transform.GetChild(currentWaypoint.GetSiblingIndex());
            }
        }
        // If it's the last waypoint, go back to the first waypoint
        if (currentWaypoint.GetSiblingIndex() < transform.childCount - 1)
        {
            return transform.GetChild(currentWaypoint.GetSiblingIndex() + 1);
        }
        else
        {
            return transform.GetChild(0);
        }
        



    }
}
