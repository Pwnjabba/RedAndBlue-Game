using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedAudio : MonoBehaviour
{
    public AudioSource projectileAudioSource, dashAudioSource;

    public AudioClip shootSound, dashSound;

    void Start()
    {
        projectileAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayShootSound()
    {
        projectileAudioSource.clip = shootSound;
        projectileAudioSource.Play();
    }

    public void PlayDashSound()
    {
        dashAudioSource.clip = dashSound;
        dashAudioSource.Play();
    }

}
