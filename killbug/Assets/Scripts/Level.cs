using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public static Level instance;
    public GameObject scoreScript;
    public int scoreMax;

    private int numEnemies = 0;

    private bool startNextLevel = false;
    private float nextLevelTimer = 3f;
    private ScoreManager scoreManager;

    private string[] levels = { "Level01", "LevelSelection" };
    int currentLevel = 1;

    private void Awake()
    {
        instance = this;

        scoreManager = (ScoreManager)gameObject.GetComponent(typeof(ScoreManager));
    }

    void Update()
    {

        if (startNextLevel)
        {
            if (nextLevelTimer <= 0)
            {
                currentLevel++;

                if (currentLevel <= levels.Length)
                {
                    string sceneName = levels[currentLevel - 1];
                    SceneManager.LoadScene(sceneName);
                }
                nextLevelTimer = 3f;
                startNextLevel = false;
            }
            else
            {
                nextLevelTimer -= Time.deltaTime;
            }
        }
        

    }

    public void AddEnemy()
    {
        numEnemies++;
    }

    public void RemoveEnemy()
    {
        numEnemies--;

        if (numEnemies == 0)
        {
            var finalScore = scoreScript.GetComponent<ScoreScript>().scoreValue;

            if (finalScore > 0)
            {
                scoreManager.SetScore(1);
            }
            if (finalScore > Mathf.Ceil((float)scoreMax / 2) && finalScore < scoreMax)
            {
                scoreManager.SetScore(2);
            }
            if (finalScore >= scoreMax)
            {
                scoreManager.SetScore(3);
            }
            startNextLevel = true;
        }
    }
}
