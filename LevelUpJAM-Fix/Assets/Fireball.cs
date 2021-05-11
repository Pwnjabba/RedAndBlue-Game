using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D col;
    SpriteRenderer sprite;

    public Vector2 velocity;
    public float speed, lifetime, lifespan;

    public LayerMask collidable;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();

        lifetime = lifespan;

    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }

        
    }
    private void FixedUpdate()
    {      
        rb.velocity = velocity;
    }

    public void InitializeProjectile(bool right)
    {
        if (right)
        {
            velocity = Vector2.right * speed;
        }
        else
        {
            velocity = Vector2.left * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == collidable)
        {
            print("yeet"); 
        }
    }
}
