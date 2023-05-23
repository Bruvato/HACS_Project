using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    [SerializeField] private GameObject[] islandPrefabs;
    [SerializeField] private Transform[] spawnLocs;
    [SerializeField] private int rows = 0;


    [SerializeField] private int islandSpacing = 10;
    [SerializeField] private GameObject mapGen;
    private static int count = 1;


    private void Start()
    {

        if (count < rows)
        {
            Instantiate(mapGen, mapGen.transform.position + Vector3.forward * islandSpacing, mapGen.transform.rotation);
            count++;
        }



        Generate();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Generate();
        }
    }

    private void Generate()
    {
        for (int i = 0; i < spawnLocs.Length; i++)
        {
            if (spawnLocs[i].childCount > 0)
            {
                Destroy(spawnLocs[i].GetChild(0).gameObject);
            }
            int ranNum = Random.Range(0, islandPrefabs.Length);
            Vector3 ranHeight = new Vector3(spawnLocs[i].position.x, Random.Range(-2, 2), spawnLocs[i].position.z);
            GameObject island = Instantiate(islandPrefabs[ranNum], ranHeight, islandPrefabs[ranNum].transform.rotation);
            island.transform.SetParent(spawnLocs[i]);

        }
    }

}
