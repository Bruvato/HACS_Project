using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;
    private PlayerMotor motor;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //tell playerMotor to move using values from our movement action
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
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
