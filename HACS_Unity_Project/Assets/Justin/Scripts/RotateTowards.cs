using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowards : MonoBehaviour
{
    public Transform target;
    public float speed = 0.5f;
    Quaternion rotGoal;
    Vector3 direction;
    void Update()
    {
        direction = (target.position - transform.position).normalized;
        rotGoal = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, speed * Time.deltaTime);
    }
}
