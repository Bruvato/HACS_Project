using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public CheckLOS los;
    private Coroutine MovementCoroutine;
    private Transform movePositionTransform;
    private Transform player;


    

    // Start is called before the first frame update
    private void Awake()
    {
        // GameObject target = GameObject.FindWithTag("target");
        // movePositionTransform = target.transform;
        navMeshAgent = GetComponent<NavMeshAgent>();

        los.onGainSight += HandleGainSight;
        los.onLoseSight += HandleLoseSight;
        
        
    }
    private void HandleGainSight(Transform target)
    {
        if (MovementCoroutine != null)
        {
            StopCoroutine(MovementCoroutine);
        }
        player = target;
        MovementCoroutine = StartCoroutine(Chase(target));
        Debug.Log("handlegain");

    }
    private void HandleLoseSight(Transform target)
    {
        if (MovementCoroutine != null)
        {
            StopCoroutine(MovementCoroutine);
        }
        player = null;
        Debug.Log("handlelose");

    }
    // Update is called once per frame

    private IEnumerator Chase(Transform target){
        // movePositionTransform = player.transform;


        WaitForSeconds wait = new WaitForSeconds(1f);

        navMeshAgent.SetDestination(target.position);
        yield return wait;



    }
}
