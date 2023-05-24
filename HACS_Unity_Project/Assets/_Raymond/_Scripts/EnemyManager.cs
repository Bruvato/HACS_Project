using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject completelvlUI;

            
    
    private void Update(){
        
        GameObject e = GameObject.FindGameObjectWithTag("Enemy");
        
        if (e==null) //replace with finding when all enemeies are dead logic
        {
            completeLevel();
            
        }

    }
    
    private void completeLevel(){
        completelvlUI.SetActive(true);
    }
}
