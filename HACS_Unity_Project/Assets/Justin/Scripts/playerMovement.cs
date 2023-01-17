using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f, baseFriction = 1.5f, baseFriction2, frictionThreshold = 0.01f, frictionThreshold2 = 0.01f, maxSpeed = 3f, walldist = 5f;
    private float moveSpeed, max, gravityA;
    //downwards force to bring the character to the ground
    public float gravity = -9.81f;
    //downwards force to keep the character on the ground
    public float groundingForce = -2f;
    public float jumpHeight = 3f;

    // reference to empty
    public Transform groundCheck, pelvis, chest;
    // sphere project radius (how wide to check around empty for ground)
    public float groundDistance = 0.4f;
    // limit type to collide with
    public LayerMask groundMask;

    //vertical forces acting on player
    Vector3 velocity, residualMove, move, move2, wallNormal, wallDirection;
    bool isGrounded;
    private Ray pelvisRay, chestRay, rightRay, leftRay, wallBuffer;
    // Ray groundRay;
    private RaycastHit hit, pelvisHit, left, right, forward, activeWall;
    private bool rightWall, leftWall, isWallRunning = false;

    // private GameObject ground;


    // Start is called before the first frame update
    void Start()
    {
        gravityA = gravity;
    }

    // Update is called once per frame
    void Update()
    {
        // groundRay = new Ray(groundCheck.position, Vector3.down);
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded)
        {
            // Physics.Raycast(groundRay, out hit, groundDistance, groundMask);
            // ground = hit.collider.gameObject;

            max = maxSpeed;
            moveSpeed = speed;

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = groundingForce;

            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed = speed * 2;
                max = maxSpeed * 2;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 acceleration = (transform.right * x + transform.forward * z).normalized;
            move += acceleration * moveSpeed;// * baseFriction;

            // if (x == 0 || z == 0)
            // {
            // if (residualMove.x > 0.1 || (residualMove.y) > 0.1)
            // {

            // }


            // }

            // if(Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.D)){} else{

            move = Vector3.ClampMagnitude(move, max);
            // if(Mathf.Abs(Vector3.Angle(acceleration, move))>90){
            // 
            // }
            if (move.magnitude > frictionThreshold)
                move -= move.normalized * Time.deltaTime * baseFriction;

            if (!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
            {
    
                // move /= baseFriction;//*Time.deltaTime;// * -move.normalized;//*Time.deltaTime;
                if (move2.magnitude < frictionThreshold2){
                    move2 /= baseFriction2;
                }
                move/=baseFriction;

            }


            if (move2.magnitude > frictionThreshold2)
                move2/= baseFriction2;//*Time.deltaTime* -move.normalized*Time.deltaTime;

            if (Input.GetButtonDown("Jump") && isGrounded)


            {
                pelvisRay = new Ray(pelvis.transform.position, pelvis.transform.forward);
                chestRay = new Ray(chest.transform.position, chest.transform.forward);

                if (Physics.Raycast(pelvisRay, out pelvisHit, walldist) && !Physics.Raycast(chestRay, walldist))
                {
                    Vector3 direction = pelvisHit.normal;
                    jump();
                    move += -direction * 100;
                    // StartCoroutine(Vault(direction));
                }
                else
                {
                    jump();
                }
            }
        }

        controller.Move(move * Time.deltaTime);
        controller.Move(move2 * Time.deltaTime);
        if (Input.GetButtonDown("Jump") && !isGrounded)
        {
            if (isWallRunning)
            {
                gravityA = gravity;
                isWallRunning = false;
            }
            else
            {

                checkWall();


                if (rightWall || leftWall)
                {
                    wallRun();
                }
            }

        }
        if (isWallRunning)
        {

            checkWall();
            wallNormal = -activeWall.normal;


            wallBuffer = new Ray(chest.transform.position, wallNormal);
            getWallDirection();
            
            if (!leftWall && !rightWall)
            {
                gravityA = gravity;
                isWallRunning = false;
            }

        }

        velocity.y += gravityA * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
    }
    IEnumerator Vault(Vector3 direction)
    {
        jump();
        yield return new WaitForSeconds(0.1f);

        controller.Move(-direction * controller.velocity.magnitude);
        yield return null;
    }

    void checkWall()
    {
        rightRay = new Ray(chest.transform.position, chest.right);
        leftRay = new Ray(chest.transform.position, -chest.right);
        if (Physics.Raycast(leftRay, out left, walldist))
        {
            activeWall = left;
            leftWall = true;
        }
        else
        {
            leftWall = false;
        }
        if (Physics.Raycast(rightRay, out right, walldist))
        {
            activeWall = right;
            rightWall = true;
        }
        else
        {
            rightWall = false;
        }

    }

    void getWallDirection()
    {
        if (rightWall)
        {
            wallDirection = Quaternion.AngleAxis(-90, gameObject.transform.up) * wallNormal;
        }
        else if (leftWall)
        {
            wallDirection = Quaternion.AngleAxis(90, gameObject.transform.up) * wallNormal;
        }
        wallDirection = wallDirection.normalized;
        move2+=wallDirection*2;
    }

    void wallRun()
    {
        isWallRunning = true;

        gravityA /= 6f;


    }

}
