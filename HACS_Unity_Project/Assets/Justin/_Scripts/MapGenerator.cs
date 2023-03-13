using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MapGenerator : MonoBehaviour
{
    public GameObject stuff, stuff2;
    private int numBuildings;
    private int[] buildingID;
    private bool[,] buildingLayout;
    public int mLength;
    private int generatedBuildings = 0;
    private int lastGeneratedr, lastGeneratedc;

    // Start is called before the first frame update
    void Awake()
    {
        buildingLayout = new bool[mLength, mLength];
        for (int i = 0; i < buildingLayout.GetLength(0); i++)
        {
            for (int j = 0; j < buildingLayout.GetLength(1); j++)
            {
                buildingLayout[i, j] = false;
            }
        }
        numBuildings = mLength;
        for (int i = 0; i < mLength; i++)
        {
            for (int j = 0; j < mLength; j++)
            {
                GenerateTiles(i, j);
            }
        }
        
        Debug.Log(buildingLayout);
        // Update is called once per frame

    }
    void GenerateTiles(int r, int c)
    {
        if (generatedBuildings < numBuildings)
        {
            int[] possiblePositions = new int[9];
            for (int i = 0; i < possiblePositions.Length; i++)
            {
                possiblePositions[i] = i;
            }
            // if (r - 1 == -1)
            // {
            //     possiblePositions[0] = -1;
            //     possiblePositions[1] = -1;
            //     possiblePositions[2] = -1;
            // }

            // if (c - 1 == -1)
            // {
            //     possiblePositions[0] = -1;
            //     possiblePositions[3] = -1;
            //     possiblePositions[6] = -1;
            // }



            int count = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {

                    if (possiblePositions[count] != -1)
                    {
                        if (isIn2dBoolArray(r + i, c + j, buildingLayout))
                        {
                            if (buildingLayout[r + i, c + j] == true)
                            {

                                possiblePositions[count] = -1;
                            }
                        }
                    }
                    count++;
                }
            }



            int maxPossible = 0;
            foreach (int n in possiblePositions)
            {
                if (n != -1)
                {
                    maxPossible++;
                }
            }

            int generated = Random.Range(0, maxPossible);
            int scroller = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (scroller == generated && possiblePositions[scroller] != -1)
                    {
                        if (isIn2dBoolArray(r + i, c + j, buildingLayout))
                        {
                            buildingLayout[r + i, c + j] = true;
                            generatedBuildings++;
                            lastGeneratedr = r + i;
                            lastGeneratedc = c + i;
                        }
                    }
                    else if (possiblePositions[scroller] == -1)
                    {
                        generated++;
                    }
                    scroller++;

                }
            }

        }
    }
    private bool isIn2dBoolArray(int row, int col, bool[,] arr)
    {
        return row < arr.GetLength(0) && row > -1 && col < arr.GetLength(1) && col > -1;

    }
    private bool isIn2dIntArray(int row, int col, int[,] arr)
    {
        return row < arr.GetLength(0) && row > -1 && col < arr.GetLength(1) && col > -1;

    }
}


