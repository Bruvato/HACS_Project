using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GunData gunData;

    void OnTriggerEnter(Collider other){
        Debug.Log("projectile hit");
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy")){
            Debug.Log("projectile damage");

            Enemy stats = other.gameObject.GetComponent<Enemy>();
            
            stats.TakeDamage(gunData.damage);
        }
    }
}
