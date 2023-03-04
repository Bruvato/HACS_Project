using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour
{
    public float armorLevel, maxhp, currenthp;
    private bool isBroken;

    // Start is called before the first frame update
    private void Awake() {
        isBroken = false;
        currenthp = maxhp;
    }
    

    // Update is called once per frame
    void Update()
    {
        if(currenthp <= 0){
            isBroken = true;
        }
        if(isBroken){
            Destroy(gameObject);
        }
    }
}
