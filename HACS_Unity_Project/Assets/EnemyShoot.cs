using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyShoot : MonoBehaviour
{
    public static Action shootAction;
    public static Action reloadAction;
    // Start is called before the first frame update
    public CheckLOS LOSChecker;
    private LayerMask targets;
    private Transform tgt;
    private Transform muzzle;
    private string muzzlePath;
    public GunData gunData;
    private Transform gunOrigin;
    Quaternion rotGoal;
    Vector3 direction;
    public float speed;

    private void HandleGainSight(Transform target)
    {
        
        tgt = target;
        
    }
    private void HandleLoseSight(Transform target)
    {
        
       tgt = null; 
        Debug.Log("handlelose");
    }
    
    void Awake() 
    {
        targets = gunData.targets;
        tgt = null;
        // muzzlePath = "Enemy Weapon Holder/"+gunData.name+"/Muzzle";
        // muzzle = GameObject.Find(muzzlePath).transform;     
        muzzle = gameObject.transform.GetChild(1);
        LOSChecker.onGainSight += HandleGainSight;
        LOSChecker.onLoseSight += HandleLoseSight;
    }
    void Start()
    {
        gunOrigin = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //rotate enemy to face player
        if(tgt != null){
            direction = (tgt.position - gunOrigin.position).normalized;
            rotGoal = Quaternion.LookRotation(direction);
            gunOrigin.rotation = Quaternion.Slerp(gunOrigin.rotation, rotGoal, speed*Time.deltaTime);


        // shoot action and reload action are basically shoot input and reload input but for enemies
            if (Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hitInfo, gunData.maxDistance, targets)){
                // shootAction?.Invoke();
            EnemyGun shoot = gameObject.GetComponent<EnemyGun>();
            shoot.Shoot();
        }

        
        // reloadAction?.Invoke();
        }
    }

}
