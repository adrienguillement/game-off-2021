using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    public Text CountDownText;
    public static bool isPaused = false;
    public static bool isCountdown = false;
    public GameObject pauseMenuUI;
    public int countDownOnResume = 3;

    void Awake()
    {

        CountDownText.enabled = false;
    }

    void Update()
    {
        if (!isCountdown)
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                    Resume();
                else
                    Pause();
            }
        }
    }

    IEnumerator Countdown(int seconds)
    {

        isCountdown = true;
        int counter = seconds;
        while (counter > 1)
        {
            yield return new WaitForSecondsRealtime(1);
            counter--;
            CountDownText.text = counter.ToString();
        }

        yield return new WaitForSecondsRealtime(1);

        CountDownText.enabled = false;
        CountDownText.text = countDownOnResume.ToString();

        Time.timeScale = 1f;
        isCountdown = false;
        isPaused = false;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);

        CountDownText.enabled = true;
        StartCoroutine(Countdown(countDownOnResume));
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quit game...");
        Application.Quit();
    }
}
