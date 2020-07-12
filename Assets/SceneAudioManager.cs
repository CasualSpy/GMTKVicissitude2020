using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAudioManager : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    public AudioClip StartOfNight;
    public AudioClip MiddleOfNight;
    public AudioClip BadMusic;
    public AudioClip GoodMusic;

    public Track currentlyPlaying;
    
    public enum Track
    {
        Start,
        Middle,
        Bad,
        Good
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayStartNight();

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayStartNight()
    {
        currentlyPlaying = Track.Start;
        audioSource.clip = StartOfNight;
        audioSource.Play();
    }

    public void PlayMiddleNight()
    {
        currentlyPlaying = Track.Middle;
        audioSource.clip = MiddleOfNight;
        audioSource.Play();
    }

    public void PlayGoodTrack()
    {
        currentlyPlaying = Track.Good;
        audioSource.clip = GoodMusic;
        audioSource.Play();
    }

    public void PlayBadTrack()
    {
        currentlyPlaying = Track.Bad;
        audioSource.clip = BadMusic;
        audioSource.Play();
    }
}
