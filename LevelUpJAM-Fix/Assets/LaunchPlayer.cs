using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPlayer : MonoBehaviour
{
    public LayerMask playerLayer;

    AudioSource audioSource;
    AudioClip launchSound;

    public Vector2 testRight, testUp;

    public float launchForce;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        testRight = transform.right;
        testUp = transform.up;
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
