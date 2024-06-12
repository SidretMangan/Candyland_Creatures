using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

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

    //Plays the given AudioClip as looping background music. Will replace any BGM that is already playing.
    public void PlayBGM(AudioClip clip) {
        bgmSource.Stop();
        bgmSource.clip = clip;
        bgmSource.Play();
    }
    
    //Stops the currently playing BGM, leaving silence.
    public void StopBGM() {
        bgmSource.Stop();
    }

    //Plays the given AudioClip as a sound effect once at the given volume
    public void PlaySFX(AudioClip clip, float volume) {
        sfxSource.PlayOneShot(clip, volume);
    }
}
