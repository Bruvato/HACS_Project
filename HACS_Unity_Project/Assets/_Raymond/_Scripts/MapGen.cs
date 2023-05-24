using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;

public class MapGen : MonoBehaviour
{
    [SerializeField] private GameObject[] islandPrefabs;
    [SerializeField] private Transform[] spawnLocs;
    [SerializeField] private int rows = 0;
    [SerializeField] private EnemyPool pool;

    public List<Transform> spawnableLocs;

    [SerializeField] private int islandSpacing = 10;
    [SerializeField] private GameObject mapGen;
    private static int count = 1;


    public void Start()
    {
        GenerateSpawnLocs();

        Generate();
        
        NavMeshBuilder.BuildNavMesh();

        pool = GameObject.FindGameObjectWithTag("EnemyPool").GetComponent<EnemyPool>();
        SpawnEnemies();
    }
    public void RemoveLocation(int index){
        spawnableLocs.RemoveAt(index);
    }
    public void SetCount(int c)
    {
        count = c;
    }
    public void SetRows(int r)
    {
        rows = r;
    }
    public int GetRows(){
        return rows;
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
            
            if(ranNum>=0&&ranNum<=3){
                spawnableLocs.Add(island.transform);
            }
        }
    }
    private void SpawnEnemies()
    {
        int isle = Random.Range(0, spawnableLocs.Count-1);
        pool.Spawn(spawnableLocs[isle]);

    }

    private void GenerateSpawnLocs()
    {
        if (count < rows)
        {
            Instantiate(mapGen, mapGen.transform.position + Vector3.forward * islandSpacing, mapGen.transform.rotation);
            count++;
        }

    }

}
