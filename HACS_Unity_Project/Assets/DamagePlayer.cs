using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] PlayerStats playerstats;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other){
        if(other.gameObject.layer == LayerMask.NameToLayer("Player")){

            PlayerStats stats = other.gameObject.transform.parent.GetComponent<PlayerStats>();
            
            stats.takeDamge(1);


        }
    }
}
