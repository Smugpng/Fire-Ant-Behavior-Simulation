using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class LarveaBehavior : MonoBehaviour
{
    public Transform myAnHill;
    private AntStats stats;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("GrowUp", 3);
        stats = GetComponent<AntStats>();
        stats.SetValues(5, 10, 0);
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
}
