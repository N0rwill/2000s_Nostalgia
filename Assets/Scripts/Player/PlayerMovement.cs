using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool freeze;

    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;

    public float GroundDrag;

    [Header("Jump")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool canJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("gruond Check")]
    public float playerHeight;
    public LayerMask groundMask;
    [SerializeField] bool isGrounded;

    [Header("Slope Handler")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;

    public enum MovementState
    {
        walking,
        sprinting,
        air
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        canJump = true;
    }

    private void Update()
    {
        // ground check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, groundMask);

        MyInput();
        SpeedContorl();
        StateHandler();

        // drag handler
        if (isGrounded)
        {
            rb.drag = GroundDrag;
        }
        else
        {
            rb.drag = 0;
        }

        if (rb.velocity.y < 0.01f && !isGrounded)
        {
            // make gravity stronger when falling
            rb.velocity = Vector3.Lerp(rb.velocity, new Vector3(rb.velocity.x, rb.velocity.y + Physics.gravity.y * Time.deltaTime, rb.velocity.z), 2.5f);
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
    }

    private void StateHandler()
    {
        // state springing
        if(isGrounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }
        // state walking
        else if(isGrounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        // state air
        else if(!isGrounded)
        {
            state = MovementState.air;
        }

        // turn off gravity when on slope
        rb.useGravity = !OnSlope();
    }

    private void MovePlayer()
    {
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
        // limit speed on slope
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        // limit speed on ground or in air
        else
        {
            Vector3 flaVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            // limit velocity if needed
            if (flaVelocity.magnitude > moveSpeed)
            {
                Vector3 limitedVelocity = flaVelocity.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
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

    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
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



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = Vector3.down * (playerHeight * 0.5f + 0.3f);
        Gizmos.DrawRay(rayOrigin, rayDirection);
    }
}
