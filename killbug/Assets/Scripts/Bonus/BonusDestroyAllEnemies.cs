using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusDestroyAllEnemies : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public GameObject shockwave;
    private bool isPlaying = false;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();


    }

    void Update()
    {
        rb.velocity = new Vector2(0, -1) * speed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {

            Instantiate(shockwave, transform.position, Quaternion.identity);

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

                Debug.Log(enemies[i].GetComponent<Renderer>().isVisible);
                if (enemies[i].GetComponent<Renderer>().isVisible)
                    Destroy(enemies[i], Random.Range(0.5f, 1.5f));
            }

            Destroy(gameObject);
        }
    }
}
