using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float timeBetweenSpawn = 5.0f;
    private GameObject[] spawnPosList;
    public GameObject[] ennemies;
    // Start is called before the first frame update
    void Start()
    {
        spawnPosList = GameObject.FindGameObjectsWithTag("Spawnpoint");
        InvokeRepeating("Spawn", 10, timeBetweenSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Spawn()
    {
        GameObject enemy = ennemies[Random.Range(0, ennemies.Length - 1)];
        Instantiate(enemy, spawnPosList[Random.Range(0, spawnPosList.Length - 1)].transform);
    }
}
