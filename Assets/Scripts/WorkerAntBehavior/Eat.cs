using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eat : MonoBehaviour
{
    [Header("Stomach Amounts")]
    [SerializeField]
    private float maxSolidFood, maxLiquidFood;
    [SerializeField]
    private float currSolidFood = 0, currLiquidFood = 0;
    private bool wantToCarry, wantToDrink;

    private bool canEat;
    WorkerAntSearch WAS;
    // Start is called before the first frame update
    void Start()
    {
        WAS = GetComponentInParent<WorkerAntSearch>();
        maxSolidFood = Random.Range(1, 5); maxLiquidFood = Random.Range(1, 5);
        if (maxLiquidFood >= maxSolidFood)
        {
            wantToDrink = true;
            wantToCarry = false;
        }
        else
        {
            wantToDrink = false;
            wantToCarry = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if( currLiquidFood >= maxLiquidFood || currSolidFood >= maxSolidFood)
        {
            WAS.IsFull();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Food" && !canEat)
        {
            canEat = true;
            Food foodToEat = other.GetComponent<Food>();
            if (wantToCarry )
            {
                Bite(foodToEat);
            }
            else if (wantToDrink)
            {
                Drink(foodToEat);
            }
        }
    }
    private void Drink(Food other)
    {
        other.LoseLiquid();
        currLiquidFood++;
        Invoke("ResetEat", 2);
    }
    private void Bite(Food other)
    {
        other.LoseSolid();
        currSolidFood++;
        Invoke("ResetEat", 2);
    }

    private void ResetEat()
    {
        canEat = false;
    }
}
