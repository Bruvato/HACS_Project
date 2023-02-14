using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform orientation;

    [Header("Detection")]
    [SerializeField] private float wallCheckDistance = 0.5f;
    [SerializeField] private float minJumpHeight = 1.5f;

    [Header("Wall Running")]
    [SerializeField] private float wallRunGravity;
    [SerializeField] private float wallRunJumpForce;
    [SerializeField] private float forceMultiplier = 100;

    [Header("Camera Effects")]
    [SerializeField] private Camera cam;
    [SerializeField] private float fov;
    [SerializeField] private float wallRunFov;
    [SerializeField] private float fovLerpTime;

    public float tilt {get; private set;}
    [SerializeField] private float WallRunCamTilt;
    [SerializeField] private float camTiltLerpTime;



    private bool onLeftWall = false;
    private bool onRightWall = false;

    private RaycastHit leftWallHit;
    private RaycastHit rightWallHit;



    private bool CanWallRun() //true if meets jump requirement
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight);
    }

    private void CheckWall() //checks wall direction
    {
        onLeftWall = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallCheckDistance);
        onRightWall = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallCheckDistance);
        Debug.DrawLine(this.transform.position, this.transform.position -orientation.right * wallCheckDistance, Color.red);
        Debug.DrawLine(this.transform.position, this.transform.position + orientation.right * wallCheckDistance, Color.red);
    }

    private void Update()
    {
        CheckWall();

        if (CanWallRun())
        {
            if (onLeftWall)
            {
                StartWallRun();
            }
            else if (onRightWall)
            {
                StartWallRun();
            }
            else
            {
                EndWallRun();

            }
        }
        else
        {
            EndWallRun();
        }
    }


    private void StartWallRun()
    {
        rb.useGravity = false;

        rb.AddForce(Vector3.up * wallRunGravity, ForceMode.Force); //wall run gravity

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, wallRunFov, fovLerpTime * Time.deltaTime); //normal fov -> wallrun fov

        if (onLeftWall)
            tilt = Mathf.Lerp(tilt, -WallRunCamTilt, camTiltLerpTime * Time.deltaTime);

        else if (onRightWall)
            tilt = Mathf.Lerp(tilt, WallRunCamTilt, camTiltLerpTime * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onLeftWall)
            {
                Vector3 wallRunJumpDirection = (transform.up + leftWallHit.normal).normalized;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce * forceMultiplier, ForceMode.Force);
            }
            else if (onRightWall)
            {
                 Vector3 wallRunJumpDirection = (transform.up + rightWallHit.normal).normalized;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce * forceMultiplier, ForceMode.Force);
            }
        }

    }

    private void EndWallRun()
    {
        rb.useGravity = true;

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, fovLerpTime * Time.deltaTime); //wallrun fov -> normal fov

            tilt = Mathf.Lerp(tilt, 0, camTiltLerpTime * Time.deltaTime);
    }
}
