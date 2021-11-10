using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spaceship : MonoBehaviour
{
    GameObject canonl, canonr;

    public GameObject bullet;/*, explosion*/
    public float speed;
    
    int delay = 0;
    public float firerate;

    Rigidbody2D rb;
    public int health = 200;

    void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        canonl = transform.Find("canonl").gameObject;
    }

    void Start()
    {
        
    }

    void Update()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        var verticalInput = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(horizontalInput * speed, verticalInput * speed);

        if (Input.GetButton("Fire2") && delay > firerate)
        {
            Shoot();
        }

        delay++;
    }

    void Shoot()
    {
        delay = 0;
        Instantiate(bullet, canonl.transform.position, Quaternion.identity);
    }

    public void Damage()
    {
        health--;
        StartCoroutine(Blink());

        if (health == 0)
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

    void Die()
    {
        //Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject, 0.1f);
    }
}