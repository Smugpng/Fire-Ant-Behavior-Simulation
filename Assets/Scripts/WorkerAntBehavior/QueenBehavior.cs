using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static LarveaBehavior;

public class QueenBehavior : MonoBehaviour
{

    public delegate void OnDrink();
    public static event OnDrink onDrink;
    public delegate void OnSpawn();
    public static event OnSpawn onSpawn;

    private NavMeshAgent antAgent;
    public Transform myAntHill;
    private AntStats stats;

    private bool canDrink = true;


    // Start is called before the first frame update
    void Start()
    {
        antAgent = GetComponent<NavMeshAgent>();
        stats = GetComponent<AntStats>();
        stats.SetValues(5, 1, 5);
    }
    private void Update()
    {
        antAgent.destination = myAntHill.position;
    }

    public void Drink()
    {
        if (canDrink)
        {
            canDrink = false;
            onDrink?.Invoke();
            stats.Eat(1);
            Invoke("ResetBite", 2);

        }
        if (stats.isFull)
        {
            onSpawn?.Invoke();
            stats.ResetFood();
        }
    }
    private void ResetBite()
    {
        canDrink = true;
    }
}
