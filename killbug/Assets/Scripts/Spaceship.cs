using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;


public class Spaceship : MonoBehaviour
{
    public int health = 3;
    public GameObject[] hearths;

    public bool isShieldActivated = false;
    public float shieldDuration = 5f;
    public GameObject shieldPrefab;
    private GameObject shield;

    public bool isAlzheimerActivated = false;
    public float alzheimerDuration = 5f;

    public bool isTripleShootActivated = false;
    public float tripleShootDuration = 5f;

    public float firerate;
    public GameObject bullet;
    public float speed;

    public AudioClip audioShot;
    public AudioClip audioDeath;

    private Vector2 mainCamera;
    private GameObject canon;
    private int delay = 0;
    private Rigidbody2D rb;

    private float shieldDurationTmp, tripleShootDurationTmp, alzheimerDurationTmp;
    private Color lerpedColorShield = Color.white;

    private Vector3 rawInputMovement;


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


        shieldDurationTmp = shieldDuration;
        tripleShootDurationTmp = tripleShootDuration;
    }

    void Update()
    {
        //var horizontalInput = Input.GetAxisRaw("Horizontal");
        var verticalInput = 0f;

        if (isAlzheimerActivated)
        {
            if (alzheimerDurationTmp > 0)
            {
                alzheimerDurationTmp -= Time.deltaTime;
            }
            else
            {
                isAlzheimerActivated = false;
                alzheimerDurationTmp = alzheimerDuration;
            }
        }
        else
        {
            //verticalInput = Input.GetAxisRaw("Vertical");
        }
        Debug.Log(rawInputMovement.y);
        rb.velocity = new Vector2(rawInputMovement.x * speed, rawInputMovement.y * speed);


        if (isShieldActivated)
        {
            if (shield == null)
            {
                shield = Instantiate(shieldPrefab, transform.position, gameObject.transform.rotation);
                shield.transform.parent = gameObject.transform;
            }

            if (shieldDurationTmp > 0)
            {
                if (shieldDurationTmp < 1.5f)
                {
                    var shieldRenderer = shield.GetComponent<Renderer>();

                    lerpedColorShield = Color.Lerp(Color.white, new Color(shieldRenderer.material.color.r, shieldRenderer.material.color.g, shieldRenderer.material.color.b, 0f), Mathf.PingPong(Time.time * 10, 1));
                    shieldRenderer.GetComponent<Renderer>().material.SetColor("_Color", lerpedColorShield);
                }
                shieldDurationTmp -= Time.deltaTime;
                // Change shield color -> 1f
            }
            else
            {
                ResetShield();
            }
        }

        if (isTripleShootActivated)
        {
            if (tripleShootDurationTmp > 0)
            {
                tripleShootDurationTmp -= Time.deltaTime;
            }
            else
            {
                tripleShootDurationTmp = tripleShootDuration;
                isTripleShootActivated = false;
            }
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

        SoundManager.PlayClip(audioShot, 0.3f);

        if (isTripleShootActivated)
        {
            GameObject rightBullet = (GameObject)Instantiate(bullet, canon.transform.position, Quaternion.Euler(new Vector3(0, 0, 40)));
            rightBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 1) * 10;

            GameObject leftBullet = (GameObject)Instantiate(bullet, canon.transform.position, Quaternion.Euler(new Vector3(0, 0, -40)));
            leftBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 1) * 10;
        }
    }

    public void Damage()
    {
        if (shield != null)
        {
            ResetShield();
            return;
        }

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
        SoundManager.PlayClip(audioDeath, 0.5f);
        
        Destroy(gameObject, 0.1f);
    }

    public void AddHealth()
    {
        hearths[health].gameObject.SetActive(true);
        health++;
    }

    public void ResetShield()
    {
        ResetShieldDurationTmp();
        isShieldActivated = false;
        Destroy(shield);
    }

    public void ResetShieldDurationTmp()
    {
        shieldDurationTmp = shieldDuration;
    }

    public void ResetTripleShootDurationTmp()
    {
        tripleShootDurationTmp = tripleShootDuration;
    }

    public void ResetAlzheimerDurationTmp()
    {
        alzheimerDurationTmp = alzheimerDuration;
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector3(inputMovement.x, inputMovement.y, 0);
    }

    public void OnAttack(InputAction.CallbackContext value)
    {
        Debug.Log(value.interaction);
            if (delay > firerate)
            {
                Shoot();
            }
    }
}
