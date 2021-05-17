using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameventController : MonoBehaviour
{
    ParticleSystem particles;
    AudioSource audioSource;
    public AudioClip gas, flame;

    public bool flaming, disabled;

    public bool playOnStart;

    public float startDownTime, startDelayTime;
    private float downTimer, delayTimer;

    private float lifeTimeCache;

    ParticleSystem.MainModule main;

    void Start()
    {
        delayTimer = startDelayTime;
        downTimer = startDownTime;
        audioSource = GetComponent<AudioSource>();
        particles = GetComponentInChildren<ParticleSystem>();

        main = particles.main;
        lifeTimeCache = main.startLifetime.constant;

        if (playOnStart)
        {
            ToggleVentMode();
            
        }


    }

    // Update is called once per frame
    void Update()
    {     
        if (disabled)
        {
            return;
        }
        
        if (flaming)
        {
            main.startLifetime = lifeTimeCache;
        }
        else
        {
            main.startLifetime = 0;
        }

        delayTimer -= Time.deltaTime;
        downTimer -= Time.deltaTime;

        if (downTimer <= 0 && delayTimer <= 0)
        {
            ToggleVentMode();
        }
    }

    void ToggleVentMode()
    {
        flaming = !flaming;
        downTimer = startDownTime;
        if (audioSource.clip == gas)
        {
            audioSource.clip = flame;
        }
        else
        {
            audioSource.clip = gas;
        }
        if (audioSource.isActiveAndEnabled)
        audioSource.Play();

    }

    public void DisableVent()
    {
        foreach (var thing in GetComponentsInChildren<Transform>())
        {
            if (thing != transform)
            thing.gameObject.SetActive(false);
        }

        disabled = true;
        audioSource.Stop();
    }
}
