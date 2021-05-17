using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMusic : MonoBehaviour
{
    public AudioClip song2;
    public MusicManager music;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Red" || collision.tag == "Blue")
        {
            MusicManager.instance.PlaySong(song2);
            MusicManager.instance.musicPlayer.volume = .8f;
        }
    }
}
