using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] ParticleSystem ptcle;
    public float hp = 100f, initHp;
    private Rigidbody rb;
    private EnemyPool pool;
    
    // Start is called before the first frame update
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        // pool = GameObject.FindGameObjectWithTag("EnemyPool").GetComponent<EnemyPool>();
        rb.isKinematic = true;
        initHp = hp;
    }
    public void TakeDamage(float damage)
    {
        hp -= damage;
        Debug.Log("damage");
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
        ParticleSystem explosion = Instantiate(ptcle, gameObject.transform.position, gameObject.transform.rotation);

        yield return new WaitForSeconds(0.1f);
        Destroy(explosion);
        Destroy(gameObject);
        // pool.killEnemy(gameObject);
    }
    void RagdollOn(){
        rb.isKinematic = false;
        rb.detectCollisions = true;
    }
}
