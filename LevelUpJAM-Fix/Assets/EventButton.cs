using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;   
public class EventButton : MonoBehaviour
{
    public UnityEvent buttonEvent;
    SpriteRenderer sprite;
    AudioSource audioSource;
    bool activated;
    void Start()
    {
        activated = false;
        sprite = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        sprite.color = Color.red;
    }
    void Update()
    {
        if (activated)
        {
            sprite.color = Color.green;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Red") || collision.gameObject.layer == LayerMask.NameToLayer("Blue") || collision.gameObject.layer == LayerMask.NameToLayer("Button"))
        {
            if (!activated)
                audioSource.Play();
            buttonEvent.Invoke();
            activated = true;

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Red") || collision.gameObject.layer == LayerMask.NameToLayer("Blue") || collision.gameObject.layer == LayerMask.NameToLayer("Button"))
        {
            if (!activated)
                audioSource.Play();
            buttonEvent.Invoke();
            activated = true;

        }
    }
}
