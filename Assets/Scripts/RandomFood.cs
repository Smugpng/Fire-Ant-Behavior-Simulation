using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFood : MonoBehaviour
{
    [SerializeField] private GameObject one,two,three;

    private void Start()
    {
        int check = Random.Range(1, 3);
        
        switch (check)
        {
            case 1: one.SetActive(true); break;
            case 2: two.SetActive(true); break;
            case 3: three.SetActive(true); break;
        }
    }


}
