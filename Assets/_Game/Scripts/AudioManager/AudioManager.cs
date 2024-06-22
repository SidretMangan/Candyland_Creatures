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
    //IMPORTANT: We may replace this with alternate Singleton architecture eventually (by attaching SingletonDontDestroy script to AudioManager GameObject)
    public static AudioManager Instance { get; private set;}

    //BGM source loops and is the top AudioSource component in the AudioManager GameObject, SFX source does not loop
    //The tense, neutral, and happy source are for the 3 smooth transition sources
    [SerializeField] private AudioSource bgmSource, sfxSource, tenseSource, neutralSource, happySource;

    // The below fields are used for the smooth transition between tenseSource, neutralSource, and happySource
    private AudioSource[] levelAudioSources = new AudioSource[3];
    [SerializeField] private int currentSourceID = 0;

    // The below fields are added to allow other scripts to more easily change the volume
    [SerializeField] private float masterVol, sfxVol, bgmVol;

    private void Awake() {

        // Code to generate singleton.
        // IMPORTANT: We may replace this with the alternate Singleton architecture eventually (by attaching SingletonDontDestroy script to AudioManager GameObject)
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else { 
            Instance = this;
        }

        // If the appropriate audio sources were not set in the inspector, we should log an error.
        if (bgmSource == null) Debug.LogError("bgmSource not found!");
        if (sfxSource == null) Debug.LogError("sfxSource not found!");
        if (tenseSource == null) Debug.LogError("tenseSource not found!");
        if (neutralSource == null) Debug.LogError("neutralSource not found!");
        if (happySource == null) Debug.LogError("happySource not found!");

        masterVol = 0.5f;
        bgmVol = 0.5f;
        sfxVol = 0.5f;
    }

    private void Start() {
        // Initializes array of smooth transition audio sources
        levelAudioSources[0] = tenseSource;
        levelAudioSources[1] = neutralSource;
        levelAudioSources[2] = happySource;
    }

    public void SetBGMVolume(float volume) {
        bgmVol = volume;
        ApplyVolumeToBGMSources();
    }

    public void SetSFXVolume(float volume) {
        sfxVol = volume;
    }

    public void SetMasterVolume(float volume) {
        masterVol = volume;
        ApplyVolumeToBGMSources();
    }

    private void ApplyVolumeToBGMSources() {
        bgmSource.volume = masterVol * bgmVol;
        for(int i = 0; i < levelAudioSources.Length; i++) {
            if(i == currentSourceID) {
                levelAudioSources[i].volume = masterVol * bgmVol;
            } else {
                levelAudioSources[i].volume = 0;
            }
        }
    }

    // Plays the given AudioClip as looping background music. Will replace any BGM that is already playing.
    // IMPORTANT: Uses bgmSource, which is *different* than the three sources used for the level BGM's that are smoothly transitioned between.
    public void PlayBGM(AudioClip clip) {
        bgmSource.Stop();
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    // Stops all audio sources.
    public void StopAll() {
        bgmSource.Stop();
        sfxSource.Stop();
        tenseSource.Stop();
        neutralSource.Stop();
        happySource.Stop();
    }

    /*
     * Starts playing BGM that can be smoothly transitioned between, given the three parameters.
     * Assumes that all three AudioClips provided as parameters are of the same length and can be played simultaneously.
     * Assumes that the "tense" clip is always the first clip to be played.
     */
    public void PlayLevelMusic(AudioClip tenseClip, AudioClip neutralClip, AudioClip happyClip) {
        // Stops bgmSource unrelated to smooth transition sources
        bgmSource.Stop();

        // Sets the clips of the appropriate sources to the audio clips provided in the parameters
        tenseSource.clip = tenseClip;
        neutralSource.clip = neutralClip;
        happySource.clip = happyClip;

        // Sets the tense source volume to 1, all others to 0
        tenseSource.volume = 1;
        neutralSource.volume = 0;
        happySource.volume = 0;

        // Plays all three sources simultaneously
        tenseSource.Play();
        neutralSource.Play();
        happySource.Play();

        // Sets currentSourceID to 0: tense = 0, neutral = 1, happy = 2
        // This corresponds with how they were initialized in the Start() function
        currentSourceID = 0;
    }

    /*
     * Transitions currently playing level music to the next appropriate bgm track.
     * Order of transition is: Tense > Neutral > Happy
     */
    public void TransitionLevelMusic() {
        currentSourceID = (currentSourceID + 1) % levelAudioSources.Length;
        TransitionLevelMusic(currentSourceID);
    }

    /*
     * Transitions currently playing level music to the given sourceID's track
     * Order of sourceID's is: 0 = Tense > 1 = Neutral > 2 = Happy
     */
    public void TransitionLevelMusic(int sourceID) {
        for(int i = 0; i < levelAudioSources.Length; i++) {
            if(i == sourceID) {
                StartCoroutine(StartFade(levelAudioSources[i], 0.5f, bgmVol * masterVol));
            } else {
                StartCoroutine(StartFade(levelAudioSources[i], 0.5f, 0));
            }
        }
        
    }

    // Helper method to smoothly fade a given audio source's volume to the given volume over the given duration, in seconds.
    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
    
    // Stops the currently playing BGM, leaving silence.
    // IMPORTANT: We should probably use StopAll() to stop sounds, and not this function.
    public void StopBGM() {
        bgmSource.Stop();
    }

    // Plays the given AudioClip as a sound effect once at the given volume
    public void PlaySFX(AudioClip clip, float volume) {
        sfxSource.PlayOneShot(clip, volume);
    }

    // Plays the given AudioClip as a sound effect once at maximum volume
    public void PlaySFX(AudioClip clip) {
        sfxSource.PlayOneShot(clip,  sfxVol * masterVol);
    }
}
