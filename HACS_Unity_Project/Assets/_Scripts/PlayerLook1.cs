using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook1 : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100.0f;
    [SerializeField] private GameObject player;

    private Vector3 rotateDirection;

    [SerializeField] private Transform firstCamTarget;
    [SerializeField] private Transform thridCamTarget;

    [SerializeField] private float posLerpSpd;
    [SerializeField] private float rotLerpSpd;


    [SerializeField] private bool thridPerson;


    [SerializeField] private Animator playerAnimator;
    private float mouseXSmooth;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (thridPerson)
        {
            transform.position = Vector3.Lerp(transform.position, thridCamTarget.position, posLerpSpd * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, thridCamTarget.rotation, rotLerpSpd * Time.deltaTime);
        }
        else if (!thridPerson)
        {
            //transform.position = firstCamTarget.position;
            Look();
        }


    }
    private void LateUpdate()
    {
        
        // if (thridPerson)
        //     transform.position = thridPersonCam.position;

        // else
        //     transform.position = firstCamTarget.position;

        
    }

    private void Look()
    {
        

        rotateDirection.x = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        player.transform.Rotate(0, rotateDirection.x, 0); //rotate on y axis

        rotateDirection.y += Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        rotateDirection.y = Mathf.Clamp(rotateDirection.y, -90, 90);
        transform.localRotation = Quaternion.Euler(-rotateDirection.y, 0, 0); //rotate on x axis
        
        
        //AnimateRotation(rotateDirection.x);
        
    }

    private void AnimateRotation(float yRot)
    {
        //playerAnimator.SetFloat("yRot", yRot);

        mouseXSmooth = Mathf.Lerp(mouseXSmooth, Input.GetAxis("Mouse X"), 1 * Time.deltaTime);
        playerAnimator.SetFloat("yRot", mouseXSmooth);
    }

}
