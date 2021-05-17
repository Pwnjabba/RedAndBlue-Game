using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmortalAudio : MonoBehaviour
{
    public static ImmortalAudio instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }
    AudioSource characterAudio;
    public AudioClip deathSound;
    public AudioClip currentClip;
    void Start()
    {
        characterAudio = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDeathSound()
    {
        characterAudio.clip = deathSound;
        characterAudio.Play();
    }

    public void PlayAudioClip(AudioClip clip)
    {
        characterAudio.clip = clip;
        characterAudio.Play();
    }

}
