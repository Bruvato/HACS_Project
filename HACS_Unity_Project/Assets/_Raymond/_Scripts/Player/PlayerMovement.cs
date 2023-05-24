using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform orientation;
    [SerializeField] private PlayerStats playerStats;

    [Header("Cam Effects")]
    [SerializeField] private Camera cam;
    [SerializeField] private float fov;
    [SerializeField] private float dashFov;
    [SerializeField] private float fovLerpTime;


    [Header("Movement")]
    [SerializeField] private float moveSpeed; //base move speed
    [SerializeField][Range(0f, 100f)] private float walkSpeed;
    [SerializeField][Range(0f, 100f)] private float sprintSpeed;
    [SerializeField][Range(0f, 100f)] private float crouchSpeed;
    [SerializeField][Range(0f, 100f)] private float dashSpeed;
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
    [SerializeField] private Vector3 slopeMoveDirection;
    [SerializeField] private float slopeAngle;

    [Header("Ground Slam")]
    [SerializeField] private float groundSlamForce;
    private bool isSlamming;
    [SerializeField] ParticleSystem groundSlamPS;

    //Inputs
    private float horizontalMovement, verticalMovement;
    [SerializeField] private Vector3 moveDirection;

    private bool jumpKey, sprintKey, crouchKey, sliding, slamKey, dashKeyDown, dashKeyUp;
    private float StartTime = 0;




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
        GroundSlam();
        Dash();

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

        jumpKey = Input.GetKeyDown(KeyCode.Space);

        sprintKey = Input.GetKey(KeyCode.LeftShift);

        dashKeyDown = Input.GetKeyDown(KeyCode.LeftShift);
        dashKeyUp = Input.GetKeyUp(KeyCode.LeftShift);


        crouchKey = Input.GetKey(KeyCode.LeftControl);
        slamKey = Input.GetKeyDown(KeyCode.LeftControl);

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
        if (sprintKey && !crouchKey && playerStats.GetStam() > 0) //spriting
        {
            setSpeed(sprintSpeed);
            playerStats.DecreaseStam(1);
        }
        else if (crouchKey && isGrounded && !sprintKey) //crouching
        {
            setSpeed(crouchSpeed);
        }

        else //walking
        {
            setSpeed(walkSpeed);
        }
    }


    private void Crouch()
    {
        if (crouchKey && isGrounded)
        {
            transform.localScale = new Vector3(1, 0.5f, 1);
        }
        else if (!crouchKey)
        {
            transform.localScale = Vector3.one;
        }

    }

    private void Slide()
    {
        if (crouchKey && isGrounded && Slidable())
        {
            moveDirection = Vector3.zero; //stop movement
            sliding = true;
        }
        else
        {
            sliding = false;
        }
    }

    private void ControlGravity()
    {
        rb.AddForce(Vector3.up * playerGravity, ForceMode.Force); //additional gravity for player
    }

    private void ControlDrag()
    {
        if (isGrounded && !sliding)
        {
            rb.drag = groundDrag;
        }
        else if (isGrounded && sliding)
        {
            rb.drag = slideDrag;
        }
        else if (!isGrounded)
        {
            rb.drag = airDrag;
        }

    }

    private void Jump()
    {
        if (jumpKey && isGrounded)
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
            slopeAngle = Vector3.Angle(slopeHit.normal, Vector3.up);
            if (slopeAngle > 0)
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

    private bool Slidable()
    {
        if (rb.velocity.magnitude > crouchSpeed)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private void GroundSlam()
    {
        if (slamKey && !isGrounded && playerStats.GetStam() > 30)
        {
            isSlamming = true;
        }
        if (isSlamming)
        {
            rb.AddForce(Vector3.down * groundSlamForce, ForceMode.Force);
            playerStats.DecreaseStam(30);

            if (isGrounded)
            {
                groundSlamPS.Play();
                isSlamming = false;
            }
        }
    }

    private float timePressed()
    {
        if (dashKeyDown)
        {
            StartTime = Time.time;
        }
        if (dashKeyUp)
        {
            return Time.time - StartTime;
        }
        return 0;
    }

    private void setSpeed(float newSpeed)
    {
        moveSpeed = Mathf.Lerp(moveSpeed, newSpeed, aceleration * Time.deltaTime);
    }

    private void Dash()
    {

        if (!crouchKey && timePressed() < 0.18f && timePressed() > 0 && playerStats.GetStam() > 20)
        {
            if (isGrounded && !OnSlope()) //flat ground
            {
                rb.AddForce(moveDirection.normalized * dashSpeed * movementMultiplier, ForceMode.Impulse);
            }
            else if (isGrounded && OnSlope()) //slope ground
            {
                rb.AddForce(slopeMoveDirection.normalized * dashSpeed * movementMultiplier, ForceMode.Impulse);
            }
            else if (!isGrounded) //in air
            {
                rb.AddForce(moveDirection.normalized * dashSpeed * movementMultiplier * airMultiplier, ForceMode.Impulse);
            }

            playerStats.DecreaseStam(20);
        }

    }


}