using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmfulProjectile : MonoBehaviour
{
    Rigidbody2D rb;

    public float speed;

    public float timer, lifeTime;

    public Vector2 velocity;
    void Start()
    {
        timer = lifeTime;
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = velocity * speed;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            collision.gameObject.GetComponent<PlayerController>().Die();
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("World"))
        {
            Destroy(gameObject);
        }

    }


}
