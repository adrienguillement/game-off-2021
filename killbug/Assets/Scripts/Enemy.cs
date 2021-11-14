using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject bullet, bonusLife, bonusTripleShot, bonusDestroyAllEnemies;
    public float xSpeed, ySpeed;
    public int score;
    public bool canShoot;
    public float fireRate, health;

    public int percentageBonusLife = 10;
    public int percentageBonusTripleShoot = 10;
    public int percentageBonusDestroyAllEnemies = 80;
    public int percentageGetBonus = 20;

    private Rigidbody2D rb;
    private GameObject canon;
    private GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        gameObject.transform.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f));

        canon = transform.Find("canon").gameObject;
    }

    void Start()
    {
        if (!canShoot) return;

        fireRate = fireRate + Random.Range(fireRate / -2, fireRate / 2);
        InvokeRepeating("Shoot", fireRate, fireRate);
    }

    void Update()
    {
        rb.velocity = new Vector2(xSpeed, ySpeed * -1);
    }

    public void Damage()
    {
        health--;

        StartCoroutine(Blink());

        if(health == 0)
        {
            Die();
        }
    }

    IEnumerator Blink()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);

        yield return new WaitForSeconds(0.1f);

        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Spaceship>().Damage();
        }
    }

    void Shoot()
    {
        if(player != null){
            if(player.transform.position.y + 0.5 < gameObject.transform.position.y)
                Instantiate(bullet, canon.transform.position, Quaternion.identity);
        }
    }

    void Die()
    {
        int randomNumber = (int)Random.Range(0, 101);

        if (randomNumber < percentageGetBonus)
        {
            int randomBonusNumber = (int)Random.Range(0, 101);

            if(randomBonusNumber > 0 && randomBonusNumber <= percentageBonusLife)
            {
                Instantiate(bonusLife, transform.position, Quaternion.identity);
            }
            else if(randomBonusNumber > percentageBonusLife && randomBonusNumber <= percentageBonusLife + percentageBonusTripleShoot)
            {
                Instantiate(bonusTripleShot, transform.position, Quaternion.identity);
            }
            else if (randomBonusNumber > percentageBonusLife && randomBonusNumber <= percentageBonusLife + percentageBonusTripleShoot + percentageBonusDestroyAllEnemies)
            {
                Instantiate(bonusDestroyAllEnemies, transform.position, Quaternion.identity);
            }
        }
        ScoreScript.scoreValue += score;
        Destroy(gameObject);
    }
}
