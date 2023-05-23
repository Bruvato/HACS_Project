using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void OnTrigger()
    {
        gameManager.StartLevel();
    }
}
