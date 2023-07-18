using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    public GameObject Laser, playerShip;
    public AudioSource laser, hit, peril;
    public Text hp;
    public Sprite[] playerSprites;
    public Animator anim;

    public float damageCooldown;
    public bool autoPlay;
    float waitForNextShot = 0.0f;
    public float projectileSpeed, fireRate = 0.5f, speedMult = 5.0f, padding = 0.5f;
    public static int playerHP = 3;
    float maxX, minX;
    bool waiting = false, sfxStarted = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerHP = 3;
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 left = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 right = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        minX = left.x + padding;
        maxX = right.x - padding;

        //Audiosource block
        hit = GameObject.Find("HitSFX").GetComponent<AudioSource>();
        laser = GameObject.Find("LaserSFX").GetComponent<AudioSource>();
        peril = GameObject.Find("PerilSFX").GetComponent<AudioSource>();

        hp = GameObject.Find("PlayerHP").GetComponent<Text>();
    }

    void Update()
    {
        Movement();
        HPCheck();
        StartCoroutine(SetHPText());
        HPSFX();
        PlayerSprite();

        damageCooldown -= Time.deltaTime;
    }

    void HPSFX()
    {
        if (playerHP == 1 && !sfxStarted)
        {
            peril.Play();
            sfxStarted = true;
        }
        else if (playerHP != 1)
        {
            peril.Pause();
            sfxStarted = false;
        }
    }
    IEnumerator SetHPText()
    {
        hp.text = "HP: " + playerHP.ToString();
        switch (playerHP)
        {
            case 3: hp.color = Color.green; break;
            case 2: hp.color = Color.grey; break;
            case 1:
                {
                    do
                    {
                        hp.color = Color.red;
                        yield return new WaitForSeconds(0.1f);
                        hp.color = Color.white;
                        yield return new WaitForSeconds(0.1f);
                    } while (playerHP == 1);
                    break;
                }
            case 0: hp.color = Color.red; break;
            default: break;
        }
    }
    void FixedUpdate()
    {
        if (!waiting) StartCoroutine(Firev2());
        if (autoPlay) AutoPlay();
    }

    void Fire()
    {
        //Making a cooldown without a coroutine
        ///NOT USED BECAUSE COROUTINES ARE AWESOME
        if (Input.GetButtonDown("Fire1") && waitForNextShot <= 0)
        {
            GameObject Projectile = Instantiate(Laser, new Vector2(transform.position.x, transform.position.y + 1f), Quaternion.identity) as GameObject;
            Projectile.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed);
            waitForNextShot = 1;
            laser.Play();
        }
        waitForNextShot -= Time.deltaTime * fireRate;
    }
    
    void Movement()
    {
        //Basic Controls handler

        //Returns either 1 or -1 if left or right of ship
        float mousePosX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;

        float deltaPos = mousePosX - transform.position.x;
        float direction =  deltaPos / Mathf.Abs(deltaPos);

        //Checks if it's greater than some epsilon so the ship doesn't jitter left to right
        if(Mathf.Abs(deltaPos) >= 0.1)
            transform.position += new Vector3(speedMult * Time.deltaTime * direction, 0f);

        //if (Input.GetAxis("Vertical") > 0) transform.position += new Vector3(0f, speedMult * Time.deltaTime);
        //if (Input.GetAxis("Vertical") < 0) transform.position += new Vector3(0f, -speedMult * Time.deltaTime);

        //Movement Restricter
        Vector3 playerPos = transform.position;
        playerPos.x = Mathf.Clamp(transform.position.x, minX, maxX);
        //playerPos.y = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = playerPos;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject != null)
        {
            Destroy(col.gameObject);
        }

        //Makes player immune to damage for some time after getting hit
        if (damageCooldown <= 0)
        {
            anim.SetTrigger("Damaged");
            playerHP--;
            if (playerHP != 0)
                hit.Play();
            damageCooldown = 1;
        }
    }

    void HPCheck()
    {
        switch (playerHP)
        {
            case 0:
                {
                    Destroy(playerShip);
                    break;
                }
            default: break;
        }
    }

    public IEnumerator Firev2()
    {
        waiting = true;
        yield return new WaitForSeconds(fireRate);
        GameObject Projectile = Instantiate(Laser, new Vector2(transform.position.x, transform.position.y + 1f), Quaternion.identity) as GameObject;
        Projectile.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed);
        laser.Play();
        waiting = false;
    }
    void PlayerSprite()
    {
        if (EnemySpawnManager.wavesCleared < 5)
        {
            GetComponent<SpriteRenderer>().sprite = playerSprites[0];
        }
        else if (EnemySpawnManager.wavesCleared < 10)
        {
            GetComponent<SpriteRenderer>().sprite = playerSprites[1];
        }
        else if (EnemySpawnManager.wavesCleared > 15)
        {
            GetComponent<SpriteRenderer>().sprite = playerSprites[2];
        }
    }

    void AutoPlay()
    {
        if(!waiting) StartCoroutine(Firev2());
        Vector3 offset = new Vector3(0, 1);
        if(Physics2D.CircleCast(transform.position + offset, GetComponent<PolygonCollider2D>().bounds.size.magnitude / 2, Vector2.up))
        {
            Random.InitState(System.DateTime.Now.Millisecond);
            if (Random.Range(0.0f, 1) > 0.5)
                InvokeRepeating("GoRight", 0, 1);
            else
                InvokeRepeating("GoLeft", 0, 1);
            //Movement Restricter
            Vector3 playerPos = transform.position;
            playerPos.x = Mathf.Clamp(transform.position.x, minX, maxX);
            //playerPos.y = Mathf.Clamp(transform.position.y, minY, maxY);
            transform.position = playerPos;
        }
    }

    void GoRight()
    {
        transform.position += new Vector3(speedMult * Time.deltaTime, 0f);
    }
    void GoLeft()
    {
        transform.position -= new Vector3(speedMult * Time.deltaTime, 0f);
    }
}