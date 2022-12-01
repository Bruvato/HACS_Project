using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    
    private Transform movePositionTransform;
    private NavMeshAgent navMeshAgent;
    private void Awake()
    {
        GameObject target = GameObject.FindWithTag("target");
        movePositionTransform = target.transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        navMeshAgent.destination = movePositionTransform.position;
    }
}
