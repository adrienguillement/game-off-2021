using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public static Level instance;

    private int numEnemies = 0;
    private bool startNextLevel = false;
    private float nextLevelTimer = 3f;

    private string[] levels = { "Level01", "LevelSelection" };
    int currentLevel = 1;

    private void Awake()
    {
        instance = this;
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
                    Debug.Log(numEnemies);
                    string sceneName = levels[currentLevel - 1];
                    SceneManager.LoadScene(sceneName);
                }
                else
                {
                    Debug.Log("END");
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
            ScoreManager.SetScore(Random.Range(0,3));
            startNextLevel = true;
        }
    }
}
