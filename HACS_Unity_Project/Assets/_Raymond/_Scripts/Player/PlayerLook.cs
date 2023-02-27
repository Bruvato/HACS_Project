using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private WallRun wallRun;
    [SerializeField] private Transform playerHead;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform orientation;

    float targetFov;
    [SerializeField] private float fovLerpTime;


    [Header("Look")]
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float sensMultiplier;

    //Inputs
    private float mouseX;
    private float mouseY;
    private float xRot;
    private float yRot;

    private void Start()
    {
        //hides cursor
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
    }

    private void Update()
    {
        MyInput();
        MoveCam();
        Look();

    }

    private void MyInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRot += mouseX * mouseSensitivity * sensMultiplier;
        xRot -= mouseY * mouseSensitivity * sensMultiplier;

        xRot = Mathf.Clamp(xRot, -90f, 90f);

    }

    private void Look()
    {
        cam.transform.rotation = Quaternion.Euler(xRot, yRot, wallRun.tilt);
        orientation.rotation = Quaternion.Euler(0, yRot, 0);
    }


    private void MoveCam()
    {
        transform.position = playerHead.position;
    }

    private void ChangeFov()
    {
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFov, fovLerpTime);
    }
}