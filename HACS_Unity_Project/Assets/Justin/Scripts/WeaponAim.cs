using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAim : MonoBehaviour
{

    public float range = 50f;
    public LayerMask targets;
    //camera
    public Transform rayEmitter;
    public Transform returnPoint;

    public float gunLength = 5f;
    public Transform gunOrigin;
    //ray origin
    Vector3 origin;
    private RaycastHit hit;
    public float speed = 0.5f;
    Quaternion rotGoal;
    Vector3 direction;
    Ray camRay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    void Update()
    {
        //assign coordinates to ray origin
        origin = new Vector3(rayEmitter.position.x, rayEmitter.position.y, rayEmitter.position.z);

        camRay = new Ray(origin, rayEmitter.forward);

        //if player is looking at an object in range
        if (Physics.Raycast(camRay, out hit, range, targets))
        {
            direction = (hit.point - gunOrigin.position).normalized;
            rotGoal = Quaternion.LookRotation(direction);
            gunOrigin.rotation = Quaternion.Slerp(gunOrigin.rotation, rotGoal, speed*Time.deltaTime);
        }
        else//if player isn't looking at anything such as sky, or no objects in range
        {
            direction = (camRay.GetPoint(range) - gunOrigin.position).normalized;
            rotGoal = Quaternion.LookRotation(direction);
            gunOrigin.rotation = Quaternion.Slerp(gunOrigin.rotation, rotGoal, speed*Time.deltaTime);
        }
    }
}
