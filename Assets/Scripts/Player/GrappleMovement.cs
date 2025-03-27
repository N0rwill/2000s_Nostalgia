using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleMovement : MonoBehaviour
{
    [Header("References")]
    private PlayerMovement pm;
    public Transform cam;
    public Transform stickyhand;
    public LayerMask grapplePull;
    public LayerMask grappleLaunch;
    public LineRenderer lr;

    [Header("Grappling")]
    public float maxGrappleDistance;
    public float grappleDelayTime;

    private Vector3 grapplePoint;

    [Header("Cooldown")]
    public float grapplingCd;
    private float grapplingCdTimer;

    [Header("Input")]
    public KeyCode grappleKey = KeyCode.Mouse1;

    private bool grappling;

    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(grappleKey)) 
        { 
            StartGrapple();
        }

        if (grapplingCdTimer > 0) 
        { 
            grapplingCdTimer -= Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        if (grappling) 
        {
            lr.SetPosition(0, stickyhand.position);
        }
    }

    private void StartGrapple() 
    {
        if (grapplingCdTimer > 0) return;
        grappling = true;
        pm.freeze = true;

        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, grappleLaunch))
        {
            grapplePoint = hit.point;

            Invoke(nameof(ExecuteGrappleLaunch), grappleDelayTime);
        }
        else if (Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, grapplePull))
        {
            grapplePoint = hit.point;

            Invoke(nameof(ExecuteGrapplePull), grappleDelayTime);
        }
        else 
        {
            grapplePoint = cam.position + cam.forward * maxGrappleDistance;

            Invoke(nameof(StopGrapple), grappleDelayTime);
        }

        lr.enabled = true;
        lr.SetPosition(1, grapplePoint);
    }

    private void ExecuteGrapplePull() 
    {
        pm.freeze = false;
    }

    private void ExecuteGrappleLaunch() 
    {
        pm.freeze = false;
    }

    private void StopGrapple() 
    {
        pm.freeze = false;
        grappling = false;
        grapplingCdTimer = grapplingCd;
        lr.enabled = false;
    }
}
