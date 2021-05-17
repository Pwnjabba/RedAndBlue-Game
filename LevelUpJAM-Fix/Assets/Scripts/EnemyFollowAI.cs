using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowAI : BaseEnemy
{
    public AudioClip deathSound;
    float distanceFromBlue, distanceFromRed;
    Vector2 velocity, direction;
    public float triggerDistance;

    bool patrolling;
    float patrolTimer;
    public float patrolTime, patrolSpeed;

    Rigidbody2D rb;
    public float followSpeed;

    public GameObject red, blue, followTarget;
    void Start()
    {
        patrolTimer = patrolTime;
        rb = GetComponent<Rigidbody2D>();
        red = GameObject.FindGameObjectWithTag("Red");
        blue = GameObject.FindGameObjectWithTag("Blue");
        patrolling = true;
        direction = Vector2.right;
    }

    // Update is called once per frame
    void Update()
    {
        patrolTimer -= Time.deltaTime;
        if (patrolTimer <= 0)
        {
            patrolTimer = patrolTime;
            SwitchDirection();
        }
        if (distanceFromRed < distanceFromBlue)
        {
            followTarget = red;
        }
        else
        {
            followTarget = blue;
        }
    }

    private void FixedUpdate()
    {
        float step = followSpeed * Time.deltaTime;
        if (patrolling)
        {
            //patrol
            velocity = Vector2.right * patrolSpeed;
            rb.velocity = new Vector2(direction.x, direction.y) * patrolSpeed;
        }
        //else
        //{
        //    //chase target
        //    velocity = Vector2.MoveTowards(transform.position, followTarget.transform.position, step);
        //    rb.velocity = velocity * followSpeed;

        //}


        distanceFromBlue = Vector2.Distance(transform.position, blue.transform.position);
        distanceFromRed = Vector2.Distance(transform.position, red.transform.position);
    }

    public override void Die()
    {
        base.Die();

        ImmortalAudio.instance.PlayAudioClip(deathSound);
    }

    void SwitchDirection()
    {
        if (direction == Vector2.right)
        {
            direction = Vector2.left;
        }
        else
        {
            direction = Vector2.right;
        }
    }
}

