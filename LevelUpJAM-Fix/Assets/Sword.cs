using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public Transform followObject;
    public Animator anim;
    public PolygonCollider2D col;
    public LayerMask collidable;

    public int damage;
    void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("SwordIdle"))
        {
            col.enabled = true;
        }
        transform.position = followObject.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Attackable"))
        {
            Debug.Log("Sword hit");
            if (collision.GetComponent<BigGhostThing>())
            {
                BigGhostThing enemy = collision.gameObject.GetComponent<BigGhostThing>();
                enemy.TakeDamage(damage);
            }
            //apply damage
            col.enabled = false;
        }
    }
}
