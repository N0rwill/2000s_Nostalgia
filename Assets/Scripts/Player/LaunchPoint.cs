using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPoint : MonoBehaviour
{
    public GameObject launchObject; //Where to get launchPoint from

    [Header("POV and Lock On Test")]
    [SerializeField] private Camera lockOnCam;
    Plane[] planes;
    Collider objCollider;
    public GameObject player;
    public float maxRange = 25;

    private void Start()
    {
        objCollider = GetComponent<Collider>();
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < maxRange)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, (player.transform.position - transform.position), out hit, maxRange))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    // In Range and i can see you!

                    player = hit.collider.gameObject;

                    planes = GeometryUtility.CalculateFrustumPlanes(lockOnCam);

                    if (GeometryUtility.TestPlanesAABB(planes, objCollider.bounds))
                    {
                        Debug.Log(name + " has been detected!");
                        player.GetComponent<GrappleMovement>().canSeeGrapple = true;
                        player.GetComponent<GrappleMovement>().grappleObject = gameObject;
                        player.GetComponent<GrappleMovement>().grapplePoint = transform.position;
                        player.GetComponent<GrappleMovement>().launchToPoint = launchObject.transform.position; //transfer position to Vector3
                    }
                    else
                    {
                        Debug.Log("Nothing has been detected");
                        player.GetComponent<GrappleMovement>().canSeeGrapple = false;
                    }
                }
                else
                {
                    player = null;
                }
            }
            else 
            {
                player = null;
            }
        }
        else 
        {
            player.GetComponent<GrappleMovement>().canSeeGrapple = false;
            player = null;
        }
        
    }
}
