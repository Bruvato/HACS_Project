using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navMeshLogic : MonoBehaviour
{
    public bool shouldHide = False, shouldChase = False, isAlert = False;
    public CheckLOS LOSChecker;
    public Enemy stats;
    [Range(0,1)]
    public float hideThreshold;
    
    // Start is called before the first frame update
    void Start()
    {
        LOSChecker.onGainSight += HandleGainSight;
        LOSChecker.onLoseSight += HandleLoseSight;
        
        
    }
    HandleGainSight

    // Update is called once per frame
    void Update()
    {
           
    }
}
