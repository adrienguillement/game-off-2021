using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    public int bulletSpeed = 10;
    public AudioClip clip;

    public AudioSource sound;
    

    public Rigidbody2D rb;
    private Vector2 directionShoot;
    //private AudioSource source {  get { return GetComponent<AudioSource>(); } }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        directionShoot = new Vector2(0, 1);

        rb.velocity = directionShoot * bulletSpeed;
    }

    void Start()
    {
        // Son fonctionnel 1
        //AudioSource.PlayClipAtPoint(clip, transform.position, 1f);



        
        //sound.PlayOneShot(clip);

        sound.Play();
    }

    void Update()
    {
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
            col.gameObject.GetComponent<IEnemy>().Damage();
            Destroy(gameObject);
        }

    }
}