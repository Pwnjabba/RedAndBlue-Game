using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastProjectile : MonoBehaviour
{
    public Rigidbody2D rb;

    public float speed;

    public float timer, lifeTime;

    int maxBounces, bounces;

    

    Vector2 projectileVelocity;
    void Start()
    {
        maxBounces = 3;
        timer = lifeTime;
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = projectileVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void LaunchProjectile(Vector2 dir)
    {
        projectileVelocity = dir * speed;
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
