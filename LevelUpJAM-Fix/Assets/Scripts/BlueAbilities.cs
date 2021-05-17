using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueAbilities : MonoBehaviour
{
    public Animator anim;
    public PlayerController blue;
    BlueAudio blueAudio;

    public Transform attackPosL, attackPosR;
    Transform currentAttackPos;
    public LayerMask enemyLayer;
    public float attackRadius;
    public int damage;

    float timeBtwAttack;
    public float startTimeBtwAttack;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        blueAudio = GetComponent<BlueAudio>();
        blue = GetComponent<PlayerController>();
    }

    void Update()
    {
        timeBtwAttack -= Time.deltaTime;

        if (Input.GetKey(KeyCode.X) && blue.isActive)
        {
            Attack();
        }
    }

    private void FixedUpdate()
    {
        SetAttackPointPosition();
    }

    void Attack()
    {
        if (timeBtwAttack <= 0)
        {
            if (blue.GetComponent<SpriteRenderer>().flipX)
            {
                anim.SetTrigger("PunchL");
            }
            else
                anim.SetTrigger("PunchR");
            timeBtwAttack = startTimeBtwAttack;
            blueAudio.PlayAttackSound();
            Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(currentAttackPos.position, attackRadius, enemyLayer);
            for (int i = 0; i < enemiesHit.Length; i++)
            {
                enemiesHit[i].transform.GetComponent<EnemyAudio>().PlayDamageSound(false);
                enemiesHit[i].GetComponent<BaseEnemy>().TakeDamage(damage);
            }
        }
    }

    void SetAttackPointPosition()
    {
        if (blue.sprite.flipX)
        {
            currentAttackPos = attackPosL;
        }
        else
        {
            currentAttackPos = attackPosR;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPosL.position, attackRadius);
        Gizmos.DrawWireSphere(attackPosR.position, attackRadius);
    }
}
