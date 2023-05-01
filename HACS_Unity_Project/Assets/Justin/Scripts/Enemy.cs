using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp = 100f, initHp;
    private Rigidbody rb;
    private EnemyPool pool;
    // Start is called before the first frame update
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        pool = GameObject.FindGameObjectWithTag("EnemyPool").GetComponent<EnemyPool>();
        rb.isKinematic = true;
        initHp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0)
        {
            RagdollOn();
            // Debug.Log("ded");
            StartCoroutine(kill());
            // pool.killEnemy(gameObject);
            
        }
    }
    IEnumerator kill(){
        Debug.Log("kill");

        yield return new WaitForSeconds(5);
        // Destroy(gameObject);
        pool.killEnemy(gameObject);
    }
    void RagdollOn(){
        rb.isKinematic = false;
        rb.detectCollisions = true;
    }
}
