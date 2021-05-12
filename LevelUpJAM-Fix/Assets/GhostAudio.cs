using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAudio : MonoBehaviour
{
    public AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
