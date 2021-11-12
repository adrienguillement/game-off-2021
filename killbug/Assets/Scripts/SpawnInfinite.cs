using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInfinite : MonoBehaviour
{
    public float rate;
    public GameObject[] enemies;
    public int waves = 1;

    private Vector2 mainCamera;

    void Awake()
    {
        mainCamera = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void Start()
    {
        InvokeRepeating("SpawnEnemy", rate, rate);
    }

    void SpawnEnemy()
    {
        for(int i=0; i<waves; i++)
        {
            Instantiate(enemies[(int)Random.Range(0, enemies.Length)], new Vector3(Random.Range(-mainCamera.x, mainCamera.x), mainCamera.y, 0), Quaternion.identity);
        }
    }
}