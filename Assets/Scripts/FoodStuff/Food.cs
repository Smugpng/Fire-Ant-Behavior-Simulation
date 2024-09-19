using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Food : MonoBehaviour
{
    
    protected float foodHealth; //Naming Health just for understanding sake
    private float solidPercentage;
    private float liquidPercentage;

    private void Start()
    {
        solidPercentage = foodHealth / Random.Range(1, 5);
        liquidPercentage = foodHealth - solidPercentage;
    }
    private void Update()
    {
        if (solidPercentage + liquidPercentage <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void LoseSolid()
    {
        solidPercentage--;
    }
    public void LoseLiquid()
    {
        solidPercentage--;
    }
}
