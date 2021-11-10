using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    Rigidbody2D rb;

    public int bulletSpeed = 5;

    public GameObject target;
    Vector2 directionShoot;


    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = ((target!= null ? target.transform.position : new Vector3(0,0,0)) - transform.position).normalized * bulletSpeed;
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // If bullet touch wall
        if (col.gameObject.tag == "boundsbottom" || col.gameObject.tag == "Teleport")
        {
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Spaceship>().Damage();
            Destroy(gameObject);
        }
    }

    public void Damage()
    {
        Destroy(gameObject);
    }
}