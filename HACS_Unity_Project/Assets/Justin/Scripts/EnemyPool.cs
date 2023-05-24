using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private MapGen mG;
    public GameObject enemyPrefab;
    private ObjectPool<GameObject> enemies;
    private GameObject enemy;
    private Transform spawnPoint;


    // Start is called before the first frame update
    void Start()
    {

        enemies = new ObjectPool<GameObject>(() =>
        {
            return Instantiate(enemyPrefab, spawnPoint);
        }, enemy =>
        {
            enemy.gameObject.SetActive(true);
        }, enemy =>
        {
            enemy.gameObject.SetActive(false);
        }, enemy =>
        {
            Destroy(enemy.gameObject);
        }, false, 10, 20);
    }

    // Update is called once per frame
    void Update()
    {
        spawnPoint = GetComponent<Transform>();
        if(Input.GetKey(KeyCode.M)){
            Debug.Log("clear");
            enemies.Clear();
        }
        
    }
    public void Spawn(Transform spawn){
        // int[] favors = new int[mG.spawnableLocs.Count];

        // for (int i = 0; i < favors.Length; i++)
        // {
        //     favors[i] = Random.Range(0,100);
        // }
        // int max = 0
        

        // for (int i = 0; i < mG.GetRows(); i++)
        // {
        //     foreach (int i in favors)
        //         {
        //             if(favors[i]>max) {
        //                 max = favors[i]
        //             }
        //         }
        //     spawnPoint = mG.spawnableLocs[max].position;
        // }
        
            spawnPoint = spawn;
            enemies.Get();

    
        
        

    }

    public void killEnemy(GameObject enemy)
    {
        Debug.Log("cleaned");
        enemies.Release(enemy);
    }

}
