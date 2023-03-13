using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLogic : MonoBehaviour
{
    public MapGenerator mG;
    public int mapLength;
    void Awake(){
        mG.Setup(mapLength);

    }
    
}
