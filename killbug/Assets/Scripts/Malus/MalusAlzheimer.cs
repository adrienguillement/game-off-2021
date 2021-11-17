using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalusAlzheimer : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -1) * speed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Spaceship>().ResetAlzheimerDurationTmp();
            col.gameObject.GetComponent<Spaceship>().isAlzheimerActivated = true;
            Destroy(gameObject);
        }
    }
}
