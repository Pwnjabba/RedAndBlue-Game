using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPlayer : MonoBehaviour
{
    public LayerMask playerLayer;

    AudioSource audioSource;
    AudioClip launchSound;

    public float launchForce;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            audioSource.Play();
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.Launch(launchForce);
        }
    }
}
