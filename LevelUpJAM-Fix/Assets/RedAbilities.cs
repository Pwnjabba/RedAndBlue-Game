using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedAbilities : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform fireballSpawnPointL, fireballSpawnPointR;
    PlayerController red;

    public GameObject particleTrail;
    public float trailTimer, trailTime;
    public float projectileSpeed, projectileLifespan;

    public float fireRate, startFireRate;
    public bool canFire;
    void Start()
    {
        fireRate = startFireRate;
        red = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!CharacterManager.instance.red.isActive)
        {
            return;
        }

        if (red.dashing)
        {
            trailTimer = trailTime;
            particleTrail.SetActive(true);
        }
        if (particleTrail.activeInHierarchy)
        {
            trailTimer -= Time.deltaTime;

            if (trailTimer <= 0)
            {
                particleTrail.SetActive(false);
            }
        }


        fireRate -= Time.deltaTime;

        if (fireRate <= 0)
        {
            canFire = true;
        }
        if (Input.GetKeyDown(KeyCode.X) && canFire)
        {
            GetComponent<RedAudio>().PlayShootSound();
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
            fireballScript.lifespan = projectileLifespan;
            fireballScript.velocity = transform.localScale.x < 0 ? Vector2.left * projectileSpeed : Vector2.right * projectileSpeed;

        }
    }
}
