using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Eat : MonoBehaviour
{
    [Header("Stomach Amounts")]
    private AntStats antStats;

    [SerializeField]
    private bool CanEat;
    public WorkerBehavior WorkerBehaviour;
    public GameObject foodVisuals;


    // Start is called before the first frame update
    void Start()
    {
        CanEat = true;
        antStats = GetComponentInParent<AntStats>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (antStats.isFull)
        {
            CanEat = false;
            WorkerBehaviour.IsFull();
            foodVisuals.SetActive(true);
        }
        else
        {

        }
        {
            foodVisuals.SetActive(false);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Food")
        {
            CanEat = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
        if (other.tag == "Food" && CanEat == true)
        {
            CanEat = false;
            Food foodToEat = other.GetComponent<Food>();
            if (antStats.needSolids)
            {
                Bite(foodToEat);
            }
            else
            {
                Drink(foodToEat);
            }
        }

        if(other.name == "AntHill")
        {
            CanEat = true;
            foodVisuals.SetActive(false);
        }
    }
    //These functions updates the current amount of the type of foods they have
    #region Eating/Drinking 
    private void Drink(Food other)
    {
        
        other.LoseLiquid();
        antStats.Drink(1);
        Invoke("ResetEat", 2);
    }
    private void Bite(Food other)
    {
        Debug.Log("Step 3");
        other.LoseSolid();
        antStats.Eat(1);
        Invoke("ResetEat", 2);
    }

    private void ResetEat()
    {
        CanEat = true;
    }
    #endregion 

}
