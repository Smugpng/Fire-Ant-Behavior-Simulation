using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntHill : MonoBehaviour
{

    public List<GameObject> antsSpawned = new List<GameObject>();
    [SerializeField]
    private GameObject ant, queen;
    public float storedFood, storedLiquid;



    private void OnEnable()
    {
        AntStats.onStachFood += GainFood;
        LarveaBehavior.onEat += LarvaeBite;
        QueenBehavior.onDrink += QueenDrink;
        QueenBehavior.onSpawn += Spawn;
    }
    // Start is called before the first frame update
    void Start() //Spawns a Queen ant at the start of the hill
    {
        SpawnQueen();
    }

    private void IncreaseHillSize()
    {
        this.transform.localScale += this.transform.localScale  * .01f;
    }
    private void SpawnQueen()
    {
        GameObject queenAnt = Instantiate(queen, this.transform.position, this.transform.rotation);
        antsSpawned.Add(queenAnt);
        QueenBehavior QB = queenAnt.AddComponent<QueenBehavior>();
        QB.myAntHill = this.transform;
        Invoke("Spawn", 1);
    }
    private void Spawn() //If Spawned it gives the ant the Larvea Stage and adds it to a list of spawned ants
    {
        IncreaseHillSize();
        GameObject larvea = Instantiate(ant, this.transform.position, this.transform.rotation);
        LarveaBehavior LB = larvea.AddComponent<LarveaBehavior>();
        LB.myAnHill = this.transform;
        antsSpawned.Add(larvea);
    }

    private void OnTriggerEnter(Collider other) //When ants come back with full stomachs and food they can drop off at the hill, this checks to make sure it is a fully grown ant that has food to spare
    {
        if(other.gameObject.GetComponent<WorkerBehavior>() != null && other.GetComponent<AntStats>()!= null)
        {
            WorkerBehavior WB = other.GetComponent<WorkerBehavior>();
            WB.inBase = true;
            AntStats AS = other.GetComponent<AntStats>();
            AS.ResetFood();
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
       
        if (other.gameObject.GetComponent<LarveaBehavior>() != null && storedFood >= 0)
        {
            LarveaBehavior LB = other.GetComponent<LarveaBehavior>();
            LB.Eat();
        }
        if (other.gameObject.GetComponent<QueenBehavior>() != null && storedLiquid >= 0) 
        {
            QueenBehavior QB = other.GetComponent<QueenBehavior>();
            QB.Drink();
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

    private void GainFood(float food, float liquid)
    {
        storedFood += food;
        storedLiquid += liquid;
    }
    private void LarvaeBite()
    {
        storedFood--;
    }
    private void QueenDrink()
    {
        storedLiquid--;
    }
}
