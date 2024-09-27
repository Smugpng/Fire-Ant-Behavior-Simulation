using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntStats : MonoBehaviour //This script stores and updates the basic values of the ant
{
    [SerializeField]
    private float hp, solidNeeds, liquidNeeds;
    [SerializeField]
    private float currSolids, currLiquid;

    public bool needSolids, isFull;


    public delegate void OnStachFood(float food, float liquid);
    public static event OnStachFood onStachFood;
    // Update is called once per frame
    void Update()
    {
       SetBools();
    }
    void SetBools()
    {
        if (solidNeeds > liquidNeeds)
        {
            needSolids = true;
        }
        else
        {
            needSolids = false;
        }
        if (currSolids >= solidNeeds || currLiquid >= liquidNeeds)
        {
            isFull = true;
        }
        else
        {
            isFull = false;
        }
    }

    #region Update Values
    public void SetValues(float hpSet, float solidSet, float liquidSet)
    {
        hp = hpSet;
        solidNeeds = solidSet;
        liquidNeeds = liquidSet;
    }

    public void LoseHealth(float hpLost)
    {
        hp -= hpLost;
    }
    public void Drink(float drinkAmt)
    {
        currLiquid += drinkAmt;
    }
    public void Eat(float eatAmt)
    {
        currSolids += eatAmt;
    }
    public void ResetFood()
    {
        onStachFood?.Invoke(currSolids, currLiquid);
        currSolids = 0;
        currLiquid = 0;
    }
    #endregion

}
