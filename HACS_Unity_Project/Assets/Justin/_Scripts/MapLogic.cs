using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLogic : MonoBehaviour
{   
    public List<GameObject> buildings = new List<GameObject>();
    public MapGenerator mG;
    private bool[,] layout;
    public int mapLength, seperation;
    void Awake(){
        mG.Setup(mapLength);
        layout = mG.getBuildingLayout();
        buildObjects();
    }
    public void buildObjects()
    {

        
        for (int r = 0; r < layout.GetLength(0); r++)
        {

            for (int c = 0; c < layout.GetLength(1); c++)
            {

                if (layout[r, c] == true)
                {
                    
                    Instantiate(buildings[Random.Range(0,buildings.Count)], new Vector3(r * seperation, -50, c * seperation), Quaternion.identity);
                    
                }

                else
                {

                    // Instantiate(stuff2, new Vector3(r * seperation, -13.7879f, c * seperation), Quaternion.identity);

                }

            }

        }

    }
    
}
