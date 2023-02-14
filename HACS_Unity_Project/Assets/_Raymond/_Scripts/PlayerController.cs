using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed = 10.0f;
    [SerializeField] private float sprintSpeed = 20.0f;
    [SerializeField] private CharacterController characterController;

    [SerializeField] private Vector3 moveDirection;

    [SerializeField] private float jumpHeight = 3.0f;
    [SerializeField] private float gravity = -9.81f;

    [SerializeField] private Vector3 velocity;
    [SerializeField] private GameObject groundChecker;
    [SerializeField] private float groundCheckDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private bool isGrounded;

    [SerializeField] private Animator playerAnimator;
    [SerializeField] private float dampTime;
    private float vz;

    
    private void Start()
    {
        moveSpeed = walkSpeed;
    }

    private void Update()
    {
        Sprint();
        Move();
        HandleGravity();
        Animate();

    }


    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal"); //get axis raw -> immediate with no steps
        float y = Input.GetAxisRaw("Vertical");//value between -1 and 1

        moveDirection = transform.right * x + transform.forward * y;
        moveDirection = moveDirection.normalized; //magnitude is 1 regardless of direction
        
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void Sprint()
    {
        if (Input.GetKeyDown("left shift"))
            moveSpeed = sprintSpeed;
        
        if (Input.GetKeyUp("left shift"))
            moveSpeed = walkSpeed;
    }

    private void OnDrawGizmos() //draw groundchecker sphere
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawSphere(groundChecker.transform.position, groundCheckDistance);
    }

    private void HandleGravity()
    {
        //true if groundchecker collides with object with ground layer mask
        isGrounded = Physics.CheckSphere(groundChecker.transform.position, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0) //reset y velocity
        {
            velocity.y = -2f;
        }
            

        // if (Input.GetButtonDown("Jump") && isGrounded)
        // {
        //     velocity.y = Mathf.Sqrt(-2f * gravity * jumpHeight);
        // }
        

        velocity.y += gravity * Time.deltaTime; //simulate gravity: accelerate downwards
        
        characterController.Move(velocity * Time.deltaTime);

    }

    public void Jump()
    {
        if (isGrounded)
            velocity.y = Mathf.Sqrt(-2f * gravity * jumpHeight);
    }


    private void Animate()
    {
        AnimateMovement();
        AnimateJumping();

    }
    private void AnimateMovement()
    {
        if (moveDirection == Vector3.zero) //idle
        {
            playerAnimator.SetFloat("vx", 0, dampTime, Time.deltaTime);
            playerAnimator.SetFloat("vy", 0, dampTime, Time.deltaTime);
        }
        else if (!Input.GetKey(KeyCode.LeftShift)) //walk
        {
            playerAnimator.SetFloat("vx", Input.GetAxis("Horizontal"), dampTime, Time.deltaTime);
            playerAnimator.SetFloat("vy", Input.GetAxis("Vertical"), dampTime, Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.LeftShift)) //sprint
        {
            playerAnimator.SetFloat("vx", Input.GetAxis("Horizontal") * 2, dampTime, Time.deltaTime);
            playerAnimator.SetFloat("vy", Input.GetAxis("Vertical") * 2, dampTime, Time.deltaTime);
        }
    }

    private void AnimateJumping()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerAnimator.SetTrigger("Jump");
        }

        playerAnimator.SetBool("Falling", !isGrounded);
        playerAnimator.SetBool("Grounded", isGrounded);

        
        if (!isGrounded)
            vz = velocity.y;

        // if (isGrounded && vz <= -15) //stop moving if hard land
        // {
        //     moveSpeed = 0;
        //     if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Movement"))
        //         moveSpeed = walkSpeed;
        // }

            
        playerAnimator.SetFloat("vz", vz);

    }

}
