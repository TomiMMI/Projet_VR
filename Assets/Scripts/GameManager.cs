using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class GameManager : MonoBehaviour
{
    static public int remaining = 0;
    private int wave = 1;
    public Material[] colors;
    public GameObject enemy;

    private float spawnTime = 2.0f;
    private GameObject[] spawnPoints;
    private GameObject[] borders;
    private GameObject[] obstacles;
    private GameObject[] textes;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.remaining = 0;
        textes = GameObject.FindGameObjectsWithTag("Text");
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawnpoint");
        borders = GameObject.FindGameObjectsWithTag("border");
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
    }

    // Update is called once per frame
    void Update()
    {
        if(remaining <= 0)
        {
            remaining = 1;

            StartCoroutine("SetupNewWave");
        }
    }
    IEnumerator Wave()
    {
        for(int i = 0; i < wave+3; i++)
        {
            yield return new WaitForSeconds(spawnTime);
            Instantiate(enemy, spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, enemy.transform.rotation);
        }
    }
    IEnumerator SetupNewWave()
    {
        yield return new WaitForSeconds(3);
        int valE, valB, valO;
        valB = Random.Range(0, colors.Length);
        do
        {
            valO = Random.Range(0, colors.Length);
        }
        while (valO == 1);
        valE = Random.Range(0, colors.Length);
        while(valE == valB)
        {
            valE = Random.Range(0, colors.Length);
        }
        foreach (GameObject obstacle in obstacles)
        {
            obstacle.GetComponent<MeshRenderer>().material = colors[valO];
        }
        foreach (GameObject wall in borders)
        {
            wall.GetComponent<MeshRenderer>().material = colors[valB];
        }
        Enemy.col = colors[valE];
        enemy.GetComponent<NavMeshAgent>().speed += 0.5f;
        enemy.GetComponent<Animator>().speed += 0.5f;
        wave++;
        remaining = wave + 3;
        spawnTime /= 1.4f;
        StartCoroutine("Wave");


    }
}
