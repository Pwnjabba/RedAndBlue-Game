using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D col;
    SpriteRenderer sprite;
    AudioSource audioSource;

    public AudioClip[] impactSounds;

    public Vector2 velocity;
    public float speed, lifetime, lifespan;

    public int damage;

    public LayerMask collidable;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Attackable"))
        {
            if (collision.GetComponent<BigGhostThing>())
            {
                BigGhostThing enemy = collision.gameObject.GetComponent<BigGhostThing>();
                enemy.TakeDamage(damage);  
            }
            Destroy(gameObject);
        }
    }

    void PlayImpactSound()
    {
        audioSource.clip = impactSounds[Random.Range(0, impactSounds.Length)];
        audioSource.Play();
    }
}
