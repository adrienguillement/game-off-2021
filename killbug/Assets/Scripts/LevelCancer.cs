using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCancer : MonoBehaviour
{
    public static LevelCancer instance;
    public GameObject scoreScript;
    public GameObject player;
    public int scoreMax;

    public GameObject enemyCancer;

    public int timeToKill;

    private int numEnemies = 0;
    private int maxEnnemies = 16;

    private bool startNextLevel = false;
    private float nextLevelTimer = 3f;
    private ScoreManager scoreManager;

    private string[] levels = { "Level05_cancer", "LevelSelection" };
    int currentLevel = 1;

    private void Awake()
    {
        instance = this;

        scoreManager = (ScoreManager)gameObject.GetComponent(typeof(ScoreManager));
    }

    private void Start()
    {
        // Spawn first enemy
        var firstEnemy = Instantiate(enemyCancer, new Vector2(0, 10), Quaternion.identity);
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
        Debug.Log("jreger");
        numEnemies--;

        Instantiate(enemyCancer, new Vector2(0, 10), Quaternion.identity);
        Instantiate(enemyCancer, new Vector2(5, 5), Quaternion.identity);

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
