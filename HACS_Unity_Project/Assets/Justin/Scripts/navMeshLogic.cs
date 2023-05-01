using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navMeshLogic : MonoBehaviour
{
    public bool shouldHide , shouldChase , isAlert ;
    public CheckLOS LOSChecker;
    public Enemy stats;
    [Range(0,1)]
    public float hideThreshold = 0.5f;
    
    // Start is called before the first frame update
    private void Awake() {
        
        shouldChase = false;
        shouldHide = false;
        isAlert = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(stats.hp>hideThreshold*stats.initHp){
        shouldChase = true;
        }
        else if(stats.hp<hideThreshold*stats.initHp){
        shouldHide = true;
        }
    }

    
}
