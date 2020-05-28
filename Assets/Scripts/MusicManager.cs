using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    AudioSource aud;
    public AudioClip invinciblemusic;
    public AudioClip levelmusic;
    public AudioClip death;
    public AudioClip LowTime;
    bool isinvincible;
    public Character player;
 
    float endtime;
	// Use this for initialization
	void Start () {
        aud = GetComponent<AudioSource>();
        isinvincible = false;
        player = FindObjectOfType<Character>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isinvincible)
        {
            if (Time.time > endtime)
            {
                isinvincible = false;
                PlayLevelMusic();
                player.isstar = false;
                player.tag = "Player";
            }
        }
	}
    public void PlayLowTime()
    {
        aud.clip = LowTime;
        aud.Play();
    }
    public void PlayInvincible()
    {
        Debug.Log("Playinginvincible");
        isinvincible = true;
        aud.clip = invinciblemusic;
        aud.Play();
        
        endtime = Time.time + 12;

    }
    public void PlayLevelMusic()
    {
        aud.clip = levelmusic;
        aud.Play();

    }
    public void PlayDeathSound()
    {
        aud.clip = death;
        aud.Play();
        aud.loop = false;
        
    }
}
