using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public GameObject skelington;
    public AudioSource music;
    public AudioSource sounds;
    public AudioClip dayMusic;
    public AudioClip nightMusic;
    public AudioClip collision;
    public AudioClip walking;
    private Player player;

    private bool playingDayMusic;
    private bool colliding;
    private int collisionTimer;
    private bool isWalking;
    private int walkingTimer;

    public float SKELETON_MUSIC_BUBBLE_RADIUS = 4.5f;

    // Use this for initialization
    void Start () {
        music = GetComponent<AudioSource>();
        player = GetComponentInParent<Player>();
        sounds = GameObject.Find("OtherSounds").GetComponent<AudioSource>();
        playingDayMusic = true;
        colliding = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (colliding)
            collisionTimer++;
        if (collisionTimer >= 20)
        {
            colliding = false;
            collisionTimer = 0;
        }
        if (isWalking)
            walkingTimer++;
        if (walkingTimer >= 20)
        {
            isWalking = false;
            walkingTimer = 0;
        }
        if (Input.GetButtonDown("ToggleMusic")) {
            if (music.isPlaying) {
                music.Stop();
            }
            else {
                music.Play();
            }
        }
        if (playingDayMusic != !player.isNight) {
            if (player.isNight) {
                music.clip = nightMusic;
                music.Play();
            }
            else {
                music.clip = dayMusic;
                music.Play();
            }
            playingDayMusic = !player.isNight;
        }
        if (skelington != null) {
            float dist = Vector3.Distance(gameObject.transform.position, skelington.transform.position);
            dist = dist - SKELETON_MUSIC_BUBBLE_RADIUS < 0 ? 0 : dist - SKELETON_MUSIC_BUBBLE_RADIUS;
            float vol = 0.75f - dist * 0.1f;
            if (vol < 0) {
                vol = 0f;
            }
            vol += 0.25f;
            if (player.fogEnabled == true) {
                vol *= 0.5f;
            }
            music.volume = vol;
        }
	}

    public void PlayCollision()
    {
        if (!colliding)
        {
            colliding = true;
            sounds.clip = collision;
            sounds.Play();
        }
    }

    public void PlayWalking()
    {
        if (!isWalking && !colliding)
        {
            isWalking = true;
            Debug.Log("Collision Sound");
            sounds.clip = walking;
            sounds.Play();
        }
    }
}
