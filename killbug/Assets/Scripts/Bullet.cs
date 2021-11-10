using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;

    int direction = 1;
    public int bulletSpeed = 10;
    public Transform transfor1;

    public GameObject target;
    Vector2 directionShoot;
    float directionAngle;


    void Awake()
    {
        transfor1 = gameObject.transform;
        target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        
    }

    public void ChangeDirection()
    {
        direction *= -1;
        
    }

    void Update()
    {
        directionShoot = Camera.main.ScreenToWorldPoint(new Vector2(target.transform.position.x, target.transform.position.y));
        Debug.Log(directionShoot);
        directionShoot = new Vector2(directionShoot.x - transfor1.position.x, directionShoot.y - transfor1.position.y);
        directionAngle = Mathf.Atan2(directionShoot.y, directionShoot.x) * Mathf.Rad2Deg;
        transfor1.rotation = Quaternion.Euler(0, 0, directionAngle);

        rb.velocity = transfor1.up * bulletSpeed;
        Debug.Log(transfor1);
        
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (direction == 1)
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

            // uncomment this to destroy enemy bullet with your bullet
            //if (col.gameObject.tag == "EnemyBullet")
            //{
            //    col.gameObject.GetComponent<Bullet>().Damage();
            //    Destroy(gameObject);
            //}
        }
        else
        {
            // If bullet touch wall
            if (col.gameObject.tag == "boundsbottom")
            {
                Destroy(gameObject);
            }
            if (col.gameObject.tag == "Player")
            {
                col.gameObject.GetComponent<Spaceship>().Damage();
                Destroy(gameObject);
            }
        }

    }

    public void Damage()
    {
        Destroy(gameObject);
    }
}
