using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartLevel : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void restartLevel()
    {
        gameManager.RestartLevel();
    }
}
