using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour
{
    public float hp = 100f;
    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0)
        {
            // Debug.Log("ded");
            StartCoroutine(kill());
            // pool.killEnemy(gameObject);
            
        }
    }
    IEnumerator kill(){
        Debug.Log("kill");

        yield return new WaitForSeconds(5);
        Destroy(gameObject);
        // pool.killEnemy(gameObject);
    }

}
