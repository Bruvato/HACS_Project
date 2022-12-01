using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
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
        
        
    }
    public void Spawn(Transform spawn){
        Debug.Log(spawn.position);
        spawnPoint.position = spawn.position;
        Debug.Log(spawnPoint.position+ "sauce");
        enemies.Get();
        
    }

    public IEnumerator killEnemy(GameObject enemy)
    {
        Debug.Log("Kill");
        yield return new WaitForSeconds(2);
        Debug.Log("cleaned");
        enemies.Release(enemy);
    }

}
