using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    public void JumpEvent()
    {
        playerController.Jump();
    }
}
