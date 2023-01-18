using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform orientation;

    [Header("Movement")]
    [SerializeField] private float moveSpeed; //base move speed
    [SerializeField] [Range(0f, 50f)] private float walkSpeed; 
    [SerializeField] [Range(0f, 50f)] private float sprintSpeed;
    [SerializeField] [Range(0f, 50f)] private float crouchSpeed;
    [SerializeField] private float aceleration;

    [SerializeField] private float movementMultiplier;
    [SerializeField] private float airMultiplier;

    [Header("Drag")]
    [SerializeField] private float groundDrag; //friction
    [SerializeField] private float airDrag; //air resistance
    [SerializeField] private float slideDrag; //friction while sliding

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float playerGravity;

    [Header("Ground Detection")]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private float groundCheckDistance;
    private bool isGrounded;

    [Header("Slope")]
    [SerializeField] private float playerHeight;
    private RaycastHit slopeHit;
    private Vector3 slopeMoveDirection;


    //Inputs
    private float horizontalMovement, verticalMovement;
    private Vector3 moveDirection;

    private bool jumping, sprinting, crouching, sliding;

    //add ground slamming



    private void Start()
    {
        rb.freezeRotation = true;
    }

    private void Update()
    {
        MyInput();

        ControlDrag();
        CheckIsGrounded();

        ControlSpeed();

        Jump();
        Crouch();
        Slide();

    }

    private void FixedUpdate()
    {
        Move();
        ControlGravity();
        
    }

    private void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);

        jumping = Input.GetKeyDown(KeyCode.Space);
        sprinting = Input.GetKey(KeyCode.LeftShift);
        crouching = Input.GetKey(KeyCode.LeftControl);

    }

    private void Move()
    {
        if (isGrounded && !OnSlope()) //flat ground
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Force);
        }
        else if (isGrounded && OnSlope()) //slope ground
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Force);
        }
        else if (!isGrounded) //in air
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Force);
        }
            
    }

    private void ControlSpeed()
    {
        if (sprinting && isGrounded)
        {
             moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, aceleration * Time.deltaTime);
        }
    }

    private void Crouch()
    {
        if (crouching && isGrounded)
        {
            transform.localScale = new Vector3(1, 0.7f, 1);
            movementMultiplier = crouchSpeed;

            if (rb.velocity.magnitude > 5)
            {
                sliding = true;
            }
            else
            {
                sliding = false;
            }
        }
        else
        {
            transform.localScale = Vector3.one;
            sliding = false;
        }
            
    }

    private void Slide()
    {
        if (sliding)
        {
            moveDirection = Vector3.zero;
            rb.drag = slideDrag;
        }
    }

    private void ControlGravity()
    {
        //additional gravity for player
        rb.AddForce(Vector3.up * playerGravity, ForceMode.Force);
    }

    private void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else if (!isGrounded)
        {
            rb.drag = airDrag;
        }
        
    }
        
    private void Jump()
    {
        if (jumping && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); //reset y velocity
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void CheckIsGrounded()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundCheckDistance, groundMask);
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
}