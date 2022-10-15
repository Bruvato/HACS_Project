using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{

    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;

    private PlayerMotor motor;
    private PlayerLook look;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();

        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.Sprint.performed += ctx => motor.Sprint();
        onFoot.Crouch.performed += ctx => motor.Crouch();

        onEnable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //tell playerMotor to move using values from our movement action
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        look.ProccessLook(onFoot.Look.ReadValue<Vector2>());
    }
    private void onEnable()
    {
        onFoot.Enable();
    }
    private void onDisable()
    {
        onFoot.Disable();
    }
}
