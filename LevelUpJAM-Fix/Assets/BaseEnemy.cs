using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public int health, maxHealth;
    public Sprite defaultSprite, highlightSprite;
    public SpriteRenderer enemySprite;

    public void Awake()
    {
        if (GetComponent<SpriteRenderer>())
            enemySprite = GetComponent<SpriteRenderer>();
        else
            enemySprite = GetComponentInChildren<SpriteRenderer>();

        defaultSprite = enemySprite.sprite;

        health = maxHealth;
    }

    

    public virtual void TakeDamage(int damage)
    {
        health -= damage;

        enemySprite.sprite = highlightSprite;
        CancelSpriteReset();

        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public void CancelSpriteReset()
    {
        CancelInvoke("ResetSprite");
        Invoke("ResetSprite", .1f);
    }

    public virtual void ResetSprite()
    {
        enemySprite.sprite = defaultSprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.GetComponent<PlayerController>())
        {
            if (collision.gameObject.GetComponent<PlayerController>().sliding)
            {
                return;
            }
            collision.gameObject.GetComponent<PlayerController>().Die();
        }

    }
}
