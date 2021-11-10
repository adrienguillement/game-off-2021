using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    Rigidbody2D rb;

    public int bulletSpeed = 10;
    Vector2 directionShoot;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        directionShoot = new Vector2(0, 1);
        Debug.Log(directionShoot);
        
        rb.velocity = directionShoot * bulletSpeed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        // If bullet touch wall
        if (col.gameObject.tag == "bounds")
        {
            Destroy(gameObject);
        }

        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<Enemy>().Damage();
            Destroy(gameObject);
        }

    }

    /*public void Damage()
    {
        Destroy(gameObject);
    }*/
}