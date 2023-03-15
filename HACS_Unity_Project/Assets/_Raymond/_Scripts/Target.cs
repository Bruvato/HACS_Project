using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{

    [SerializeField] private float health = 100;
    [SerializeField] private Transform spawnLocation;

    public void TakeDamage(float damage)
    {

        health -= damage;
        
        if (health <= 0)
        {
            Spawn();
            Destroy(gameObject);
        }

    }

    private void Spawn()
    {
        float x = Random.Range(-5, 5) + spawnLocation.position.x;
        float y = Random.Range(-5, 5) + spawnLocation.position.y;
        float z = Random.Range(-5, 5) + spawnLocation.position.z;

        Instantiate(gameObject, new Vector3(x, y, z), Quaternion.identity);
    }


}
