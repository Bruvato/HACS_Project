using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject completelvlUI;
    [SerializeField] private MapGen mG;

            
    
    private void Update(){
        
        GameObject e = GameObject.FindGameObjectWithTag("Enemy");
        
        if (Input.GetKeyDown(KeyCode.V)) //replace with finding when all enemeies are dead logic
        {
            completeLevel();
            
        }

    }
    
    private void completeLevel(){
        completelvlUI.SetActive(true);
    }
}
