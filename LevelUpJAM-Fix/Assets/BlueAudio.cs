using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BlueAudio : MonoBehaviour
{
    public AudioSource blueAudio;
    public AudioClip[] swordSounds;
    public PlayerController blue;
    void Start()
    {
        blue = GetComponent<PlayerController>();
        blueAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySwordSound()
    {
        blueAudio.pitch = Random.Range(.75f, 1.25f);
        blueAudio.clip = swordSounds[Random.Range(0, swordSounds.Length)];
        blueAudio.Play();
    }
}
