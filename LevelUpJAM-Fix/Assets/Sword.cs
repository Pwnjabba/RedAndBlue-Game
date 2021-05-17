using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public Transform followObject;
    public Animator anim;
    public PolygonCollider2D col;
    public LayerMask collidable;
    public AudioSource audioSource;
    public AudioClip[] swordSounds;

    public int damage;

    public Transform attackPoint;
    public float radius;

    bool hit;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        col = GetComponent<PolygonCollider2D>();
        col.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, radius, collidable);

        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit: " + enemy.name);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }

    private void FixedUpdate()
    {
        //if (!anim.GetCurrentAnimatorStateInfo(0).IsName("SwordIdle") && !hit)
        //{
        //    col.enabled = true;
        //}
        //transform.position = followObject.position;
        //if (anim.GetCurrentAnimatorStateInfo(0).IsName("SwordIdle"))
        //{
        //    hit = false;
        //    col.enabled = false;
        //}
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Attackable"))
    //    {
    //        hit = true;
    //        Debug.Log("Sword hit");
    //        if (collision.GetComponent<EnemyAI>())
    //        {
    //            EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();
    //            enemy.TakeDamage(damage);
    //            enemy.enemyAudio.PlayDamageSound(false);
    //            col.enabled = false;
    //        }
    //        col.enabled = false;
    //    }
    //}

    public void PlaySwordSound()
    {
        audioSource.pitch = Random.Range(.75f, 1.25f);
        audioSource.clip = swordSounds[Random.Range(0, swordSounds.Length)];
        audioSource.Play();
    }
}
