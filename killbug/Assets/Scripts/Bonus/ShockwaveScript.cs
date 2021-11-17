using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveScript : MonoBehaviour
{
    private ParticleSystem particleSystem;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
