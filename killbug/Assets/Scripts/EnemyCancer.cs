using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCancer : MonoBehaviour, IEnemy
{
    public GameObject bullet, bonusLife, bonusTripleShot, bonusDestroyAllEnemies, bonusShield;
    public GameObject malusAlzheimer;
    public float xSpeed, ySpeed;
    public int score;
    public bool canShoot;
    public float fireRate, health;

    public int percentageBonusLife = 10;
    public int percentageBonusTripleShoot = 5;
    public int percentageBonusDestroyAllEnemies = 5;
    public int percentageBonusShield = 80;

    public int percentageMalusAlzheimer = 100;

    public int percentageGetSomething = 20;

    private Rigidbody2D rb;
    private GameObject canon;
    private GameObject player;
    private ScoreScript scoreScript;

    private bool isShooting = false;

    private Vector2 mainCamera;



    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        gameObject.transform.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f));

        canon = transform.Find("canon").gameObject;

        mainCamera = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        LevelCancer.instance.AddEnemy();
    }

    void Start()
    {
        scoreScript = GameObject.Find("ScoreScript").GetComponent<ScoreScript>();

        if (!canShoot) return;

        fireRate = fireRate + Random.Range(fireRate / -2, fireRate / 2);
    }

    void Update()
    {
        rb.velocity = new Vector2(xSpeed, ySpeed * -1);

        if(gameObject.transform.position.y < mainCamera.y)
        {
            if (!isShooting)
            {
                isShooting = true;
                InvokeRepeating("Shoot", fireRate, fireRate);
            }
        }
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
        scoreScript.scoreValue += score;

        int randomNumber = (int)Random.Range(0, 101);

        if (randomNumber < percentageGetSomething)
        {
            int randomBonusOrMalusNumber = (int)Random.Range(0, 2);

            if(randomBonusOrMalusNumber == 0) // spawn bonus
            {

                int randomBonusNumber = (int)Random.Range(0, 101);

                if (randomBonusNumber > 0 && randomBonusNumber <= percentageBonusLife)
                {
                    Instantiate(bonusLife, transform.position, Quaternion.identity);
                }
                else if (randomBonusNumber > percentageBonusLife && randomBonusNumber <= percentageBonusLife + percentageBonusTripleShoot)
                {
                    Instantiate(bonusTripleShot, transform.position, Quaternion.identity);
                }
                else if (randomBonusNumber > percentageBonusLife && randomBonusNumber <= percentageBonusLife + percentageBonusTripleShoot + percentageBonusDestroyAllEnemies)
                {
                    Instantiate(bonusDestroyAllEnemies, transform.position, Quaternion.identity);
                }
                else if (randomBonusNumber > percentageBonusLife && randomBonusNumber <= percentageBonusLife + percentageBonusTripleShoot + percentageBonusDestroyAllEnemies + percentageBonusShield)
                {
                    Instantiate(bonusShield, transform.position, Quaternion.identity);
                }
            }
            else
            {
                int randomMalusNumber = (int)Random.Range(0, 101);

                if (randomMalusNumber > 0 && randomMalusNumber <= percentageMalusAlzheimer)
                {
                    Instantiate(malusAlzheimer, transform.position, Quaternion.identity);
                }
            }
        }

        Destroy(gameObject);
    }

    public void OneShot()
    {
        StartCoroutine(DestroyDelay());
    }

    private IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        Die();
    }

    private void OnDestroy()
    {
        LevelCancer.instance.RemoveEnemy((health == 0), transform.position);
    }
}
