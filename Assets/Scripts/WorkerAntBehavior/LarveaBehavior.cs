using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;

public class LarveaBehavior : MonoBehaviour
{
    public delegate void OnEat();
    public static event OnEat onEat;
    NavMeshAgent agent;

    public Transform myAnHill;
    private AntStats stats;
    private bool canEat = true;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        Invoke("GrowUp", 60);
        stats = GetComponent<AntStats>();
        stats.SetValues(5, 5, 1);
    }
    private void FixedUpdate()
    {
        agent.destination = myAnHill.position;
    }
    void GrowUp() //Just adds the worker behavior and sets some variables
    {
        stats.SetValues(10, Random.Range(1,5), Random.Range(5,10));
        this.transform.localScale = new Vector3(.1f, .1f, .1f);
        WorkerBehavior worker = gameObject.AddComponent<WorkerBehavior>();
        worker.antHill = myAnHill;
        Eat eat = GetComponentInChildren<Eat>();
        eat.WorkerBehaviour = worker;
        Destroy(GetComponent<LarveaBehavior>());
    }

    public void Eat()
    {
        if (canEat)
        {
            canEat = false;
            onEat?.Invoke();
            stats.Eat(1);
            Invoke("ResetBite", 2);
            
        }
        if (stats.isFull) GrowUp(); 
    }
    private void ResetBite()
    {
        canEat = true;
    }

}
