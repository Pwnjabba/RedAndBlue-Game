using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : BaseEnemy
{
    Rigidbody2D rb;

    public GameObject fireProjectile, iceProjectile;
    public AudioClip deathSound;

    public EnemyAudio enemyAudio;
    public Transform[] pathNodes;
    public int nextNode;
    bool up;
    public bool iceEnemy;

    public float speed, projectileSpeed;

    public float fireInterval;
    float fireRate;


    float moveTime;
    public float moveInterval;

    Vector2 direction, velocity;

    public bool diePermanently;
    public int enemyID;


    public void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.Space) && iceEnemy)
        {
            IceProjectile();
        }

        if (iceEnemy)
        {
            if (fireRate <= 0)
            {
                fireRate = fireInterval;
                IceProjectile();
            }
            else
            {
                fireRate -= Time.deltaTime;
            }
        }

    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, pathNodes[nextNode].position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, pathNodes[nextNode].position) < 0.2f)
        {
            if (moveTime <= 0)
            {
                if (nextNode == 1)
                {
                    nextNode = 0;
                }
                else
                {
                    nextNode++;
                }
                if (iceEnemy)
                {
                    //IceProjectile();
                }
                else
                {
                    FireProjectile();
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

    void SwitchDirection()
    {
        if (direction == Vector2.up)
        {
            direction = Vector2.down;
        }
        else
        {
            direction = Vector2.up;
        }
    }

    public override void Die()
    {
        if (diePermanently)
        PermadeadEnemies.instance.permaDeadEnemyIDs.Add(enemyID);
        base.Die();
        ImmortalAudio.instance.PlayAudioClip(deathSound);


    }

    void IceProjectile()
    {
        GameObject newProjectile = Instantiate(iceProjectile, transform);
        newProjectile.transform.SetParent(null);
        Icicle iceProjectileScript = newProjectile.GetComponent<Icicle>();
    }
    void FireProjectile()
    {
        GameObject newProjectile = Instantiate(fireProjectile);
        HarmfulProjectile projectileScript = newProjectile.GetComponent<HarmfulProjectile>();
        projectileScript.speed = projectileSpeed;     
        newProjectile.transform.position = transform.position;
        projectileScript.velocity = transform.rotation.y != 0 ? Vector2.left : Vector2.right;       
    }

}
