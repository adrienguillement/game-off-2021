using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{

    public static int scoreValue;
    Text score;

    void Start()
    {
        score = GetComponent<Text>();
        scoreValue = 0;
    }

    void Update()
    {
        score.text = scoreValue.ToString();
    }
}
