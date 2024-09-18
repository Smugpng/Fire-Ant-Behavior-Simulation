using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;

public class WorkerAntSearch : MonoBehaviour
{

    private NavMeshAgent antAgent;

    [SerializeField]
    private float range;

    public Transform antHill;
    [SerializeField]
    private LayerMask foodLayer;
    
    // Start is called before the first frame update
    void Start()
    {
        antAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (antAgent.remainingDistance <= antAgent.stoppingDistance)
        {
            Debug.Log("Searching Again");
            Vector3 point;
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, range, transform.up, out hit, range*10, foodLayer))
            {
                point = hit.point;
                antAgent.SetDestination(point);
                Debug.Log("Found some food");
            }
            else if (RandomSearch(antHill.position, range,out point)) 
            {
                antAgent.SetDestination(point);
            }
        }
    }

    bool RandomSearch(Vector3 antHill, float range, out Vector3 point)
    {
        Vector3 rndPos = antHill  + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if(NavMesh.SamplePosition(rndPos, out hit, 1, NavMesh.AllAreas)) 
        {
            point = hit.position;
            return true;
        }
        point = Vector3.zero;
        return false;

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, range);
    }
}
