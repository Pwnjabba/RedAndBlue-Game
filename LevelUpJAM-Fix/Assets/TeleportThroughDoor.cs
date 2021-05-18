using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportThroughDoor : MonoBehaviour
{
    bool activated;
    public BossAI boss;
    public AudioSource bossMusic, music;
    public AudioClip bossSong;
    public MusicManager musicManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Red")
        {
            if (!activated)
            {
                MusicManager.instance.PlaySong(bossSong);
                MusicManager.instance.musicPlayer.volume = 1f;
                boss.initiated = true;            
                activated = true;
                CharacterManager.instance.red.transform.position = new Vector2(CharacterManager.instance.red.transform.position.x - 3f, CharacterManager.instance.red.transform.position.y);
            }

        }
    }
}
