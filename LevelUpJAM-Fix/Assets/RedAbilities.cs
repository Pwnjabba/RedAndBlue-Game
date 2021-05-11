using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedAbilities : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform fireballSpawnPointL, fireballSpawnPointR;

    public float speed, lifespan;

    public float fireRate, startFireRate;
    bool canFire;
    void Start()
    {
        startFireRate = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (!CharacterManager.instance.red.isActive)
        {
            return;
        }

        fireRate -= Time.deltaTime;

        if (fireRate <= 0)
        {
            canFire = true;
        }
        if (Input.GetKeyDown(KeyCode.F) && canFire)
        {
            fireRate = startFireRate;
            canFire = false;
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();

            GameObject newFireball = Instantiate(fireballPrefab, sprite.flipX ? fireballSpawnPointL : fireballSpawnPointR);
            if (!sprite.flipX)
            {
                newFireball.transform.localScale = new Vector2(newFireball.transform.localScale.x * -1, newFireball.transform.localScale.y);
            }
            newFireball.transform.SetParent(null);
            Fireball fireballScript = newFireball.GetComponent<Fireball>();
            fireballScript.lifespan = lifespan;
            fireballScript.velocity = sprite.flipX ? Vector2.left * speed : Vector2.right * speed;

        }
    }
}
