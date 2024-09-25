using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntHill : MonoBehaviour
{

    public List<GameObject> antsSpawned = new List<GameObject>();
    [SerializeField]
    private GameObject ant, queen;
    private float timer = 0f;
    private int seconds = 0;
    public float storedFood, storedLiquid;

    [SerializeField]
    [Header("Number 1-10")]
    private int spawnChance = 1;
    


    // Start is called before the first frame update
    void Start() //Spawns a Queen ant at the start of the hill
    {
        GameObject queenAnt = Instantiate(queen);
        antsSpawned.Add(queenAnt);
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        SpawnCheck();
    }

    private void Timer()
    {
        timer += Time.deltaTime;
        seconds = (int)timer;

    }
    private void SpawnCheck() //After Every 3 Seconds there is a random chance to spawn a ant based of the spawnChance Int
    {
        if (seconds >= 3 )
        {
            timer = 0f;
            int rng = Random.Range(1, 10);
            if( spawnChance >= rng)
            {
                Spawn();
            }
        }
    }
    private void Spawn() //If Spawned it gives the ant the Larvea Stage and adds it to a list of spawned ants
    {
        GameObject larvea = Instantiate(ant);
        LarveaBehavior LB = larvea.AddComponent<LarveaBehavior>();
        LB.myAnHill = this.transform;
        antsSpawned.Add(larvea);
    }

    private void OnTriggerEnter(Collider other) //When ants come back with full stomachs and food they can drop off at the hill, this checks to make sure it is a fully grown ant that has food to spare
    {
        if(other.gameObject.GetComponent<WorkerBehavior>() != null && other.GetComponent<AntStats>()!= null)
        {
            Debug.Log("IN HILL");
            WorkerBehavior WB = other.GetComponent<WorkerBehavior>();
            WB.inBase = true;
            AntStats AS = other.GetComponent<AntStats>();
            AS.ResetFood(this);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<WorkerBehavior>() != null)
        {
            WorkerBehavior WB = other.GetComponent<WorkerBehavior>();
            WB.inBase = false;
        }
    }
}
