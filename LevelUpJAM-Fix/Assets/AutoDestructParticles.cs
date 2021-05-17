using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestructParticles : MonoBehaviour
{
    ParticleSystem ps;

    public float timer, lifeTime;
    void Start()
    {
        timer = lifeTime;
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Destroy(gameObject);
        }
        if (ps)
        {
            if (!ps.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}
