using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void completeLevel()
    {
        gameManager.CompleteLevel();
    }
}
