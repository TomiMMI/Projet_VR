using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    static public int remaining = 0;
    static public bool gameEnded = false;
    private int wave = 1;
    public int score;
    public Material[] colors;
    public GameObject enemy;

    private GameObject[] spawnPoints;
    private GameObject[] borders;
    private GameObject[] obstacles;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawnpoint");
        borders = GameObject.FindGameObjectsWithTag("border");
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnded)
        {
            Destroy(gameObject);
        }
        if(remaining == 0)
        {
            remaining = 1;
            StartCoroutine("SetupNewWave");
        }
    }
    IEnumerator Wave()
    {
        for(int i = 0; i < wave+3; i++)
        {
            yield return new WaitForSeconds(2);
            Instantiate(enemy, spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, enemy.transform.rotation);
        }
    }
    IEnumerator SetupNewWave()
    {
        yield return new WaitForSeconds(5);
        int valE, valB, valO;
        valB = Random.Range(0, colors.Length);
        valO = Random.Range(0, colors.Length);
        valE = Random.Range(0, colors.Length);
        while(valE == valB)
        {
            valE = Random.Range(0, colors.Length);
        }
        foreach(GameObject obstacle in obstacles)
        {
            obstacle.GetComponent<MeshRenderer>().material = colors[valO];
        }
        foreach (GameObject wall in borders)
        {
            wall.GetComponent<MeshRenderer>().material = colors[valB];
        }
        Enemy.col = colors[valE];
        enemy.GetComponent<NavMeshAgent>().speed += 0.5f;
        enemy.GetComponent<Animator>().speed += 0.3f;
        wave++;
        remaining = wave + 3;
        StartCoroutine("Wave");


    }
}
