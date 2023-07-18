using UnityEngine;

public class HitHandler : MonoBehaviour {

    int hp;
    public Animator anim;
    public GameObject ship, EnemyLaser;
    public Sprite[] Damage;
    public float enemyLaserTimer, enemyFireRate, enemyProjectileSpeed;
    public int pointValue = 100;
    private ScoreKeeper scoreKeeper;
    public AudioClip destroyed;
    private Vector3 curTransform;
    [System.NonSerialized]
    public float laserMod = 1;


    void OnTriggerEnter2D (Collider2D col)
    {
        if(gameObject != null)
            Destroy(col.gameObject);
        hp--;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        hp = 3;
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
        
    }
    void Update()
    {
        SetSprite();
        AnimationHandler();
    }
    void FixedUpdate()
    {
        Fire();
    }
    void OnDestroy()
    {
        Vector3 curPos = transform.position;
        AudioSource.PlayClipAtPoint(destroyed, curPos);
    }
    void LoadSprites()
    {
        int SpriteIndex = hp - 1;
        GetComponent<SpriteRenderer>().sprite = Damage[SpriteIndex];
    }
    void SetSprite()
    {
        switch (hp)
        {
            case 2: LoadSprites(); break;
            case 1: LoadSprites(); break;
            case 0: scoreKeeper.Score(pointValue);
                EnemySpawnManager.totalPoints += pointValue;
                EnemySpawnManager.childAmount--;
                Destroy(ship);
                break;
            default: break;
        }
    }

    void Fire()
    {
        if (enemyLaserTimer <= 0)
        {
            if(EnemyLaser != null)
            {
                EnemyLaser = Instantiate(EnemyLaser, transform.position, Quaternion.identity) as GameObject;
                EnemyLaser.transform.localScale = EnemyLaser.transform.localScale * laserMod;
                EnemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyProjectileSpeed);
                enemyLaserTimer = 1;
            }
        }
        enemyLaserTimer -= Time.deltaTime * enemyFireRate * Random.value;
    }

    void AnimationHandler()
    {
        int rand = Random.Range(0, 18000);
        //Circling between stages 3 and 5
        if(rand > 17900 && EnemySpawnManager.wavesCleared > 1 && EnemySpawnManager.wavesCleared < 5)
        {
            anim.SetTrigger("Circling");
        }
        //Swoop has been cut for now
        if (rand > 17990 && EnemySpawnManager.wavesCleared > 4 && EnemySpawnManager.wavesCleared < 10)
            anim.SetTrigger("Charge");
        
    }

    //For animation purposes (NOT WORKING)
    void ZeroTransform()
    {
        curTransform = transform.position;
        transform.position = Vector3.zero;
    }

    void ResetTransform()
    {
        transform.position = curTransform;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}