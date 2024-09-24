using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;

public class WorkerBehavior : MonoBehaviour
{

    [Header("Ants")]
    public Transform antHill;
    private NavMeshAgent antAgent;
    [SerializeField]
    private LayerMask antLayer;
    

    [Header("Smell")]
    [SerializeField]
    private float range = 20;
    [SerializeField]
    private float maxRange = 50;
    

    [Header("Food")]
    [SerializeField]
    private LayerMask foodLayer;
    [SerializeField] private bool foundFood;
    public GameObject foodToEat;
    public bool inBase = false, isFull = false;



    // Start is called before the first frame update
    void Start()
    {
        antLayer = 1<<7;
        foodLayer = 1<<6;
        antAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() //Updates bools
    {
        Search();
        if(foodToEat == null && foundFood)
        {
            foundFood = false;
        }
        if (inBase && isFull)
        {
            isFull = false;
            Search();
        }
    }
    private void Search()
    {
        if (antAgent.remainingDistance <= antAgent.stoppingDistance && !foundFood && !isFull) //If the ant is close enough to their stopping point they try looking again
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

    bool RandomSearch(Vector3 antHill, float range, out Vector3 point) //If they cant find any food find a random pos within the min and max ranges
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

    private void TellOthers(Vector3 point,GameObject food) //If the ant finds food they tell all the other ants around them so they can all eat it
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position,range,transform.forward,maxRange,antLayer);
        if(hits.Length > 0)
        {
            foreach (RaycastHit hit in hits)
            {
                WorkerBehavior foundAnts = hit.collider.gameObject.GetComponent<WorkerBehavior>();
                foundAnts.FoundFood(point);
                foundAnts.foodToEat = food;
            }
        }
    }
    public void FoundFood(Vector3 location) //Updated bool to make sure they dont keep searching while the find food
    {
        if (!foundFood && !isFull)
        {
            antAgent.SetDestination(location);
            foundFood = true;
        }
        
    }
    public void IsFull()
    {
        isFull = true;
        antAgent.SetDestination(antHill.position);
    }
}
