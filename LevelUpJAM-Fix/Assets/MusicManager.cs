using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public AudioClip song1, song2, song3;
    public int currentSong;
    public AudioSource musicPlayer;

    void Awake()
    {
        //if we don't have an [_instance] set yet
        if (!instance)
            instance = this;
        //otherwise, if we do, kill this thing
        else
            Destroy(this.gameObject);


        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        musicPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<BossAI>().initiated)
        {
            if (musicPlayer.clip != song3)
            {
                musicPlayer.clip = song3;
                musicPlayer.volume = 1f;
                musicPlayer.Play();
            }
        }
        if (CheckpointManager.instance.currentCheckPoint == CheckpointManager.instance.checkPoints[0] || CheckpointManager.instance.currentCheckPoint == CheckpointManager.instance.checkPoints[1])
        {
            if (musicPlayer.clip != song1)
            {
                musicPlayer.clip = song1;
                musicPlayer.volume = .8f;
                musicPlayer.Play();
            }

        }
        if (CheckpointManager.instance.currentCheckPoint == CheckpointManager.instance.checkPoints[2] || CheckpointManager.instance.currentCheckPoint == CheckpointManager.instance.checkPoints[3] || CheckpointManager.instance.currentCheckPoint == CheckpointManager.instance.checkPoints[4])
        {
            if (musicPlayer.clip != song2 && !FindObjectOfType<BossAI>().initiated)
            {
                musicPlayer.clip = song2;
                musicPlayer.volume = .8f;
                musicPlayer.Play();
            }
        }
        
    }

    public void PlaySong(AudioClip song)
    {
        musicPlayer.clip = song;
        musicPlayer.Play();
    }
}
