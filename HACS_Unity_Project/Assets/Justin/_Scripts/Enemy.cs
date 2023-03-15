using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float currenthp, maxhp = 100f;
    private Rigidbody rb;
    private EnemyPool pool;
    // Start is called before the first frame update
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        pool = GameObject.FindGameObjectWithTag("EnemyPool").GetComponent<EnemyPool>();
        rb.isKinematic = true;
        currenthp = maxhp;
    }
    void OnEnable(){
        rb.isKinematic = true;
        currenthp = maxhp;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currenthp <= 0)
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
