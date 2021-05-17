using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityProjectile : MonoBehaviour
{
    public Rigidbody2D rb;

    public float speed;

    public float timer, lifeTime;

    int maxBounces, bounces;

    Vector2 projectileVelocity;
    void Start()
    {
        maxBounces = 5;
        timer = lifeTime;
        rb = GetComponent<Rigidbody2D>();

        LaunchProjectile();
        rb.velocity = projectileVelocity * speed;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        //if (timer <= 0)
        //{
        //    Destroy(gameObject);
        //}
    }

    void LaunchProjectile()
    {
        int rand = Random.Range(0, 1);
        if (rand == 1)
        {
            projectileVelocity = Vector2.up + Vector2.left;
        }
        else
        {
            projectileVelocity = Vector2.up + Vector2.right;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            collision.gameObject.GetComponent<PlayerController>().Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("World"))
        {
            bounces++;
            if (bounces >= maxBounces)
            {
                Destroy(gameObject);
            }
        }
    }


}
