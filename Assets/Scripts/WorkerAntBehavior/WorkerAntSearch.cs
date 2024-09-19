using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;

public class WorkerAntSearch : MonoBehaviour
{

    [Header("Ants")]
    public Transform antHill;
    private NavMeshAgent antAgent;
    [SerializeField]
    private LayerMask antLayer;

    [Header("Smell")]
    [SerializeField]
    private float range;
    [SerializeField]
    private float maxRange;
    


    [Header("Food")]
    [SerializeField]
    private LayerMask foodLayer;
    [SerializeField] private bool foundFood;
    public GameObject foodToEat;



    // Start is called before the first frame update
    void Start()
    {
        antAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Search();
        if(foodToEat == null && foundFood)
        {
            foundFood = false;
        }
    }
    private void Search()
    {
        if (antAgent.remainingDistance <= antAgent.stoppingDistance && !foundFood) //If the ant is close enough to their stopping point they try looking again
        {
            Vector3 point;
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, range, transform.forward, out hit, maxRange, foodLayer)) //If they find food they go to it and send out a signal to ants in the area
            {
                foodToEat = hit.collider.gameObject;
                point = hit.point;
                antAgent.SetDestination(point);
                foundFood = true;
                TellOthers(point, foodToEat);
            }
            else if (RandomSearch(antHill.position, range, out point) && !foundFood) //Else they look for another random spot
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

    private void TellOthers(Vector3 point,GameObject food)
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position,range,transform.forward,maxRange,antLayer);
        if(hits.Length > 0)
        {
            foreach (RaycastHit hit in hits)
            {
                WorkerAntSearch foundAnts = hit.collider.gameObject.GetComponent<WorkerAntSearch>();
                foundAnts.FoundFood(point);
                foundAnts.foodToEat = food;
            }
        }
    }
    public void FoundFood(Vector3 location)
    {
        if(!foundFood)
        antAgent.SetDestination(location);
        foundFood = true;
    }
    public void IsFull()
    {
        antAgent.SetDestination(antHill.position);
    }
}
