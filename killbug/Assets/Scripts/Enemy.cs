using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject bullet, explosion;
    GameObject canon;

    public float xSpeed, ySpeed;
    public int score;

    public bool canShoot;
    public float fireRate, health;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        canon = transform.Find("canon").gameObject;
    }

    void Start()
    {
        if (!canShoot) return;

        fireRate = fireRate + Random.Range(fireRate / -2, fireRate / 2);
        InvokeRepeating("Shoot", fireRate, fireRate);

    }

    void Update()
    {
        rb.velocity = new Vector2(xSpeed, ySpeed * -1);
        
    }

    public void Damage()
    {
        health--;

        StartCoroutine(Blink());

        if(health == 0)
        {
            Die();
           
        }
    }

    IEnumerator Blink()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);

        yield return new WaitForSeconds(0.1f);

        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Spaceship>().Damage();
            Die();
        }
    }

    void Shoot()
    {
        GameObject temp = (GameObject)Instantiate(bullet, canon.transform.position, Quaternion.identity);
        temp.GetComponent<Bullet>().ChangeDirection();
    }

    void Die()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + score);
        Destroy(gameObject);
    }
}
