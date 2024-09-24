using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Eat : MonoBehaviour
{
    [Header("Stomach Amounts")]
    private AntStats antStats;

    private bool canEat;
    public WorkerBehavior WAS;


    // Start is called before the first frame update
    void Start()
    {
        antStats = GetComponentInParent<AntStats>();
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (antStats.isFull)
        {
            
            canEat = true;
            WAS.IsFull();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Food" && !canEat)
        {
            canEat = true;
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
        other.LoseSolid();
        antStats.Eat(1);
        Invoke("ResetEat", 2);
    }

    private void ResetEat()
    {
        canEat = false;
    }
    #endregion 

}
