using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusTripleShoot : MonoBehaviour
{
    public float speed;
    public float durationMax = 5;

    private Rigidbody2D rb;
    private GameObject player;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -1) * speed;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(durationMax > 0)
        {
            durationMax -= Time.deltaTime;
        }
        else
        {
            player.GetComponent<Spaceship>().DisableTripleShoot();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            durationMax = 0;
            col.gameObject.GetComponent<Spaceship>().EnableTripleShoot();
            Destroy(gameObject);
        }
    }
}
