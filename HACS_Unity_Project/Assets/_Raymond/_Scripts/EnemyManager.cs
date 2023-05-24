using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject completelvlUI;
    [SerializeField] private PlayerStats playerStats;

            
    
    private void Update(){
        
        GameObject e = GameObject.FindGameObjectWithTag("Enemy");
        
        if (e==null&& playerStats.IsDead() == false) //replace with finding when all enemeies are dead logic
        {
            completeLevel();
            
        }

    }
    
    private void completeLevel(){
        completelvlUI.SetActive(true);
    }
}
