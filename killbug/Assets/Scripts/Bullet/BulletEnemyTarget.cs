using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyTarget : MonoBehaviour
{
    public int bulletSpeed = 5;
    
    private GameObject target;
    private Rigidbody2D rb;
    private Vector2 directionShoot;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();

        // Target player position if exist
        rb.velocity = ((target!= null ? target.transform.position : new Vector3(0,0,0)) - transform.position).normalized * bulletSpeed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // If bullet touch wall
        if (col.gameObject.tag == "boundsbottom" || col.gameObject.tag == "Teleport")
        {
            Destroy(gameObject);
        }
        else if (col.gameObject.tag == "Player")
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