using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D col;
    SpriteRenderer sprite;
    AudioSource audioSource;

    float distanceFromSpawn;
    public float maxDistanceFromSpawn;

    Vector2 spawnPos;

    public AudioClip[] impactSounds;

    public Vector2 velocity;
    public float speed, lifetime, lifespan;

    public int damage;

    public LayerMask collidable;

    public GameObject explosionEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        spawnPos = transform.position;

        lifetime = lifespan;

    }

    // Update is called once per frame
    void Update()
    {
        distanceFromSpawn = Vector2.Distance(spawnPos, transform.position);
        lifetime -= Time.deltaTime;

        if (lifetime <= 0 || distanceFromSpawn >= maxDistanceFromSpawn)
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
            velocity = Vector2.right * speed;
        else
            velocity = Vector2.left * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Attackable") || collision.gameObject.layer == LayerMask.NameToLayer("World"))
        {
            if (collision.GetComponent<BaseEnemy>())
            {
                BaseEnemy enemy = collision.gameObject.GetComponent<BaseEnemy>();
                enemy.TakeDamage(damage);
                enemy.transform.GetComponent<EnemyAudio>().PlayDamageSound(true);
            }
            GameObject explosion = Instantiate(explosionEffect);
            explosion.transform.position = transform.position;
            Destroy(gameObject);
        }
    }

    void PlayImpactSound()
    {
        audioSource.clip = impactSounds[Random.Range(0, impactSounds.Length)];
        audioSource.Play();
    }
}
