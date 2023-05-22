using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    [SerializeField] private GameObject[] islandPrefabs;
    [SerializeField] private Transform[] spawnLocs;
    [SerializeField] private GameObject mapGen;
    [SerializeField] private int level = 0;
    [SerializeField] private int tileLen = 10;

    private void Start()
    {
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
            GameObject island = Instantiate(islandPrefabs[ranNum], spawnLocs[i].position, islandPrefabs[ranNum].transform.rotation);
            island.transform.SetParent(spawnLocs[i]);

        }
    }

}
