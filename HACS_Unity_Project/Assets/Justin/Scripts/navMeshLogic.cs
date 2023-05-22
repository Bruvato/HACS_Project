using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navMeshLogic : MonoBehaviour
{
    public bool shouldHide, shouldChase, isAlert;
    public CheckLOS LOSChecker;
    public Enemy stats;
    [Range(0, 1)]
    public float hideThreshold = 0.5f;

    // Start is called before the first frame update
    private void Awake()
    {

        shouldChase = false;
        shouldHide = false;
        isAlert = false;
        UpdateStatus();
    }

    // Update is called once per frame
    public void UpdateStatus()
    {
        if (stats.hp > hideThreshold * stats.initHp)
        {
            shouldChase = true;
        }
        else
        {
            shouldChase = false;
        }

        if (stats.hp <= hideThreshold * stats.initHp)
        {
            shouldHide = true;
        }
        else
        {
            shouldHide = false;
        }

        /*command list: 
        0 = stand ground
        1 = retreat
        2 = advance
        3 = search
        4 = rally
        5 = 
        */
    }


}
