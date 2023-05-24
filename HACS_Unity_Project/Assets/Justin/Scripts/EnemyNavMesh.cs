using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    public navMeshLogic logic;
    public bool hide, chase, stationary;
    public Transform player;
    public LayerMask hidableLayers;
    public CheckLOS LOSChecker;
    [Range(-1, 1)]
    public float hideSensitivity = 0;
    [Range(1, 10)]
    public float minPlayerDistance = 5f;

    private Coroutine MovementCoroutine;
    private Collider[] colliders = new Collider[10];

    private Transform movePositionTransform;
    public NavMeshAgent navMeshAgent;
    [SerializeField] private int rotationSpeed = 5;


    private void Awake()
    {
        GetLogic();

        // GameObject target = GameObject.FindWithTag("target");
        // movePositionTransform = target.transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        LOSChecker.onGainSight += HandleGainSight;
        LOSChecker.onLoseSight += HandleLoseSight;
    }

    private void Update()
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            Quaternion rotation = transform.rotation;
            transform.rotation = Quaternion.Slerp(rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private void HandleGainSight(Transform target)
    {
        GetLogic();
        if (MovementCoroutine != null)
        {
            StopCoroutine(MovementCoroutine);
        }
        player = target;
        if (stationary == false)
        {
            if (hide == true)
            {
                MovementCoroutine = StartCoroutine(Hide(target));
            }
            if (chase == true)
            {
                MovementCoroutine = StartCoroutine(Chase(target));
            }
        }

    }
    private void HandleLoseSight(Transform target)
    {
        if (MovementCoroutine != null)
        {
            StopCoroutine(MovementCoroutine);
        }
        player = null;

    }
    private void GetLogic()
    {

        logic.UpdateStatus();
        hide = logic.shouldHide;
        chase = logic.shouldChase;
        stationary = logic.stationary;

    }
    private IEnumerator Look(Transform target)
    {
        WaitForSeconds Wait = new WaitForSeconds(0.1f);
        while (true)
        {

            yield return Wait;
        }
    }
    private IEnumerator Chase(Transform target)
    {
        WaitForSeconds Wait = new WaitForSeconds(0.1f);
        while (chase == true)
        {

            GetLogic();

            navMeshAgent.SetDestination(target.position);

            yield return Wait;
        }
        HandleGainSight(target);

    }


    private IEnumerator Hide(Transform target)
    {
        WaitForSeconds Wait = new WaitForSeconds(1f);
        while (hide == true)
        {

            GetLogic();

            Debug.Log("hiding");

            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i] = null;
            }

            int hits = Physics.OverlapSphereNonAlloc(navMeshAgent.transform.position, LOSChecker.c.radius, colliders, hidableLayers);
            Debug.Log(hits);

            int hitReduction = 0;
            for (int i = 0; i < hits; i++)
            {
                if (Vector3.Distance(colliders[i].transform.position, target.position) < minPlayerDistance) //|| colliders[i].bounds.size.y < MinObstacleHeight)
                {
                    colliders[i] = null;
                    hitReduction++;
                }
            }
            hits -= hitReduction;
            System.Array.Sort(colliders, ColliderArraySortComparer);

            for (int i = 0; i < hits; i++)
            {

                if (NavMesh.SamplePosition(colliders[i].transform.position, out NavMeshHit hit, 2f, navMeshAgent.areaMask))
                {

                    if (NavMesh.FindClosestEdge(hit.position, out hit, navMeshAgent.areaMask))
                    {

                        if (Vector3.Dot(hit.normal, (target.position - hit.position).normalized) < hideSensitivity)
                        {
                            navMeshAgent.SetDestination(hit.position);
                            Debug.Log("hit1");
                            break;


                        }
                        else
                        {
                            if (NavMesh.SamplePosition(colliders[i].transform.position - (target.position - hit.position).normalized * 2, out NavMeshHit hit2, 2f, navMeshAgent.areaMask))
                            {
                                if (Vector3.Dot(hit2.normal, (target.position - hit2.position).normalized) < hideSensitivity)
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

    }
    // private void Update()
    // {
    //     // navMeshAgent.destination = movePositionTransform.position;
    // }
    public int ColliderArraySortComparer(Collider A, Collider B)
    {
        if (A == null && B != null)
        {
            return 1;
        }
        else if (A != null && B == null)
        {
            return -1;
        }
        else if (A == null && B == null)
        {
            return 0;
        }
        else
        {
            return Vector3.Distance(navMeshAgent.transform.position, A.transform.position).CompareTo(Vector3.Distance(navMeshAgent.transform.position, B.transform.position));
        }


    }



}


