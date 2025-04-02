using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float wheelySpeed;
    public float speedTransitionSpeed = 10f;
    private float currentTargetSpeed;

    public float GroundDrag;

    [Header("Sprint")]
    private bool isSprinting;

    [Header("Wheely")]
    public bool hasWheely;
    private bool isWheelying;

    [Header("Jump")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    private bool canJump;
    public float fallMultiplier;
    public float fallMultiplierTransitionSpeed;
    private float fallTimer = 0f;
    public float maxFallSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode slowKey = KeyCode.LeftControl;
    private bool canSlowDown;
    public float slowDownCooldown;

    [Header("gruond Check")]
    public float playerHeight;
    public LayerMask groundMask;
    [SerializeField] bool isGrounded;

    [Header("Slope Handler")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    [Header("Grapple Mechanics")]
    public bool freeze;
    public bool activeGrapple;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;

    public enum MovementState
    {
        freeze,
        walking,
        sprinting,
        wheelying,
        air
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        // settings on start
        canJump = true;
        canSlowDown = true;

        moveSpeed = walkSpeed;
        currentTargetSpeed = walkSpeed;

        isSprinting = false;
        isWheelying = false;
    }

    private void Update()
    {
        // ground check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, groundMask);

        MyInput();
        SpeedContorl();
        StateHandler();
        ApplyFallMultiplier();
        ClampFallSpeed();

        PlayerActiveSpeed();

        // drag handler
        if (isGrounded && !activeGrapple)
        {
            rb.drag = GroundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // jump
        if (Input.GetKey(jumpKey) && canJump && isGrounded)
        {
            canJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // speed progression: walking -> sprinting -> wheelying
        if (Input.GetKeyDown(sprintKey))
        {
            if (!isSprinting && !isWheelying)
            {
                // Start sprinting
                isSprinting = true;
            }
            else if (isSprinting && hasWheely)
            {
                // Start wheelying
                isWheelying = true;
                isSprinting = false;
            }
        }
        // Handle slow key
        else if (Input.GetKeyDown(slowKey) && canSlowDown)
        {
            if (isWheelying)
            {
                // wheely -> sprint
                isWheelying = false;
                isSprinting = true;
                Invoke(nameof(ResetSlowDownKey), slowDownCooldown);
            }
            else if (isSprinting)
            {
                // sprint -> walk
                isSprinting = false;
                Invoke(nameof(ResetSlowDownKey), slowDownCooldown);
            }
        }
        // Reset to walking if no movement
        else if (verticalInput <= 0)
        {
            isSprinting = false;
            isWheelying = false;
        }
    }

    private void StateHandler()
    {
        // state frozen from grappeling
        if (freeze)
        {
            state = MovementState.freeze;
            currentTargetSpeed = 0;
            rb.velocity = Vector3.zero;
            Debug.Log("Player is frozen");
        }
        // state wheely
        else if (isWheelying)
        {
            state = MovementState.wheelying;
            currentTargetSpeed = wheelySpeed;
            Debug.Log("Player is wheelying");
        }
        // state sprinting
        else if (isSprinting)
        {
            state = MovementState.sprinting;
            currentTargetSpeed = sprintSpeed;
            Debug.Log("Player is sprinting");
        }
        // state walking
        else if (isGrounded)
        {
            state = MovementState.walking;
            currentTargetSpeed = walkSpeed;
            Debug.Log("Player is walking");
        }
        // state air
        else if (!isGrounded)
        {
            state = MovementState.air;
        }

        // turn off gravity when on slope
        rb.useGravity = !OnSlope();
    }

    private void MovePlayer()
    {
        // Can't move if grappling
        if (activeGrapple) return;

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on slope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
            {
                rb.AddForce(Vector3.down * 50f, ForceMode.Force);
            }

            // if player is not holding a directional input, slow to a stop
            if (horizontalInput == 0 && verticalInput == 0 && isGrounded)
            {
                rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, 1f);
            }
        }

        // on ground
        else if (isGrounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (!isGrounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier, ForceMode.Force);
    }

    private void SpeedContorl()
    {
        // Can't move if grappling
        if (activeGrapple) return;

        // Smoothly transition to target speed
        moveSpeed = Mathf.Lerp(moveSpeed, currentTargetSpeed, Time.deltaTime * speedTransitionSpeed);

        // limit speed on slope
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = Vector3.Lerp(rb.velocity, rb.velocity.normalized * moveSpeed, Time.deltaTime * speedTransitionSpeed);
        }

        // limit speed on ground or in air
        else
        {
            Vector3 flatVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            // limit velocity if needed
            if (flatVelocity.magnitude > moveSpeed)
            {
                Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
                rb.velocity = Vector3.Lerp(rb.velocity, new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z), Time.deltaTime * speedTransitionSpeed);
            }
        }
    }

    private void Jump()
    {
        exitingSlope = true;

        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        canJump = true;
        exitingSlope = false;
    }

    private void ResetSlowDownKey()
    {
        canSlowDown = true;
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

    private void ApplyFallMultiplier()
    {
        if (!isGrounded && !activeGrapple && rb.velocity.y < 0)
        {
            // Increment fall timer when falling
            fallTimer += Time.deltaTime;

            // Use a smoother curve for the transition (easing function)
            float t = 1 - Mathf.Pow(0.5f, fallTimer * fallMultiplierTransitionSpeed);
            float currentMultiplier = Mathf.Lerp(1f, 1f + fallMultiplier, t);

            // Apply multiplier directly to gravity instead of adding force
            Physics.gravity = new Vector3(0, -9.81f * currentMultiplier, 0);
        }
        else
        {
            // Reset the timer and gravity when not falling
            fallTimer = 0f;
            Physics.gravity = new Vector3(0, -9.81f, 0);
        }
    }

    private void ClampFallSpeed()
    {
        if (!isGrounded && rb.velocity.y < -maxFallSpeed)
        {
            // Clamp the vertical velocity to the maximum fall speed
            rb.velocity = new Vector3(rb.velocity.x, -maxFallSpeed, rb.velocity.z);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = Vector3.down * (playerHeight * 0.5f + 0.3f);
        Gizmos.DrawRay(rayOrigin, rayDirection);
    }

    public void PlayerActiveSpeed()
    {
        // calculate the speed of the player
        float speed = rb.velocity.magnitude;
        Debug.Log("Player is moving at " + speed + "m/s");
    }



    // GRAPPLE MECHANICS
    private bool enableMovementOnNextTouch;

    public void LaunchToPosition(Vector3 targetPosition, float trajectoryHeight)
    {
        activeGrapple = true;

        velocityToSet = CalculateLaunchVelocity(transform.position, targetPosition, trajectoryHeight);
        Invoke(nameof(SetVelocity), 0.1f);

        Invoke(nameof(ResetRestrictions), 3f);
    }

    public void ResetRestrictions()
    {
        activeGrapple = false;
    }

    private Vector3 velocityToSet;

    private void SetVelocity()
    {
        enableMovementOnNextTouch = true;
        rb.velocity = velocityToSet;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (enableMovementOnNextTouch)
        {
            enableMovementOnNextTouch = false;
            ResetRestrictions();

            GetComponent<GrappleMovement>().StopGrapple();
        }
    }

    // Kinematic equasion for the Grapple Launch mechanic launch distance
    public Vector3 CalculateLaunchVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity)
            + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

        return velocityXZ + velocityY;
    }
}
