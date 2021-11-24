using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelCancer : MonoBehaviour
{
    public static LevelCancer instance;
    public GameObject scoreScript;
    public GameObject player;
    public int scoreMax;
    public GameObject endLevelUI;
    public GameObject[] stars;
    public Sprite starSprite;

    public GameObject enemyCancer;

    public int timeToKill;

    private int numEnemies = 0;
    private int numEnemiesKills = 0;
    private int numEnemiesSpawn = 0;
    private int maxEnnemies = 32;

    private bool startNextLevel = false;
    private bool isLevelEnded = false;
    private float nextLevelTimer = 3f;
    private ScoreManager scoreManager;
    private Vector2 mainCamera;

    private string[] levels = { "Level05_cancer", "LevelSelection" };
    int currentLevel = 1;

    private void Awake()
    {
        instance = this;

        scoreManager = (ScoreManager)gameObject.GetComponent(typeof(ScoreManager));

        mainCamera = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    private void Start()
    {
        // Spawn first enemy
        var firstEnemy = Instantiate(enemyCancer, new Vector2(0, 10), Quaternion.identity);
    }

    void Update()
    {
        if (isLevelEnded)
        {
            UpdateStarsImage();
            endLevelUI.SetActive(true);
        }
    }

    private void UpdateStarsImage()
    {
        Debug.Log(scoreManager.GetScore());
        for (int i = 0; i < scoreManager.GetScore(); i++)
        {
            stars[i].gameObject.GetComponent<Image>().sprite = starSprite;
        }
    }

    public void AddEnemy()
    {
        numEnemies++;
        numEnemiesSpawn++;
    }

    public void RemoveEnemy(bool isPlayerKill)
    {
        numEnemies--;

        if (isPlayerKill)
        {
            numEnemiesKills++;

            if (numEnemiesSpawn + 1 <= maxEnnemies) Instantiate(enemyCancer, new Vector2(Random.Range(-mainCamera.x, 0), Random.Range(2, mainCamera.y)), Quaternion.identity);
            if (numEnemiesSpawn + 2 <= maxEnnemies) Instantiate(enemyCancer, new Vector2(Random.Range(0, mainCamera.x), Random.Range(2, mainCamera.y)), Quaternion.identity);
        }

        if (numEnemies == 0)
        {
            if ((numEnemiesKills * 100 / maxEnnemies) >= 80)
                scoreManager.SetScore(3);
            else if (numEnemiesKills > Mathf.Ceil((float)maxEnnemies / 2) && numEnemiesKills < maxEnnemies)
                scoreManager.SetScore(2);
            else if (numEnemiesKills > 0)
                scoreManager.SetScore(1);

            endLevelUI.SetActive(true);
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
