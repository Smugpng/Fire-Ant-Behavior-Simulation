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

    [SerializeField]
    [Header("Number 1-10")]
    private int spawnChance = 1;
    


    // Start is called before the first frame update
    void Start()
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
    private void SpawnCheck()
    {
        if (seconds >= 30 )
        {
            timer = 0f;
            int rng = Random.Range(1, 10);
            if( spawnChance >= rng)
            {
                Spawn();
            }
        }
    }
    private void Spawn()
    {
        GameObject workerAnt = Instantiate(ant);
        WorkerAntSearch worker = workerAnt.GetComponent<WorkerAntSearch>();
        worker.antHill = this.transform;
        antsSpawned.Add(workerAnt);
    }
}
