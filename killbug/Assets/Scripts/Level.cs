using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public static Level instance;
    public GameObject scoreScript;
    public int scoreMax;
    public GameObject endLevelUI;
    public GameObject[] stars;
    public Sprite starSprite;

    private int numEnemies = 0;

    private GameObject player;
    private bool isLevelEnded = false;
    private ScoreManager scoreManager;

    private string[] levels = { "Level01", "LevelSelection" };

    private void Awake()
    {
        instance = this;

        player = GameObject.FindGameObjectWithTag("Player");
        scoreManager = (ScoreManager)gameObject.GetComponent(typeof(ScoreManager));
    }

    void Update()
    {
        if (isLevelEnded)
        {
            UpdateStarsImage();
            endLevelUI.SetActive(true);
        }
        if (player == null)
        {
            isLevelEnded = true;
        }
    }

    private void UpdateStarsImage()
    {
        for (int i = 0; i < scoreManager.GetScore(); i++)
        {
            stars[i].gameObject.GetComponent<Image>().sprite = starSprite;
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
            isLevelEnded = true;
        }
    }


    public void RestartLevel()
    {
        endLevelUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    public void QuitGame()
    {
        Debug.Log("Quit game...");
        Application.Quit();
    }
}
