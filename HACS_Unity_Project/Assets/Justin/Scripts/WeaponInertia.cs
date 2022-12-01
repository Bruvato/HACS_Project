using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInertia : MonoBehaviour
{
    public float smooth;
    public float swayMultiplayer;
    public GameObject camera;
    private float sens;



    private void Start()
    {
        sens = camera.GetComponent<MouseLook>().mouseSensitivity;
    }
    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplayer * sens;
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplayer * sens;




        Quaternion rotationX = Quaternion.AngleAxis(mouseY, Vector3.left);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);


        Quaternion targetRotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    
    }

}