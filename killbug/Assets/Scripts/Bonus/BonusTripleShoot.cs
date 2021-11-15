using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusTripleShoot : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;
    private GameObject player;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -1) * speed;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {

            col.gameObject.GetComponent<Spaceship>().ResetTripleShootDurationTmp();
            col.gameObject.GetComponent<Spaceship>().isTripleShootActivated = true;
            Destroy(gameObject);
        }
    }
}
