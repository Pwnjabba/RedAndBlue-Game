using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedAudio : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip shootSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayShootSound()
    {
        audioSource.clip = shootSound;
        audioSource.Play();
    }

}
