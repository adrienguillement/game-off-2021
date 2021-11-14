using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spaceship : MonoBehaviour
{
    public int health = 3;
    public GameObject[] hearths;

    public float firerate;
    public GameObject bullet;
    public float speed;

    public Animator leftanim;

    private Vector2 mainCamera;
    private GameObject canon;
    private int delay = 0;
    private Rigidbody2D rb;
    private bool isTripleShootActivated = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        canon = transform.Find("canon").gameObject;
        mainCamera = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        // Display right count of hearts "life"
        for (int i = 0; i < health; i++)
        {
            hearths[i].gameObject.SetActive(true);
        }
    }

    void Update()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        var verticalInput = Input.GetAxisRaw("Vertical");

        leftanim.SetFloat("speed", horizontalInput * speed);

        rb.velocity = new Vector2(horizontalInput * speed, verticalInput * speed);

        if (Input.GetButton("Fire2") && delay > firerate)
        {
            Shoot();
        }

        delay++;

        Teleport();
        Bounds();

    }

    void Bounds()
    {
        if (gameObject.transform.position.y > mainCamera.y - 0.5f)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, mainCamera.y -0.5f);
        }
        else if (gameObject.transform.position.y < -mainCamera.y)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, -mainCamera.y);
        }
    }

    void Teleport()
    {
        if (gameObject.transform.position.x > mainCamera.x)
        {
            gameObject.transform.position = new Vector2(-(gameObject.transform.position.x - 0.5f), gameObject.transform.position.y);
        }
        else if (gameObject.transform.position.x < -mainCamera.x)
        {
            gameObject.transform.position = new Vector2(-(gameObject.transform.position.x + 0.5f), gameObject.transform.position.y);
        }
    }

    void Shoot()
    {
        delay = 0;
        Instantiate(bullet, canon.transform.position, Quaternion.identity);

        if (isTripleShootActivated)
        {
            GameObject rightBullet = (GameObject)Instantiate(bullet, canon.transform.position, Quaternion.identity);
            rightBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 1) * 10;

            GameObject leftBullet = (GameObject)Instantiate(bullet, canon.transform.position, Quaternion.identity);
            leftBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 1) * 10;
        }
    }

    public void Damage()
    {
        health--;

        StartCoroutine(Blink());

        if (health == 0)
        {
            Die();
        }

        // UI - set hearth not visible
        hearths[health].gameObject.SetActive(false);
    }

    IEnumerator Blink()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);

        yield return new WaitForSeconds(0.1f);

        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
    }

    void Die()
    {
        Destroy(gameObject, 0.1f);
    }

    public void AddHealth()
    {
        hearths[health].gameObject.SetActive(true);
        health++;
    }

    public void EnableTripleShoot()
    {
        isTripleShootActivated = true;
    }

    public void DisableTripleShoot()
    {
        isTripleShootActivated = false;
    }
}
