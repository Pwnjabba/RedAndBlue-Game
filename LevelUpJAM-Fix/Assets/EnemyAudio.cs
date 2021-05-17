using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip redDamage, blueDamage;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDamageSound(bool isRed)
    {
        if (isRed)
        {
            audioSource.clip = redDamage;
        }
        else if (!isRed)
        {
            audioSource.clip = blueDamage;
        }

        audioSource.Play();
    }
}
