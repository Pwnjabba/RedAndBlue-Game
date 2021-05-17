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
    public GameObject gravProjectile;
    public AudioClip deathSound;

    public EnemyAudio enemyAudio;
    public Transform[] pathNodes;
    public int nextNode;
    bool up;
    public bool iceEnemy;

    public float speed, projectileSpeed;

    public float fireInterval;
    float fireRate;

    public Phase currentPhase;

    float moveTime;
    public float moveInterval;

    Vector2 direction, velocity;

    public bool diePermanently;
    public int enemyID;

    public int pathNodesLength;

    public void Start()
    {
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
        pathNodesLength = pathNodes.Length;
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
        fireRate -= Time.deltaTime;

        if (fireRate <= 0)
        {
            fireRate = fireInterval;
            GravityProjectile();
        }
    }

    void PhaseTwo()
    {
        fireInterval = 1f;
        if (fireRate <= 0)
        {
            GravityProjectile();
        }
    }

    void PhaseThree()
    {

    }

    void ChangePhase()
    {
        if (health > maxHealth * .66f)
        {
            currentPhase = Phase.One;
        }
        else if (health <= maxHealth * .66f)
        {
            currentPhase = Phase.Two;
        }
        else if (health <= maxHealth * .33f)
        {
            currentPhase = Phase.Three;
        }
    }
    private void FixedUpdate()
    {
        if (!initiated)
        {
            return;
        }
        transform.position = Vector2.MoveTowards(transform.position, pathNodes[nextNode].position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, pathNodes[nextNode].position) < 0.2f)
        {
            if (moveTime <= 0)
            {
                if (nextNode < pathNodes.Length - 1)
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

    void GravityProjectile()
    {
        GameObject newProjectile = Instantiate(gravProjectile, transform);
        newProjectile.transform.SetParent(null);
        //newProjectile.transform.position = transform.position;
        
    }


    public override void Die()
    {
        if (diePermanently)
            PermadeadEnemies.instance.permaDeadEnemyIDs.Add(enemyID);
        base.Die();
        ImmortalAudio.instance.PlayAudioClip(deathSound);


    }

}
