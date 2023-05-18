using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour
{
    public navMeshLogic logic;
    public bool chase;
    public Transform player;
    public CheckLOS LOSChecker;
    [Range(-1, 1)]
    public float minPlayerDistance = 5f;
    public LayerMask hidableLayers;


    private Coroutine MovementCoroutine;
    private Collider[] colliders = new Collider[10];

    private Transform movePositionTransform;
    public NavMeshAgent navMeshAgent;


    private void Awake()
    {
        // GameObject target = GameObject.FindWithTag("target");
        // movePositionTransform = target.transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        chase = logic.shouldChase;
        LOSChecker.onGainSight += HandleGainSight;
        LOSChecker.onLoseSight += HandleLoseSight;
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

    private IEnumerator Chase(Transform target)
    {
        WaitForSeconds Wait = new WaitForSeconds(0.75f);
        chase = logic.shouldChase;
        while (chase == true)
        {
            navMeshAgent.SetDestination(target.position);
            break;
        }
        yield return Wait;

        
    }
    // private void Update()
    // {
    //     // navMeshAgent.destination = movePositionTransform.position;
    // }
}


