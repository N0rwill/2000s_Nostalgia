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
    private void Start()
    {
         

        objCollider = GetComponent<Collider>();
    }
    private void Update()
    {
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
}
