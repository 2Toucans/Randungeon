using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public GameObject skelington;
    public AudioSource music;
    public AudioClip dayMusic;
    public AudioClip nightMusic;
    private Player player;

    private bool playingDayMusic;

    public const float SKELETON_MUSIC_BUBBLE_RADIUS = 4.5f;

    // Use this for initialization
    void Start () {
        music = GetComponent<AudioSource>();
        player = GetComponentInParent<Player>();
        playingDayMusic = true;
	}
	
	// Update is called once per frame
	void Update () {
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
            float vol = 0.75f - dist * 0.07f;
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
}
