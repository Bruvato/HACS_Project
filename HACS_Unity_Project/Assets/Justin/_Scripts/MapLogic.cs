using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLogic : MonoBehaviour
{   
    public List<GameObject> buildings = new List<GameObject>();
    public MapGenerator mG;
    public int mapLength, seperation;
    void Awake(){
        mG.Setup(mapLength);

        buildObjects();
    }
    public void buildObjects()
    {

        
        for (int r = 0; r < buildingLayout.GetLength(0); r++)
        {

            for (int c = 0; c < buildingLayout.GetLength(1); c++)
            {

                if (buildingLayout[r, c] == true)
                {
                    
                    Instantiate(buildings[Random.Range(0,2)], new Vector3(r * seperation, -13.7879f, c * seperation), Quaternion.identity);
                    
                }

                else
                {

                    // Instantiate(stuff2, new Vector3(r * seperation, -13.7879f, c * seperation), Quaternion.identity);

                }

            }

        }

    }
    
}
