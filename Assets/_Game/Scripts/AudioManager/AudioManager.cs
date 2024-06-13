using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/*
 * Script that contains public methods to play sounds and background music in game.
 * Separates BGM and SFX so that we can control each source's volume individually
    (if players want to turn up or down SFX volume but not BGM volume and vice-versa).
 * Attached as component to AudioManager GameObject (singleton do not destroy object)
 */

public class AudioManager : MonoBehaviour
{
    //Singleton declaration.
    //IMPORTANT: Replace with alternate Singleton architecture eventually (by attaching SingletonDontDestroy script to AudioManager GameObject)
    public static AudioManager Instance { get; private set;}

    //BGM source loops and is the top AudioSource component in the AudioManager GameObject, SFX source does not loop
    [SerializeField] private AudioSource bgmSource, sfxSource, tenseSource, neutralSource, happySource;

    private AudioSource[] levelAudioSources = new AudioSource[3];
    [SerializeField] private int currentSourceID = 0;

    private void Awake() {

        //Code to generate singleton.
        //IMPORTANT: Replace with alternate Singleton architecture eventually (by attaching SingletonDontDestroy script to AudioManager GameObject)
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else { 
            Instance = this;
        }

        if (bgmSource == null) {
            Debug.LogError("bgmSource not found!");
        }

        if (sfxSource == null) {
            Debug.LogError("sfxSource not found!");
        }
    }

    private void Start() {
        levelAudioSources[0] = tenseSource;
        levelAudioSources[1] = neutralSource;
        levelAudioSources[2] = happySource;
    }

    //Plays the given AudioClip as looping background music. Will replace any BGM that is already playing.
    public void PlayBGM(AudioClip clip) {
        bgmSource.Stop();
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    public void StopAll() {
        bgmSource.Stop();
        sfxSource.Stop();
        tenseSource.Stop();
        neutralSource.Stop();
        happySource.Stop();
    }

    public void PlayLevelMusic(AudioClip tenseClip, AudioClip neutralClip, AudioClip happyClip) {
        bgmSource.Stop();

        tenseSource.clip = tenseClip;
        neutralSource.clip = neutralClip;
        happySource.clip = happyClip;

        tenseSource.volume = 1;
        neutralSource.volume = 0;
        happySource.volume = 0;

        tenseSource.Play();
        neutralSource.Play();
        happySource.Play();

        currentSourceID = 0;
    }

    public void TransitionLevelMusic() {
        currentSourceID = (currentSourceID + 1) % levelAudioSources.Length;
        for(int i = 0; i < levelAudioSources.Length; i++) {
            if(i == currentSourceID) {
                levelAudioSources[i].volume = 1;
            } else {
                levelAudioSources[i].volume = 0;
            }
        }
    }
    
    //Stops the currently playing BGM, leaving silence.
    public void StopBGM() {
        bgmSource.Stop();
    }

    //Plays the given AudioClip as a sound effect once at the given volume
    public void PlaySFX(AudioClip clip, float volume) {
        sfxSource.PlayOneShot(clip, volume);
    }

    //Plays the given AudioClip as a sound effect once at maximum volume
    public void PlaySFX(AudioClip clip) {
        sfxSource.PlayOneShot(clip, 1f);
    }
}
