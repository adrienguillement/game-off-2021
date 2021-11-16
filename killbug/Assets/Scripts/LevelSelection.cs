using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{

    public bool unlocked;
    public Image unlockImage;
    public GameObject[] stars;

    void Start()
    {
        if (int.Parse(gameObject.name) == 1)
        {
            unlocked = true;
        }
    }

    private void Update()
    {
        UpdateLevelImage();
        UpdateLevelStatus();
    }

    private void UpdateLevelImage()
    {
        if(!unlocked)
        {
            unlockImage.gameObject.SetActive(true);

            stars[0].gameObject.SetActive(false);
            stars[1].gameObject.SetActive(false);
            stars[2].gameObject.SetActive(false);
        }
        else // level unlocked
        {
            unlockImage.gameObject.SetActive(false);

            stars[0].gameObject.SetActive(true);
            stars[1].gameObject.SetActive(true);
            stars[2].gameObject.SetActive(true);
        }
    }

    private void UpdateLevelStatus()
    {
        // level1 : Lv0 : 3
        int PreviousLevelNum = int.Parse(gameObject.name) - 2;

        if (PlayerPrefs.GetInt("Lv" + PreviousLevelNum) > 0)
        {
            unlocked = true;
        }
    }

    public void Selection(string levelName)
    {
        if (unlocked)
        {
            SceneManager.LoadScene(levelName);
        }
    }
}
