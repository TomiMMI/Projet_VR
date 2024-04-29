using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UtilityManager : MonoBehaviour
{
    public TextMeshPro highScoreText;
    public int score = 0;
    private int highScore = 0;
    public TextMeshPro actualScore;
    public GameObject gameController;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(gameController, transform) ;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(score);
    }
    public void gameOver()
    {
        if (score > highScore)
        {
            highScore = score;
            highScoreText.text = highScore.ToString();
        }

        foreach(GameObject a in GameObject.FindGameObjectsWithTag("Enemy")){
            Destroy(a);
        }
        score = 0;
        actualizeScore();
        Destroy(GameObject.FindGameObjectWithTag("GameController"));
        Instantiate(gameController);

    }
    public void actualizeScore()
    {
        actualScore.text = score.ToString();
    }
}
