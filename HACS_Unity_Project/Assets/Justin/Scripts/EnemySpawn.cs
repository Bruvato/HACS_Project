using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private EnemyPool enemyPool;
    // Start is called before the first frame update
    void Start()
    {
        enemyPool = GameObject.Find("EnemyPool").GetComponent<EnemyPool>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)){

            enemyPool.Spawn(transform);


        }
    }
}
