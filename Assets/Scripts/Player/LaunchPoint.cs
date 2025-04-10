using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPoint : MonoBehaviour
{
    public Vector3 launchPoint;
    public GameObject launchObject; //Where to get launchPoint from

    [Header("POV and Lock On Test")]
    [SerializeField] private Camera lockOnCam;
    Plane[] planes;
    Collider objCollider;
    GameObject player;
    private void Start()
    {
        launchPoint = launchObject.transform.position; //transfer position to Vector

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
        }
        else
        {
            Debug.Log("Nothing has been detected");
            player.GetComponent<GrappleMovement>().canSeeGrapple = false;
        }
    }
}
