using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHide : MonoBehaviour
{
    public Transform player;
    public LayerMask hidableLayers;
    public CheckLOS LOSChecker;
    [Range(-1, 1)]
    public float hideSensitivity = 0;

    private Coroutine MovementCoroutine;
    private Collider[] colliders = new Collider[10];

    private Transform movePositionTransform;
    public NavMeshAgent navMeshAgent;
    private void Awake()
    {
        // GameObject target = GameObject.FindWithTag("target");
        // movePositionTransform = target.transform;
        navMeshAgent = GetComponent<NavMeshAgent>();

        LOSChecker.onGainSight += HandleGainSight;
        LOSChecker.onLoseSight += HandleLoseSight;
    }

    private void HandleGainSight(Transform target)
    {
        if (MovementCoroutine != null)
        {
            StopCoroutine(MovementCoroutine);
        }
        // player = target;
        MovementCoroutine = StartCoroutine(Hide(target));
        Debug.Log("handlegain");

    }
    private void HandleLoseSight(Transform target)
    {
        if (MovementCoroutine != null)
        {
            StopCoroutine(MovementCoroutine);
        }
        // player = null;
        Debug.Log("handlelose");

    }

    private IEnumerator Hide(Transform Target)
    {
        while (true)
        {
            Debug.Log("while");

            WaitForSeconds Wait = new WaitForSeconds(1f);


            int hits = Physics.OverlapSphereNonAlloc(navMeshAgent.transform.position, LOSChecker.c.radius, colliders, hidableLayers);
            Debug.Log("overlap sphere");
            Debug.Log(hits);

            for (int i = 0; i < hits; i++)
            {

                if (NavMesh.SamplePosition(colliders[i].transform.position, out NavMeshHit hit, 2f, navMeshAgent.areaMask))
                {
                    Debug.Log("samplePosition");

                    if (NavMesh.FindClosestEdge(hit.position, out hit, navMeshAgent.areaMask))
                    {
                        
                        if (Vector3.Dot(hit.normal, (Target.position - hit.position).normalized) < hideSensitivity)
                        {
                            navMeshAgent.SetDestination(hit.position);
                            Debug.Log("hit1");
                            break;


                        }
                        else
                        {
                            if (NavMesh.SamplePosition(colliders[i].transform.position - (Target.position - hit.position).normalized * 2, out NavMeshHit hit2, 2f, navMeshAgent.areaMask))
                            {
                                if (Vector3.Dot(hit2.normal, (Target.position - hit2.position).normalized) < hideSensitivity)
                                {
                                    navMeshAgent.SetDestination(hit2.position);
                                    Debug.Log("hit2");

                                    break;
                                }
                            }
                        }
                    }
                }
                // break;
            }
            yield return Wait;

        }
        // private void Update()
        // {
        //     // navMeshAgent.destination = movePositionTransform.position;
        // }
    }
}
