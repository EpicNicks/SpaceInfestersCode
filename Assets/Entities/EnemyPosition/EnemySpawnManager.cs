using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemySpawnManager : MonoBehaviour {

    Vector3 centerPos = new Vector3(0, 1.98f, 0);

    public GameObject enemyPrefab;
    public AudioSource waveClear;
    public Text WavesCleared;
    public float width, height, speed, spawnDelay = 0.5f, procedural;
    private float level = 3.0f, xMax, xMin;
    float beastTime = 15;
    private bool movingRight = true, hasFinished = false;
    public static int wavesCleared = 0;
    public static int totalPoints;
    public static int childAmount;

    void Start() {

        //Make the playspace restrictions
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
        xMax = rightEdge.x;
        xMin = leftEdge.x;

        StartCoroutine(RandomSpawn());
        waveClear = GameObject.Find("WaveClearSFX").GetComponent<AudioSource>();
        WavesCleared = GameObject.Find("WavesCleared").GetComponent<Text>();
        
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }
    void Update()
    {
        FormationHandler();
        if (AllMembersDead())
        {
            StartCoroutine(Respawn());
        }
        WavesCleared.text = "Waves Cleared: " + wavesCleared.ToString();
    }

    ///Use if you want every spot to be spawned into
    void SpawnUntilFull()
    {
        Transform freePosition = NextFreePosition();
        procedural = Random.value * 10f;

            if (freePosition)
            {
                GameObject enemy = Instantiate(enemyPrefab, freePosition.transform.position, Quaternion.identity) as GameObject;
                enemy.transform.parent = freePosition;
            }
            if (NextFreePosition())
            {
                Invoke("SpawnUntilFull", spawnDelay);
            }
    }
    Transform NextFreePosition()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount == 0)
            {
                return childPositionGameObject;
            }
        }
        return null;
    }

    bool AllMembersDead()
    {
        foreach(Transform childPositionGameObject in transform)
        {
            if(childPositionGameObject.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }

    ///Use if you want to randomly spawn in enemies in randomly selected spawn locations
    ///CURRENTLY IN USE
    IEnumerator RandomSpawn()
    {
        childAmount = 0;

        if (procedural <= level)
        {
            foreach (Transform child in transform)
            {
                float procedural = Random.value * 10;
                if (procedural <= level)
                {
                    GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
                    enemy.transform.parent = child;
                    childAmount++;
                }
                yield return new WaitForSeconds(spawnDelay);
            }
        }
        hasFinished = true;
        print(hasFinished);
    }
    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2);
        print("Respawned");
        //Checking again ensures the respawn doesn't overlap
        if (AllMembersDead() && hasFinished)
        {
            BonusObserver.WaveBonus(totalPoints);

            waveClear.Play();
            Debug.Log("All enemies destroyed. You should probably code some other formations or something");
            level += 1f;
            PlayerMove.playerHP = 3;
            StartCoroutine(RandomSpawn());
            ///Find out why this is happening twice
            wavesCleared++;
            hasFinished = false;
            yield return new WaitForSeconds(3);
            hasFinished = true;
        }
    }

    void FormationHandler()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else if (!movingRight)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else
        {
            Debug.Log("You fucked up");
        }

        float rightEdgeOfFormation = transform.position.x + 0.5f * width;
        float leftEdgeOfFormation = transform.position.x - 0.5f * width;

        if (rightEdgeOfFormation > xMax)
        {
            movingRight = false;
        }
        else if (leftEdgeOfFormation < xMin)
        {
            movingRight = true;
        }
    }

    ///Very expensive method
    ///Do NOT use in functions that run every frame
    int GetChildAmount()
    {
        int i = 0;

        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.GetComponentInChildren<HitHandler>())
            {
                i++;
            }
        }
        return i;
    }

    float time;
    bool flag = true;
    void SpecialFormations()
    {
        if (childAmount == 1)
        {
            if (flag)
            {
                time = Time.time;
            }
            if (Time.time - time >= beastTime)
            {
                HitHandler lastShip = GetComponentInChildren<HitHandler>();
                lastShip.anim.Play("Beast Mode");
                time = 0;
                flag = false;
            }
        }
    }

/*
    void Spawn()
    {
        foreach (Transform child in transform)
        {
            float procedural = Random.value * 10;
            if (procedural <= level)
            {
                GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
                enemy.transform.parent = child;
            }
        }
    }
*/
}