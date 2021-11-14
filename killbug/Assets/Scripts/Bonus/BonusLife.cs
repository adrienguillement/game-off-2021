using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLife : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;

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
            if(col.gameObject.GetComponent<Spaceship>().health < 5)
            {
                col.gameObject.GetComponent<Spaceship>().AddHealth();
                Destroy(gameObject);
            }
        }
    }
}
