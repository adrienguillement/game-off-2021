using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int levelIndex;

    public void SetScore(int starsNum)
    {
        PlayerPrefs.SetInt("Lv" + levelIndex, starsNum);
    }
}
