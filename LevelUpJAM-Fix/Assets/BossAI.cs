using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Phase
{
    One,
    Two,
    Three
}
public class BossAI : BaseEnemy
{
    Rigidbody2D rb;

    public bool initiated;
    public Door door;
    public GameObject gravProjectile, fastProjectile;
    public AudioClip deathSound, EndSong;

    public Transform rotationThing;

    public Transform phase3point;

    public EnemyAudio enemyAudio;
    public Transform[] pathNodes1, path2, path3;
    public Transform[] currentPath;
    public int nextNode;
    bool up;
    public bool iceEnemy;

    public float speed, projectileSpeed;

    public float fireInterval;
    float fireRate;

    float p2timer;
    public float p2time;

    bool phase2mode;
    Vector3 startPos;
    public Phase currentPhase;

    float moveTime;
    public float moveInterval;

    Vector2 direction, velocity;

    public bool diePermanently;
    public int enemyID;

    public int pathNodesLength;


    public void Start()
    {
        startPos = transform.position;
        p2time = 5f;
        p2timer = p2time;
        currentPhase = Phase.One;
        fireRate = fireInterval;
        moveTime = moveInterval;
        if (PermadeadEnemies.instance.permaDeadEnemyIDs.Contains(enemyID))
        {
            Destroy(gameObject);
        }
        moveTime = moveInterval;
        rb = GetComponent<Rigidbody2D>();
        enemyAudio = GetComponent<EnemyAudio>();
        up = true;
        direction = Vector2.up;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    transform.position = Vector2.MoveTowards(transform.position, pathNodes[nextNode].position, speed * Time.deltaTime);
    //    if (Vector2.Distance(transform.position, pathNodes[nextNode].position) < 0.2f)
    //    {
    //        if (moveTime <= 0)
    //        {
    //            if (nextNode == 1)
    //            {
    //                nextNode = 0;
    //            }
    //            else
    //            {
    //                nextNode++;
    //            }
    //            FireProjectile();
    //            moveTime = moveInterval;
    //        }
    //    }
    //    else
    //    {
    //        moveTime -= Time.deltaTime;
    //    }

    //}

    private void Update()
    {
        if (!initiated)
        {
            return;
        }
        pathNodesLength = currentPath.Length;
        if (currentPhase == Phase.One)
        {
            PhaseOne();
        }
        else if (currentPhase == Phase.Two)
        {
            PhaseTwo();
        }
        else if (currentPhase == Phase.Three)
        {
            PhaseThree();
        }
        ChangePhase();

    }

    void PhaseOne()
    {
        currentPath = pathNodes1;
        fireRate -= Time.deltaTime;
        fireInterval = 1f;
        if (fireRate <= 0)
        {
            fireRate = fireInterval;
            GravityProjectile();
        }
    }

    void PhaseTwo()
    {
        currentPath = path2;
        fireRate -= Time.deltaTime;
        p2timer -= Time.deltaTime;

        if (p2timer <= 0)
        {
            phase2mode = !phase2mode;
            p2timer = p2time;
        }
        if (fireRate <= 0 && phase2mode)
        {
            fireInterval = 1f;
            fireRate = fireInterval;
            GravityProjectile();
        }
        else if (fireRate <= 0 && !phase2mode)
        {
            fireInterval = .25f;
            fireRate = fireInterval;
            FastProjectile();
        }
    }

    void PhaseThree()
    {
        currentPath = path3;
        fireRate -= Time.deltaTime;
        if (fireRate <= 0)
        {
            fireInterval = .18f;
            fireRate = fireInterval;
            FastProjectile2();
        }
    }

    void ChangePhase()
    {
        if (health <= 20)
        {
            currentPhase = Phase.Three;
            return;
        }
        else if (health > maxHealth * .66f)
        {
            currentPhase = Phase.One;
        }
        else if (health <= maxHealth * .66f)
        {
            currentPhase = Phase.Two;
        }

    }
    private void FixedUpdate()
    {
        if (!initiated)
        {
            return;
        }
        if (currentPhase == Phase.Three)
        {
            return;
        }
        transform.position = Vector2.MoveTowards(transform.position, currentPath[nextNode].position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, currentPath[nextNode].position) < 0.2f)
        {
            if (moveTime <= 0)
            {
                if (nextNode < currentPath.Length - 1)
                {
                    nextNode++;
                }
                else
                {
                    nextNode = 0;
                }
                moveTime = moveInterval;
            }
        }
        else
        {
            moveTime -= Time.deltaTime;
        }
        //if (moveTime <= 0)
        //{
        //    moveTime = moveInterval;
        //    SwitchDirection();
        //    FireProjectile();
        //}

        //velocity = new Vector2(direction.x, direction.y) * speed;

        //rb.velocity = velocity;
    }

    void FastProjectile()
    {
        GameObject newProectile = Instantiate(fastProjectile);
        newProectile.transform.position = transform.position;
        newProectile.GetComponent<FastProjectile>().LaunchProjectile(rotationThing.transform.right);


    }

    void FastProjectile2()
    {
        GameObject newProectile = Instantiate(fastProjectile);
        newProectile.transform.position = transform.position;
        newProectile.GetComponent<FastProjectile>().LaunchProjectile(rotationThing.transform.right);
        newProectile.transform.localScale = newProectile.transform.localScale * .5f;
        newProectile.GetComponent<FastProjectile>().speed = 3f;

    }
    void GravityProjectile()
    {
        GameObject newProjectile = Instantiate(gravProjectile, transform);
        newProjectile.transform.SetParent(null);
        //newProjectile.transform.position = transform.position;
        
    }


    public override void Die()
    {
        if (!initiated)
        {
            return;
        }
        foreach (var thing in FindObjectsOfType<FastProjectile>())
        {
            Destroy(thing.gameObject);
        }
        ImmortalAudio.instance.PlayAudioClip(deathSound);
        MusicManager.instance.musicPlayer.Stop();
        MusicManager.instance.PlaySong(MusicManager.instance.song4);
        door.Disable();
        if (diePermanently)
            PermadeadEnemies.instance.permaDeadEnemyIDs.Add(enemyID);
        base.Die();



    }

}
