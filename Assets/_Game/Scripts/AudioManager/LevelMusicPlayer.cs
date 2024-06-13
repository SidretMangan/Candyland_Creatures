using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 * An example class of how to play smoothly transitioning BGM for each level.
 * Attached as component to "Level Jukebox" GameObject
 *      We can put a "level jukebox" inside each of each level with different AudioClips set in the inspector.
 */
public class LevelMusicPlayer : MonoBehaviour
{
    //Declarations for AudioClips containing the three different intensities of BGM
    [SerializeField] private AudioClip tenseClip;
    [SerializeField] private AudioClip neutralClip;
    [SerializeField] private AudioClip happyClip;

    //TESTING PURPOSES: This is a button to swap between the different intensities of music
    [SerializeField] private Button testBtn;

    private void Awake() {
        // TESTING PURPOSES: This adds a listener to the button
        testBtn.onClick.AddListener(() => {
            AudioManager.Instance.TransitionLevelMusic();
        });
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayLevelMusic(tenseClip, neutralClip, happyClip);
    }
}
