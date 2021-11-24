using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightDepression : MonoBehaviour
{
    private Light2D lightSource;

    private float maxInnerRadius = 5f;
    private float minInnerRadius = 1f;
    private float currentInnerRadius;
    private bool isIncreasing = true;

    void Start()
    {
        lightSource = gameObject.GetComponent<Light2D>();
        currentInnerRadius = lightSource.pointLightInnerRadius;
    }

    void Update()
    {

        lightSource.pointLightInnerRadius = currentInnerRadius;
        Debug.Log("currentInnterRadius : " + currentInnerRadius);

        if (currentInnerRadius <= minInnerRadius)
        {
            isIncreasing = true;
        }

        if (currentInnerRadius <= maxInnerRadius && isIncreasing)
        {
            isIncreasing = true;
            currentInnerRadius += currentInnerRadius * (Time.deltaTime / 2);
        }
        else if (currentInnerRadius >= minInnerRadius)
        {
            isIncreasing = false;
            currentInnerRadius -= currentInnerRadius * (Time.deltaTime / 2);
        }
    }
}
