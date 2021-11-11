using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    public int bulletSpeed = 10;

    private Rigidbody2D rb;
    private Vector2 directionShoot;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        directionShoot = new Vector2(0, 1);
        
        rb.velocity = directionShoot * bulletSpeed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        // If bullet touch wall
        if (col.gameObject.tag == "bounds")
        {
            Destroy(gameObject);
        }
        else if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<Enemy>().Damage();
            Destroy(gameObject);
        }

    }
}