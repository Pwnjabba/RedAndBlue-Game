using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BlueAudio : MonoBehaviour
{
    public AudioSource blueAudio;
    public GameObject iceTrail;
    public PlayerController blue;

    float trailTimer;
    public float trailLifetime;

    public AudioClip attack1, attack2;
    void Start()
    {
        blue = GetComponent<PlayerController>();
        blueAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (blue.sliding)
        {
            trailTimer = trailLifetime;
            iceTrail.SetActive(true);
        }
        else
        {
            trailTimer -= Time.deltaTime;
            if (trailTimer <= 0)
            {
                iceTrail.SetActive(false);
            }
        }
    }

    public void PlayAttackSound()
    {
        int rand = Random.Range(0, 1);

        blueAudio.clip = rand == 1 ? attack2 : attack1;
        blueAudio.Play();
    }


}
