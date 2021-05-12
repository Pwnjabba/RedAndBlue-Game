using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigGhostThing : MonoBehaviour
{
    [SerializeField] public int health, maxHealth;
    public ProgressBar healthbar;

    SpriteRenderer enemySprite;

    public Sprite defaultSprite, highlightSprite;


    public void Start()
    {
        enemySprite = GetComponent<SpriteRenderer>();
        defaultSprite = enemySprite.sprite;
        health = maxHealth;
        healthbar.maximum = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.current = health;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public virtual void TakeDamage(int damage)
    {
        enemySprite.sprite = highlightSprite;
        health -= damage;

        Invoke("ResetSprite", .1f);

    }

    public virtual void ResetSprite()
    {
        enemySprite.sprite = defaultSprite;
    }

}
