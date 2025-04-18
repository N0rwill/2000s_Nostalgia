using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GrappleMovement : MonoBehaviour
{
    [Header("References")]
    private PlayerMovement playermove;
    public Transform cam;
    public Transform stickyhand;
    public LayerMask grapple;
    public LineRenderer lr;

    [Header("Grappling")]
    public float maxGrappleDistance;
    public float grappleDelayTime;
    public float overshootYAxis;
    public GameObject grappleObject;

    public Vector3 grapplePoint;
    public Vector3 launchToPoint;

    [Header("Pulling")]
    public float pullForce = 25f;

    [Header("Cooldown")]
    public float grapplingCd;
    private float grapplingCdTimer;

    [Header("Input")]
    public KeyCode grappleKey = KeyCode.Mouse1;

    [Header("Animation")]
    public Animator animator;

    private bool grappling;

    public bool canSeeGrapple = false;



    void Start()
    {

        playermove = GetComponent<PlayerMovement>();

    }

    void Update()
    {

        if (Input.GetKeyDown(grappleKey))
        {
            StartGrapple();
        }

        if (grapplingCdTimer > 0) //constant cooldown timer. When 0, can grapple again
        {
            grapplingCdTimer -= Time.deltaTime;

            playermove.freeze = false;
            grappling = false;
            lr.enabled = false;
            grapplePoint = cam.position + cam.forward * maxGrappleDistance;
        }
    }

    private void LateUpdate()
    {
        if (grappling)
        {
            lr.SetPosition(0, stickyhand.position); //set the line renderer from you to wherever the stickyhand connects while grappling.
        }
    }

    private void StartGrapple()
    {

        if (grapplingCdTimer > 0) return; //fails grapple if cooldown

        grappling = true;

        animator.SetTrigger("Grapple"); //play the grapple animation

        playermove.freeze = true; //freeze the player for dramatic effect

        RaycastHit hit;

        if (canSeeGrapple)
        {
            playermove.freeze = true; //freeze the player for dramatic effect

            Debug.Log("LAUNCHING");

            StartCoroutine(animationwaiterlaunch());
        }

        else if (Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, grapple)) //sends out a raycast to see if it hits a launch object, a pull object, or nothing.
        {
            playermove.freeze = true; //freeze the player for dramatic effect

            if (hit.collider.gameObject.CompareTag("GrapplePull"))
            {
                grapplePoint = hit.point; //Again, point where the grapple hits

                grappleObject = hit.collider.gameObject; //Again, game object that the player just hit with the grapple.

                transform.LookAt(grapplePoint); //Turns the character toward the point they nail.

                StartCoroutine(animationwaiterpull());

            }
            else
            {
                playermove.freeze = true; //freeze the player for dramatic effect

                grapplePoint = hit.point; //Again, point where the grapple hits

                transform.LookAt(grapplePoint); //Turns the character toward the point they nail.

                StartCoroutine(animationwaiterfail());
            }

        }

        else
        {
            playermove.freeze = true; //freeze the player for dramatic effect

            grapplePoint = cam.position + cam.forward * maxGrappleDistance; //If the player misses anything, just put the grapple out the max distance

            transform.LookAt(grapplePoint); //Turns the character toward the point they nail.

            StartCoroutine(animationwaiterfail());
        }
    }

    private void ExecuteGrapplePull()
    {
        playermove.freeze = false; //unfreeze the player

        grappleObject.GetComponent<GrapplePulling>().pullForce = pullForce; //Pull the game object at the force that the player has on it.
        grappleObject.GetComponent<GrapplePulling>().pull = true;

        Invoke(nameof(StopGrapple), 1f); //Only grapple for a little bit
    }

    private void ExecuteGrappleLaunch()
    {
        playermove.freeze = false; //unfreeze the player

        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z); //The bottom of the player

        float grapplePointRelativeYPos = launchToPoint.y - lowestPoint.y; //Calculate the difference between player Y Pos and grapple point Y Pos
        float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis; //Highest point on launch will be the difference between the two Y Pos, plus whatever arc you give it.

        if (grapplePointRelativeYPos < 0) highestPointOnArc = overshootYAxis; //If player is higher than the grapple point, just make the highest point and arc the same thing.

        playermove.LaunchToPosition(launchToPoint, highestPointOnArc); //Launch the player to the point along the arc (Calculation in Player Movement)

        Invoke(nameof(StopGrapple), 1f); //Only grapple for a little bit
    }

    public void StopGrapple()
    {
        playermove.freeze = false;
        grappling = false;
        grapplingCdTimer = grapplingCd;
        lr.enabled = false;

        if (grappleObject.GetComponent<GrapplePulling>() != null)
        {
            grappleObject.GetComponent<GrapplePulling>().pull = false;
            grappleObject.GetComponent<GrapplePulling>().pullForce = 0;
            grappleObject = null;

            //Just undo everything, and set the grapple cooldown

            playermove.freeze = false;
            grappling = false;
            grapplingCdTimer = grapplingCd;
            lr.enabled = false;
        }

        else if (grappleObject = null)
        {
            //Just undo everything, and set the grapple cooldown

            playermove.freeze = false;
            grappling = false;
            grapplingCdTimer = grapplingCd;
            lr.enabled = false;

            return;
        }

        else
        {
            //Just undo everything, and set the grapple cooldown

            playermove.freeze = false;
            grappling = false;
            grapplingCdTimer = grapplingCd;
            lr.enabled = false; return;
        }
    }

    public bool IsGrappling()
    {
        //Can't grapple while grappling
        return grappling;
    }

    IEnumerator animationwaiterlaunch()
    {
        yield return new WaitForSeconds(1);

        lr.SetPosition(1, grapplePoint); //Create a line renderer for the grapple, launching it out to the point the player connects with, or max distance.
        lr.enabled = true;

        Invoke(nameof(ExecuteGrappleLaunch), grappleDelayTime);
    }

    IEnumerator animationwaiterpull()
    {
        yield return new WaitForSeconds(1);

        lr.SetPosition(1, grapplePoint); //Create a line renderer for the grapple, launching it out to the point the player connects with, or max distance.
        lr.enabled = true;

        Invoke(nameof(ExecuteGrapplePull), grappleDelayTime);
    }

    IEnumerator animationwaiterfail()
    {
        yield return new WaitForSeconds(1);

        lr.SetPosition(1, grapplePoint); //Create a line renderer for the grapple, launching it out to the point the player connects with, or max distance.
        lr.enabled = true;

        Invoke(nameof(StopGrapple), grappleDelayTime);
    }
}
