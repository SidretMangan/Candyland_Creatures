using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// An example class of how to play a sound effect using the Audio Manager. It will be the same for BGM.
public class ExampleAudioPlayer : MonoBehaviour
{

    // All game objects that make sound will contain references to the sounds that they will make as fields which will be set in the inspector.
    [SerializeField] private AudioClip sfxToPlay;

    // Start is called before the first frame update
    void Start()
    {
        if (sfxToPlay != null) AudioManager.Instance.PlaySFX(sfxToPlay);
    }
}
