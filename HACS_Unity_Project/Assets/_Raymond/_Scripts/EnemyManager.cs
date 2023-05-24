using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject levelUI;
    [SerializeField] private GameObject completelvlUI;

    private void Update(){


        if (Input.GetKeyDown(KeyCode.L)) //replace with finding when all enemeies are dead logic
        {
            completeLevel();
        }

    }
    
    private void completeLevel(){
        levelUI.SetActive(true);
        completelvlUI.SetActive(true);
    }
}