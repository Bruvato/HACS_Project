using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private Transform playerHead;
    [SerializeField] private Transform cam;
    [SerializeField] private Transform orientation;

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
        cam.rotation = Quaternion.Euler(xRot, yRot, 0);
        orientation.rotation = Quaternion.Euler(0, yRot, 0);
    }


    private void MoveCam()
    {
        transform.position = playerHead.position;
    }
}
