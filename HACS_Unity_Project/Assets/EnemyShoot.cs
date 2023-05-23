using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyShoot : MonoBehaviour
{
    public static Action shootAction;
    public static Action reloadAction;
    // Start is called before the first frame update
    private LayerMask targets;

    private Transform muzzle;
    private string muzzlePath;
    private GunData gunData;
    
    
    void Awake() 
    {
        targets = gunData.targets;
        muzzlePath = "Enemy Weapon Holder/"+gunData.name+"/Muzzle";
        muzzle = GameObject.Find(muzzlePath).transform;     
        
    }

    // Update is called once per frame
    void Update()
    {

        // shoot action and reload action are basically shoot input and reload input but for enemies
        if (Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hitInfo, gunData.maxDistance, targets)){
        shootAction?.Invoke();
        }

        
        // reloadAction?.Invoke();
    }
}
