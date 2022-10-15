using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private bool crouching;
    private bool lerpCrouch;
    private bool sprinting;

    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    public float crouchTimer = 0f;
    

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        isGrounded = controller.isGrounded;

        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime; 
            float lerpPercent = crouchTimer / 1;
            lerpPercent *= lerpPercent;
            if (crouching)
                controller.height = Mathf.Lerp(controller.height, 1, lerpPercent);
            else
                controller.height = Mathf.Lerp(controller.height, 2, lerpPercent);

            if (lerpPercent > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }
    //recieve the inputs for our inputManager and apply them to our character controller
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;

        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity *Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3f * gravity);
        }
    }

    public void Sprint() //toggle sprint
    {
        sprinting = !sprinting;
        if (sprinting)
            speed = 8;
        else
            speed = 5;
    }

    public void Crouch() //toggle crouch
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;

        if (crouching)
            speed = 3;
        else
            speed = 5;
    }
}
