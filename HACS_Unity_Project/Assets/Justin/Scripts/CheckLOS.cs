using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class CheckLOS : MonoBehaviour
{
    public SphereCollider c;
    public float fieldOfView = 90f;
    public LayerMask LineOfSightLayers;
    public delegate void GainSightEvent(Transform target);
    public GainSightEvent onGainSight;
    public delegate void LoseSightEvent(Transform target);
    public GainSightEvent onLoseSight;
    private Coroutine CheckForLineOfSightCoroutine;

    private void Awake()
    {
        c = GetComponent<SphereCollider>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!CheckLineOfSight(other.transform))
        {
            CheckForLineOfSightCoroutine =  StartCoroutine(CheckForLineOfSight(other.transform));

        }
    }

    private void OnTriggerExit(Collider other)
    {        onLoseSight?.Invoke(other.transform);
        if (CheckForLineOfSightCoroutine != null)
        {
            StopCoroutine(CheckForLineOfSightCoroutine);
        }
    }
    private bool CheckLineOfSight(Transform target)
    {
        Vector3 direction = (target.transform.position - this.transform.position).normalized;
        
        // Debug.DrawLine(this.transform.position, this.transform.position+10*direction, Color.white, 100000000f, false);
        // Debug.DrawRay(this.transform.position, direction, Color.white, 10000000f, false);

        float dotProduct = Vector3.Dot(this.transform.forward, direction);
        if (dotProduct >= Mathf.Cos(fieldOfView)){
            if (Physics.Raycast(this.transform.position, direction, out RaycastHit hit, c.radius, LineOfSightLayers))
            {
                // if(hit.gameObject.layer = )
                Debug.Log(c.radius);
                onGainSight?.Invoke(target);
                return true;
            }
        }
        return false;
    
    }
    private IEnumerator CheckForLineOfSight(Transform target){
        
        WaitForSeconds wait = new WaitForSeconds(1f);
        
        while(!CheckLineOfSight(target))
        {
            yield return wait;

            // yield return wait;
        }   

    }
}