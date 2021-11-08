using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{

    GameObject player;
    bool gameOver = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(player==null && !gameOver)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        gameOver = true;
        StartCoroutine(LoadGameOver());
    }

    IEnumerator LoadGameOver()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
}
