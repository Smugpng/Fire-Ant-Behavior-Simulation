using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;

public class LarveaBehavior : MonoBehaviour
{
    public delegate void OnEat();
    public static event OnEat onEat;

    public Transform myAnHill;
    private AntStats stats;
    private bool canEat = true;
    
    // Start is called before the first frame update
    void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = myAnHill.position;
        Invoke("GrowUp", 60);
        stats = GetComponent<AntStats>();
        stats.SetValues(5, 5, 1);
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
        Debug.Log("Step 2");
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
