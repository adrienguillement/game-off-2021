using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusDestroyAllEnemies : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public ParticleSystem shockwave;
    private bool isPlaying = false;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    void Start()
    {
        Debug.Log("Start");
        //shockwave = GetComponent<ParticleSystem>();
        shockwave.Clear();    // Reset the particles
    }

    void Update()
    {
        Debug.Log("Update");
        rb.velocity = new Vector2(0, -1) * speed;
        if (!isPlaying)
        {
            Debug.Log("isPlaying");
            shockwave.Play();
            isPlaying = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Killing");
            //If(!shockwave.isPlaying) shockwave.Play();
            GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");

            for (int i = 0; i < bullets.Length; i++)
            {
                Destroy(bullets[i], Random.Range(0.5f, 1.5f));
            }

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            
            for(int i=0; i < enemies.Length; i++)
            {
                Destroy(enemies[i], Random.Range(0.5f, 1.5f));
            }


            Destroy(gameObject);
        }
    }
}
